using ThrFrd.GameTrans.Infrastructure.Entities.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.Entity;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Entity
{
    public class FaceValue : IAccessible<T_FaceValue, FaceValue>
    {
        public long ID { get; set; }
        public long InventoryId { get; set; }
        public string GameName { get; set; }
        public decimal Price { get; set; }
        public decimal Value { get; set; }

        protected override void Covariant(T_FaceValue model)
        {
            if (model != null)
            {
                ID = model.ID;
                InventoryId = model.InventoryId;
                GameName = model.GameName;
                Price = model.Price;
                Value = model.Value;
            }

        }
        protected override T_FaceValue Inverter(FaceValue item)
        {
            if (item != null)
            {
                return new T_FaceValue()
                {
                     ID = item.ID,
                     InventoryId = item.InventoryId,
                     GameName = item.GameName,
                        Price = item.Price,
                        Value = item.Value
                };
            }
            return null;
        }


    }
}
