using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoodMan.Tool.Framework.Cache
{
    public abstract class IStorable<T> where T : class
    {
        protected abstract bool ValidateParameter(params string[] keys);
        protected abstract T LoadDataItem(params string[] keys);
        protected abstract string BuildKey(params string[] keys);
        protected abstract string BuildKey(T data);
        
        public T GetItem(params string[] keys)
        {
            try
            {
                string key = BuildKey(keys);
                T item = CacheBag.GetCache<T>(key);
                if (item == null)
                {
                    item = LoadDataItem(keys);
                    if (item != null)
                    {
                        CacheBag.Add(key, item, CacheExpireStrategy.Absolute, new TimeSpan(0, 5, 0));
                    }
                }
                return item;
            }
            catch (Exception e)
            {
                LogHelper.Write(CommonLogger.System, LogLevel.Error, e);
            }
            return default(T);
        }
        public T AppendItem(T data)
        {
            if (data != null)
            {
                string key = BuildKey(data);
                var item = CacheBag.Add<T>(key, data, CacheExpireStrategy.Absolute, new TimeSpan(0, 5, 0));
                return item;
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
