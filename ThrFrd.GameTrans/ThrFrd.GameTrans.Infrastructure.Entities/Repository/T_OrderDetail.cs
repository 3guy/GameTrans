using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.EFContext;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Repository
{
    [Table("T_Order_Detail")]
   public class T_OrderDetail:IEntity<T_OrderDetail>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        public override T_OrderDetail Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.ID == Id);
        }

        public override T_OrderDetail Find(System.Linq.Expressions.Expression<Func<T_OrderDetail, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.OrderDetail.FirstOrDefault(where);
                if (item != null)
                {
                    this.ID = item.ID;
                    this.TotalPrice = item.TotalPrice;
                    this.ProductName = item.ProductName;
                    this.GameID = item.GameID;
                    this.Numbers = item.Numbers;
                    this.UnitPrice = item.UnitPrice;
                    this.PayTime = item.PayTime;
                    this.PayAccount = item.PayAccount;
                    this.GoldAccount = item.GoldAccount;
                    this.PayDeviceAccount = item.PayDeviceAccount;
                    this.Pay4PlayerId = item.Pay4PlayerId;
                    return this;

                }
                return null;
            }
        }
        public override bool Any(System.Linq.Expressions.Expression<Func<T_OrderDetail, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.OrderDetail.Any(where);
            }
        }





    }
}
