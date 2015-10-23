using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using ThrFrd.GameTrans.Infrastructure.Entities.EFContext;
using ThrFrd.GameTrans.Tool;
using System.Data.Entity;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Repository
{
    public abstract class IEntity<T> 
        where T:class
    {
        public T FindByKey(string key)
        {
            using (Context ctx = new Context())
            {
                return ctx.Set<T>().Find(key);
            }
        }
        /// <summary>
        /// 通过单值查找，将通过缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract T Find(string key);
        public abstract T Find(Expression<Func<T, bool>> where);
        public abstract bool Any(Expression<Func<T, bool>> where);
        public virtual int PostModify()
        {
            try
            {
                LogHelper.Write(CommonLogger.Application, LogLevel.Trace,
                    String.Format("对象：{0}执行PostModify操作。对象值为：{1}",
                    this.GetType().FullName, this.ToJson()));
            }
            catch { }

            using (Context ctx = new Context())
            {
                ctx.Entry(this).State = EntityState.Modified;
                return ctx.SaveChanges();
            }
        }
        public virtual int PostDelete()
        {
            try
            {
                LogHelper.Write(CommonLogger.Application, LogLevel.Trace,
                    String.Format("对象：{0}执行PostDelete操作。对象值为：{1}",
                    this.GetType().FullName, this.ToJson()));
            }
            catch { }
            using (Context ctx = new Context())
            {
                ctx.Entry(this).State = EntityState.Deleted;
                return ctx.SaveChanges();
            }
        }
        public virtual int PostAdd()
        {
            try
            {
                LogHelper.Write(CommonLogger.Application, LogLevel.Trace,
                    String.Format("对象：{0}执行PostAdd操作。对象值为：{1}",
                    this.GetType().FullName, this.ToJson()));
            }
            catch { }
            using (Context ctx = new Context())
            {
                ctx.Entry(this).State = EntityState.Added;
                return ctx.SaveChanges();
            }
        }
    }
}
