using General.Framework.Filters;
using Microsoft.AspNetCore.Mvc;

namespace General.Framework.Controllers.Admin
{
    /// <summary>
    /// 只要登录就可以使用
    /// </summary>
    [AdminAuthFilter]
    public class PublicAdminController : AdminAreaController
    {
    }
}