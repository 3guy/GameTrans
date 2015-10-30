using ThrFrd.GameTrans.Infrastructure.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.Entity;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Entity
{
   public  class Platform:IAccessible<T_Platform, Platform>
    {
        public long ID { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string GamePlatform { get; set; }


        protected override void Covariant(T_Platform model)
        {
            if (model != null)
            {
                this.ID = model.ID;
                this.User = model.User;
                this.Password = model.Password;
                this.GamePlatform = model.GamePlatform;
            }

        }
        protected override T_Platform Inverter(Platform item)
        {
            if (item != null)
            {
                return new T_Platform()
                {
                  ID = item.ID,
                  User = item.User,
                  Password = item.Password,
                  GamePlatform = item.GamePlatform
                };
            }
            return null;
        }

    }
}
