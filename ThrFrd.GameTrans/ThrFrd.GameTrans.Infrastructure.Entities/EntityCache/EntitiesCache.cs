using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Component.Cache;
using ThrFrd.GameTrans.Tool;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Cache
{
    public abstract class EntitiesCache<T> where T : class
    {
        protected abstract bool ValidateParameter(params string[] keys);
        protected abstract T LoadDataItem(params string[] keys);
        protected abstract string BuildKey(params string[] keys);
        protected abstract string BuildKey(T data);
        public virtual T GetItem(params string[] keys)
        {
            try
            {
                string key = BuildKey(keys);
                if (String.IsNullOrEmpty(key))
                {
                    return default(T);
                }
                T item = CacheBag.GetCache<T>(key);
                if (item == null)
                {
                    item = LoadDataItem(keys);
                    if (item != null)
                    {
                        CacheBag.Add<T>(key, item, CacheExpireStrategy.Absolute, new TimeSpan(0, 5, 0));
                    }
                }
                return item;
            }
            catch (Exception e)
            {
                LogHelper.Write(CommonLogger.Application, LogLevel.Error, e);
            }
            return default(T);
        }
        public T AppendItem(T data)
        {
            if (data != null)
            {
                string key = BuildKey(data);
                return CacheBag.Add<T>(key, data, CacheExpireStrategy.Absolute, new TimeSpan(0, 5, 0));
            }
            return null;
        }
        public void RefreshItem(params string[] keys)
        {
            string key = BuildKey(keys);
            CacheBag.RemoveCache(key);
        }
    }
}
