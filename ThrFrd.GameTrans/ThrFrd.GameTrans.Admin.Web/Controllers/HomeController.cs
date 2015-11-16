using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThrFrd.GameTrans.Admin.WebLogic;
using ThrFrd.GameTrans.Tool;
using ThrFrd.GameTrans.Admin.Biz;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.Text.RegularExpressions;
using ThrFrd.GameTrans.Infrastructure.Configuration;

namespace ThrFrd.GameTrans.Admin.Web.Controllers
{
    [Auth(Order = 1)]
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            // TODO: 初始化数据

            return View();
        }

        public string GetPendingWork()
        {
            try
            { }
            catch (Exception ex)
            {
                LogHelper.Write(CommonLogger.Web, LogLevel.Error, ex);
            }
            return null;
        }

    }
}
