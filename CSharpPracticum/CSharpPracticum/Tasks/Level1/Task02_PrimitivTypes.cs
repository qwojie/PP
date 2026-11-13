using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    public class Task02_PrimitivTypes : ITask
    {
        public string Title => "Задание 2. Переменные примитивных типов";
        public string Description => "объявить переменные всех примитивных типов и вывести их значения и типы.";
        public string[] Requirements => new string[]
        {
        "Объявить минимум:int,long,double,float,decimal,bool,char,string",
        "Для каждой переменной вывести значение иGetType().Name.",
        "decimal— с суффиксом m,float— сf."
        };

        public void Run()
        {
            int num = 12;
            long chis = 677347282738387128;
            double doub = 12.2;
            float temu = 45f;
            decimal dec = 12m;
            bool boobs = true;
            char cham = 'F';
            string sting = "sixseven";

            Console.WriteLine(num.GetType().Name + " "  + num);
            Console.WriteLine(chis.GetType().Name + " " + chis);
            Console.WriteLine(doub.GetType().Name + " " + doub);
            Console.WriteLine(temu.GetType().Name + " " + temu);
            Console.WriteLine(dec.GetType().Name + " " + dec);
            Console.WriteLine(boobs.GetType().Name + " " + boobs);
            Console.WriteLine(cham.GetType().Name + " " + cham);
            Console.WriteLine(sting.GetType().Name + " " + sting);

        }

    }
}
