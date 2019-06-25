using Newtonsoft.Json;

namespace General.Core.Data
{
    /// <summary>
    /// appsettings支持
    /// </summary>
    public class AppSettingsProvider
    {
        public string SettingStr { get; set; }
        public static MqConfigDomMapAppsettiongJson MqConfig { get; set; }

    }
}