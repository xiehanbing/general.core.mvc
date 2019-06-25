namespace General.RabbitMq
{
    /// <summary>
    /// rabbitmq client 接口
    /// </summary>
    public interface IRabbitMqClient
    {
        /// <summary>
        /// rabbitmqclient  数据上下文
        /// </summary>
        RabbitMqClientContext Context { get; set; }
        /// <summary>
        /// 消息被本地激活事件，通过该绑定事件来获取消息队列推送过来的消息， 只能绑定一个事件处理程序
        /// </summary>
        event ActionEvent ActionEventMessage;
        /// <summary>
        /// 触发一个事件，向队列推送一个事件消息。
        /// </summary>
        /// <param name="eventMessage">消息类型实例</param>
        /// <param name="exchange">Exchange名称</param>
        /// <param name="queue">队列名称</param>
        void TriggerEventMessage(EventMessage eventMessage, string exchange, string queue);
        /// <summary>
        /// 开始消息队列的默认监听 （队列）
        /// </summary>
        void OnListening();
        /// <summary>
        /// direct 模式监听
        /// </summary>
        void OnListeningByDirect();
        /// <summary>
        /// fanout 模式监听
        /// </summary>
        void OnListeningByFanout();
        /// <summary>
        /// topic 模式监听
        /// </summary>
        void OnListeningByTopic();
    }
}