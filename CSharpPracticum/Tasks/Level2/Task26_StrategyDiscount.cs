using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level2
{
    public class Task26_StrategyDiscount : ITask
    {
        public string Title => "Задание 26. Паттерн «Стратегия» (скидки)";

        public string Description => "реализовать калькулятор скидок через паттерн «Стратегия».";

        public string[] Requirements => new string[]
        {
        "Интерфейс IDiscountStrategy с методом Apply(decimal price).",
        "Реализации: NoDiscount, VipDiscount (10%), PromoDiscount (20%).",
        "Класс PriceCalculator, принимающий стратегию в конструкторе.",
        "Продемонстрировать расчёт цены с каждой стратегией."
        };

        public void Run()
        {
            decimal basePrice = 1000m;

            PriceCalculator calcNo = new PriceCalculator(new NoDiscount());
            PriceCalculator calcVip = new PriceCalculator(new VipDiscount());
            PriceCalculator calcPromo = new PriceCalculator(new PromoDiscount());

            Console.WriteLine($"Базовая цена: {basePrice}");
            Console.WriteLine($"Без скидки: {calcNo.Calculate(basePrice)}");
            Console.WriteLine($"VIP (10%): {calcVip.Calculate(basePrice)}");
            Console.WriteLine($"Промо (20%): {calcPromo.Calculate(basePrice)}");
        }
    }

    public interface IDiscountStrategy
    {
        decimal Apply(decimal price);
    }

    public class NoDiscount : IDiscountStrategy
    {
        public decimal Apply(decimal price)
        {
            return price;
        }
    }

    public class VipDiscount : IDiscountStrategy
    {
        public decimal Apply(decimal price)
        {
            return price * 0.90m;
        }
    }

    public class PromoDiscount : IDiscountStrategy
    {
        public decimal Apply(decimal price)
        {
            return price * 0.80m;
        }
    }

    public class PriceCalculator
    {
        private readonly IDiscountStrategy _strategy;

        public PriceCalculator(IDiscountStrategy strategy)
        {
            _strategy = strategy;
        }

        public decimal Calculate(decimal price)
        {
            return _strategy.Apply(price);
        }
    }
}
