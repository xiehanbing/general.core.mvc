using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace General.RabbitMq.BaseListener
{
    public class RabbitListener : IHostedService
    {
        private readonly IConnection connection;
        private readonly IModel channel;

        public RabbitListener(IOptions<AppConfiguration> options)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = options.Value.RabbitHost,
                    UserName = options.Value.RabbitUserName,
                    Password = options.Value.RabbitPassword,
                    Port = options.Value.RabbitPort,
                };
                connection = factory.CreateConnection();
                channel = connection.CreateModel();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Register();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            connection.Close();
            return Task.CompletedTask;
        }

        /// <summary>
        /// 路由键
        /// </summary>
        protected string routeKey;
        /// <summary>
        /// 队列名称
        /// </summary>
        protected string queueName;
        /// <summary>
        /// 处理消息的方法 可重写
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public virtual bool Process(string message)
        {
            //todo 处理消息
            throw new NotImplementedException();
        }
        /// <summary>
        /// 注册消费者监听
        /// </summary>
        public void Register()
        {
            channel.ExchangeDeclare(exchange: "message", type: "topic");
            channel.QueueDeclare(queueName, false);
            channel.QueueBind(queueName, "message", routeKey);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                var result = Process(message);
                if (result)
                {
                    channel.BasicAck(ea.DeliveryTag, false);
                }
            };
            channel.BasicConsume(queueName, false, consumer);
        }
        /// <summary>
        /// 关闭链接
        /// </summary>
        public void DeRegister()
        {
            connection.Close();
        }
    }
}