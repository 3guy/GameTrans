using Lending.Mall.Infrastructure.Entities.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.Entity;

namespace Lending.Mall.Infrastructure.Entities.Entity
{
    public class FaceValue : IAccessible<T_FaceValue, FaceValue>
    {
        public long ID { get; set; }
        public long AppId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Value { get; set; }

        protected override void Covariant(T_FaceValue model)
        {
            if (model != null)
            {
                ID = model.ID;
                AppId = model.AppId;
                Name = model.Name;
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
                        AppId = item.AppId,
                        Name = item.Name,
                        Price = item.Price,
                        Value = item.Value
                };
            }
            return null;
        }


    }
}
