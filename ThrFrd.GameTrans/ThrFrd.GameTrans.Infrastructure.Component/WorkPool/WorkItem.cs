using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Amib.Threading;

namespace ThrFrd.GameTrans.Infrastructure.Component.WorkPool
{
    internal class WorkItem<T>
    {
        /// <summary>
        /// 工作分类标示
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 工作项ID
        /// </summary>
        public string Key { get; set; }

        private WorkType workType;
        public WorkItem(WorkType type)
        {
            this.workType = type;
        }

        public WaitWorkItem Build(T context)
        {
            var handle = WorkFactory<T,object>.GetHandle(workType, context);
            return handle.Process(context);
        }
    }

    public class WaitWorkItem
    {
        public Action<object> Action { get; set; }
        public object Context { get; set; }
        /// <summary>
        /// 表示是否执行
        /// </summary>
        private bool isDo = true;
        public bool IsDo
        {
            get
            {
                return isDo;
            }
            set
            {
                isDo = value;
            }
        }
    }


    internal class WorkResultItem<T,TResult> where TResult : class
    {
        /// <summary>
        /// 工作分类标示
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 工作项ID
        /// </summary>
        public string Key { get; set; }

        private WorkType workType;
        public WorkResultItem(WorkType type)
        {
            this.workType = type;
        }

        public WaitWorkResultItem<TResult> Build(T context) 
        {
            var handle = WorkFactory<T, TResult>.GetRHandle(workType, context);
            return handle.Process(context);
        }
    }
    public class WaitWorkResultItem<TResult>
    {
        public Amib.Threading.Func<object, TResult> Action { get; set; }
        public object Context { get; set; }
        /// <summary>
        /// 表示是否执行
        /// </summary>
        private bool isDo = true;
        public bool IsDo
        {
            get
            {
                return isDo;
            }
            set
            {
                isDo = value;
            }
        }
    }
}
