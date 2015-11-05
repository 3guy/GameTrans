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
    public class T_FaceValue : IEntity<T_FaceValue>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public long AppId { get; set; }
        public decimal RMB { get; set; }
        public decimal ForeignCurrency { get; set; }
        public string Notes { get; set; }

        public override T_FaceValue Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.Id == Id);
        }

        public override T_FaceValue Find(System.Linq.Expressions.Expression<Func<T_FaceValue, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.FaceValue.FirstOrDefault(where);
                if (item != null)
                {
                    Id = item.Id;
                    Name = item.Name;
                    AppId = item.AppId;
                    RMB = item.RMB;
                    ForeignCurrency = item.ForeignCurrency;
                    Notes = item.Notes;
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
