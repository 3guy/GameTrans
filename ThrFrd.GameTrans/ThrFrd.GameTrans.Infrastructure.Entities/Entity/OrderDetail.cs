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
    /// 订单详情表
    /// </summary>
    public class OrderDetail : IAccessible<T_OrderDetail, OrderDetail>
    {
        /// <summary>
        /// 
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long GameID { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int Numbers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime PayTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PayAccount { get; set; }
        /// <summary>
        /// 挂单元宝数
        /// </summary>
        public long GoldAccount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PayDeviceAccount { get; set; }
        /// <summary>
        /// 对应游戏玩家player表id
        /// </summary>
        public long Pay4PlayerId { get; set; }

        protected override void Covariant(T_OrderDetail model)
        {
            if (model != null)
            {
                this.ID = model.ID;
                this.ProductName = model.ProductName;
                this.GameID = model.GameID;
                this.Numbers = model.Numbers;
                this.UnitPrice = model.UnitPrice;
                this.TotalPrice = model.TotalPrice;
                this.PayTime = model.PayTime;
                this.PayAccount = model.PayAccount;
                this.GoldAccount = model.GoldAccount;
                this.PayDeviceAccount = model.PayDeviceAccount;
                this.Pay4PlayerId = model.Pay4PlayerId;

            }
        }

        protected override T_OrderDetail Inverter(OrderDetail model)
        {
            if (model != null)
            {
                var entity = new T_OrderDetail();
                entity.ID = model.ID;
                entity.ProductName = model.ProductName;
                entity.GameID = model.GameID;
                entity.Numbers = model.Numbers;
                entity.UnitPrice = model.UnitPrice;
                entity.TotalPrice = model.TotalPrice;
                entity.PayTime = model.PayTime;
                entity.PayAccount = model.PayAccount;
                entity.GoldAccount = model.GoldAccount;
                entity.PayDeviceAccount = model.PayDeviceAccount;
                entity.Pay4PlayerId = model.Pay4PlayerId;
                return entity;
            }
            return null;
        }
    }
}
