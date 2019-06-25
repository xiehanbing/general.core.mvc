using System;
using General.Core.Extension;
using General.RabbitMq.Config;
using RabbitMQ.Client;

namespace General.RabbitMq
{
    /// <summary>
    /// RabbitMQClient创建工厂。
    /// </summary>
    public class RabbitMqClientFactory
    {
        /// <summary>
        /// 创建一个单例的RabbitMqClient实例。
        /// </summary>
        /// <returns>IRabbitMqClient</returns>
        public static IRabbitMqClient CreateRabbitMqClientInstance()
        {
            var mqConfig = MqConfigDomFactory.CreateConfigDomInstance();
            var rabbitMqClientContext = new RabbitMqClientContext
            {
                ListenQueueName = mqConfig.MqListenQueueName,
                InstanceCode = Guid.NewGuid().ToString(),
                ExchangeName = mqConfig.ExchangeName,
                ExchangeType =mqConfig.ExchangeType,
                RoutingKey = mqConfig.RoutingKey
            };

            RabbitMqClient.Instance = new RabbitMqClient
            {
                Context = rabbitMqClientContext
            };

            return RabbitMqClient.Instance;
        }

        /// <summary>
        /// 创建一个IConnection。
        /// </summary>
        /// <returns></returns>
        internal static IConnection CreateConnection()
        {
            var mqConfigDom = MqConfigDomFactory.CreateConfigDomInstance(); //获取MQ的配置

            const ushort heartbeat = 60;
            var factory = new ConnectionFactory()
            {
                HostName = mqConfigDom.MqHost,
                UserName = mqConfigDom.MqUserName,
                Password = mqConfigDom.MqPassword,
                Port = mqConfigDom.Port,
                VirtualHost = mqConfigDom.VirtualHost,
                RequestedHeartbeat = heartbeat, //心跳超时时间
                AutomaticRecoveryEnabled = true //自动重连
            };

            return factory.CreateConnection(); //创建连接对象
        }

        /// <summary>
        /// 创建一个IModel。
        /// </summary>
        /// <param name="connection">IConnection.</param>
        /// <returns></returns>
        internal static IModel CreateModel(IConnection connection)
        {
            return connection.CreateModel(); //创建通道
        }
    }
}