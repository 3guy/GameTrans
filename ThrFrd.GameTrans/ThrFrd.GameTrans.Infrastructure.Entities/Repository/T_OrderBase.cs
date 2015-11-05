using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.EFContext;
using ThrFrd.GameTrans.Infrastructure.Entities.Repository;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Repository
{
    /// <summary>
    /// 订单表
    /// </summary>
    [Table("T_Order_Base")]
    public class T_OrderBase : IEntity<T_OrderBase>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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


        public override T_OrderBase Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.ID == Id);
        }

        public override T_OrderBase Find(System.Linq.Expressions.Expression<Func<T_OrderBase, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.OrderBase.FirstOrDefault(where);
                if (item != null)
                {
                    this.ID = item.ID;
                    this.TotalPrice = item.TotalPrice;
                    this.StateID    = item.StateID;
                    this.Source = item.Source;
                    this.PayerName = item.PayerName;
                    this.CreateTime = item.CreateTime;
                    this.Comments = item.Comments;
                    this.TransferAccountNumber=item.TransferAccountNumber;
                    this.TransferredAmount = item.TransferredAmount;
                    this.TransferMethod = item.TransferMethod;
                    this.TransferAccountTime = item.TransferAccountTime;
                    this.BeneficiaryAccountNo = item.BeneficiaryAccountNo;
                    return this;

                }
                return null;
            }
        }
        public override bool Any(System.Linq.Expressions.Expression<Func<T_OrderBase, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.OrderBase.Any(where);
            }
        }


    }
}
