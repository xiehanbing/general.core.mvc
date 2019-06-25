namespace General.RabbitMq.BaseListener
{
    /// <summary>
    /// 配置
    /// </summary>
    public class AppConfiguration
    {
        /// <summary>
        /// host
        /// </summary>
        public string RabbitHost { get; set; }
        /// <summary>
        /// userNAME
        /// </summary>
        public string RabbitUserName { get; set; }
        /// <summary>
        /// password
        /// </summary>
        public string RabbitPassword { get; set; }
        /// <summary>
        /// port
        /// </summary>
        public int  RabbitPort { get; set; }
    }
}