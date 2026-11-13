using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level2
{
    public class Task29_FactoryNotification : ITask
    {
        public string Title => "Задание 29. Паттерн «Фабрика» (уведомления)";

        public string Description => "реализовать фабрику уведомлений.";

        public string[] Requirements => new string[]
        {
        "Интерфейс INotification с методом Send(string message).",
        "Реализации: EmailNotification, SmsNotification, PushNotification.",
        "NotificationFactory.Create(NotificationType type) — возвращает нужную реализацию.",
        "enum NotificationType { Email, Sms, Push }.",
        "Продемонстрировать создание и отправку через фабрику."
        };

        public void Run()
        {
            INotification email = NotificationFactory.Create(NotificationType.Email);
            INotification sms = NotificationFactory.Create(NotificationType.Sms);
            INotification push = NotificationFactory.Create(NotificationType.Push);

            email.Send("Ваш заказ оформлен.");
            sms.Send("Код подтверждения: 4812.");
            push.Send("У вас новое сообщение!");
        }
    }

    public enum NotificationType
    {
        Email,
        Sms,
        Push
    }

    public interface INotification
    {
        void Send(string message);
    }

    public class EmailNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine($"[Email]: {message}");
        }
    }

    public class SmsNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine($"[SMS]: {message}");
        }
    }

    public class PushNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine($"[Push]: {message}");
        }
    }

    public static class NotificationFactory
    {
        public static INotification Create(NotificationType type)
        {
            switch (type)
            {
                case NotificationType.Email:
                    return new EmailNotification();
                case NotificationType.Sms:
                    return new SmsNotification();
                case NotificationType.Push:
                    return new PushNotification();
                default:
                    throw new ArgumentException("Неизвестный тип уведомления");
            }
        }
    }
}
