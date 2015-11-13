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
    [Table("T_Player")]
    public class T_Player : IEntity<T_Player>
    {

        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long PreviousGoldAccount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long laterGoldAccount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TradeTax { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ProductTax { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EquipmentName { get; set; }

        public override T_Player Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.Id == Id);
        }

        public override T_Player Find(Expression<Func<T_Player, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.Player.FirstOrDefault(where);
                if (item != null)
                {
                    this.Id = item.Id;

                    this.PlayerName = item.PlayerName;

                    this.RoleName = item.RoleName;

                    this.Rank = item.Rank;

                    this.PreviousGoldAccount = item.PreviousGoldAccount;

                    this.laterGoldAccount = item.laterGoldAccount;

                    this.TradeTax = item.TradeTax;

                    this.ProductTax = item.ProductTax;

                    this.EquipmentName = item.EquipmentName;

                }
                return null;
            }
        }

        public override bool Any(Expression<Func<T_Player, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.Player.Any(where);
            }
        }
    }
}
