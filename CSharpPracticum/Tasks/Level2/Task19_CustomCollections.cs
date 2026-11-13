using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level2
{
    public class Task19_CustomCollections : ITask
    {
        public string Title => "Задание 19. Свой стек/очередь на массиве";

        public string Description => "реализовать MyStack<T> и MyQueue<T> на массиве.";

        public string[] Requirements => new string[]
        {
        "Стек: Push, Pop, Peek, Count, IsEmpty.",
        "Очередь: Enqueue, Dequeue, Peek, Count, IsEmpty.",
        "При переполнении — расширять внутренний массив (удвоение).",
        "Pop/Dequeue на пустой коллекции — бросать InvalidOperationException."
        };

        public void Run()
        {
            MyStack<int> stack = new MyStack<int>();
            for (int i = 1; i <= 6; i++)
            {
                stack.Push(i * 10);
            }
            Console.WriteLine($"Стек Peek: {stack.Peek()}, Count: {stack.Count}");
            while (!stack.IsEmpty)
            {
                Console.Write(stack.Pop() + " ");
            }
            Console.WriteLine();

            try
            {
                stack.Pop();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Ошибка стека: {ex.Message}");
            }

            MyQueue<string> queue = new MyQueue<string>();
            for (int i = 1; i <= 6; i++)
            {
                queue.Enqueue("Элемент " + i);
            }
            Console.WriteLine($"Очередь Peek: {queue.Peek()}, Count: {queue.Count}");
            while (!queue.IsEmpty)
            {
                Console.Write(queue.Dequeue() + " | ");
            }
            Console.WriteLine();

            try
            {
                queue.Dequeue();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Ошибка очереди: {ex.Message}");
            }
        }
    }

    public class MyStack<T>
    {
        private T[] _items = new T[4];
        private int _count = 0;

        public int Count
        {
            get { return _count; }
        }

        public bool IsEmpty
        {
            get { return _count == 0; }
        }

        public void Push(T item)
        {
            if (_count == _items.Length)
            {
                Array.Resize(ref _items, _items.Length * 2);
            }
            _items[_count++] = item;
        }

        public T Pop()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Стек пуст");
            }

            T item = _items[--_count];
            _items[_count] = default!;
            return item;
        }

        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Стек пуст");
            }

            return _items[_count - 1];
        }
    }

    public class MyQueue<T>
    {
        private T[] _items = new T[4];
        private int _head = 0;
        private int _tail = 0;
        private int _count = 0;

        public int Count
        {
            get { return _count; }
        }

        public bool IsEmpty
        {
            get { return _count == 0; }
        }

        public void Enqueue(T item)
        {
            if (_count == _items.Length)
            {
                T[] newArr = new T[_items.Length * 2];
                for (int i = 0; i < _count; i++)
                {
                    newArr[i] = _items[(_head + i) % _items.Length];
                }
                _items = newArr;
                _head = 0;
                _tail = _count;
            }

            _items[_tail] = item;
            _tail = (_tail + 1) % _items.Length;
            _count++;
        }

        public T Dequeue()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Очередь пуста");
            }

            T item = _items[_head];
            _items[_head] = default!;
            _head = (_head + 1) % _items.Length;
            _count--;
            return item;
        }

        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Очередь пуста");
            }

            return _items[_head];
        }
    }
}
