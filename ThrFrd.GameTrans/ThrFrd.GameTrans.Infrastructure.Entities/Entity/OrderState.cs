using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.Repository;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Entity
{
   public  class OrderState:IAccessible<T_OrderState, OrderState>
    {
        public int ID { get; set; }
        public string State { get; set; }
        protected override void Covariant(T_OrderState model)
        {
            if (model != null)
            {
                this.ID = model.ID;
                this.State = model.State;
               
            }

        }
        protected override T_OrderState Inverter(OrderState item)
        {
            if (item != null)
            {
                return new T_OrderState()
                {
                    ID = item.ID,
                    State = item.State
                    
                };
            }
            return null;
        }
    }
}
