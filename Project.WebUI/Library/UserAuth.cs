using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebUI.Library
{
    public class UserAuth : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filtercontext)
        {
            var SessionControl = filtercontext.HttpContext.Session.GetString("UserSession");
            if (SessionControl == null)
                filtercontext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                                        new { action = "Index", controller = "Account" }));
        }
    }
}
