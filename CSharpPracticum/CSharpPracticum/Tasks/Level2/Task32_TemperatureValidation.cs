using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level2
{
    public class Task32_TemperatureValidation : ITask
    {
        public string Title => "Задание 32. Свойства с валидацией (Temperature)";

        public string Description => "класс Temperature с валидацией в set.";

        public string[] Requirements => new string[]
        {
        "Свойство Celsius с проверкой в set: не ниже −273.15.",
        "При некорректном значении — бросать ArgumentOutOfRangeException.",
        "Вычисляемое свойство Fahrenheit (только get).",
        "Продемонстрировать корректное и некорректное присваивание."
        };

        public void Run()
        {
            Temperature temp = new Temperature(25);
            Console.WriteLine($"Корректно: {temp.Celsius} °C = {temp.Fahrenheit} °F");

            try
            {
                Console.WriteLine("Пытаемся присвоить -300 °C...");
                temp.Celsius = -300;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Ошибка валидации: {ex.Message}");
            }
        }
    }

    public class Temperature
    {
        private double _celsius;

        public double Celsius
        {
            get { return _celsius; }
            set
            {
                if (value < -273.15)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Температура не может быть ниже абсолютного нуля (-273.15 °C).");
                }
                _celsius = value;
            }
        }

        public double Fahrenheit
        {
            get { return _celsius * 9.0 / 5.0 + 32.0; }
        }

        public Temperature(double celsius)
        {
            Celsius = celsius;
        }
    }
}
