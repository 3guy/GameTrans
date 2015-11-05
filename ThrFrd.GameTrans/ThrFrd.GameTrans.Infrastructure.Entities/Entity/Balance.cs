using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.Repository;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Entity
{
   public  class Balance:IAccessible<T_Balance,Balance>
    {
        public long Id { get; set; }
        public string AppId { get; set; }
        public decimal Incoming { get; set; }
        public decimal Outting { get; set; }
        public string User { get; set; }
        public string OperationTime { get; set; }

        protected override void Covariant(T_Balance model)
        {
            if (model != null)
            {
                this.Id = model.Id;
                this.AppId = model.AppId;
                this.Incoming = model.Incoming;
                this.Outting = model.Outting;
                this.User = model.User;
                this.OperationTime = model.OperationTime;
            }

        }
        protected override T_Balance Inverter(Balance item)
        {
            if (item != null)
            {
                return new T_Balance()
                {
                    Id = item.Id,
                    AppId = item.AppId,
                    Incoming = item.Incoming,
                    Outting = item.Outting,
                    User = item.User,
                    OperationTime = item.OperationTime
                };
            }
            return null;
        }

    }
}
