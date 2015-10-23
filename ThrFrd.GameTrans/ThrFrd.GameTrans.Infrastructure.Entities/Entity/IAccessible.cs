using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using ThrFrd.GameTrans.Infrastructure.Entities.Repository;
using ThrFrd.GameTrans.Tool;
using ThrFrd.GameTrans.Infrastructure.Entities.EFContext;
using System.Data.Entity.Validation;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Entity
{
    public abstract class IAccessible<dbEntity, lcEntity>
        where dbEntity : IEntity<dbEntity>, new()
        where lcEntity : IAccessible<dbEntity, lcEntity>, new()
    {
        protected abstract void Covariant(dbEntity model);
        protected abstract dbEntity Inverter(lcEntity model);
        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="key">必须为主键值</param>
        /// <returns></returns>
        public lcEntity FindByKey(string key)
        {
            try
            {
                var item = new dbEntity().FindByKey(key);
                if (item != null)
                {
                    Covariant(item);
                    return this as lcEntity;
                }
            }
            catch (Exception e)
            {
                LogHelper.Write(CommonLogger.Application, LogLevel.Error, e);
            }
            return null;
        }
        public bool Any(Expression<Func<dbEntity, bool>> where)
        {
            try
            {
                return new dbEntity().Any(where);
            }
            catch (Exception e)
            {
                LogHelper.Write(CommonLogger.Application, LogLevel.Error, e);
            }
            return false;
        }

        /// <summary>
        /// 通过单值查找，将可能通过缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual lcEntity Find(string key)
        {
            try
            {
                var item = new dbEntity().Find(key);
                if (item != null)
                {
                    Covariant(item);
                    return this as lcEntity;
                }
            }
            catch (Exception e)
            {
                LogHelper.Write(CommonLogger.Application, LogLevel.Error, e);
            }
            return null;
        }
        public virtual lcEntity Find(Expression<Func<dbEntity, bool>> where)
        {
            try
            {
                var item = new dbEntity().Find(where);
                if (item != null)
                {
                    Covariant(item);
                    return this as lcEntity;
                }
            }
            catch (Exception e)
            {
                LogHelper.Write(CommonLogger.Application, LogLevel.Error, e);
            }
            return null;
        }
        public virtual List<lcEntity> FindAll(Expression<Func<dbEntity, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.Set<dbEntity>().Where(where).ToList()
                    .Select(c => new lcEntity().Set(c))
                    .ToList();
            }
        }
        public virtual List<lcEntity> FindAll<TKey>(Expression<Func<dbEntity, bool>> where, Expression<Func<dbEntity, TKey>> orderby, int page, int pageSize)
        {
            using (Context ctx = new Context())
            {
                return ctx.Set<dbEntity>().Where(where)
                    .OrderByDescending(orderby)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList()
                    .Select(c => new lcEntity().Set(c))
                    .ToList();
            }
        }
        public List<lcEntity> FindAll()
        {
            using (Context ctx = new Context())
            {
                return ctx.Set<dbEntity>().ToList()
                    .Select(c => new lcEntity().Set(c))
                    .ToList();
            }
        }
        public virtual List<lcEntity> FindSomething<TKey>(
            Expression<Func<dbEntity, bool>> where,
            Expression<Func<dbEntity, TKey>> orderby,
            int page, int pagesize)
        {
            using (Context ctx = new Context())
            {
                return ctx.Set<dbEntity>().Where(where)
                    .OrderByDescending(orderby)
                    .Skip(page * pagesize)
                    .Take(pagesize)
                    .ToList()
                    .Select(c => new lcEntity().Set(c))
                    .ToList();
            }
        }
        public virtual lcEntity PostAdd()
        {
            try
            {
                var item = Inverter(this as lcEntity);
                if (item.PostAdd() > 0)
                {
                    Covariant(item);
                    return this as lcEntity;
                }
            }
            catch (DbEntityValidationException e)
            {
                LogHelper.Write(CommonLogger.Application, LogLevel.Error, e);
            }
            catch (Exception ex)
            {
                LogHelper.Write(CommonLogger.Application, LogLevel.Error, ex);
            }
            return null;
        }
        public virtual lcEntity PostModify()
        {
            try
            {
                var item = Inverter(this as lcEntity);
                if (item.PostModify() > 0)
                {
                    Covariant(item);
                    return this as lcEntity;
                }
            }
            catch (Exception e)
            {
                LogHelper.Write(CommonLogger.Application, LogLevel.Error, e);
            }
            return null;
        }
        public virtual lcEntity PostDelete()
        {
            try
            {
                var item = Inverter(this as lcEntity);
                if (item.PostDelete() > 0)
                {
                    Covariant(item);
                    return this as lcEntity;
                }
            }
            catch (Exception e)
            {
                LogHelper.Write(CommonLogger.Application, LogLevel.Error, e);
            }
            return null;
        }
        public lcEntity Set(dbEntity dbModel)
        {
            Covariant(dbModel);
            return this as lcEntity;
        }
    }
}
