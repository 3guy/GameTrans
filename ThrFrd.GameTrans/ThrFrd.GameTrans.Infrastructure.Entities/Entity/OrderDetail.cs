using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.Repository;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Entity
{
    public class OrderDetail : IAccessible<T_OrderDetail, OrderDetail>
    {
        public long ID { get; set; }
        public string ProductName { get; set; }
        public long GameID { get; set; }
        public int Numbers { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public System.DateTime PayTime { get; set; }
        public string PayAccount { get; set; }
        public Nullable<long> GoldAccount { get; set; }
        public string PayDeviceAccount { get; set; }
        public Nullable<long> Pay4PlayerId { get; set; }


        protected override void Covariant(T_OrderDetail model)
        {
            if (model != null)
            {
                this.ID = model.ID;
                this.TotalPrice = model.TotalPrice;
                this.ProductName = model.ProductName;
                this.GameID = model.GameID;
                this.Numbers = model.Numbers;
                this.UnitPrice = model.UnitPrice;
                this.PayTime = model.PayTime;
                this.PayAccount = model.PayAccount;
                this.GoldAccount = model.GoldAccount;
                this.PayDeviceAccount = model.PayDeviceAccount;
                this.Pay4PlayerId = model.Pay4PlayerId;
            }
        }

        protected override T_OrderDetail Inverter(OrderDetail model)
        {
            if (model != null)
            {
                return new T_OrderDetail()
                {
                    ID = model.ID,
                    TotalPrice = model.TotalPrice,
                    ProductName = model.ProductName,
                    GameID = model.GameID,
                    Numbers = model.Numbers,
                    UnitPrice = model.UnitPrice,
                    PayTime = model.PayTime,
                    PayAccount = model.PayAccount,
                    GoldAccount = model.GoldAccount,
                    PayDeviceAccount = model.PayDeviceAccount,
                    Pay4PlayerId = model.Pay4PlayerId


                };
            }
            return null;
        }

    }
}
