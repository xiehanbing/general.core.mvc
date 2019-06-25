using RabbitMQ.Client;

namespace General.RabbitMq
{
    public class RabbitMqClientContext
    {
        /// <summary>
        /// 用于发消息的connection
        /// </summary>
        public IConnection SendConnection { get; set; }
        /// <summary>
        /// 用户发消息的Channel
        /// </summary>
        public IModel SendChannel { get; set; }
        /// <summary>
        /// 用于监听的Connection
        /// </summary>
        public IConnection ListenConnection { get; set; }
        /// <summary>
        /// 用于监听的channel
        /// </summary>
        public IModel ListenChannel { get; set; }
        /// <summary>
        /// 默认监听的队列名称
        /// </summary>
        public string ListenQueueName { get; set; }
        /// <summary>
        /// 实例编号
        /// </summary>
        public string InstanceCode { get; set; }
        /// <summary>
        /// 交换机名称
        /// </summary>
        public string ExchangeName { get; set; }
        /// <summary>
        /// 交换机类型
        /// </summary>
        public string ExchangeType { get; set; }
        /// <summary>
        /// 路由键
        /// </summary>
        public string RoutingKey { get; set; }
    }
}