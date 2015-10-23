using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace GoodMan.Tool.Configration.WorkPool.Email
{
    public class EmailPoolElement : ConfigurationElement
    {
        [ConfigurationProperty("partition", IsKey=true, IsRequired = true)]
        public string Partition
        {
            get
            {
                return (string)base["partition"];
            }
            set
            {
                base["partition"] = value;
            }
        }
        [ConfigurationProperty("enable", IsRequired = true)]
        public bool Enable
        {
            get
            {
                return (bool)base["enable"];
            }
            set
            {
                base["enable"] = value;
            }
        }
        [ConfigurationProperty("serverlist", IsDefaultCollection = true)]
        public EmailPoolItemElementCollection ServerList
        {
            get
            {
                return (EmailPoolItemElementCollection)base["serverlist"];
            }
        }
    }
}
