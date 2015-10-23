using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using ThrFrd.GameTrans.Tool;
using ThrFrd.GameTrans.Infrastructure.Configuration;
using ThrFrd.GameTrans.Infrastructure.Component.WorkPool;

namespace ThrFrd.GameTrans.Infrastructure.Entities.EFContext
{
    class EFIntercepterLogging : DbCommandInterceptor
    {
        const bool SQLTrace = true;
        /// <summary>
        /// 执行时间超过此值将被记录
        /// </summary>
        const int logValue = 200;
        /// <summary>
        /// 执行时间超过此值将被发出警告
        /// </summary>
        const int alterValue = 500;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        public override void ScalarExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            base.ScalarExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }
        public override void ScalarExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            _stopwatch.Stop();
            if (interceptionContext.Exception != null)
            {
                LogHelper.Write(CommonLogger.DataBase, LogLevel.Error, String.Format("Exception:{1} \r\n --> Error executing command: {0}", command.CommandText, interceptionContext.Exception.ToString()));
#if !DEBUG
                WorkPool.Append<NormalNotifyEmail>(WorkType.email_for_normalnotify,
                 new NormalNotifyEmail()
                 {
                     Message = String.Format("Exception:{1} \r\n --> Error executing command: {0}", command.CommandText, interceptionContext.Exception.ToString()),
                     Title = SiteSettings.SiteName + "--数据执行警告",
                     Recipient = "guopeng@rongmofang.com"
                 });
#endif
            }
            else
            {
                if (SQLTrace)
                {
                    if (_stopwatch.ElapsedMilliseconds >= logValue)
                    {
                        LogHelper.Write(CommonLogger.DataBase, LogLevel.Trace, String.Format("\r\n执行时间:{0} 毫秒\r\n-->ScalarExecuted.Command:{1}", _stopwatch.ElapsedMilliseconds, command.CommandText));
                    }
                }
            }
            base.ScalarExecuted(command, interceptionContext);
        }

        public override void NonQueryExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            base.NonQueryExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }
        public override void NonQueryExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            _stopwatch.Stop();
            if (interceptionContext.Exception != null)
            {
                LogHelper.Write(CommonLogger.DataBase, LogLevel.Error, String.Format("Exception:{1} \r\n --> Error executing command:\r\n {0}", command.CommandText, interceptionContext.Exception.ToString()));
#if !DEBUG
                WorkPool.Append<NormalNotifyEmail>(WorkType.email_for_normalnotify,
                 new NormalNotifyEmail()
                 {
                     Message = String.Format("Exception:{1} \r\n --> Error executing command:\r\n {0}", command.CommandText, interceptionContext.Exception.ToString()),
                     Title = SiteSettings.SiteName + "--数据执行警告",
                     Recipient = "guopeng@rongmofang.com"
                 });
#endif
            }
            else
            {
                if (SQLTrace)
                {
                    if (_stopwatch.ElapsedMilliseconds >= logValue)
                    {
                        LogHelper.Write(CommonLogger.DataBase, LogLevel.Trace, String.Format("\r\n执行时间:{0} 毫秒\r\n-->NonQueryExecuted.Command:\r\n{1}", _stopwatch.ElapsedMilliseconds, command.CommandText));
                    }
                }
            }
            base.NonQueryExecuted(command, interceptionContext);
        }

        public override void ReaderExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext)
        {
            base.ReaderExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }

        public override void ReaderExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext)
        {
            _stopwatch.Stop();
            if (interceptionContext.Exception != null)
            {
                LogHelper.Write(CommonLogger.DataBase, LogLevel.Error, String.Format("Exception:{1} \r\n --> Error executing command:\r\n {0}", command.CommandText, interceptionContext.Exception.ToString()));
#if !DEBUG
                WorkPool.Append<NormalNotifyEmail>(WorkType.email_for_normalnotify,
                 new NormalNotifyEmail()
                 {
                     Message = String.Format("Exception:{1} \r\n --> Error executing command:\r\n {0}", command.CommandText, interceptionContext.Exception.ToString()),
                     Title = SiteSettings.SiteName + "--数据执行警告",
                     Recipient = "guopeng@rongmofang.com"
                 });
#endif
            }

            else
            {
                if (SQLTrace)
                {
                    if (_stopwatch.ElapsedMilliseconds >= logValue)
                    {
                        LogHelper.Write(CommonLogger.DataBase, LogLevel.Trace, String.Format("\r\n执行时间:{0} 毫秒 \r\n -->ReaderExecuted.Command:\r\n{1}", _stopwatch.ElapsedMilliseconds, command.CommandText));
                    }
                }
            }
            base.ReaderExecuted(command, interceptionContext);
        }
    }
}
