using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThrFrd.GameTrans.Admin.WebLogic;

namespace ThrFrd.GameTrans.Admin.Web.Controllers
{
    [Auth(Order = 1)]
    [RoleFilter(Order = 2)]
    [ErrorHandle]
    public class BaseDataController : BaseController
    {
        //
        // GET: /BaseData/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AccountMgt()
        {
            return View();
        }

        public ActionResult GameMgt()
        {
            return View();
        }

    }
}
