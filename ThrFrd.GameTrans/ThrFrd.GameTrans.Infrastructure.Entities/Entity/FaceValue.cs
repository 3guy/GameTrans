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
        public long Id { get; set; }
        public string Name { get; set; }
        public long AppId { get; set; }
        public decimal RMB { get; set; }
        public decimal ForeignCurrency { get; set; }
        public string Notes { get; set; }

        protected override void Covariant(T_FaceValue model)
        {
            if (model != null)
            {
                Id = model.Id;
                Name = model.Name;
                AppId = model.AppId;
                RMB = model.RMB;
                ForeignCurrency = model.ForeignCurrency;
                Notes = model.Notes;
            }

        }
        protected override T_FaceValue Inverter(FaceValue item)
        {
            if (item != null)
            {
                return new T_FaceValue()
                {
                    Id = item.Id,
                    Name = item.Name,
                    AppId = item.AppId,
                    RMB = item.RMB,
                    ForeignCurrency = item.ForeignCurrency,
                    Notes = item.Notes,
                };
            }
            return null;
        }


    }
}
