\# SQL notes



> Учебная БД: PracticeDB. Скрипт создания — `database/init.sql`.

> Все запросы выполнялись в SSMS 22.



\## Task 01 — Создание базы и таблиц



\### Запрос



```sql

CREATE DATABASE PracticeDB;

GO

USE PracticeDB;

GO



CREATE TABLE Departments (

&#x20;   DepartmentId   INT PRIMARY KEY IDENTITY(1,1),

&#x20;   DepartmentName NVARCHAR(100) NOT NULL

);



CREATE TABLE Employees (

&#x20;   EmployeeId   INT PRIMARY KEY IDENTITY(1,1),

&#x20;   EmployeeName NVARCHAR(100) NOT NULL,

&#x20;   Salary       DECIMAL(10,2) NOT NULL,

&#x20;   HireDate     DATE NOT NULL,

&#x20;   DepartmentId INT NOT NULL,

&#x20;   ManagerId    INT NULL,

&#x20;   CONSTRAINT FK\_Employees\_Departments FOREIGN KEY (DepartmentId)

&#x20;       REFERENCES Departments(DepartmentId),

&#x20;   CONSTRAINT FK\_Employees\_Manager FOREIGN KEY (ManagerId)

&#x20;       REFERENCES Employees(EmployeeId)

);



CREATE TABLE Projects (

&#x20;   ProjectId   INT PRIMARY KEY IDENTITY(1,1),

&#x20;   ProjectName NVARCHAR(100) NOT NULL,

&#x20;   StartDate   DATE NOT NULL

);



CREATE TABLE EmployeeProjects (

&#x20;   EmployeeId INT NOT NULL,

&#x20;   ProjectId  INT NOT NULL,

&#x20;   Hours      INT NOT NULL DEFAULT 0,

&#x20;   CONSTRAINT PK\_EmployeeProjects PRIMARY KEY (EmployeeId, ProjectId),

&#x20;   CONSTRAINT FK\_EP\_Employees FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId),

&#x20;   CONSTRAINT FK\_EP\_Projects  FOREIGN KEY (ProjectId)  REFERENCES Projects(ProjectId)

);

```



\### Результат



```text

Команды успешно выполнены. Создана БД PracticeDB и 4 таблицы: Departments, Employees, Projects, EmployeeProjects.

```



\### Вывод



Освоил создание базы данных, таблиц, первичных ключей (`PRIMARY KEY IDENTITY`), составного первичного ключа и внешних ключей (`FOREIGN KEY`), включая самоссылающийся внешний ключ (`ManagerId -> EmployeeId`).



\---



\## Task 02 — INSERT: заполнение данными



\### Запрос



```sql

INSERT INTO Departments (DepartmentName) VALUES

&#x20;   (N'Разработка'),

&#x20;   (N'Аналитика'),

&#x20;   (N'Тестирование'),

&#x20;   (N'Менеджмент');



INSERT INTO Employees (EmployeeName, Salary, HireDate, DepartmentId, ManagerId) VALUES

&#x20;   (N'Анна Иванова',    65000, '2020-03-15', 1, NULL),

&#x20;   (N'Пётр Сидоров',    72000, '2019-07-01', 1, NULL),

&#x20;   (N'Мария Кузнецова', 55000, '2021-11-20', 1, NULL),

&#x20;   (N'Иван Петров',     48000, '2023-02-10', 1, NULL),

&#x20;   (N'Ольга Смирнова',  61000, '2020-09-05', 2, NULL),

&#x20;   (N'Дмитрий Орлов',   58000, '2022-04-18', 2, NULL),

&#x20;   (N'Елена Волкова',   52000, '2023-06-01', 2, NULL),

&#x20;   (N'Сергей Морозов',  45000, '2021-12-12', 3, NULL),

&#x20;   (N'Наталья Соколова',43000, '2024-01-15', 3, NULL),

&#x20;   (N'Алексей Лебедев', 90000, '2018-05-25', 4, NULL);



INSERT INTO Projects (ProjectName, StartDate) VALUES

&#x20;   (N'CRM System',      '2023-01-10'),

&#x20;   (N'Mobile App',      '2023-05-01'),

&#x20;   (N'Data Warehouse',  '2022-11-20'),

&#x20;   (N'AI Assistant',    '2024-02-01'),

&#x20;   (N'Legacy Refactor', '2021-06-15');



INSERT INTO EmployeeProjects (EmployeeId, ProjectId, Hours) VALUES

&#x20;   (1, 1, 120), (1, 3, 80),

&#x20;   (2, 1, 150), (2, 4, 60),

&#x20;   (3, 2, 100), (4, 2, 90),

&#x20;   (4, 5, 40),  (5, 3, 110),

&#x20;   (6, 3, 70),  (6, 4, 50),

&#x20;   (7, 2, 95),  (8, 5, 130),

&#x20;   (10, 4, 200),(10, 1, 30);



SELECT 

&#x20;   (SELECT COUNT(\*) FROM Departments) AS DepartmentsCount,

&#x20;   (SELECT COUNT(\*) FROM Employees) AS EmployeesCount,

&#x20;   (SELECT COUNT(\*) FROM Projects) AS ProjectsCount,

&#x20;   (SELECT COUNT(\*) FROM EmployeeProjects) AS EmployeeProjectsCount;

```



\### Результат



```text

DepartmentsCount | EmployeesCount | ProjectsCount | EmployeeProjectsCount

\-----------------|----------------|---------------|----------------------

4                | 10             | 5             | 14

```



\### Вывод



Научился добавлять сразу несколько строк одним запросом `INSERT INTO ... VALUES` с использованием префикса `N` для корректной записи кириллицы в `NVARCHAR`, а также проверил заполнение таблиц через `SELECT COUNT(\*)`.

---

## Task 03 — SELECT + WHERE: базовые выборки

### Запрос

```sql
-- 1. Сотрудники с зарплатой > 50000
SELECT EmployeeId, EmployeeName, Salary
FROM Employees
WHERE Salary > 50000;

-- 2. Сотрудники, нанятые в 2023 году
SELECT EmployeeId, EmployeeName, HireDate
FROM Employees
WHERE YEAR(HireDate) = 2023;

-- 3. Сотрудники, чьё имя начинается на «А»
SELECT EmployeeId, EmployeeName
FROM Employees
WHERE EmployeeName LIKE N'А%';

-- 4. Сотрудники из отдела DepartmentId = 2
SELECT EmployeeId, EmployeeName, DepartmentId
FROM Employees
WHERE DepartmentId = 2;

-- 5. Сотрудники с зарплатой в диапазоне 40000–60000
SELECT EmployeeId, EmployeeName, Salary
FROM Employees
WHERE Salary BETWEEN 40000 AND 60000;
```

### Результат

```text
Запрос 1 (Salary > 50000): 7 строк (Анна Иванова, Пётр Сидоров, Мария Кузнецова, Ольга Смирнова, Дмитрий Орлов, Елена Волкова, Алексей Лебедев).
Запрос 2 (YEAR(HireDate) = 2023): 2 строки (Иван Петров, Елена Волкова).
Запрос 3 (LIKE N'А%'): 2 строки (Анна Иванова, Алексей Лебедев).
Запрос 4 (DepartmentId = 2): 3 строки (Ольга Смирнова, Дмитрий Орлов, Елена Волкова).
Запрос 5 (BETWEEN 40000 AND 60000): 6 строк (Мария Кузнецова, Иван Петров, Дмитрий Орлов, Елена Волкова, Сергей Морозов, Наталья Соколова).
```

### Вывод

Отработал фильтрацию данных через `WHERE` с операторами сравнения, функцией извлечения года `YEAR()`, поиском по шаблону `LIKE N'А%'` для кириллицы и диапазоном `BETWEEN`.

---

## Task 04 — ORDER BY + TOP: сортировка и лимит

### Запрос

```sql
-- 1. Топ-3 по зарплате (убывание)
SELECT TOP 3 EmployeeId, EmployeeName, Salary
FROM Employees
ORDER BY Salary DESC;

-- 2. Топ-5 самых давних сотрудников по HireDate (возрастание)
SELECT TOP 5 EmployeeId, EmployeeName, HireDate
FROM Employees
ORDER BY HireDate ASC;

-- 3. Сотрудники, отсортированные по отделу (возрастание), затем по зарплате (убывание)
SELECT EmployeeId, EmployeeName, DepartmentId, Salary
FROM Employees
ORDER BY DepartmentId ASC, Salary DESC;
```

### Результат

```text
Запрос 1 (TOP 3 по зарплате): Алексей Лебедев (90000.00), Пётр Сидоров (72000.00), Анна Иванова (65000.00).
Запрос 2 (TOP 5 по HireDate ASC): Алексей Лебедев (2018-05-25), Пётр Сидоров (2019-07-01), Анна Иванова (2020-03-15), Ольга Смирнова (2020-09-05), Мария Кузнецова (2021-11-20).
Запрос 3 (DepartmentId ASC, Salary DESC): 10 строк, сгруппированных по отделам от 1 до 4, внутри каждого отдела от большей зарплаты к меньшей.
```

### Вывод

Отработал ограничение выборки через `TOP N` и многоуровневую сортировку через `ORDER BY` по возрастанию (`ASC`) и убыванию (`DESC`).

---

## Task 05 — Агрегация: COUNT, SUM, AVG, MIN, MAX

### Запрос

```sql
SELECT 
    COUNT(*) AS TotalEmployees,
    SUM(Salary) AS TotalSalary,
    ROUND(AVG(Salary), 2) AS AverageSalary,
    MIN(Salary) AS MinSalary,
    MAX(Salary) AS MaxSalary,
    COUNT(CASE WHEN Salary > 50000 THEN 1 END) AS EmployeesAbove50k
FROM Employees;
```

### Результат

```text
TotalEmployees | TotalSalary | AverageSalary | MinSalary | MaxSalary | EmployeesAbove50k
---------------|-------------|---------------|-----------|-----------|------------------
10             | 589000.00   | 58900.000000  | 43000.00  | 90000.00  | 7
```

### Вывод

Отработал агрегатные функции `COUNT`, `SUM`, `AVG` с округлением через `ROUND()`, `MIN`, `MAX`, а также условный подсчёт строк через `COUNT(CASE WHEN ... THEN 1 END)`.

---

## Task 06 — GROUP BY: статистика по отделам

### Запрос

```sql
SELECT 
    DepartmentId,
    COUNT(*) AS EmployeeCount,
    ROUND(AVG(Salary), 2) AS AverageSalary,
    SUM(Salary) AS TotalSalary
FROM Employees
GROUP BY DepartmentId
ORDER BY AverageSalary DESC;
```

### Результат

```text
DepartmentId | EmployeeCount | AverageSalary | TotalSalary
-------------|---------------|---------------|------------
4            | 1             | 90000.000000  | 90000.00
1            | 4             | 60000.000000  | 240000.00
2            | 3             | 57000.000000  | 171000.00
3            | 2             | 44000.000000  | 88000.00
```

### Вывод

Отработал группировку строк с помощью `GROUP BY` для расчёта агрегатных показателей по каждому отделу и сортировку сгруппированных данных по вычисляемому столбцу `AverageSalary DESC`.

---

## Task 07 — GROUP BY + HAVING: фильтрация групп

### Запрос

```sql
-- 1. Отделы, в которых больше 2 сотрудников
SELECT 
    DepartmentId,
    COUNT(*) AS EmployeeCount
FROM Employees
GROUP BY DepartmentId
HAVING COUNT(*) > 2;

-- 2. Отделы со средней зарплатой > 55000
SELECT 
    DepartmentId,
    ROUND(AVG(Salary), 2) AS AverageSalary
FROM Employees
GROUP BY DepartmentId
HAVING AVG(Salary) > 55000;

-- 3. Отделы, где суммарная зарплата > 150000
SELECT 
    DepartmentId,
    SUM(Salary) AS TotalSalary
FROM Employees
GROUP BY DepartmentId
HAVING SUM(Salary) > 150000;
```

### Результат

```text
Запрос 1 (COUNT(*) > 2): DepartmentId 1 (4 сотрудника), DepartmentId 2 (3 сотрудника).
Запрос 2 (AVG(Salary) > 55000): DepartmentId 1 (60000.00), DepartmentId 2 (57000.00), DepartmentId 4 (90000.00).
Запрос 3 (SUM(Salary) > 150000): DepartmentId 1 (240000.00), DepartmentId 2 (171000.00).
```

### Вывод

Отработал фильтрацию сгруппированных данных с помощью `HAVING`, который применяется после группировки `GROUP BY` и позволяет отбирать группы по значениям агрегатных функций (`COUNT`, `AVG`, `SUM`).

---

## Task 08 — INNER JOIN: сотрудники и отделы

### Запрос

```sql
SELECT 
    e.EmployeeName,
    d.DepartmentName,
    e.Salary
FROM Employees e
INNER JOIN Departments d ON e.DepartmentId = d.DepartmentId
ORDER BY d.DepartmentName ASC, e.Salary DESC;
```

### Результат

```text
EmployeeName     | DepartmentName | Salary
-----------------|----------------|---------
Ольга Смирнова   | Аналитика      | 61000.00
Дмитрий Орлов    | Аналитика      | 58000.00
Елена Волкова    | Аналитика      | 52000.00
Алексей Лебедев  | Менеджмент     | 90000.00
Пётр Сидоров     | Разработка     | 72000.00
Анна Иванова     | Разработка     | 65000.00
Мария Кузнецова  | Разработка     | 55000.00
Иван Петров      | Разработка     | 48000.00
Сергей Морозов   | Тестирование   | 45000.00
Наталья Соколова | Тестирование   | 43000.00
```

### Вывод

Отработал соединение двух таблиц через `INNER JOIN ... ON` с использованием псевдонимов (`e` и `d`), а также двухуровневую сортировку по названию отдела (`ASC`) и зарплате (`DESC`).
---

## Task 09 — LEFT JOIN: сотрудники без проектов

### Запрос

```sql
SELECT 
    e.EmployeeId,
    e.EmployeeName
FROM Employees e
LEFT JOIN EmployeeProjects ep ON e.EmployeeId = ep.EmployeeId
WHERE ep.EmployeeId IS NULL;
```

### Результат

```text
EmployeeId | EmployeeName
-----------|-----------------
9          | Наталья Соколова
```

### Вывод

Отработал левое внешнее соединение `LEFT JOIN` с фильтрацией `IS NULL` по правой таблице (`EmployeeProjects`), чтобы найти записи из левой таблицы (`Employees`), для которых отсутствуют совпадения.

---

## Task 10 — Множественный JOIN: сотрудники, отделы, проекты

### Запрос

```sql
SELECT 
    e.EmployeeName,
    d.DepartmentName,
    p.ProjectName,
    ep.Hours
FROM Employees e
INNER JOIN Departments d ON e.DepartmentId = d.DepartmentId
INNER JOIN EmployeeProjects ep ON e.EmployeeId = ep.EmployeeId
INNER JOIN Projects p ON ep.ProjectId = p.ProjectId
ORDER BY d.DepartmentName ASC, e.EmployeeName ASC, p.ProjectName ASC;
```

### Результат

```text
EmployeeName    | DepartmentName | ProjectName     | Hours
----------------|----------------|-----------------|------
Дмитрий Орлов   | Аналитика      | AI Assistant    | 50
Дмитрий Орлов   | Аналитика      | Data Warehouse  | 70
Елена Волкова   | Аналитика      | Mobile App      | 95
Ольга Смирнова  | Аналитика      | Data Warehouse  | 110
Алексей Лебедев | Менеджмент     | AI Assistant    | 200
Алексей Лебедев | Менеджмент     | CRM System      | 30
Анна Иванова    | Разработка     | CRM System      | 120
Анна Иванова    | Разработка     | Data Warehouse  | 80
Иван Петров     | Разработка     | Legacy Refactor | 40
Иван Петров     | Разработка     | Mobile App      | 90
Мария Кузнецова | Разработка     | Mobile App      | 100
Пётр Сидоров    | Разработка     | AI Assistant    | 60
Пётр Сидоров    | Разработка     | CRM System      | 150
Сергей Морозов  | Тестирование   | Legacy Refactor | 130
```

### Вывод

Отработал последовательное соединение четырёх таблиц (`Employees`, `Departments`, `EmployeeProjects`, `Projects`) через цепочку `INNER JOIN` и трёхуровневую сортировку результатов.

---

## Task 11 — Self-join: иерархия руководителей

### Запрос

```sql
SELECT 
    e.EmployeeName AS EmployeeName,
    m.EmployeeName AS ManagerName
FROM Employees e
LEFT JOIN Employees m ON e.ManagerId = m.EmployeeId;
```

### Результат

```text
EmployeeName     | ManagerName
-----------------|----------------
Анна Иванова     | NULL
Пётр Сидоров     | NULL
Мария Кузнецова  | Пётр Сидоров
Иван Петров      | Анна Иванова
Ольга Смирнова   | NULL
Дмитрий Орлов    | Ольга Смирнова
Елена Волкова    | NULL
Сергей Морозов   | NULL
Наталья Соколова | Алексей Лебедев
Алексей Лебедев  | NULL
```

### Вывод

Разобрался, как связывать таблицу саму с собой через псевдонимы (`e` и `m`) с помощью `LEFT JOIN`, чтобы вывести руководителя для каждого сотрудника и сохранить `NULL` для тех, у кого нет начальника.

---

## Task 12 — Подзапросы в WHERE

### Запрос

```sql
-- 1. Сотрудники с зарплатой выше средней
SELECT EmployeeId, EmployeeName, Salary
FROM Employees
WHERE Salary > (SELECT AVG(Salary) FROM Employees);

-- 2. Сотрудники с максимальной зарплатой
SELECT EmployeeId, EmployeeName, Salary
FROM Employees
WHERE Salary = (SELECT MAX(Salary) FROM Employees);
```

### Результат

```text
Запрос 1 (Salary > AVG): 4 строки — Анна Иванова (65000.00), Пётр Сидоров (72000.00), Ольга Смирнова (61000.00), Алексей Лебедев (90000.00).
Запрос 2 (Salary = MAX): 1 строка — Алексей Лебедев (90000.00).
```

### Вывод

Научился использовать одиночные подзапросы в условии `WHERE` для фильтрации строк относительно агрегированных значений (`AVG` и `MAX`) по всей таблице.

---

## Task 13 — Скалярные подзапросы в SELECT

### Запрос

```sql
SELECT 
    e.EmployeeName,
    e.Salary,
    e.DepartmentId,
    ROUND(e.Salary - (
        SELECT AVG(e2.Salary)
        FROM Employees e2
        WHERE e2.DepartmentId = e.DepartmentId
    ), 2) AS DiffFromDeptAvg
FROM Employees e;
```

### Результат

```text
EmployeeName     | Salary   | DepartmentId | DiffFromDeptAvg
-----------------|----------|--------------|----------------
Анна Иванова     | 65000.00 | 1            | 5000.000000
Пётр Сидоров     | 72000.00 | 1            | 12000.000000
Мария Кузнецова  | 55000.00 | 1            | -5000.000000
Иван Петров      | 48000.00 | 1            | -12000.000000
Ольга Смирнова   | 61000.00 | 2            | 4000.000000
Дмитрий Орлов    | 58000.00 | 2            | 1000.000000
Елена Волкова    | 52000.00 | 2            | -5000.000000
Сергей Морозов   | 45000.00 | 3            | 1000.000000
Наталья Соколова | 43000.00 | 3            | -1000.000000
Алексей Лебедев  | 90000.00 | 4            | 0.000000
```

### Вывод

Освоил коррелированные скалярные подзапросы в списке `SELECT`: внутренний запрос вычисляет среднюю зарплату для отдела текущей строки и сразу считает отклонение.

---

## Task 14 — EXISTS / NOT EXISTS

### Запрос

```sql
-- 1. Сотрудники, у которых есть хотя бы один проект
SELECT e.EmployeeId, e.EmployeeName
FROM Employees e
WHERE EXISTS (
    SELECT 1 
    FROM EmployeeProjects ep 
    WHERE ep.EmployeeId = e.EmployeeId
);

-- 2. Сотрудники, у которых нет проектов
SELECT e.EmployeeId, e.EmployeeName
FROM Employees e
WHERE NOT EXISTS (
    SELECT 1 
    FROM EmployeeProjects ep 
    WHERE ep.EmployeeId = e.EmployeeId
);
```

### Результат

```text
Запрос 1 (EXISTS): 9 сотрудников (все, кроме Натальи Соколовой).
Запрос 2 (NOT EXISTS): 1 сотрудник — EmployeeId 9 (Наталья Соколова).
```

### Вывод

Понял, как проверять наличие и отсутствие связанных записей в другой таблице через коррелированные подзапросы с `EXISTS` и `NOT EXISTS` без использования `JOIN`.

---

## Task 15 — UNION / UNION ALL

### Запрос

```sql
-- Вариант 1: UNION (уникальные строки)
SELECT EmployeeId, EmployeeName, Salary, HireDate
FROM Employees
WHERE Salary > 60000
UNION
SELECT EmployeeId, EmployeeName, Salary, HireDate
FROM Employees
WHERE YEAR(HireDate) < 2022;

-- Вариант 2: UNION ALL (с дубликатами)
SELECT EmployeeId, EmployeeName, Salary, HireDate
FROM Employees
WHERE Salary > 60000
UNION ALL
SELECT EmployeeId, EmployeeName, Salary, HireDate
FROM Employees
WHERE YEAR(HireDate) < 2022;
```

### Результат

```text
UNION: 6 уникальных строк (Анна Иванова, Пётр Сидоров, Мария Кузнецова, Ольга Смирнова, Сергей Морозов, Алексей Лебедев).
UNION ALL: 10 строк (4 сотрудника, попавшие под оба условия, продублированы).
```

### Вывод

Наглядно увидел разницу между `UNION` и `UNION ALL`: `UNION` автоматически убирает дубликаты (6 строк против 10), но требует дополнительной сортировки/хэширования для поиска повторов.

---

## Task 16 — UPDATE: изменение данных

### Запрос

```sql
-- SELECT ДО изменений
SELECT EmployeeId, EmployeeName, Salary, HireDate, DepartmentId FROM Employees;

-- 1. Увеличить зарплату всех сотрудников отдела 1 на 10%
UPDATE Employees SET Salary = Salary * 1.10 WHERE DepartmentId = 1;

-- 2. Увеличить зарплату сотрудников, нанятых до 2022 года, на 5000
UPDATE Employees SET Salary = Salary + 5000 WHERE YEAR(HireDate) < 2022;

-- 3. Обновить HireDate одного сотрудника
UPDATE Employees SET HireDate = '2024-02-01' WHERE EmployeeId = 9;

-- SELECT ПОСЛЕ изменений
SELECT EmployeeId, EmployeeName, Salary, HireDate, DepartmentId FROM Employees;
```

### Результат

```text
До: Анна Иванова (65000.00), Пётр Сидоров (72000.00), Мария Кузнецова (55000.00), Иван Петров (48000.00), Наталья Соколова (HireDate: 2024-01-15).
После: Анна Иванова (76500.00), Пётр Сидоров (84200.00), Мария Кузнецова (65500.00), Иван Петров (52800.00), Наталья Соколова (HireDate: 2024-02-01).
```

### Вывод

Отработал точечное обновление данных через `UPDATE ... SET ... WHERE` и проверил правильность расчётов с помощью `SELECT` до и после изменений.

---

## Task 17 — DELETE: удаление данных

### Запрос

```sql
-- 1. Удаление назначений с Hours = 0
INSERT INTO EmployeeProjects (EmployeeId, ProjectId, Hours) VALUES (9, 1, 0);
SELECT COUNT(*) AS BeforeDeleteZeroHours FROM EmployeeProjects;
DELETE FROM EmployeeProjects WHERE Hours = 0;
SELECT COUNT(*) AS AfterDeleteZeroHours FROM EmployeeProjects;

-- 2. Удаление сотрудника по EmployeeId = 8 (предварительно удаляем его назначения из-за FK)
DELETE FROM EmployeeProjects WHERE EmployeeId = 8;
DELETE FROM Employees WHERE EmployeeId = 8;
SELECT COUNT(*) AS AfterDeleteEmployee8 FROM Employees;

-- 3. Демонстрация DELETE с WHERE и без WHERE на тестовой копии таблицы
SELECT * INTO EmployeesCopy FROM Employees;
DELETE FROM EmployeesCopy WHERE DepartmentId = 3;
SELECT COUNT(*) AS CopyAfterDeleteWhere FROM EmployeesCopy;
DELETE FROM EmployeesCopy;
SELECT COUNT(*) AS CopyAfterDeleteAll FROM EmployeesCopy;
DROP TABLE EmployeesCopy;
```

### Результат

```text
BeforeDeleteZeroHours: 15 -> AfterDeleteZeroHours: 14
AfterDeleteEmployee8: 9
CopyAfterDeleteWhere: 8 -> CopyAfterDeleteAll: 0
```

### Вывод

Научился безопасно удалять связанные записи с учётом ограничений внешних ключей (`FOREIGN KEY`) и проверил разницу между `DELETE` с условием `WHERE` и полной очисткой таблицы.

---

## Task 18 — Транзакции: BEGIN TRAN / COMMIT / ROLLBACK

### Запрос

```sql
SELECT EmployeeId, EmployeeName, Salary AS InitialSalary FROM Employees WHERE EmployeeId = 4;

-- 1. Транзакция с ROLLBACK
BEGIN TRANSACTION;
UPDATE Employees SET Salary = 99999.00 WHERE EmployeeId = 4;
SELECT EmployeeId, EmployeeName, Salary AS InsideTranBeforeRollback FROM Employees WHERE EmployeeId = 4;
ROLLBACK TRANSACTION;
SELECT EmployeeId, EmployeeName, Salary AS AfterRollback FROM Employees WHERE EmployeeId = 4;

-- 2. Транзакция с COMMIT
BEGIN TRANSACTION;
UPDATE Employees SET Salary = 60000.00 WHERE EmployeeId = 4;
SELECT EmployeeId, EmployeeName, Salary AS InsideTranBeforeCommit FROM Employees WHERE EmployeeId = 4;
COMMIT TRANSACTION;
SELECT EmployeeId, EmployeeName, Salary AS AfterCommit FROM Employees WHERE EmployeeId = 4;
```

### Результат

```text
InitialSalary: 52800.00
InsideTranBeforeRollback: 99999.00 -> AfterRollback: 52800.00
InsideTranBeforeCommit: 60000.00 -> AfterCommit: 60000.00
```

### Вывод

На практике проверил работу транзакций: `ROLLBACK` полностью отменяет незафиксированные изменения и возвращает исходное состояние, а `COMMIT` сохраняет их в базе.

---

## Task 19 — Представления (VIEW)

### Запрос

```sql
CREATE VIEW vw_EmployeeDetails AS
SELECT 
    e.EmployeeName,
    d.DepartmentName,
    p.ProjectName,
    ep.Hours
FROM Employees e
INNER JOIN Departments d ON e.DepartmentId = d.DepartmentId
INNER JOIN EmployeeProjects ep ON e.EmployeeId = ep.EmployeeId
INNER JOIN Projects p ON ep.ProjectId = p.ProjectId;
GO

SELECT * FROM vw_EmployeeDetails
ORDER BY DepartmentName, EmployeeName, ProjectName;
GO

DROP VIEW vw_EmployeeDetails;
GO
```

### Результат

```text
Возвращено 13 строк с полями EmployeeName, DepartmentName, ProjectName, Hours. Представление успешно создано, прочитано и удалено через DROP VIEW.
```

### Вывод

Научился инкапсулировать сложный запрос с несколькими `JOIN` в виртуальную таблицу (`VIEW`), обращаться к ней через обычный `SELECT` и удалять её.

---

## Task 20 — Индексы: создание и анализ

### Запрос

```sql
CREATE NONCLUSTERED INDEX IX_Employees_DepartmentId ON Employees(DepartmentId);
CREATE NONCLUSTERED INDEX IX_Employees_Salary ON Employees(Salary);
GO
SET STATISTICS IO ON;
-- 1. Без использования индекса (Clustered Index Scan)
SELECT EmployeeId, EmployeeName, DepartmentId, Salary
FROM Employees WITH (INDEX(0))
WHERE DepartmentId = 1 AND Salary > 50000;
-- 2. С использованием созданного индекса (Index Seek + Key Lookup)
SELECT EmployeeId, EmployeeName, DepartmentId, Salary
FROM Employees WITH (INDEX(IX_Employees_DepartmentId))
WHERE DepartmentId = 1 AND Salary > 50000;
SET STATISTICS IO OFF;
```

Результат

text Запрос 1 (без индекса): План — Clustered Index Scan (стоимость пакета 32%), логических операций чтения (logical reads) — 2. Запрос 2 (с индексом IX_Employees_DepartmentId): План — Index Seek + Key Lookup (стоимость пакета 68%), логических операций чтения (logical reads) — 10. 

Вывод

Научился создавать некластерные индексы и анализировать статистику ввода-вывода (STATISTICS IO) и планы выполнения. Увидел, что на маленькой таблице из 9 строк полное сканирование (2 logical reads) обходится дешевле, чем поиск по индексу с последующим Key Lookup для каждой из 4 найденных строк (10 logical reads).
