using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ThrFrd.GameTrans.Infrastructure.Configuration.WorkPool.Email
{
    public class ShortMessagePoolItemElement : ConfigurationElement
    {
        [ConfigurationProperty("username", IsKey = true, IsRequired = true)]
        public string UserName
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

        [ConfigurationProperty("tail", IsRequired = true)]
        public string Tail
        {
            get
            {
                return (string)base["tail"];
            }
            set
            {
                base["tail"] = value;
            }
        }
    }
}
