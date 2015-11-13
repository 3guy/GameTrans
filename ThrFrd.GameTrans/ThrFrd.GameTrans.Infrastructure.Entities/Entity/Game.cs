using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.EFContext;
using ThrFrd.GameTrans.Infrastructure.Entities.Repository;
using ThrFrd.GameTrans.Infrastructure.Entities.Enum;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Entity
{
    /// <summary>
    /// 游戏信息
    /// </summary>
    public class Game : IAccessible<T_Game, Game>
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        public ItemStatus state;
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

        protected override void Covariant(T_Game model)
        {
            if (model != null)
            {
                this.id = model.id;
                this.name = model.name;
                this.state = (ItemStatus)model.state;
                this.createtime = model.createtime;
                this.CreateUser = model.CreateUser;
                this.comment = model.comment;

            }
        }

        protected override T_Game Inverter(Game model)
        {
            if (model != null)
            {
                var entity = new T_Game();
                entity.id = model.id;
                entity.name = model.name;
                entity.state = (int)model.state;
                entity.createtime = model.createtime;
                entity.CreateUser = model.CreateUser;
                entity.comment = model.comment;
                return entity;
            }
            return null;
        }
    }
}
