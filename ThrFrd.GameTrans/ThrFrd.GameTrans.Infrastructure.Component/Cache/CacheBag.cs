using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Tool;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

namespace ThrFrd.GameTrans.Infrastructure.Component.Cache
{
    /*
        * Member : GetMember_{Expiression} 例如GetMember_Username == "...@aa.com"
        * 
        * 
        */
    public class CacheBag
    {
        private static ICacheManager cache = CacheFactory.GetCacheManager("defaultCache");
        private static int nextLogItemCount = 200;
        public static int CurrentCacheItemCount
        {
            get
            {
                return cache.Count;
            }
        }
        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static object GetCache(string key)
        {
            return cache.GetData(key);
        }
        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static T GetCache<T>(string key)
            where T : class
        {
            return cache.GetData(key) as T;
        }
        /// <summary>
        /// 移除缓存对象
        /// </summary>
        /// <param name="key">键</param>
        public static void RemoveCache(string key)
        {
            cache.Remove(key);
        }
        public static void Clear()
        {
            cache.Flush();
        }
        public static bool Contains(string key)
        {
            return cache.Contains(key);
        }
        /// <summary>
        /// 添加一项至缓存，并返回当前添加的内容
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="strategy">过期方式</param>
        /// <param name="timespan">如果是绝对过期：则表示在据当前时间多长时间后过期。如果是相对过期，则表示相对时间间隔</param>
        /// <returns></returns>
        public static object Add(string key, object value, CacheExpireStrategy strategy, TimeSpan timespan)
        {
            return Add(key, value, CacheItemPriority.Normal, strategy, timespan);
        }
        public static object Add(string key, object value,CacheItemPriority priority, CacheExpireStrategy strategy, TimeSpan timespan)
        {
            try
            {
                if (CurrentCacheItemCount == nextLogItemCount)
                {
                    LogHelper.Write(CommonLogger.Cache, LogLevel.Info, "当前缓存数量为：" + cache.Count.ToString());
                    nextLogItemCount += 200;
                }
                if (strategy == CacheExpireStrategy.Absolute)
                {
                    cache.Add(key, value, priority, null, new AbsoluteTime(DateTime.Now.Add(timespan)));
                    return value;
                }
                else if (strategy == CacheExpireStrategy.Sliding)
                {
                    cache.Add(key, value, priority, null, new SlidingTime(timespan));
                    return value;
                }
            }
            catch (Exception e)
            {
                LogHelper.Write(CommonLogger.Cache, LogLevel.Error, e);
            }
            return null;
        }
        public static object Add(string key, object value, Action<string, object, CacheItemRemovedReason> refreshAction, DateTime absExpireDate)
        {
            try
            {
                cache.Add(key, value, CacheItemPriority.Normal, new CacheItemRefreshAction(refreshAction), new AbsoluteTime(absExpireDate));
                if (CurrentCacheItemCount == nextLogItemCount)
                {
                    LogHelper.Write(CommonLogger.Cache, LogLevel.Info, "当前缓存数量为：" + cache.Count.ToString());
                    nextLogItemCount += 200;
                }
                return value;
            }
            catch (Exception e)
            {
                LogHelper.Write(CommonLogger.Cache, LogLevel.Error, e);
            }
            return null;
        }
        /// <summary>
        /// 添加一项至缓存，并返回当前添加的内容
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="refreshAction">当缓存被移除的时候，执行的方法</param>
        /// <param name="strategy">过期方式</param>
        /// <param name="timespan">如果是绝对过期：则表示在据当前时间多长时间后过期。如果是相对过期，则表示相对时间间隔</param>
        /// <returns></returns>
        public static object Add(string key, object value, Action<string, object, CacheItemRemovedReason> refreshAction, CacheExpireStrategy strategy, TimeSpan timespan)
        {
            try
            {
                if (CurrentCacheItemCount == nextLogItemCount)
                {
                    LogHelper.Write(CommonLogger.Cache, LogLevel.Info, "当前缓存数量为：" + cache.Count.ToString());
                    nextLogItemCount += 200;
                }
                if (strategy == CacheExpireStrategy.Absolute)
                {
                    cache.Add(key, value, CacheItemPriority.Normal, new CacheItemRefreshAction(refreshAction), new AbsoluteTime(DateTime.Now.Add(timespan)));
                    return value;
                }
                else if (strategy == CacheExpireStrategy.Sliding)
                {
                    cache.Add(key, value, CacheItemPriority.Normal, new CacheItemRefreshAction(refreshAction), new SlidingTime(timespan));
                    return value;
                }
            }
            catch (Exception e)
            {
                LogHelper.Write(CommonLogger.Cache, LogLevel.Error, e);
            }
            return null;
        }
        public static T Add<T>(string key, object value, CacheExpireStrategy strategy, TimeSpan timespan)
            where T : class
        {
            return Add(key, value, strategy, timespan) as T;
        }
        public static T Add<T>(string key, object value, Action<string, object, CacheItemRemovedReason> refreshAction, DateTime AbsExpireDate)
            where T : class
        {
            return Add(key, value, refreshAction, AbsExpireDate) as T;
        }
        public static T Add<T>(string key, object value, Action<string, object, CacheItemRemovedReason> refreshAction, CacheExpireStrategy strategy, TimeSpan timespan)
           where T : class
        {
            return Add(key, value, refreshAction, strategy, timespan) as T;
        }
    }
    public enum CacheExpireStrategy
    {
        Sliding = 0,
        Absolute = 1
    }
    class CacheItemRefreshAction : ICacheItemRefreshAction
    {
        private Action<string, object, CacheItemRemovedReason> action = null;
        public CacheItemRefreshAction(Action<string, object, CacheItemRemovedReason> action)
        {
            this.action = action;
        }
        #region ICacheItemRefreshAction 成员

        public void Refresh(string removedKey, object expiredValue, CacheItemRemovedReason removalReason)
        {
            this.action.Invoke(removedKey, expiredValue, removalReason);
        }

        #endregion
    }
}
