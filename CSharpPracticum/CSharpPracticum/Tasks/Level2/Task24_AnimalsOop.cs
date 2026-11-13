using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level2
{
    public class Task24_AnimalsOop : ITask
    {
        public string Title => "Задание 24. ООП: Animal → Dog/Cat/Bird";

        public string Description => "построить иерархию животных.";

        public string[] Requirements => new string[]
        {
        "Абстрактный класс Animal: свойство Name, абстрактный метод MakeSound(), переопределённый ToString().",
        "Классы Dog, Cat, Bird — наследники, реализуют MakeSound().",
        "Bird дополнительно имеет метод Fly().",
        "Продемонстрировать полиморфизм: List<Animal> с разными животными, вызов MakeSound() в цикле."
        };

        public void Run()
        {
            List<Animal> animals = new List<Animal>
            {
                new Dog("Бобик"),
                new Cat("Мурка"),
                new Bird("Кеша")
            };

            foreach (Animal animal in animals)
            {
                Console.WriteLine(animal.ToString());
                animal.MakeSound();
                if (animal is Bird bird)
                {
                    bird.Fly();
                }
            }
        }
    }

    public abstract class Animal
    {
        public string Name { get; set; }

        protected Animal(string name)
        {
            Name = name;
        }

        public abstract void MakeSound();

        public override string ToString()
        {
            return $"{GetType().Name} по имени {Name}";
        }
    }

    public class Dog : Animal
    {
        public Dog(string name) : base(name)
        {
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{Name}: Гав-гав!");
        }
    }

    public class Cat : Animal
    {
        public Cat(string name) : base(name)
        {
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{Name}: Мяу!");
        }
    }

    public class Bird : Animal
    {
        public Bird(string name) : base(name)
        {
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{Name}: Чирик-чирик!");
        }

        public void Fly()
        {
            Console.WriteLine($"{Name} летит!");
        }
    }
}
