using General.Entity.User;
using General.Framework.Security.Admin;

namespace General.Framework.Infrastructure
{
    public class WorkContext:IWorkContext
    {
        private IAuthenticationService _authenticationService;
        public WorkContext(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        public SysUser CurrentUser
        {
            get { return _authenticationService.GetCurrentUser(); }
        }
    }
}