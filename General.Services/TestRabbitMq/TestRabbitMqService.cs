using System;
using System.IO;
using General.RabbitMq;

namespace General.Services.TestRabbitMq
{
    public class TestRabbitMqService : ITestRabbitMqService
    {
        private static string filePath = @"E:\File\myGeneralRabbitlog{0}.txt";
        public void Listing()
        {
            RabbitMqClient.Instance.ActionEventMessage += MqClient_ActionEventMessage;
            RabbitMqClient.Instance.OnListeningByFanout();
        }

        private static void MqClient_ActionEventMessage(EventMessageResult eventMessageResult)
        {
            if (eventMessageResult.EventMessageBytes.EventMessageMarkcode == "")
            {
                var message = eventMessageResult.MessageBytes;
                filePath = string.Format(filePath, DateTime.Now.ToString("yyyyMMdd"));
                using (FileStream stream = new FileStream(filePath, FileMode.Append))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine($"{DateTime.Now},正在在执行！");
                    writer.WriteLine($"{DateTime.Now},{message}");
                }
                eventMessageResult.IsOperationOk = true; //处理成功
            }
        }
    }
}