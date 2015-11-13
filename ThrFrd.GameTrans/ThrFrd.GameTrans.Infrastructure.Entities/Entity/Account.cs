using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.EFContext;
using ThrFrd.GameTrans.Infrastructure.Entities.Repository;
using ThrFrd.GameTrans.Infrastructure.Entities.Enum;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Entity
{
    /// <summary>
    /// APPID账户表
    /// </summary>
    public class Account : IAccessible<T_Account, Account>
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
        /// 余额
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ItemStatus State { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Comments { get; set; }

        protected override void Covariant(T_Account model)
        {
            if (model != null)
            {
                this.Id = model.Id;
                this.AppId = model.AppId;
                this.Balance = model.Balance;
                this.State = (ItemStatus)model.State;
                this.Comments = model.Comments;

            }
        }

        protected override T_Account Inverter(Account model)
        {
            if (model != null)
            {
                var entity = new T_Account();
                entity.Id = model.Id;
                entity.AppId = model.AppId;
                entity.Balance = model.Balance;
                entity.State = (int)model.State;
                entity.Comments = model.Comments;
                return entity;
            }
            return null;
        }
    }
}
