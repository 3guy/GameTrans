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
    /// 后台用户表
    /// </summary>
    [Table("T_User")]
    public class T_User : IEntity<T_User>
    {
        [Key]
        [Column(TypeName = "varchar")]
        [StringLength(128)]
        public string UserName { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(20)]
        public string RealName { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(128)]
        public string Password { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(11)]
        public string HandPhone { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(11)]
        public string TelePhone { get; set; }
        public int Status { get; set; }
        public int SystemRole { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(128)]
        public string CreateUser { get; set; }
        public DateTime CreateTime { get; set; }

        public override T_User Find(string key)
        {
            return Find(c => c.UserName == key);
        }

        public override T_User Find(System.Linq.Expressions.Expression<Func<T_User, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.User.FirstOrDefault(where);
                if (item != null)
                {
                    this.CreateTime = item.CreateTime;
                    this.CreateUser = item.CreateUser;
                    this.UserName = item.UserName;
                    this.Password = item.Password;
                    this.TelePhone = item.TelePhone;
                    this.HandPhone = item.HandPhone;
                    this.RealName = item.RealName;
                    this.Status = item.Status;
                    this.SystemRole = item.SystemRole;
                    return this;
                }
                return null;
            }
        }

        public override bool Any(System.Linq.Expressions.Expression<Func<T_User, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.User.Any(where);
            }
        }
    }
}
