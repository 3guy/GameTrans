using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace GoodMan.Tool.Configration.WorkPool.Email
{
    public class EmailPoolItemElement : ConfigurationElement
    {
        [ConfigurationProperty("address", IsKey = true, IsRequired = true)]
        public string Address
        {
            get
            {
                return (string)base["address"];
            }
            set
            {
                base["address"] = value;
            }
        }

        [ConfigurationProperty("username", IsRequired = true)]
        public string Username
        {
            get
            {
                return (string)base["username"];
            }
            set
            {
                base["username"] = value;
            }
        }
        [ConfigurationProperty("password", IsRequired = true)]
        public string Password
        {
            get
            {
                return (string)base["password"];
            }
            set
            {
                base["password"] = value;
            }
        }
    }
}
