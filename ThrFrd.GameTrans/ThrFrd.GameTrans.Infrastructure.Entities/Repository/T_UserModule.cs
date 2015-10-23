using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.EFContext;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Repository
{
    /// <summary>
    /// 后台权限映射
    /// </summary>
    [Table("T_UserModule")]
    public class T_UserModule : IEntity<T_UserModule>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Column(TypeName = "varchar")]
        public string UserName { get; set; }
        public int ModuleID { get; set; }

        public override T_UserModule Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.ID == Id);
        }

        public override T_UserModule Find(System.Linq.Expressions.Expression<Func<T_UserModule, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.UserModule.FirstOrDefault(where);
                if (item != null)
                {
                    this.ID = item.ID;
                    this.ModuleID = item.ModuleID;
                    this.UserName = item.UserName;
                    return this;
                }
                return null;
            }
        }

        public override bool Any(System.Linq.Expressions.Expression<Func<T_UserModule, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.UserModule.Any(where);
            }
        }
    }
}
