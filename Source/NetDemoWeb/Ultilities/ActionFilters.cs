using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoWeb.Ultilities
{
    //dùng để phân quyền
    public class AuthActionFilter : ActionFilterAttribute, IActionFilter
    {
        public int RequiredPermission { get; set; }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //throw new NotImplementedException();

        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // if (AddHelpers.IsLogged(null) == false)
            //if (AddHelpers.GetUserInfo(null) == null)
            //{
            //    filterContext.Result = new RedirectResult("~/Account/Login");
            //    return;
            //}
            //var ui = AddHelpers.GetUserInfo(null);
            //if (ui.Permission < RequiredPermission)
            //{
            //    filterContext.Result = new HttpUnauthorizedResult();
            //}
        }
    }
}