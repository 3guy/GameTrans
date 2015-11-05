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
    /// 库存表
    /// </summary>
    [Table("T_Inventory")]
    public class T_Inventory:IEntity<T_Inventory>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string AppId { get; set; }
        public decimal Balance { get; set; }
        public int State { get; set; }
        public System.DateTime LastChargeTime { get; set; }
        public decimal LastChargePrice { get; set; }
        public string Comments { get; set; }

        public override T_Inventory Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.Id == Id);
        }

        public override T_Inventory Find(System.Linq.Expressions.Expression<Func<T_Inventory, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.Inventory.FirstOrDefault(where);
                if (item != null)
                {
                    this.Id = item.Id;
                    this.AppId = item.AppId;
                    this.Balance = item.Balance;
                    this.State = item.State;
                    this.LastChargePrice = item.LastChargePrice;
                    this.LastChargeTime = item.LastChargeTime;
                    this.Comments = item.Comments;
                    return this;
                }
                return null;
            }
        }

        public override bool Any(System.Linq.Expressions.Expression<Func<T_Inventory, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.Inventory.Any(where);
            }
        }



    }
}
