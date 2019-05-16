namespace General.Framework.Security.Admin
{
    public interface IAuthenticationService
    {
        Entity.User.SysUser GetCurrentUser();
    }
}