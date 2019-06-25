using System;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace General.RabbitMq
{
    /// <summary>
    /// 表示消息到达客户端发起的事件。
    /// </summary>
    /// <param name="result">EventMessageResult 事件消息对象</param>
    public delegate void ActionEvent(EventMessageResult result);
    public class RabbitMqClient : IRabbitMqClient
    {
        #region static fields
        /// <summary>
        /// 客户端实例私有字段
        /// </summary>
        private static IRabbitMqClient _instanceClient;
        /// <summary>
        /// 返回全局唯一的rabbitmqclient 实例
        /// </summary>
        public static IRabbitMqClient Instance
        {
            get
            {
                if (_instanceClient == null)
                {
                    RabbitMqClientFactory.CreateRabbitMqClientInstance();
                }
                return _instanceClient;
            }
            internal set { _instanceClient = value; }
        }

        #endregion


        #region Instance fields
        /// <summary>
        /// 数据上下文
        /// </summary>
        public RabbitMqClientContext Context { get; set; }
        /// <summary>
        /// 事件激活委托实例
        /// </summary>
        private ActionEvent _actionMessage;
        /// <summary>
        /// 当侦听的队列中有消息到达时触发的执行事件
        /// </summary>
        public event ActionEvent ActionEventMessage
        {
            add
            {
                if (_actionMessage == null)
                    _actionMessage += value;
            }
            remove
            {
                if (_actionMessage != null)
                    _actionMessage -= value;
            }
        }


        #endregion


        #region Send method
        /// <summary>
        /// 触发一个事件且将事件打包成消息发送到远程队列中
        /// </summary>
        /// <param name="eventMessage">发送的消息实例</param>
        /// <param name="exchange">exchange名称</param>
        /// <param name="queue">队列名称</param>
        public void TriggerEventMessage(EventMessage eventMessage, string exchange, string queue)
        {
            Context.SendConnection = RabbitMqClientFactory.CreateConnection();//获取链接
            using (Context.SendConnection)
            {
                Context.SendChannel = RabbitMqClientFactory.CreateModel(Context.SendConnection);//获得通道
                const byte deliveryMode = 2;
                using (Context.SendChannel)
                {
                    var messageSerializer = MessageSerializerFactory.CreateMessageSerializerInstance();//反序列化消息
                    var properties = Context.SendChannel.CreateBasicProperties();
                    properties.DeliveryMode = deliveryMode;//表示持久化消息 
                    //推送消息
                    Context.SendChannel.BasicPublish(exchange, queue, properties, messageSerializer.SerializerBytes(eventMessage));
                }
            }
        }
        #endregion

        #region receive method
        /// <summary>
        /// 开始侦听默认的队列
        /// </summary>
        public void OnListening()
        {
            Task.Factory.StartNew(ListenInit);
        }
        /// <summary>
        /// direct 
        /// </summary>
        public void OnListeningByDirect()
        {
            Task.Factory.StartNew(ListenByDirectInit);
        }
        /// <summary>
        /// fanout
        /// </summary>
        public void OnListeningByFanout()
        {
            Task.Factory.StartNew(ListenByFanoutInit);
        }
        /// <summary>
        /// topic
        /// </summary>
        public void OnListeningByTopic()
        {
            Task.Factory.StartNew(ListenByTopicInit);
        }

        /// <summary>
        /// 侦听初始化
        /// </summary>
        private void ListenInit()
        {
            Context.ListenConnection = RabbitMqClientFactory.CreateConnection();
            Context.ListenConnection.ConnectionShutdown += (o, e) =>
            {
                //todo log 记录日志信息 connection shutdown:e.ReplyText
                //if (LogLocation.Log.IsNotNull())
                //    LogLocation.Log.WriteInfo("SCM.RabbitMQClient", "connection shutdown:" + e.ReplyText);
            };
            Context.ListenChannel = RabbitMqClientFactory.CreateModel(Context.ListenConnection);
            //创建消费者
            var consumer = new EventingBasicConsumer(Context.ListenChannel);
            consumer.Received += Consumer_Recevied;
            //一次只获取一个消息进行消费
            Context.ListenChannel.BasicQos(0, 1, false);
            Context.ListenChannel.BasicConsume(Context.ListenQueueName, false, consumer);
        }
        /// <summary>
        /// Direct 模式初始化
        /// </summary>
        private void ListenByDirectInit()
        {
            Context.ListenConnection = RabbitMqClientFactory.CreateConnection();
            Context.ListenConnection.ConnectionShutdown += (o, e) =>
            {
                //todo log 记录日志信息 connection shutdown:e.ReplyText
                //if (LogLocation.Log.IsNotNull())
                //    LogLocation.Log.WriteInfo("SCM.RabbitMQClient", "connection shutdown:" + e.ReplyText);
            };
            Context.ListenChannel = RabbitMqClientFactory.CreateModel(Context.ListenConnection);
            Context.ListenChannel.ExchangeDeclare(Context.ExchangeName, Context.ExchangeType);
            Context.ListenChannel.QueueDeclare(Context.ListenQueueName, false, false, false);
            Context.ListenChannel.QueueBind(Context.ListenQueueName, Context.ExchangeName, Context.RoutingKey);
            //创建消费者
            //创建消费者
            var consumer = new EventingBasicConsumer(Context.ListenChannel);
            consumer.Received += Consumer_Recevied;
            //一次只获取一个消息进行消费
            Context.ListenChannel.BasicQos(0, 1, false);
            Context.ListenChannel.BasicConsume(Context.ListenQueueName, false, consumer);
        }
        /// <summary>
        /// Fanout 模式初始化
        /// </summary>
        private void ListenByFanoutInit()
        {
            Context.ListenConnection = RabbitMqClientFactory.CreateConnection();
            Context.ListenConnection.ConnectionShutdown += (o, e) =>
            {
                //todo log 记录日志信息 connection shutdown:e.ReplyText
                //if (LogLocation.Log.IsNotNull())
                //    LogLocation.Log.WriteInfo("SCM.RabbitMQClient", "connection shutdown:" + e.ReplyText);
            };
            Context.ListenChannel = RabbitMqClientFactory.CreateModel(Context.ListenConnection);

            Context.ListenChannel.ExchangeDeclare(Context.ExchangeName, Context.ExchangeType);
            Context.ListenChannel.QueueDeclare(Context.ListenQueueName, false, false, false);
            Context.ListenChannel.QueueBind(Context.ListenQueueName, Context.ExchangeName, Context.RoutingKey);
            //创建消费者
            var consumer = new EventingBasicConsumer(Context.ListenChannel);

            consumer.Received += Consumer_Recevied;
            //一次只获取一个消息进行消费
            Context.ListenChannel.BasicQos(0, 1, false);
            Context.ListenChannel.BasicConsume(Context.ListenQueueName, false, consumer);
        }
        /// <summary>
        /// Topic 模式初始化
        /// </summary>
        private void ListenByTopicInit()
        {
            Context.ListenConnection = RabbitMqClientFactory.CreateConnection();
            Context.ListenConnection.ConnectionShutdown += (o, e) =>
            {
                //todo log 记录日志信息 connection shutdown:e.ReplyText
                //if (LogLocation.Log.IsNotNull())
                //    LogLocation.Log.WriteInfo("SCM.RabbitMQClient", "connection shutdown:" + e.ReplyText);
            };
            Context.ListenChannel = RabbitMqClientFactory.CreateModel(Context.ListenConnection);
            Context.ListenChannel.ExchangeDeclare(Context.ExchangeName, Context.ExchangeType);
            Context.ListenChannel.QueueDeclare(Context.ListenQueueName, false, false, false);
            Context.ListenChannel.QueueBind(Context.ListenQueueName, Context.ExchangeName, Context.RoutingKey);
            //创建消费者
            var consumer = new EventingBasicConsumer(Context.ListenChannel);
            consumer.Received += Consumer_Recevied;
            //一次只获取一个消息进行消费
            Context.ListenChannel.BasicQos(0, 1, false);
            Context.ListenChannel.BasicConsume(Context.ListenQueueName, false, consumer);
        }
        /// <summary>
        /// 接受到消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Consumer_Recevied(object sender, BasicDeliverEventArgs e)
        {
            try
            {
                //获取消息返回对象
                var result = EventMessage.BuildEventMessageResult(e.Body);
                //触发外部监听事件
                _actionMessage?.Invoke(result);
                if (!result.IsOperationOk)
                {
                    //未能消费此消息，重新放入队列头
                    Context.ListenChannel.BasicReject(e.DeliveryTag, true);
                }
                else if (!Context.ListenChannel.IsClosed)
                {
                    Context.ListenChannel.BasicAck(e.DeliveryTag, false);
                }

            }
            catch (Exception exception)
            {
                //todo 记录日志
                //Console.WriteLine(exception);
                //throw;
            }
        }

        #endregion

        #region dispose
        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            if (Context.SendConnection == null) return;

            if (Context.SendConnection.IsOpen)
                Context.SendConnection.Close();

            Context.SendConnection.Dispose();
        }


        #endregion
    }
}