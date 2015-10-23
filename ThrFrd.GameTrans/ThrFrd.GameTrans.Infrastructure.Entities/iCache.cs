using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Component;
using ThrFrd.GameTrans.Infrastructure.Component.Cache;
using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace ThrFrd.GameTrans.Infrastructure.Entities
{
    public class iCache
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="loadAction"></param>
        /// <param name="duration">单位(s)</param>
        /// <param name="immortal">是否永远保存（当因为超时而被移除时，自动重载）</param>
        /// <returns></returns>
        public static T Get<T>(string key, Func<T> loadAction, int duration, bool immortal = false)
            where T : class
        {
            T data = CacheBag.GetCache<T>(key);
            if (data == null)
            {
                using (CriticalRegion region = new CriticalRegion(key))
                {
                    data = CacheBag.GetCache<T>(key);
                    if (data == null)
                    {
                        data = loadAction.Invoke();
                        if (data != null)
                        {
                            return CacheBag.Add<T>(key, data, (k, v, reason) =>
                            {
                                if (immortal && reason == CacheItemRemovedReason.Expired)
                                {
                                    Get<T>(key, loadAction, duration, immortal);
                                }
                            }, CacheExpireStrategy.Absolute, new TimeSpan(0, 0, duration));
                        }
                    }
                }
            }
            return data;
        }
        public static void Refrash(string key)
        {
            CacheBag.RemoveCache(key);
        }
    }
}
