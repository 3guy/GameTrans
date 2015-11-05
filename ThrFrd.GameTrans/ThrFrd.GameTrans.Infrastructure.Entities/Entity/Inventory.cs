using ThrFrd.GameTrans.Infrastructure.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.Entity;
using ThrFrd.GameTrans.Infrastructure.Entities.Enum;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Entity
{
    public class Inventory:IAccessible<T_Inventory, Inventory>
    {

        public long Id { get; set; }
        public string AppId { get; set; }
        public decimal Balance { get; set; }
        public int State { get; set; }
        public System.DateTime LastChargeTime { get; set; }
        public decimal LastChargePrice { get; set; }
        public string Comments { get; set; }


        protected override void Covariant(T_Inventory model)
        {
            if (model != null)
            {
                this.Id = model.Id;
                this.AppId = model.AppId;
                this.Balance = model.Balance;
                this.State = model.State;
                this.LastChargePrice = model.LastChargePrice;
                this.LastChargeTime = model.LastChargeTime;
                this.Comments = model.Comments;
            }

        }
        protected override T_Inventory Inverter(Inventory item)
        {
            if (item != null)
            {
                return new T_Inventory()
                {
                   Id = item.Id,
                   AppId = item.AppId,
                   Balance = item.Balance,
                   State = item.State,
                   LastChargePrice = item.LastChargePrice,
                   LastChargeTime = item.LastChargeTime,
                   Comments = item.Comments
              };
            }
            return null;
        }
    }
}
