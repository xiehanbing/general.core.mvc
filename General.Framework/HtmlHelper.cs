using General.Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace General.Framework
{
    public static class HtmlHelperExtension
    {
        public static bool OwnPermission(this HtmlHelper helper, string message)
        {
            return true;
        }

        public static IWorkContext GetWorkContext(this IHtmlHelper helper)
        {
            return Core.EngineContext.CurrentEngin.Resolve<IWorkContext>();
        }
    }
}