namespace General.Framework.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWorkContext
    {
        /// <summary>
        /// 当前登录用户
        /// </summary>
        Entity.User.SysUser CurrentUser { get; }

        void InitRegister();
    }
}