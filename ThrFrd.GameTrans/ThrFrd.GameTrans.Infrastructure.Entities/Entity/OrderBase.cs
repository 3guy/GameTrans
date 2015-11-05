using ThrFrd.GameTrans.Infrastructure.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.Entity;
using ThrFrd.GameTrans.Infrastructure.Entities.Enum;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Entity
{
    public class OrderBase : IAccessible<T_OrderBase, OrderBase>
    {
        public long ID { get; set; }
        public System.DateTime CreateTime { get; set; }
        public decimal TotalPrice { get; set; }
        public int StateID { get; set; }
        public string Source { get; set; }
        public string PayerName { get; set; }
        public string Comments { get; set; }
        public string TransferAccountNumber { get; set; }
        public Nullable<decimal> TransferredAmount { get; set; }
        public Nullable<System.DateTime> TransferAccountTime { get; set; }
        public string TransferMethod { get; set; }
        public string BeneficiaryAccountNo { get; set; }

        protected override void Covariant(T_OrderBase model)
        {
            if (model != null)
            {
                this.ID = model.ID;
                this.TotalPrice = model.TotalPrice;
                this.StateID = model.StateID;
                this.Source = model.Source;
                this.PayerName = model.PayerName;
                this.CreateTime = model.CreateTime;
                this.Comments = model.Comments;
                this.TransferAccountNumber = model.TransferAccountNumber;
                this.TransferredAmount = model.TransferredAmount;
                this.TransferMethod = model.TransferMethod;
                this.TransferAccountTime = model.TransferAccountTime;
                this.BeneficiaryAccountNo = model.BeneficiaryAccountNo;
            }
        }

        protected override T_OrderBase Inverter(OrderBase model)
        {
            if (model != null)
            {
                return new T_OrderBase()
                {
                    ID = model.ID,
                    TotalPrice = model.TotalPrice,
                    StateID    = model.StateID,
                    Source = model.Source,
                    PayerName = model.PayerName,
                    CreateTime = model.CreateTime,
                    Comments = model.Comments,
                    TransferAccountNumber=model.TransferAccountNumber,
                    TransferredAmount = model.TransferredAmount,
                    TransferMethod = model.TransferMethod,
                    TransferAccountTime = model.TransferAccountTime,
                    BeneficiaryAccountNo = model.BeneficiaryAccountNo
                };
            }
            return null;
        }
    }
}
