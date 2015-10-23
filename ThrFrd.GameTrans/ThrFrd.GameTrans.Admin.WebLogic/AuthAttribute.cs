using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ThrFrd.GameTrans.Infrastructure.Entities.Entity;
using ThrFrd.GameTrans.Infrastructure.Entities.Enum;

namespace ThrFrd.GameTrans.Admin.WebLogic
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthAttribute : AuthorizeAttribute
    {
        private MemberRole[] status = null;
        public AuthAttribute(params MemberRole[] status)
        {
            this.status = status;
        }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            string username = httpContext.User.Identity.Name;
            if (String.IsNullOrEmpty(username))
            {
                username = System.Web.HttpContext.Current.User.Identity.Name;
                if (String.IsNullOrEmpty(username))
                {
                    return false;
                }
            }
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }
            var user = new User().Find(username);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string url = System.Web.HttpContext.Current.Request.RawUrl;
            string ToUrl = string.Empty;
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                ToUrl = "/account/login?returnurl=" + System.Web.HttpContext.Current.Server.UrlEncode(url);
            }
            else
            {
                ToUrl = "/home/forbidden";
            }
            filterContext.Result = new RedirectResult(ToUrl);
        }
    }
}
