using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace CSharpPracticum.Menu
{
    public class LevelMenu
    {
        public static void Start() {

            bool IsRun = true;
            while (IsRun)
            {
                Console.WriteLine("=== Практикум по C# ===");
                Console.WriteLine("1. Уровень 1 — Базовый (задания 1–15)");
                Console.WriteLine("2. Уровень 2 — Средний (задания 16–35)");
                Console.WriteLine("3. Уровень 3 — Продвинутый (задания 36–50)");
                Console.WriteLine("4. Выход");

                Console.Write("Выберите уровень (1-4): ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("=== 1. Уровень 1 — Базовый ===");
                            break;
                        case 2:
                            Console.WriteLine("=== 2. Уровень 2 — Средний ===");
                            break;
                        case 3:
                            Console.WriteLine("=== 3. Уровень 3 — Продвинутый ===");
                            break;
                        case 4:
                            return;
                        default:
                            Console.WriteLine("Нужно выбрать от 1 до 4");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ВВЕДИТЕ ЧИСЛО ОТ 1 ДО 4 БЛИН");
                }
            }
        }
    }
}
