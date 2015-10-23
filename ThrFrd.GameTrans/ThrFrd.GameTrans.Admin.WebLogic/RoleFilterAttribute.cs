using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThrFrd.GameTrans.Tool;
using System.Web.Mvc;
using ThrFrd.GameTrans.Admin.Biz;

namespace ThrFrd.GameTrans.Admin.WebLogic
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class RoleFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string userName = filterContext.HttpContext.User.Identity.Name;
            if (String.IsNullOrEmpty(userName))
            {
                filterContext.Result = new RedirectResult("/account/login");
                return;
            }
            string url = filterContext.HttpContext.Request.Path.ToLower();
            var module = ModuleBiz.GetModelListByUser(userName);
            var moduleList = module.Where(c => c.Url.ToLower().Contains(url)).ToList();
            if (moduleList == null || moduleList.Count <= 0)
            {
                string msg = EncodeByBase64.Encode("无访问权限，请联系管理员");
                filterContext.Result = new RedirectResult("/SystemTip/FailTip?msg=" + msg + "&backurl=" + System.Web.HttpContext.Current.Server.UrlEncode("/home/index"));
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
