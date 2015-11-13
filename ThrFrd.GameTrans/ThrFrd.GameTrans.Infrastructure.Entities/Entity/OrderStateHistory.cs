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
    /// 订单历史记录表（记录订单状态变更信息）
    /// </summary>
    public class OrderStateHistory : IAccessible<T_OrderStateHistory, OrderStateHistory>
    {
        /// <summary>
        /// 
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public OrderStatus PreviousState { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public OrderStatus CurrentState { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Creater { get; set; }

        protected override void Covariant(T_OrderStateHistory model)
        {
            if (model != null)
            {
                this.ID = model.ID;
                this.PreviousState = (OrderStatus)model.PreviousState;
                this.CurrentState = (OrderStatus)model.CurrentState;
                this.CreateTime = model.CreateTime;
                this.Creater = model.Creater;

            }
        }

        protected override T_OrderStateHistory Inverter(OrderStateHistory model)
        {
            if (model != null)
            {
                var entity = new T_OrderStateHistory();
                entity.ID = model.ID;
                entity.PreviousState = (int)model.PreviousState;
                entity.CurrentState = (int)model.CurrentState;
                entity.CreateTime = model.CreateTime;
                entity.Creater = model.Creater;
                return entity;
            }
            return null;
        }
    }
}
