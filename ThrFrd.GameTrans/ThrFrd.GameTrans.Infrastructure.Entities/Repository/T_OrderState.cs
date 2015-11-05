using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.EFContext;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Repository
{
    [Table("T_OrderState")]
    public class T_OrderState:IEntity<T_OrderState>
    {
        public int ID { get; set; }
        public string State { get; set; }

        public override T_OrderState Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.ID == Id);
        }

        public override T_OrderState Find(System.Linq.Expressions.Expression<Func<T_OrderState, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.OrderState.FirstOrDefault(where);
                if (item != null)
                {
                    this.ID = item.ID;
                    this.State = item.State;

                    return this;

                }
                return null;
            }
        }
        public override bool Any(System.Linq.Expressions.Expression<Func<T_OrderState, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.OrderState.Any(where);
            }
        }

    }
}
