using General.Core;
using General.Framework.Filters;
using General.Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace General.Framework.Controllers.Admin
{
    /// <summary>
    /// 只要登录就可以使用
    /// </summary>
    [AdminAuthFilter]
    public class PublicAdminController : AdminAreaController
    {
        private readonly IWorkContext _workContext;

        public PublicAdminController()
        {

            _workContext = EngineContext.CurrentEngin.Resolve<IWorkContext>();
        }

        public IWorkContext WorkContext => _workContext;
    }
}