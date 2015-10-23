using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ThrFrd.GameTrans.Admin.Biz;
using ThrFrd.GameTrans.Infrastructure.Entities.Entity;

namespace ThrFrd.GameTrans.Admin.WebLogic
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userName = System.Web.HttpContext.Current.User.Identity.Name;
            var moduleList = ModuleBiz.GetModelListByUserForMenu(userName);
            ViewBag.moduleList = moduleList.Where(c => c.IsDisplay).ToList();
            ViewBag.User = new User().Find(userName);
        }
    }
}
