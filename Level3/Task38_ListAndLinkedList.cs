using CSharpPracticum.Common;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CSharpPracticum.Tasks.Level3
{
    public class Task38_ListAndLinkedList : ITask
    {
        public string Title => "Задание 38. List<T> vs LinkedList<T> (бенчмарк)";

        public string Description => "сравнить вставку в середину List<T> и LinkedList<T>.";

        public string[] Requirements => new string[]
        {
        "Создать List<int> и LinkedList<int> по 100 000 элементов.",
        "Вставить 1000 элементов в середину каждой коллекции.",
        "Замерить время через Stopwatch.",
        "Вывести оба времени и объяснить результат в комментарии."
        };

        public void Run()
        {
            int initialCount = 100000;
            int insertCount = 1000;

            List<int> list = new List<int>(initialCount);
            LinkedList<int> linkedList = new LinkedList<int>();

            for (int i = 0; i < initialCount; i++)
            {
                list.Add(i);
                linkedList.AddLast(i);
            }

            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < insertCount; i++)
            {
                list.Insert(list.Count / 2, i);
            }
            sw.Stop();
            long listTime = sw.ElapsedMilliseconds;

            LinkedListNode<int>? middleNode = linkedList.First;
            for (int i = 0; i < initialCount / 2 && middleNode != null; i++)
            {
                middleNode = middleNode.Next;
            }

            sw.Restart();
            if (middleNode != null)
            {
                for (int i = 0; i < insertCount; i++)
                {
                    linkedList.AddAfter(middleNode, i);
                }
            }
            sw.Stop();
            long linkedListTime = sw.ElapsedMilliseconds;

            Console.WriteLine($"Вставка {insertCount} элементов в середину:");
            Console.WriteLine($"List<int>: {listTime} мс");
            Console.WriteLine($"LinkedList<int>: {linkedListTime} мс");
            Console.WriteLine("Объяснение: LinkedList быстрее при вставке по известному узлу (меняются только ссылки), но медленнее при поиске по индексу. List сдвигает все элементы в массиве при каждой вставке.");
        }
    }
}
