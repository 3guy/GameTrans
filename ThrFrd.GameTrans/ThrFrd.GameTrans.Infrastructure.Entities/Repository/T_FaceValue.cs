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
   public class T_FaceValue:IEntity<T_FaceValue>
    {
       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public long AppId { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(500)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Value { get; set; }

        public override T_FaceValue Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.ID == Id);
        }

        public override T_FaceValue Find(System.Linq.Expressions.Expression<Func<T_FaceValue, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.FaceValue.FirstOrDefault(where);
                if (item != null)
                {
                    ID = item.ID;
                    AppId = item.AppId;
                    Name = item.Name;
                    Price = item.Price;
                    Value = item.Value;
                    return this;
                }
                return null;
            }
        }

        public override bool Any(System.Linq.Expressions.Expression<Func<T_FaceValue, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.FaceValue.Any(where);
            }
        }

    }
}
