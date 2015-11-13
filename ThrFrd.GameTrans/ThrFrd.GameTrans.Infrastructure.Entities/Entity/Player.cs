using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.EFContext;
using ThrFrd.GameTrans.Infrastructure.Entities.Repository;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Entity
{
    /// <summary>
    /// 记录买家每次交易前后详细的装备信息
    /// </summary>
    public class Player : IAccessible<T_Player, Player>
    {
        /// <summary>
        /// 
        /// </summary>
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

        protected override void Covariant(T_Player model)
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

        protected override T_Player Inverter(Player model)
        {
            if (model != null)
            {
                var entity = new T_Player();
                entity.Id = model.Id;
                entity.PlayerName = model.PlayerName;
                entity.RoleName = model.RoleName;
                entity.Rank = model.Rank;
                entity.PreviousGoldAccount = model.PreviousGoldAccount;
                entity.laterGoldAccount = model.laterGoldAccount;
                entity.TradeTax = model.TradeTax;
                entity.ProductTax = model.ProductTax;
                entity.EquipmentName = model.EquipmentName;
                return entity;
            }
            return null;
        }
    }
}
