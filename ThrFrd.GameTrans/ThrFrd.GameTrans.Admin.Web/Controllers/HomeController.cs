using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThrFrd.GameTrans.Admin.WebLogic;
using ThrFrd.GameTrans.Infrastructure.Entities.EFContext;
using ThrFrd.GameTrans.Infrastructure.Entities.Entity;
using ThrFrd.GameTrans.Infrastructure.Entities.Enum;
using ThrFrd.GameTrans.Tool;
using ThrFrd.GameTrans.Admin.Biz;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.Text.RegularExpressions;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HPSF;
using NPOI.HSSF.Util;
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


    }
}
