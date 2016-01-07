using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThrFrd.GameTrans.Admin.Biz;
using ThrFrd.GameTrans.Admin.WebLogic;
using ThrFrd.GameTrans.Infrastructure.Entities.Entity;

namespace ThrFrd.GameTrans.Admin.Web.Controllers
{
    //[Auth(Order = 1)]
    //[RoleFilter(Order = 2)]
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

        public ActionResult InsertOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOrder()
        {
            string userName = System.Web.HttpContext.Current.User.Identity.Name;
            if (userName != null && userName != "")
            {
                OrderBase orderBase = new OrderBase();
                orderBase.CreateTime = DateTime.Now;
                orderBase.CreaterUser = userName;
                orderBase.TransferAccountNumber = Request["transferAccountNumber"].ToString();
                orderBase.TransferAccountTime = DateTime.ParseExact(Request["transferAccountTime"],"yyyy-MM-dd HH:mm", CultureInfo.CurrentCulture);
                orderBase.TransferMethod = Request["transferMethod"].ToString();
                orderBase.TransferredAmount = Convert.ToDecimal(Request["transferAmount"].ToString());
                orderBase.BeneficiaryAccountNo = Request["beneficiaryAccountNo"].ToString();
                orderBase.Comments = Request["comments"].ToString();
                orderBase.Source = Request["source"].ToString();
                orderBase.PayerName = userName;


                OrderDetail orderDetail = new OrderDetail();
                //orderDetail.OrderID = orderBase.ID; 
                orderDetail.ProductName = Request["productName"].ToString();
                orderDetail.Numbers = Convert.ToInt32(Request["ProductAccount"].ToString());
                orderDetail.UnitPrice = Convert.ToDecimal(Request["originPrice"].ToString());
                orderDetail.TotalPrice = Convert.ToDecimal(Request["totalPrice"].ToString());
                orderDetail.PayTime = DateTime.ParseExact(Request["payTime"], "yyyy-MM-dd HH:mm", CultureInfo.CurrentCulture); 
                orderDetail.PayAccount = Request["payAccount"].ToString();
                orderDetail.GoldAccount = Convert.ToInt64(Request["goldAccount"].ToString());
                //orderDetail.PayDeviceAccount = Request["payDeviceAccount"].ToString();
                orderDetail.Pay4PlayerId =Convert.ToInt64(Request["pay4PlayerId"].ToString());

                bool insertStatus = PlaceOrderBiz.InsertOrder(orderBase, orderDetail);

                if (insertStatus)
                    return Content("1");
                else
                    return Content("0");

            }
            else
            {
                return Content("0");
            }

        }

    }
}
