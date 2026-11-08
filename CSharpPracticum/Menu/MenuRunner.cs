using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Menu
{
    public class MenuRunner
    {

        public static void Start()
        {

            while (true)
            {
                Console.Clear();

                Console.WriteLine("=== Практикум по C# ===");
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("1. Уровень 1 — Базовый (задания 1–15)");
                Console.WriteLine("2. Уровень 2 — Средний (задания 16–35)");
                Console.WriteLine("3. Уровень 3 — Продвинутый (задания 36–50)");
                Console.WriteLine("4. Выход");
                Console.WriteLine("-------------------------------------");
                Console.Write("Выберите уровень (1-4):");
                

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            LevelMenu.ShowLevel1();
                            break;
                        case 2:
                            LevelMenu.ShowLevel2();
                            break;
                        case 3:
                            LevelMenu.ShowLevel3();
                            break;
                        case 4:
                            return;
                    }
                }
                else
                {
                    Console.WriteLine("ВВЕДИТЕ ЧИСЛО ОТ 1 ДО 4 БЛИН");
                    Console.ReadLine();
                }


            }
        }

    }
}
