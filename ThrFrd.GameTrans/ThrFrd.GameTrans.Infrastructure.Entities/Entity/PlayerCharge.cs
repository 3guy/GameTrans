using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.Repository;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Entity
{
   public  class PlayerCharge:IAccessible<T_PlayerCharge, PlayerCharge>
    {

        public long Id { get; set; }
        public string PlayerName { get; set; }
        public string RoleName { get; set; }
        public int Rank { get; set; }
        public string PreviousGoldAccount { get; set; }
        public string laterGoldAccount { get; set; }
        public decimal TradeTax { get; set; }
        public decimal ProductTax { get; set; }
        public string EquipmentName { get; set; }


        protected override void Covariant(T_PlayerCharge model)
        {
            if (model != null)
            {
                this.Id = model.Id;
                this.PlayerName = model.PlayerName;
                this.RoleName = model.RoleName;
                this.Rank = model.Rank;
                this.PreviousGoldAccount = model.PreviousGoldAccount;
                this.laterGoldAccount = model.laterGoldAccount;
                this.TradeTax = model.TradeTax;
                this.ProductTax = model.ProductTax;
                this.EquipmentName = model.EquipmentName;
                
            }

        }
        protected override T_PlayerCharge Inverter(PlayerCharge item)
        {
            if (item != null)
            {
                return new T_PlayerCharge()
                {  Id = item.Id,
                PlayerName = item.PlayerName,
                RoleName = item.RoleName,
                Rank = item.Rank,
                PreviousGoldAccount = item.PreviousGoldAccount,
                laterGoldAccount = item.laterGoldAccount,
                TradeTax = item.TradeTax,
                ProductTax = item.ProductTax,
                EquipmentName = item.EquipmentName
                   
                };
            }
            return null;
        }

    }
}
