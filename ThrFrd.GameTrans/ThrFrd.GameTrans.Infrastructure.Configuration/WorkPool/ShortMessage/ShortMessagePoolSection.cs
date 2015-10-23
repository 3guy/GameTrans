using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ThrFrd.GameTrans.Infrastructure.Configuration.WorkPool.Email
{
    public class ShortMessagePool : ConfigurationElement
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public ShortMessagePoolElementCollection Pool
        {
            get
            {
                return (ShortMessagePoolElementCollection)base[""];
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
