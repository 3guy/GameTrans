using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.EFContext;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Repository
{
    [Table("T_Balance")]
   public  class T_Balance:IEntity<T_Balance>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string AppId { get; set; }
        public decimal Incoming { get; set; }
        public decimal Outting { get; set; }
        public string User { get; set; }
        public string OperationTime { get; set; }


        public override T_Balance Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.Id == Id);
        }

        public override T_Balance Find(System.Linq.Expressions.Expression<Func<T_Balance, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.Balance.FirstOrDefault(where);
                if (item != null)
                {
                    this.Id = item.Id;
                    this.AppId = item.AppId;
                    this.Incoming = item.Incoming;
                    this.Outting = item.Outting;
                    this.User = item.User;
                    this.OperationTime = item.OperationTime;
                   
                    return this;

                }
                return null;
            }
        }
        public override bool Any(System.Linq.Expressions.Expression<Func<T_Balance, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.Balance.Any(where);
            }
        }
    }
}
