using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.EFContext;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Repository
{
    //记录玩家充值前后装备，宝石等详细信息。
    [Table("T_PlayerCharge")]
   public  class T_PlayerCharge:IEntity<T_PlayerCharge>
   {
       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string PlayerName { get; set; }
        public string RoleName { get; set; }
        public int Rank { get; set; }
        public string PreviousGoldAccount { get; set; }
        public string laterGoldAccount { get; set; }
        public decimal TradeTax { get; set; }
        public decimal ProductTax { get; set; }
        public string EquipmentName { get; set; }

        public override T_PlayerCharge Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.Id == Id);
        }

        public override T_PlayerCharge Find(System.Linq.Expressions.Expression<Func<T_PlayerCharge, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.PlayerCharge.FirstOrDefault(where);
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
                    return this;

                }
                return null;
            }
        }
        public override bool Any(System.Linq.Expressions.Expression<Func<T_PlayerCharge, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.PlayerCharge.Any(where);
            }
        }

    }
}
