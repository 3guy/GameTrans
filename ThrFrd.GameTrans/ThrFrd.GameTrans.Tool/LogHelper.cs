using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using System.Web;

namespace ThrFrd.GameTrans.Tool
{
    public class LogHelper
    {
        public static void Write(CommonLogger logType, LogLevel level, string message)
        {
            Logger logger = LoggerRepository.BuildLogger(logType);
            StringBuilder info = new StringBuilder();
            if (HttpContext.Current != null)
            {
                var urlReferrer = HttpContext.Current.Request.UrlReferrer;
                if (urlReferrer != null)
                {
                    info.AppendLine("前一个请求地址：" + urlReferrer.ToString());
                }
                var behavior = HttpContext.Current.Request.Cookies["user"];
                if (behavior != null)
                {
                    info.AppendLine("据用户跟踪，用户为：" + behavior);
                }
                info.AppendLine("MethodType:" + HttpContext.Current.Request.HttpMethod);
                info.AppendLine("ClientIP:" + HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());
                info.AppendLine("Browser:" + HttpContext.Current.Request.Browser.Type);
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    info.AppendLine("LoginUser:" + HttpContext.Current.User.Identity.Name);
                }
                info.AppendLine("请求地址：" + HttpContext.Current.Request.RawUrl);
            }
            DoWrite(logger, level, info.ToString() + message);
        }
        public static void Write(CommonLogger logType, LogLevel level, string info, Exception exp)
        {
            Write(logType, level, info + Environment.NewLine + GetInnerExceptionMessage(exp));
        }
        public static void Write(CommonLogger logType, Exception exp)
        {
            Write(logType, LogLevel.Error, GetInnerExceptionMessage(exp));
        }
        public static void Write(CommonLogger logType, LogLevel level, Exception exp, string more = "")
        {
            Write(logType, level, more + Environment.NewLine + GetInnerExceptionMessage(exp));
        }

        /// <summary>
        /// 最多记录三层内部异常
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        private static string GetInnerExceptionMessage(Exception exp)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine);
            sb.AppendLine(exp.Message);
            sb.AppendLine(exp.StackTrace);
            if (exp.InnerException != null)
            {
                sb.Append(Environment.NewLine);
                sb.AppendLine(exp.InnerException.Message);
                sb.AppendLine(exp.InnerException.StackTrace);
                if (exp.InnerException.InnerException != null)
                {
                    sb.Append(Environment.NewLine);
                    sb.AppendLine(exp.InnerException.InnerException.Message);
                    sb.AppendLine(exp.InnerException.InnerException.StackTrace);
                }
            }
            sb.AppendLine("**********************************");
            return sb.ToString();
        }
        private static void DoWrite(Logger logger, LogLevel level, string message)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    logger.Debug(message);
                    break;
                case LogLevel.Info:
                    logger.Info(message);
                    break;
                case LogLevel.Trace:
                    logger.Trace(message);
                    break;
                case LogLevel.Error:
                default:
                    logger.Error(message);
                    break;
            }
        }
    }
    public enum LogLevel
    {
        Error,
        Info,
        Trace,
        Debug
    }
    public enum CommonLogger
    {
        /// <summary>
        /// 应用程序级别
        /// </summary>
        Application,
        /// <summary>
        /// 一般性错误
        /// </summary>
        Web,
        /// <summary>
        /// 缓存失败
        /// </summary>
        Cache,
        /// <summary>
        /// 数据库相关
        /// </summary>
        DataBase,
        /// <summary>
        /// 微信支付
        /// </summary>
        TenPay,
        /// <summary>
        /// 支付宝
        /// </summary>
        Alipay,
        /// <summary>
        /// API接口使用
        /// </summary>
        Api,

        #region 定时任务使用
        /// <summary>
        /// 自动执行服务使用
        /// 公用
        /// </summary>
        Service,
        #endregion
    }
    public class LoggerRepository
    {
        public static Logger BuildLogger(CommonLogger logger)
        {
            switch (logger)
            {
                default:
                case CommonLogger.Application:
                    return LogManager.GetLogger("Application");
                case CommonLogger.DataBase:
                    return LogManager.GetLogger("DataBase");                
                case CommonLogger.Cache:
                    return LogManager.GetLogger("Cache");
                case CommonLogger.Service:
                    return LogManager.GetLogger("Service");
                case CommonLogger.TenPay:
                    return LogManager.GetLogger("WeiXinPay");
                case CommonLogger.Alipay:
                    return LogManager.GetLogger("Alipay");
                case CommonLogger.Api:
                    return LogManager.GetLogger("Api");
            }
        }
    }
}
