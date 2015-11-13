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
    [Table("T_AccountRate")]
    public class T_AccountRate : IEntity<T_AccountRate>
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
        /// 汇率
        /// </summary>
        public decimal Rate { get; set; }


        public override T_AccountRate Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.Id == Id);
        }

        public override T_AccountRate Find(Expression<Func<T_AccountRate, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.AccountRate.FirstOrDefault(where);
                if (item != null)
                {
                    this.Id = item.Id;

                    this.AppId = item.AppId;

                    this.Rate = item.Rate;

                }
                return null;
            }
        }

        public override bool Any(Expression<Func<T_AccountRate, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.AccountRate.Any(where);
            }
        }
    }
}