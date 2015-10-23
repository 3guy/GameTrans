using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Component.WorkPool.WorkHandle.ShortMessage;

namespace ThrFrd.GameTrans.Infrastructure.Component.WorkPool
{
    internal class WorkFactory<T,TResult> where TResult : class
    {
        static Dictionary<WorkType, KeyValuePair<IWorkHandle<T>, Type>> dict = new Dictionary<WorkType, KeyValuePair<IWorkHandle<T>, Type>>();
        static Dictionary<WorkType, KeyValuePair<IWorkResultHandle<T, TResult>, Type>> dictR =
            new Dictionary<WorkType, KeyValuePair<IWorkResultHandle<T, TResult>, Type>>();
        static WorkFactory()
        {
            dict.Add(WorkType.shortmessage_for_validateCode,
                new KeyValuePair<IWorkHandle<T>, Type>(
                    new ValidateCodeShortMessageHandle<T>(),
                    typeof(ValidateCodeMessageModel)));
            
        }
        private static IWorkHandle<T> GetHandleWithParam(WorkType type, T context)
        {
            if (dict.ContainsKey(type))
            {
                var item = dict[type];
                if (item.Value == typeof(T))
                {
                    return item.Key as IWorkHandle<T>;
                }
            }
            return null;
        }
        private static IWorkResultHandle<T, TResult> GetRHandleWithParam(WorkType type, T context)
        {
            if (dictR.ContainsKey(type))
            {
                var item = dictR[type];
                if (item.Value == typeof(T))
                {
                    return item.Key as IWorkResultHandle<T, TResult>;
                }
            }
            return null;
        }
        
        public static IWorkHandle<T> GetHandle(WorkType type, T context)
        {
            return GetHandleWithParam(type, context);
        }

        public static IWorkResultHandle<T, TResult> GetRHandle(WorkType type, T context)
        {
            return GetRHandleWithParam(type, context);
        }
    }
}
