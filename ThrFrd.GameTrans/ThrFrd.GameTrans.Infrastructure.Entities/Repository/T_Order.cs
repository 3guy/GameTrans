using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.EFContext;
using ThrFrd.GameTrans.Infrastructure.Entities.Repository;

namespace Lending.Mall.Infrastructure.Entities.Repository
{
    /// <summary>
    /// 订单表
    /// </summary>
    [Table("T_Order")]
    public class T_Order : IEntity<T_Order>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(500)]
        public string OrderId { get; set; }
        [Column(TypeName = "nvarchar")]
        public string Details { get; set; }
        //to be define enum value 
        public string State { get; set; }

        public Nullable<decimal> Commissions { get; set; }

        public long UserId { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(500)]
        public string Comments { get; set; }

        public override T_Order Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.ID == Id);
        }

        public override T_Order Find(System.Linq.Expressions.Expression<Func<T_Order, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.Order.FirstOrDefault(where);
                if (item != null)
                {
                    this.ID = item.ID;
                    this.OrderId = item.OrderId;
                    this.Details = item.Details;
                    this.State = item.State;
                    this.Commissions = item.Commissions;
                    this.UserId = item.UserId;
                    this.CreateTime = item.CreateTime;
                    this.Comments = item.Comments;
                    return this;

                }
                return null;
            }
        }
        public override bool Any(System.Linq.Expressions.Expression<Func<T_Order, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.Order.Any(where);
            }
        }


    }
}
