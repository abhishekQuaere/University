using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UniversityRecruitment.Utilities;

namespace UniversityRecruitment.Filters
{
    public class AuthorizeAdmin : AuthorizeAttribute
    {
        SessionManager sm = new SessionManager();
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var rd = HttpContext.Current.Request.RequestContext.RouteData;
            string currentAction = rd.GetRequiredString("action");
            string currentController = rd.GetRequiredString("controller");
            bool isValidUser = false;

            if (filterContext.HttpContext.Request.UrlReferrer == null || filterContext.HttpContext.Request.Url.Host != filterContext.HttpContext.Request.UrlReferrer.Host)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {

                }
                else
                {
                    sm.UserExceptionSession = "URL Tempering is not allowed Please do not refresh or try to edit Url.";
                    filterContext.Result = new RedirectToRouteResult(
                                       new RouteValueDictionary
                           {
                                       { "action", "Login" },
                                       { "controller", "Home" }
                           });
                }
            }


            if (sm.userId != 0)
            {
                isValidUser = true;
            }
            else
            {
                isValidUser = true;

                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new EmptyResult();
                    filterContext.HttpContext.Response.StatusCode = 401;
                    filterContext.HttpContext.Response.End();
                }
                else
                {
                    if (currentController == "Admin")
                    {
                        sm.UserExceptionSession = "Your session has expired and you have been Logged Out.";
                        filterContext.Result = new RedirectToRouteResult(
                                           new RouteValueDictionary
                               {
                                       { "action", "Login" },
                                       { "controller", "Home" }
                               });
                    }
                    else
                    {
                        sm.UserExceptionSession = "Your session has expired and you have been Logged Out.";
                        filterContext.Result = new RedirectToRouteResult(
                                           new RouteValueDictionary
                               {
                                       { "action", "Login" },
                                       { "controller", "Home" }
                               });
                    }
                }
            }

            if (!isValidUser)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new EmptyResult();
                    filterContext.HttpContext.Response.StatusCode = 401;
                    filterContext.HttpContext.Response.End();
                }
                else
                {
                    sm.UserExceptionSession = "Your are not authorized.";
                    filterContext.Result = new RedirectToRouteResult(
                                       new RouteValueDictionary
                           {
                                       { "action", "Login" },
                                       { "controller", "Home" }
                           });
                }
            }

        }
    }
}