using MvcPresentationLayer.Models;
using System.Web.Mvc;
public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (string.IsNullOrWhiteSpace(Cookie.Get("SentinelaDoValeLogin")))
            {
                filterContext.Result = new RedirectResult(Url.Action("Logar", "Login"));
            }
            base.OnActionExecuting(filterContext);
        }
    }