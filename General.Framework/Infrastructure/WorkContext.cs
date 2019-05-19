using General.Entity.User;
using General.Framework.Security.Admin;

namespace General.Framework.Infrastructure
{
    public class WorkContext:IWorkContext
    {
        private IAdminAuthService _authenticationService;
        public WorkContext(IAdminAuthService authenticationService)
        {
            _authenticationService = authenticationService;


        }
        public SysUser CurrentUser
        {
            get { return _authenticationService.GetCurrentUser(); }
        }
    }
}