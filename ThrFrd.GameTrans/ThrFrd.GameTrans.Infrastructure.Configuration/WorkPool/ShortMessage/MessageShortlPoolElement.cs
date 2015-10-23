using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ThrFrd.GameTrans.Infrastructure.Configuration.WorkPool.Email
{
    public class ShortMessagePoolElement : ConfigurationElement
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
        [ConfigurationProperty("accountlist", IsDefaultCollection = true)]
        public ShortMessagePoolItemElementCollection AccountList
        {
            get
            {
                return (ShortMessagePoolItemElementCollection)base["accountlist"];
            }
        }
    }
}
