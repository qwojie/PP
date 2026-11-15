using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace SqlPracticum;

internal class Program
{
    private static async Task Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        string? connectionString = Environment.GetEnvironmentVariable("PRACTICE_DB_CONNECTION");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            Console.WriteLine("Ошибка: переменная окружения PRACTICE_DB_CONNECTION не задана.");
            return;
        }

        while (true)
        {
            Console.WriteLine("\n=== Практикум SQL + C# (Задания 28–30) ===");
            Console.WriteLine("1. Задание 28 — Вывести список сотрудников (SELECT + OpenAsync)");
            Console.WriteLine("2. Задание 29 — Добавить сотрудника (параметризованный INSERT)");
            Console.WriteLine("3. Задание 30 — Мини-отчёт по отделам (LEFT JOIN + GROUP BY)");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите пункт меню: ");

            string? choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    await ShowEmployeesAsync(connectionString);
                    break;
                case "2":
                    await HandleAddEmployeeAsync(connectionString);
                    break;
                case "3":
                    ShowDepartmentReport(connectionString);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный пункт меню.");
                    break;
            }
        }
    }

    // Задание 28: асинхронное подключение, SELECT через SqlDataReader и обработка SqlException
    private static async Task ShowEmployeesAsync(string connectionString)
    {
        const string query = "SELECT EmployeeName, Salary FROM Employees ORDER BY EmployeeId;";

        try
        {
            await using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand(query, connection);
            await using SqlDataReader reader = await command.ExecuteReaderAsync();

            if (!reader.HasRows)
            {
                Console.WriteLine("Список сотрудников пуст.");
                return;
            }

            Console.WriteLine($"{"Сотрудник",-25} | {"Зарплата",12}");
            Console.WriteLine(new string('-', 40));

            while (await reader.ReadAsync())
            {
                string name = reader.GetString(0);
                decimal salary = reader.GetDecimal(1);
                Console.WriteLine($"{name,-25} | {salary,12:N2}");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Ошибка SQL Server (код {ex.Number}): {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
        }
    }

    private static async Task HandleAddEmployeeAsync(string connectionString)
    {
        Console.Write("Введите имя сотрудника: ");
        string? name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Имя не может быть пустым.");
            return;
        }

        Console.Write("Введите зарплату: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal salary) || salary <= 0)
        {
            Console.WriteLine("Некорректная зарплата.");
            return;
        }

        Console.Write("Введите ID отдела (1-Разработка, 2-Аналитика, 3-Тестирование, 4-Менеджмент): ");
        if (!int.TryParse(Console.ReadLine(), out int departmentId))
        {
            Console.WriteLine("Некорректный ID отдела.");
            return;
        }

        await AddEmployee(connectionString, name.Trim(), salary, departmentId);
    }

    // Задание 29: параметризованный INSERT, проверка дубликата/FK и SELECT для подтверждения
    private static async Task AddEmployee(string connectionString, string name, decimal salary, int departmentId)
    {
        const string checkDuplicateSql = "SELECT COUNT(*) FROM Employees WHERE EmployeeName = @name;";
        const string insertSql = @"
            INSERT INTO Employees (EmployeeName, Salary, HireDate, DepartmentId, ManagerId)
            OUTPUT INSERTED.EmployeeId
            VALUES (@name, @salary, CAST(GETDATE() AS DATE), @deptId, NULL);";
        const string confirmSql = @"
            SELECT EmployeeId, EmployeeName, Salary, HireDate, DepartmentId
            FROM Employees
            WHERE EmployeeId = @id;";

        try
        {
            await using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            // Проверка на дубликат по имени
            await using (var checkCmd = new SqlCommand(checkDuplicateSql, connection))
            {
                checkCmd.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = name;
                int existingCount = (int)(await checkCmd.ExecuteScalarAsync() ?? 0);
                if (existingCount > 0)
                {
                    Console.WriteLine($"Предупреждение: сотрудник с именем «{name}» уже существует в базе.");
                    return;
                }
            }

            // Вставка с параметрами
            int newId;
            await using (var insertCmd = new SqlCommand(insertSql, connection))
            {
                insertCmd.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = name;
                var salaryParam = insertCmd.Parameters.Add("@salary", SqlDbType.Decimal);
                salaryParam.Precision = 10;
                salaryParam.Scale = 2;
                salaryParam.Value = salary;
                insertCmd.Parameters.Add("@deptId", SqlDbType.Int).Value = departmentId;

                object? result = await insertCmd.ExecuteScalarAsync();
                newId = Convert.ToInt32(result);
            }

            Console.WriteLine($"Сотрудник успешно добавлен с EmployeeId = {newId}. Подтверждение из БД:");

            // SELECT для подтверждения
            await using (var confirmCmd = new SqlCommand(confirmSql, connection))
            {
                confirmCmd.Parameters.Add("@id", SqlDbType.Int).Value = newId;
                await using SqlDataReader reader = await confirmCmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    Console.WriteLine(
                        $"ID: {reader.GetInt32(0)} | Имя: {reader.GetString(1)} | " +
                        $"Зарплата: {reader.GetDecimal(2):N2} | Дата найма: {reader.GetDateTime(3):dd.MM.yyyy} | " +
                        $"Отдел ID: {reader.GetInt32(4)}");
                }
            }
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            Console.WriteLine($"Ошибка внешнего ключа (FK violation): отдела с ID = {departmentId} не существует.");
        }
        catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
        {
            Console.WriteLine("Ошибка уникальности: такая запись уже существует в базе.");
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Ошибка SQL Server (код {ex.Number}): {ex.Message}");
        }
    }

    // Задание 30: мини-отчёт по отделам с выравниванием и обработкой NULL / пустого результата
    private static void ShowDepartmentReport(string connectionString)
    {
        const string query = @"
            SELECT 
                d.DepartmentName, 
                COUNT(e.EmployeeId) AS EmpCount, 
                AVG(e.Salary) AS AvgSalary 
            FROM Departments d 
            LEFT JOIN Employees e ON d.DepartmentId = e.DepartmentId 
            GROUP BY d.DepartmentName
            ORDER BY d.DepartmentName;";

        try
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            using var command = new SqlCommand(query, connection);
            using SqlDataReader reader = command.ExecuteReader();

            if (!reader.HasRows)
            {
                Console.WriteLine("Отделы в базе данных не найдены.");
                return;
            }

            Console.WriteLine($"{"Отдел",-20} | {"Сотрудников",12} | {"Средняя зарплата",18}");
            Console.WriteLine(new string('-', 56));

            while (reader.Read())
            {
                string deptName = reader.GetString(0);
                int empCount = reader.GetInt32(1);
                string avgSalaryText = reader.IsDBNull(2)
                    ? "N/A"
                    : $"{reader.GetDecimal(2):N2}";

                Console.WriteLine($"{deptName,-20} | {empCount,12} | {avgSalaryText,18}");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Ошибка SQL Server (код {ex.Number}): {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
        }
    }
}

