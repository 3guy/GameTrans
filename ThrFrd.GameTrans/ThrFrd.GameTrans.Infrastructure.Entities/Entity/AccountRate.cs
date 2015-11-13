using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.EFContext;
using ThrFrd.GameTrans.Infrastructure.Entities.Repository;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Entity
{
    /// <summary>
    /// 账户汇率表
    /// </summary>
    public class AccountRate : IAccessible<T_AccountRate, AccountRate>
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal Rate { get; set; }

        protected override void Covariant(T_AccountRate model)
        {
            if (model != null)
            {
                this.Id = model.Id;
                this.AppId = model.AppId;
                this.Rate = model.Rate;

            }
        }

        protected override T_AccountRate Inverter(AccountRate model)
        {
            if (model != null)
            {
                var entity = new T_AccountRate();
                entity.Id = model.Id;
                entity.AppId = model.AppId;
                entity.Rate = model.Rate;
                return entity;
            }
            return null;
        }
    }
}
