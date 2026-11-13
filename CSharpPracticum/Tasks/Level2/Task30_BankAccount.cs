using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level2
{
    public class Task30_BankAccount : ITask
    {
        public string Title => "Задание 30. BankAccount + свой Exception";

        public string Description => "реализовать банковский счёт с собственным исключением.";

        public string[] Requirements => new string[]
        {
        "Класс BankAccount с приватным балансом.",
        "Методы Deposit(decimal), Withdraw(decimal).",
        "Свой класс InsufficientFundsException : Exception.",
        "Withdraw бросает исключение, если сумма больше баланса.",
        "Продемонстрировать: успешное снятие и попытку снять больше баланса (с try/catch)."
        };

        public void Run()
        {
            BankAccount account = new BankAccount(500m);
            Console.WriteLine($"Начальный баланс: {account.Balance}");

            account.Deposit(300m);
            Console.WriteLine($"После пополнения на 300: {account.Balance}");

            account.Withdraw(200m);
            Console.WriteLine($"После снятия 200: {account.Balance}");

            try
            {
                Console.WriteLine("Пытаемся снять 1000...");
                account.Withdraw(1000m);
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine($"Перехвачено исключение: {ex.Message}");
            }

            Console.WriteLine($"Итоговый баланс: {account.Balance}");
        }
    }

    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException(string message) : base(message)
        {
        }
    }

    public class BankAccount
    {
        private decimal _balance;

        public decimal Balance
        {
            get { return _balance; }
        }

        public BankAccount(decimal initialBalance)
        {
            _balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Сумма пополнения должна быть больше нуля.");
            }
            _balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Сумма снятия должна быть больше нуля.");
            }
            if (amount > _balance)
            {
                throw new InsufficientFundsException($"Недостаточно средств. Баланс: {_balance}, запрошено: {amount}");
            }
            _balance -= amount;
        }
    }
}
