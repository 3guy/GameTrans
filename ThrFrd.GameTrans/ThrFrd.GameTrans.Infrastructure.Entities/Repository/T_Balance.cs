using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.EFContext;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Repository
{
    [Table("T_Balance")]
    public class T_Balance : IEntity<T_Balance>
    {

        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime OperationTime { get; set; }

        public override T_Balance Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.Id == Id);
        }

        public override T_Balance Find(Expression<Func<T_Balance, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.Balance.FirstOrDefault(where);
                if (item != null)
                {
                    this.Id = item.Id;

                    this.AppId = item.AppId;

                    this.Amount = item.Amount;

                    this.UserName = item.UserName;

                    this.OperationTime = item.OperationTime;

                }
                return null;
            }
        }

        public override bool Any(Expression<Func<T_Balance, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.Balance.Any(where);
            }
        }
    }
}
