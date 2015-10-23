using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ThrFrd.GameTrans.Infrastructure.Configuration;
using ThrFrd.GameTrans.Tool;
using Amib.Threading;
namespace ThrFrd.GameTrans.Infrastructure.Component.WorkPool
{
    public static class WorkPool
    {
        static volatile SmartThreadPool pool = null;
        static volatile Dictionary<GroupPool, IWorkItemsGroup> Group;
        static WorkPool()
        {
            int minsize = ConfigReader.WorkPool.MinThreadSize;
            int maxsize = ConfigReader.WorkPool.MaxThreadSize;
            try
            {
                pool = new SmartThreadPool(10 * 1000, maxsize, minsize);
                Group = new Dictionary<GroupPool, IWorkItemsGroup>();
                
                Group.Add(GroupPool.ShortMessageGroup,
                    pool.CreateWorkItemsGroup(ConfigReader.WorkPool.ShortMessageQueue.ThreadGroupSize));

            }
            catch (Exception e)
            {
                LogHelper.Write(CommonLogger.Application, LogLevel.Error, e);
                throw e;
            }
                
        }

        public static void Run() { }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="workType"></param>
        /// <param name="context"></param>
        public static void Append<T>(WorkType workType, T context, System.Action callback = null)
        {
            var workItem = new WorkItem<T>(workType).Build(context);
            if (workItem.IsDo) //如果被要求执行
            {
                AddNewTask(workType, workItem.Action, workItem.Context, callback);
            }
        }
        public static void Append<T, TResult>(WorkType workType, T context, Action<TResult> callback = null)
            where TResult : class
        {
            var workItem = new WorkResultItem<T,TResult>(workType).Build(context);

            if (workItem.IsDo) //如果被要求执行
            {
                AddNewTask(workType, workItem.Action, workItem.Context, callback);
            }
        }
        private static void AddNewTask(WorkType workType, Action<object> doAction, object context, System.Action callback)
        {
            var iworkitem = workType.GetWorkPool()
                .QueueWorkItem(doAction, context);
            if (callback != null)
            {
                while (!iworkitem.IsCompleted)
                {
                    Thread.Sleep(5);
                }
                callback.Invoke();
            }
        }
        private static void AddNewTask<T>(WorkType workType, 
            Amib.Threading.Func<object,T> doAction, object context, 
            System.Action<T> callback)
        {

            var iworkitem = workType.GetWorkPool()
                .QueueWorkItem(doAction, context);
            if (callback != null)
            {
                while (!iworkitem.IsCompleted)
                {
                    Thread.Sleep(5);
                }
                if (iworkitem.Result is T)
                {
                    callback.Invoke(iworkitem.Result);
                }
                else
                {
                    callback.Invoke(default(T));
                }
            }
        }
        private static IWorkItemsGroup GetWorkPool(this WorkType workType)
        {
            switch (workType)
            {
                default:
                case WorkType.shortmessage_for_validateCode:
                    return Group[GroupPool.ShortMessageGroup];
            }
        }
    }
    public enum WorkType
    {
        /// <summary>
        /// 短信-验证码
        /// </summary>
        shortmessage_for_validateCode
    }
    public enum GroupPool
    { 
        /// <summary>
        /// 短信线程组
        /// </summary>
        ShortMessageGroup
    }
}
