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
    /// 订单基础信息
    /// </summary>
    public class OrderBase : IAccessible<T_OrderBase, OrderBase>
    {
        /// <summary>
        /// 
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderStatus State { get; set; }
        /// <summary>
        /// 交易猫/ 手动下单，登录user name
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 充值人
        /// </summary>
        public string PayerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// 转账订单号
        /// </summary>
        public string TransferAccountNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal TransferredAmount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime TransferAccountTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TransferMethod { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BeneficiaryAccountNo { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreaterUser { get; set; }

        protected override void Covariant(T_OrderBase model)
        {
            if (model != null)
            {
                this.ID = model.ID;
                this.CreateTime = model.CreateTime;
                this.TotalPrice = model.TotalPrice;
                this.State = (OrderStatus)model.State;
                this.Source = model.Source;
                this.PayerName = model.PayerName;
                this.Comments = model.Comments;
                this.TransferAccountNumber = model.TransferAccountNumber;
                this.TransferredAmount = model.TransferredAmount;
                this.TransferAccountTime = model.TransferAccountTime;
                this.TransferMethod = model.TransferMethod;
                this.BeneficiaryAccountNo = model.BeneficiaryAccountNo;
                this.CreaterUser = model.CreaterUser;

            }
        }

        protected override T_OrderBase Inverter(OrderBase model)
        {
            if (model != null)
            {
                var entity = new T_OrderBase();
                entity.ID = model.ID;
                entity.CreateTime = model.CreateTime;
                entity.TotalPrice = model.TotalPrice;
                entity.State = (int)model.State;
                entity.Source = model.Source;
                entity.PayerName = model.PayerName;
                entity.Comments = model.Comments;
                entity.TransferAccountNumber = model.TransferAccountNumber;
                entity.TransferredAmount = model.TransferredAmount;
                entity.TransferAccountTime = model.TransferAccountTime;
                entity.TransferMethod = model.TransferMethod;
                entity.BeneficiaryAccountNo = model.BeneficiaryAccountNo;
                entity.CreaterUser = model.CreaterUser;
                return entity;
            }
            return null;
        }
    }
}
