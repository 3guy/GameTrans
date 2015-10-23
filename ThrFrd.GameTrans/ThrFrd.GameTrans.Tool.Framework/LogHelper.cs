using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace GoodMan.Tool.Framework
{
    public class LogHelper
    {
        public static void Write(string loggerName, LogLevel level, string message)
        {
            DoWrite(LogManager.GetLogger(loggerName), level, message);
        }
        public static void Write(CommonLogger logType, LogLevel level, string message)
        {
            Logger logger = LoggerRepository.BuildLogger(logType);
            DoWrite(logger, level, message);
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
        System,
        WebError,
        Support,
        UserSystem,
        ExServer,
    }
    public class LoggerRepository
    {
        public static Logger BuildLogger(CommonLogger logger)
        {
            switch (logger)
            {
                case CommonLogger.UserSystem:
                    return LogManager.GetLogger("UserSystem");
                case CommonLogger.WebError:
                    return LogManager.GetLogger("WebError");
                case CommonLogger.ExServer:
                    return LogManager.GetLogger("ExServer");
                case CommonLogger.Support:
                    return LogManager.GetLogger("Support");
                default:
                case CommonLogger.System:
                    return LogManager.GetLogger("System");
            }
        }
    }
}
