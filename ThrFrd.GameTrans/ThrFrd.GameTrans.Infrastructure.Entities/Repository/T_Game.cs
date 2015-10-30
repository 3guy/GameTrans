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
    public class T_Game : IEntity<T_Game>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(500)]
        public string Name { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(128)]
        public string CreateUser { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(500)]
        public string Comment { get; set; }

        public override T_Game Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.ID == Id);
        }

        public override T_Game Find(System.Linq.Expressions.Expression<Func<T_Game, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.Game.FirstOrDefault(where);
                if (item != null)
                {
                    ID = item.ID;
                    Name = item.Name;
                    CreateTime = item.CreateTime;
                    CreateUser = item.CreateUser;
                    Comment = item.Comment;
                    return this;
                }
                return null;
            }
        }

        public override bool Any(System.Linq.Expressions.Expression<Func<T_Game, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.Game.Any(where);
            }
        }

    }
}
