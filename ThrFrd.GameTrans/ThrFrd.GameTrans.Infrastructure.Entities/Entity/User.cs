using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.Enum;
using ThrFrd.GameTrans.Infrastructure.Entities.Repository;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Entity
{
    public class User : IAccessible<T_User, User>
    {
        public string UserName { get; set; }
        public string RealName { get; set; }
        public string Password { get; set; }
        public string HandPhone { get; set; }
        public string TelePhone { get; set; }
        public SystemRole SystemRole { get; set; }
        public ItemStatus Status { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateTime { get; set; }

        protected override void Covariant(T_User model)
        {
            if (model != null)
            {
                this.CreateTime = model.CreateTime;
                this.CreateUser = model.CreateUser;
                this.UserName = model.UserName;
                this.Password = model.Password;
                this.HandPhone = model.HandPhone;
                this.TelePhone = model.TelePhone;
                this.SystemRole = (SystemRole)model.SystemRole;
                this.RealName = model.RealName;
                this.Status = (ItemStatus)model.Status;
            }
        }

        protected override T_User Inverter(User model)
        {
            if (model != null)
            {
                return new T_User()
                {
                    CreateTime = model.CreateTime,
                    CreateUser = model.CreateUser,
                    UserName = model.UserName,
                    Password = model.Password,
                    HandPhone = model.HandPhone,
                    TelePhone = model.TelePhone,
                    SystemRole = (int)model.SystemRole,
                    RealName = model.RealName,
                    Status = (int)model.Status
                };
            }
            return null;
        }
    }
}
