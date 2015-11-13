using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading;
using ThrFrd.GameTrans.Infrastructure.Configuration;
using ThrFrd.GameTrans.Infrastructure.Entities.EFContext;
using ThrFrd.GameTrans.Tool;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Repository
{
    /// <summary>
    /// 验证码流水
    /// </summary>
    [Table("T_CheckCodeStream")]
    public class T_CheckCodeStream : IEntity<T_CheckCodeStream>
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(128)]
        public string UserName { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(11)]
        public string Mobile { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(32)]
        public string Code { get; set; }
        public int Type { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime ExpireDate { get; set; }

        public override T_CheckCodeStream Find(string key)
        {
            return Find(c => UserName == key);
        }

        public override T_CheckCodeStream Find(System.Linq.Expressions.Expression<Func<T_CheckCodeStream, bool>> where)
        {
            using(Context ctx = new Context())
            {
                var item = ctx.CheckCodeStream.FirstOrDefault(where);
                if (item != null)
                {
                    this.Id = item.Id;
                    this.UserName = item.UserName;
                    this.Mobile = item.Mobile;
                    this.Code = item.Code;
                    this.Type = item.Type;
                    this.RecordDate = item.RecordDate;
                    this.ExpireDate = item.ExpireDate;
                    if (SiteSettings.EnableTrace)
                    {
                        LogHelper.Write(CommonLogger.DataBase, LogLevel.Trace,
                            String.Format("ThreadId:{0}, 对象：{1}(hashCode:{2})执行Find操作。对象值为：{3}",
                            Thread.CurrentThread.ManagedThreadId.ToString(), this.GetType().FullName, this.GetHashCode(), this.ToJson()));
                    }
                    return this;
                }
                return null;
            }
        }

        public override bool Any(System.Linq.Expressions.Expression<Func<T_CheckCodeStream, bool>> where)
        {
            using(Context ctx = new Context())
            {
                return ctx.CheckCodeStream.Any(where);
            }
        }
    }
}
