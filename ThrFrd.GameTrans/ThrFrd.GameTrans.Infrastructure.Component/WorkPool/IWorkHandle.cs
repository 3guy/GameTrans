using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThrFrd.GameTrans.Infrastructure.Component.WorkPool
{
    interface IWorkHandle<T>
    {
        WaitWorkItem Process(T context);
    }
    interface IWorkResultHandle<T,TResult>
    {
        WaitWorkResultItem<TResult> Process(T context);
    }
}
