using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ThrFrd.GameTrans.Tool;

namespace ThrFrd.GameTrans.Admin.WebLogic
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ErrorHandleAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            try
            {
                string info = string.Empty;
                var urlReferrer = filterContext.HttpContext.Request.UrlReferrer;
                if (urlReferrer != null)
                {
                    info = "前一个请求地址：" + urlReferrer.ToString() + Environment.NewLine;
                }
                var behavior = filterContext.HttpContext.Request.Cookies["user"];
                if (behavior != null)
                {
                    info += "据用户跟踪，用户为：" + behavior + Environment.NewLine;
                }
                info += "MethodType:" + filterContext.HttpContext.Request.HttpMethod + Environment.NewLine;
                info += "ClientIP:" + filterContext.HttpContext.Request.ServerVariables["REMOTE_ADDR"].ToString() + Environment.NewLine;
                info += "Browser:" + filterContext.HttpContext.Request.Browser.Type + Environment.NewLine;
                if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    info += "LoginUser:" + filterContext.HttpContext.User.Identity.Name + Environment.NewLine;
                }
                info += "请求地址：" + filterContext.HttpContext.Request.RawUrl + Environment.NewLine;
                if (filterContext.RouteData != null && filterContext.RouteData.Values != null)
                {
                    info += "请求参数：" + filterContext.RouteData.Values.ToJson();
                }
                if (!(filterContext.Exception is ArgumentException))
                {
                    LogHelper.Write(CommonLogger.Application, LogLevel.Error, info + Environment.NewLine,
                        filterContext.Exception);
                }
            }
            catch (Exception e)
            {
                LogHelper.Write(CommonLogger.Application, LogLevel.Error, "ErrorHandle异常：", e);
            }
            filterContext.RequestContext.HttpContext.Response.Redirect("/home/error");
        }
    }
}
