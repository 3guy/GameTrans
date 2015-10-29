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
  public   class T_Platform:IEntity<T_Platform>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string GamePlatform { get; set; }
        public override T_Platform Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.ID == Id);
        }

        public override T_Platform Find(System.Linq.Expressions.Expression<Func<T_Platform, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.Platform.FirstOrDefault(where);
                if (item != null)
                {
                    this.ID = item.ID;
                    this.User = item.User;
                    this.Password = item.Password;
                    this.GamePlatform = item.GamePlatform;
                    return this;
                }
                return null;
            }
        }

        public override bool Any(System.Linq.Expressions.Expression<Func<T_Platform, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.Platform.Any(where);
            }
        }

    }
}
