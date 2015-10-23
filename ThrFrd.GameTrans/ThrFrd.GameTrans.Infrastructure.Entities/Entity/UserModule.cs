using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.Repository;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Entity
{
    public class UserModule : IAccessible<T_UserModule, UserModule>
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public int ModuleID { get; set; }

        public Module Module { get; set; }
        public UserModule LoadModule(T_Module t_module)
        {
            this.Module = new Module().Set(t_module);
            return this;
        }
        protected override void Covariant(T_UserModule model)
        {
            if (model != null)
            {
                this.ID = model.ID;
                this.ModuleID = model.ModuleID;
                this.UserName = model.UserName;
            }
        }

        protected override T_UserModule Inverter(UserModule model)
        {
            if (model != null)
            {
                return new T_UserModule()
                {
                    ID = model.ID,
                    ModuleID = model.ModuleID,
                    UserName = model.UserName
                };
            }
            return null;
        }

        public UserModule Set(T_UserModule dbModel)
        {
            Covariant(dbModel);
            return this;
        }
    }
}

