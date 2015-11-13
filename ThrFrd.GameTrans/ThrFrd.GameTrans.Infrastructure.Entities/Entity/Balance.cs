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
    /// 账户流水记录表
    /// </summary>
    public class Balance : IAccessible<T_Balance, Balance>
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
        /// 
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime OperationTime { get; set; }

        protected override void Covariant(T_Balance model)
        {
            if (model != null)
            {
                this.Id = model.Id;
                this.AppId = model.AppId;
                this.Amount = model.Amount;
                this.UserName = model.UserName;
                this.OperationTime = model.OperationTime;

            }
        }

        protected override T_Balance Inverter(Balance model)
        {
            if (model != null)
            {
                var entity = new T_Balance();
                entity.Id = model.Id;
                entity.AppId = model.AppId;
                entity.Amount = model.Amount;
                entity.UserName = model.UserName;
                entity.OperationTime = model.OperationTime;
                return entity;
            }
            return null;
        }
    }
}
