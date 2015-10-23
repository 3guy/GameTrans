using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThrFrd.GameTrans.Admin.WebLogic;
using ThrFrd.GameTrans.Tool;

namespace ThrFrd.GameTrans.Admin.Web.Controllers
{
    [ErrorHandle]
    public class SystemTipController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FailTip(string msg, string backurl)
        {
            ViewBag.message = EncodeByBase64.Decode(msg);
            ViewBag.Url = backurl;
            return View();
        }
    }
}
