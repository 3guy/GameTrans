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
    [Table("T_Account")]
    public class T_Account : IEntity<T_Account>
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
        /// 余额
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Comments { get; set; }


        public override T_Account Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.Id == Id);
        }

        public override T_Account Find(Expression<Func<T_Account, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.Account.FirstOrDefault(where);
                if (item != null)
                {
                    this.Id = item.Id;
                    this.AppId = item.AppId;
                    this.Balance = item.Balance;
                    this.State = item.State;
                    this.Comments = item.Comments;
                }
                return null;
            }
        }

        public override bool Any(Expression<Func<T_Account, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.Account.Any(where);
            }
        }
    }
}