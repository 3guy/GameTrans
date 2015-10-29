using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.EFContext;
using ThrFrd.GameTrans.Infrastructure.Entities.Repository;

namespace Lending.Mall.Infrastructure.Entities.Repository
{
    public class T_Commission:IEntity<T_Commission>
    {
        public long ID { get; set; }
        public long GameID { get; set; }
        public Nullable<decimal> Price { get; set; }
        //Better to define enum 
        public string Type { get; set; }
        public Nullable<System.DateTime> Time { get; set; }

        public override T_Commission Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.ID == Id);
        }

        public override T_Commission Find(System.Linq.Expressions.Expression<Func<T_Commission, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.Commission.FirstOrDefault(where);
                if (item != null)
                {
                    ID = item.ID;
                    GameID  = item.GameID;
                    Time = item.Time;
                    Type = item.Type;
                    return this;
                }
                return null;
            }
        }

        public override bool Any(System.Linq.Expressions.Expression<Func<T_Commission, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.Commission.Any(where);
            }
        }

    }
}
