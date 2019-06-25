using System;
using General.RabbitMq.BaseListener;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace General.RabbitMq.Listener
{
    public class ChapterLister : RabbitListener
    {
        private readonly ILogger<RabbitListener> _logger;
        // 因为Process函数是委托回调,直接将其他Service注入的话两者不在一个scope,
        // 这里要调用其他的Service实例只能用IServiceProvider CreateScope后获取实例对象
        private readonly IServiceProvider _services;
        public ChapterLister(IServiceProvider services,
            IOptions<AppConfiguration> options,
            ILogger<RabbitListener> logger) : base(options)
        {
            _logger = logger;
            _services = services;
        }

        public override bool Process(string message)
        {
            var taskMessage = JToken.Parse(message);
            if (taskMessage == null)
            {
                //todo 记录 message 日志
                //返回false 的是否 直接驳回此消息，表示处理不了
                return false;
            }

            try
            {
                using (var scope = _services.CreateScope())
                {
                    //
                    //var service = scope.ServiceProvider.GetRequiredService<>();
                    return true;
                }
            }
            catch (Exception exception)
            {
                //todo 记录日志
                Console.WriteLine(exception);
                return false;
            }
        }
    }
}