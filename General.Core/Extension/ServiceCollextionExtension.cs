using System;
using System.Linq;
using System.Reflection;
using General.Core.Data;
using General.Core.Librs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace General.Core.Extension
{
    /// <summary>
    /// IServiceCollection 容器的扩展类
    /// </summary>
    public static class ServiceCollextionExtension
    {
        /// <summary>
        /// 程序集注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="serviceLifetime">生命周期</param>
        public static void AddAssembly(this IServiceCollection services, string assemblyName, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services) + "为空");

            if (string.IsNullOrEmpty(assemblyName))
                throw new ArgumentNullException(nameof(assemblyName) + "为空");

            //get services  assembly 获取指定程序集 
            var serviceAssembly = RuntimeHelper.GetAssemblyByName(assemblyName);
            if (serviceAssembly == null)
            {
                throw new ArgumentNullException(nameof(serviceAssembly) + ".dll 不存在");
            }
            var types = serviceAssembly.GetTypes();

            //去除抽象类和泛型类
            var list = types.Where(o => o.IsClass && !o.IsAbstract && !o.IsGenericType).ToList();
            foreach (var type in list)
            {
                var interfaceList = type.GetInterfaces();

                if (interfaceList.Any())
                {
                    var typeInterfac = interfaceList.First();
                    switch (serviceLifetime)
                    {
                        case ServiceLifetime.Scoped:
                            services.AddScoped(typeInterfac, type);
                            break;
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(typeInterfac, type);
                            break;
                        case ServiceLifetime.Transient:
                            services.AddTransient(typeInterfac, type);
                            break;
                        default:
                            services.AddScoped(typeInterfac, type);
                            break;
                    }


                    //services.AddScoped(typeInterfac, type);
                }
            }
        }

        /// <summary>
        /// 注入自定义的 节点
        /// </summary>
        /// <param name="configuration"></param>
        public static void LoadAppsetting(this IConfiguration configuration)
        {
            var setting = configuration.GetChildren().FirstOrDefault(o => o.Key.Equals("MQ"));
            if (setting == null) throw new ArgumentNullException("MQ配置为空");
            var child = setting.GetChildren().ToList();
            if (child == null) throw new ArgumentNullException("MQ配置为空");
            var mqConfig = new MqConfigDomMapAppsettiongJson();
            var newType = mqConfig.GetType();
            foreach (var item in newType.GetRuntimeProperties())
            {
                var typeName = item.Name;
                var value = child.FirstOrDefault(o => o.Key.Equals(typeName))?.Value ?? "";
                newType.GetProperty(typeName).SetValue(mqConfig, value, null);
            }
            AppSettingsProvider.MqConfig = mqConfig;
        }
    }
}