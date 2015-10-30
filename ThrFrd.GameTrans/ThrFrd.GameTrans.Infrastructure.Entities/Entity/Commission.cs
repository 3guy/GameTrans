using ThrFrd.GameTrans.Infrastructure.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.Enum;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Entity
{
   public class Commission:IAccessible<T_Commission,Commission>
    {
        public long ID { get; set; }
        public long GameID { get; set; }
        public Nullable<decimal> Price { get; set; }
        public CommissionType Type { get; set; }
        public Nullable<System.DateTime> Time { get; set; }
        public string Comment { get; set; }

        protected override void Covariant(T_Commission model)
        {
            if (model != null)
            {
                ID = model.ID;
                GameID = model.GameID;
                Time = model.Time;
                Type = (CommissionType)model.Type;
                Price = model.Price;
                Comment = model.Comment;
            }

        }
        protected override T_Commission Inverter(Commission item)
        {
            if (item != null)
            {
                return new T_Commission()
                {
                   ID = item.ID,
                   GameID  = item.GameID,
                   Time = item.Time,
                   Type = (int)item.Type,
                   Price = item.Price,
                   Comment = item.Comment
                };
            }
            return null;
        }


    }
}
