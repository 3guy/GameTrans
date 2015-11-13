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
    [Table("T_OrderStateHistory")]
    public class T_OrderStateHistory : IEntity<T_OrderStateHistory>
    {

        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PreviousState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CurrentState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Creater { get; set; }

        public override T_OrderStateHistory Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.ID == Id);
        }

        public override T_OrderStateHistory Find(Expression<Func<T_OrderStateHistory, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.OrderStateHistory.FirstOrDefault(where);
                if (item != null)
                {
                    this.ID = item.ID;

                    this.PreviousState = item.PreviousState;

                    this.CurrentState = item.CurrentState;

                    this.CreateTime = item.CreateTime;

                    this.Creater = item.Creater;

                }
                return null;
            }
        }

        public override bool Any(Expression<Func<T_OrderStateHistory, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.OrderStateHistory.Any(where);
            }
        }
    }
}