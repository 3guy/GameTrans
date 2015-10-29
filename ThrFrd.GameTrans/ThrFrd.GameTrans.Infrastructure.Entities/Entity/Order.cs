using Lending.Mall.Infrastructure.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.Entity;

namespace Lending.Mall.Infrastructure.Entities.Entity
{
    public class Order : IAccessible<T_Order,Order>
    {
        public long ID { get; set; }
        public string OrderId { get; set; }
        public string Details { get; set; }
        //to be define enum value 
        public string State { get; set; }
        public Nullable<decimal> Commissions { get; set; }
        public long UserId { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string Comments { get; set; }


        protected override void Covariant(T_Order model)
        {
            if (model != null)
            {
                this.ID = model.ID;
                this.OrderId = model.OrderId;
                this.Details = model.Details;
                this.State = model.State;
                this.Commissions = model.Commissions;
                this.UserId = model.UserId;
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
                OrderId = model.OrderId,
                Details = model.Details,
                State = model.State,
                Commissions = model.Commissions,
                UserId = model.UserId,
                CreateTime = model.CreateTime,
                Comments = model.Comments
                };
            }
            return null;
        }
    }
}
