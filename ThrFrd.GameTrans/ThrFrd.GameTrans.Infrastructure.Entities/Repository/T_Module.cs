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
    /// 后台模块结构表
    /// </summary>
    [Table("T_Module")]
    public class T_Module : IEntity<T_Module>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Column(TypeName = "varchar")]
        public string Code { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(64)]
        public string Name { get; set; }
        public int Level { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(128)]
        public string Url { get; set; }
        public int ParentID { get; set; }
        public int SeqNo { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Icon { get; set; }
        [Column(TypeName = "bit")]
        public bool IsDisplay { get; set; }

        public override T_Module Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.ID == Id);
        }

        public override T_Module Find(System.Linq.Expressions.Expression<Func<T_Module, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.Module.FirstOrDefault(where);
                if (item != null)
                {
                    this.ID = item.ID;
                    this.Code = item.Code;
                    this.Name = item.Name;
                    this.ParentID = item.ParentID;
                    this.SeqNo = item.SeqNo;
                    this.Url = item.Url;
                    this.Icon = item.Icon;
                    this.IsDisplay = item.IsDisplay;
                    this.Level = item.Level;
                    return this;
                }
                return null;
            }
        }

        public override bool Any(System.Linq.Expressions.Expression<Func<T_Module, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.Module.Any(where);
            }
        }
    }
}
