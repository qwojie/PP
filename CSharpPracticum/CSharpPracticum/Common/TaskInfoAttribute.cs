using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Common
{
    public static class TaskInfoAttribute
    {
        public static void Show(ITask task)
        {
            Console.WriteLine("================================");
            Console.WriteLine(task.Title);
            Console.WriteLine("================================");
            Console.WriteLine();
                
            Console.WriteLine("Описание:\n");
            Console.WriteLine(task.Description);

            Console.WriteLine("\nТребования:\n");

            foreach (string requirement in task.Requirements)
            {
                Console.WriteLine($" - {requirement}");
            }

            Console.WriteLine("================================");
        }
    }
}
