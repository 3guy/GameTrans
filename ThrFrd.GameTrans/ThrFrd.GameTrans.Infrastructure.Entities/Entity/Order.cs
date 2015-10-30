using ThrFrd.GameTrans.Infrastructure.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.Entity;
using ThrFrd.GameTrans.Infrastructure.Entities.Enum;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Entity
{
    public class Order : IAccessible<T_Order, Order>
    {
        public long ID { get; set; }
        public string OrderCode { get; set; }
        public string Details { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Nullable<decimal> Commissions { get; set; }
        public string UserName { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string Comments { get; set; }

        protected override void Covariant(T_Order model)
        {
            if (model != null)
            {
                this.ID = model.ID;
                this.OrderCode = model.OrderCode;
                this.Details = model.Details;
                this.OrderStatus = (OrderStatus)model.OrderStatus;
                this.Commissions = model.Commissions;
                this.UserName = model.UserName;
                this.CreateTime = model.CreateTime;
                this.Comments = model.Comments;
            }
        }

        protected override T_Order Inverter(Order model)
        {
            if (model != null)
            {
                return new T_Order()
                {
                    ID = model.ID,
                    OrderCode = model.OrderCode,
                    Details = model.Details,
                    OrderStatus = (int)model.OrderStatus,
                    Commissions = model.Commissions,
                    UserName = model.UserName,
                    CreateTime = model.CreateTime,
                    Comments = model.Comments
                };
            }
            return null;
        }
    }
}
