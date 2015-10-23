using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ThrFrd.GameTrans.Infrastructure.Component.Cache;
using ThrFrd.GameTrans.Tool;
using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace ThrFrd.GameTrans.Infrastructure.Component
{
    internal class CriticalKey
    {
        public DateTime keyTime { get; set; }
        public CriticalKey()
        {
            keyTime = DateTime.Now;
        }
    }
    public class CriticalRegion : IDisposable
    {
        private static object commonKey = new object();
        //private static Dictionary<string, object> synchronousKey = new Dictionary<string, object>();
        private string scopeKey = string.Empty;
        private string token = string.Empty;
        private object key = null;
        public CriticalRegion(string scopeKey)
        {
            this.scopeKey = scopeKey;
            if (!CacheBag.Contains(scopeKey))
            {
                lock (commonKey)
                {
                    if (!CacheBag.Contains(scopeKey))
                    {
                        CacheBag.Add(scopeKey,
                            new object(),
                            CacheItemPriority.High,
                            CacheExpireStrategy.Sliding,
                            new TimeSpan(0, 10, 0));
                    }
                }
            }
        }
        public bool Set()
        {
            key = CacheBag.GetCache(scopeKey);
            if (key == null)
            {
                return false;
            }
            try
            {
                if (Monitor.TryEnter(key, 60000))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                LogHelper.Write(CommonLogger.Application, LogLevel.Error, e);
            }
            return false;
        }
        public void Dispose()
        {
            if (key != null)
            {
                try
                {
                    Monitor.Exit(key);
                }
                catch (Exception e)
                {
                    LogHelper.Write(CommonLogger.Application, LogLevel.Error, e);
                }
            }
        }
    }
}
