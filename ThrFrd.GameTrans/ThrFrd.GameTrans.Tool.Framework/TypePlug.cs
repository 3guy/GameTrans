using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoodMan.Tool.Framework
{
    public static class TypePlug
    {
        public static int ToInt32(this object o)
        {
            if (o == null)
            {
                throw new ArgumentException("转型失败，不能为null");
            }
            int result = 0;
            if (Int32.TryParse(o.ToString(), out result))
            {
                return result;
            }
            throw new ArgumentException("转型失败，不为Int类型");
        }
        public static int ToInt32(this object o, int defaultVal)
        {
            if (o == null) return defaultVal;
            return o.ToString().ToInt32(defaultVal);
        }
        public static int ToInt32(this string str, int defaultVal)
        {
            int result = 0;
            if (Int32.TryParse(str, out result))
            {
                return result;
            }
            return defaultVal;
        }

        public static decimal ToDecimal(this object o, decimal defaultVal)
        {
            if (o == null) return defaultVal;
            return o.ToString().ToDecimal(defaultVal);
        }
        public static decimal ToDecimal(this string str, decimal defaultVal)
        {
            decimal result = 0m;
            if (decimal.TryParse(str, out defaultVal))
            {
                return result;
            }
            return defaultVal;
        }

        public static DateTime ToDateTime(this object o, DateTime defaultVal)
        {
            if (o == null) return defaultVal;
            return o.ToString().ToDateTime(defaultVal);
        }
        public static DateTime ToDateTime(this string str, DateTime defaultVal)
        {
            DateTime dt;
            if (DateTime.TryParse(str, out dt))
            {
                return dt;
            }
            return defaultVal;
        }
        public static string ToJson(this object o)
        {
            if (o == null)
            {
                return String.Empty;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(o);
        }

        public static T ToObject<T>(this string json)
        {
            try
            {
                if (String.IsNullOrEmpty(json))
                {
                    return default(T);
                }
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                return default(T);
            }
        }
    }
}
