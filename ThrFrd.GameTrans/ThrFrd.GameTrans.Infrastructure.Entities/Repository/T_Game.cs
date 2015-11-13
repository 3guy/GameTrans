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
    [Table("T_Game")]
    public class T_Game : IEntity<T_Game>
    {

        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }

        public int state { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime createtime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string comment { get; set; }

        public override T_Game Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.id == Id);
        }

        public override T_Game Find(Expression<Func<T_Game, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.Game.FirstOrDefault(where);
                if (item != null)
                {
                    this.id = item.id;

                    this.name = item.name;
                    this.state = item.state;
                    this.createtime = item.createtime;

                    this.CreateUser = item.CreateUser;

                    this.comment = item.comment;

                }
                return null;
            }
        }

        public override bool Any(Expression<Func<T_Game, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.Game.Any(where);
            }
        }
    }
}
