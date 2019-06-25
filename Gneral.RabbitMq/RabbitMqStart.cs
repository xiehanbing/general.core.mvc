using System;
using System.IO;

namespace General.RabbitMq
{
    public class RabbitMqStart
    {
        private static string filePath = @"E:\File\myGeneralRabbitlog{0}.txt";
        public static void RabbitMqRun()
        {
            RabbitMqRegister rabbitMqHelper = new RabbitMqRegister();
            rabbitMqHelper.ReviceMessage("queue.", "", ReciveMsg);
        }
        static void ReciveMsg(string message)
        {
            filePath = string.Format(filePath, DateTime.Now.ToString("yyyyMMdd"));
            using (FileStream stream = new FileStream(filePath, FileMode.Append))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine($"{DateTime.Now},正在在执行！");
                writer.WriteLine($"{DateTime.Now},{message}");
            }
        }
    }
}