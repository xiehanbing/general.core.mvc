using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace General.RabbitMq
{
    public class RabbitMqRegister
    {
        private readonly ConnectionFactory _factory;
        private IConnection _connection;
        private readonly RabbitMQ.Client.IModel _chanel;
        private const string exchangeName = "exchange_test";
        private const string exchangeType = "fanout";//direct
        private const string routingKey = "key.test.private.two";
        public RabbitMqRegister()
        {
            _factory = new ConnectionFactory()
            {
                UserName = "guest",
                Password = "guest",
                HostName = "127.0.0.1",
                VirtualHost = "v_2"
            };
            _connection = _factory.CreateConnection();
            _chanel = _connection.CreateModel();
        }

        private void Open()
        {

        }

        //private void Close()
        //{
        //    _chanel.Close();
        //    _connection.Close();
        //}

        public void SendMessage(string queueName, string message)
        {
            Open();
            //todo 发送消息
            _chanel.QueueDeclare(queueName, false, false, true, null);
            var bytes = Encoding.UTF8.GetBytes(message);
            _chanel.BasicPublish("", queueName, null, bytes);
            //Console.WriteLine("发送消息");
            //Close();
        }

        public void ReviceMessage(string queueName, string someone, Action<string> eventHandel)
        {
            EventingBasicConsumer consumer = new EventingBasicConsumer(_chanel);
            _chanel.ExchangeDeclare(exchangeName, exchangeType, false, false, null);
            //_chanel.ExchangeDeclare(exchangeName, exchangeType);
            _chanel.QueueDeclare(queueName, false, false, false, null);//持久化  排他性 自动删除
            _chanel.QueueBind(queueName, exchangeName, routingKey, null);
            //_chanel.QueueBind(queueName, exchangeName, routingKey + someone);//交换机与
            consumer.Received += (ch, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body);
                eventHandel?.Invoke(message);
                _chanel.BasicAck(ea.DeliveryTag, false);//确认该消息已被消费
            };
            _chanel.BasicConsume(queueName, false, consumer);
            //_chanel.BasicConsume(queueName, false, consumer); //启动消费者

            // Console.WriteLine("客户端2已启动");
        }
        public void SendMessage(string message, string someone, Action<string> eventHandel)
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            _chanel.BasicPublish(exchangeName, routingKey + someone, null, bytes);
            eventHandel?.Invoke(message);
        }
    }
}