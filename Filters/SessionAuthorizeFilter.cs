using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebFormL1.Filters
{
    public class SessionAuthorizeFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Session.GetString("IsAuthenticated") != "true")
            {
                context.Result = new RedirectToActionResult("Login", "Authenticate", null);
            }
        }
    }
}