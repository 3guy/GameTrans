using ThrFrd.GameTrans.Infrastructure.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.Entity;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Entity
{
   public class Game:IAccessible<T_Game,Game>
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreateUser { get; set; }
        public string Comment { get; set; }

        protected override void Covariant(T_Game model)
        {
            if (model != null)
            {
                this.ID = model.ID;
                this.Name = model.Name;
                this.CreateTime = model.CreateTime;
                this.CreateUser = model.CreateUser;
                this.Comment = model.Comment;
            }

        }
        protected override T_Game Inverter(Game item)
        {
            if (item != null)
            {
                return new T_Game()
                {
                    ID = item.ID,
                    Name = item.Name,
                    CreateTime = item.CreateTime,
                    CreateUser = item.CreateUser,
                    Comment = item.Comment
                };
            }
            return null;
        }


    }
}
