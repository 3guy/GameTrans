using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThrFrd.GameTrans.Admin.Biz;
using ThrFrd.GameTrans.Admin.WebLogic;

namespace ThrFrd.GameTrans.Admin.Web.Controllers
{
    [Auth(Order = 1)]
    [RoleFilter(Order = 2)]
    [ErrorHandle]
    public class PlaceOrderController : BaseController
    {
        //
        // GET: /PlaceOrder/

        public ActionResult Index(int page = 0, int pageSize = 10, string key = "", int status = -100, string starttime = "", string endtime = "")
        {
            int count = 0;
            ViewBag.orderlist = PlaceOrderBiz.GetPlaceOrderListByPage(page, pageSize, key, status, starttime, endtime, ref count);
            ViewBag.strquery = string.Format("{0},{1},{2},{3}", key, status, starttime, endtime);

            return View();
        }

    }
}
