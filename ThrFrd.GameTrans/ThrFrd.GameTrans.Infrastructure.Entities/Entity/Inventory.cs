﻿using ThrFrd.GameTrans.Infrastructure.Entities.Repository;
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

        public long ID { get; set; }
        public long AppID { get; set; }
        public Nullable<decimal> Money { get; set; }
        public InventoryState State { get; set; }
        public Nullable<System.DateTime> SoldTime { get; set; }


        protected override void Covariant(T_Inventory model)
        {
            if (model != null)
            {
                this.ID = model.ID;
                this.AppID = model.AppID;
                this.Money = model.Money;
                this.State = (InventoryState)model.State;
                this.SoldTime = model.SoldTime;
            }

        }
        protected override T_Inventory Inverter(Inventory item)
        {
            if (item != null)
            {
                return new T_Inventory(){ID = item.ID,
                AppID = item.AppID,
                Money = item.Money,
                State = (int)item.State,
                SoldTime = item.SoldTime
            };
            }
            return null;
        }
    }
}