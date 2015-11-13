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
    [Table("T_FaceValue")]
    public class T_FaceValue : IEntity<T_FaceValue>
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
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Price { get; set; }


        public override T_FaceValue Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.Id == Id);
        }

        public override T_FaceValue Find(Expression<Func<T_FaceValue, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.FaceValue.FirstOrDefault(where);
                if (item != null)
                {
                    this.Id = item.Id;

                    this.Name = item.Name;

                    this.Price = item.Price;

                }
                return null;
            }
        }

        public override bool Any(Expression<Func<T_FaceValue, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.FaceValue.Any(where);
            }
        }
    }
}
