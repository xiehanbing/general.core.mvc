namespace General.Core.Data
{
    /// <summary>
    /// 对应配置文件的属性
    /// </summary>
    public class MqConfigDomMapAppsettiongJson
    {
        /// <summary>
        /// 消息队列的地址。
        /// </summary>
        public string MqHost { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string MqUserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string MqPassword { get; set; }

        /// <summary>
        /// 客户端默认监听的队列名称
        /// </summary>
        public string MqListenQueueName { get; set; }
        /// <summary>
        /// 交换机名称
        /// </summary>
        public string ExchangeName { get; set; }
        /// <summary>
        /// 交换机类型
        /// </summary>
        public string ExchangeType { get; set; }

        /// <summary>
        /// 端口 默认5672
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// 虚拟机
        /// </summary>
        public string VirtualHost { get; set; } = "/";
        /// <summary>
        /// 路由键
        /// </summary>
        public string RoutingKey { get; set; }
    }
}