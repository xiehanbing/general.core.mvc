using System;
using System.Configuration;
using General.Core.Data;
using Microsoft.Extensions.Options;

namespace General.RabbitMq.Config
{
    /// <summary>
    /// <see cref="MqConfigDom"/>创建工厂。
    /// </summary>
    public class MqConfigDomFactory
    {
        private static MqConfigDomMapAppsettiongJson _settings = General.Core.Data.AppSettingsProvider.MqConfig;
        /// <summary>
        /// 创建MqConfigDom一个实例。
        /// </summary>
        /// <returns>MqConfigDom</returns>
        internal static MqConfigDom CreateConfigDomInstance()
        {
            return GetConfigFormAppStting();
        }

        /// <summary>
        /// 获取物理配置文件中的配置项目。
        /// </summary>
        /// <returns></returns>
        private static MqConfigDom GetConfigFormAppStting()
        {
            var result = new MqConfigDom();

            var mqHost = _settings.MqHost;
            //var mqHost = ConfigurationManager.AppSettings["MQ:MqHost"];
            if (string.IsNullOrEmpty(mqHost))
                throw new Exception("RabbitMQ地址配置错误");
            result.MqHost = mqHost;
            var mqUserName = _settings.MqUserName;
            //var mqUserName = ConfigurationManager.AppSettings["MQ:MqUserName"];
            if (string.IsNullOrEmpty(mqUserName))
                throw new Exception("RabbitMQ用户名不能为NULL");

            result.MqUserName = mqUserName;
            var mqPassword = _settings.MqPassword;
            //var mqPassword = ConfigurationManager.AppSettings["MQ:MqPassword"];
            if (string.IsNullOrEmpty(mqPassword))
                throw new Exception("RabbitMQ密码不能为NULL");

            result.MqPassword = mqPassword;

            //var mqListenQueueName = ConfigurationManager.AppSettings["MQ:MqListenQueueName"];
            var mqListenQueueName = _settings.MqListenQueueName;
            if (string.IsNullOrEmpty(mqListenQueueName))
                throw new Exception("RabbitMQClient 默认侦听的MQ队列名称不能为NULL");

            result.MqListenQueueName = mqListenQueueName;
            var mqExchangeName = _settings.ExchangeName;
            //var mqExchangeName = ConfigurationManager.AppSettings["MQ:MqExchangeName"];
            if (string.IsNullOrEmpty(mqExchangeName))
            {
                mqExchangeName = "";
            }
            result.ExchangeName =mqExchangeName;
            var mqExchangeType = _settings.ExchangeType;
            //var mqExchangeType = ConfigurationManager.AppSettings["MQ:MqExchangeType"];
            if (!string.IsNullOrEmpty(mqExchangeType))
            {
                result.ExchangeType = mqExchangeType;
            }
            else
            {
                result.ExchangeType = ExchangeType.Fanout.ToString();
            }

            var port = _settings.Port;
            //var port = ConfigurationManager.AppSettings["MQ:MqPort"];
            if (!string.IsNullOrEmpty(port))
            {
                result.Port = Convert.ToInt32(port);
            }

            var vitualHost = _settings.VirtualHost;
            //var vitualHost = ConfigurationManager.AppSettings["MQ:VirtualHost"];
            if (string.IsNullOrEmpty(vitualHost))
            {
                vitualHost = "/";
            }
            result.VirtualHost = vitualHost;
            var routeKey = _settings.RoutingKey;
            //var routeKey = ConfigurationManager.AppSettings["MQ:RoutingKey"];
            result.RoutingKey = routeKey;
            return result;
        }
    }
}