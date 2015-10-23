using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace GoodMan.Tool.Configration.WorkPool.Email
{
    public class EmailPool : ConfigurationElement
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public EmailPoolElementCollection Pool
        {
            get
            {
                return (EmailPoolElementCollection)base[""];
            }
        }

        [ConfigurationProperty("threadgroupsize", IsRequired = true)]
        public int ThreadGroupSize
        {
            get
            {
                var i = base["threadgroupsize"];
                int threadgroupsize = 0;
                if (Int32.TryParse(i.ToString(), out threadgroupsize))
                {
                    return threadgroupsize > 0 ? threadgroupsize : 1;
                }
                return 1;
            }
        }
    }
}
