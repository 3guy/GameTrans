using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using GoodMan.Tool.Configration.WorkPool.Email;
using GoodMan.Tool.Configration.WorkPool.CommonWork;

namespace GoodMan.Tool.Configration.WorkPool
{
    public class WorkPoolSection : ConfigurationSection
	{
        [ConfigurationProperty("emailqueue",IsRequired= true)]
        public EmailPool EmailQueue
        {
            get
            {
                return (EmailPool)base["emailqueue"];
            }
        }

        [ConfigurationProperty("commonwork", IsRequired = true)]
        public CommonWorkPool CommonWorkQueue
        {
            get 
            {
                return (CommonWorkPool)base["commonwork"];
            }
        }

        [ConfigurationProperty("minthreadsize", IsRequired = true)]
        public int MinThreadSize
        {
            get
            {
                var i = base["minthreadsize"];
                int minThreadsize = 0;
                if (Int32.TryParse(i.ToString(), out minThreadsize))
                {
                    return minThreadsize > 0 ? minThreadsize : 5;
                }
                return 5;
            }
        }

        [ConfigurationProperty("maxthreadsize", IsRequired = true)]
        public int MaxThreadSize
        {
            get
            {
                var i = base["maxthreadsize"];
                int maxThreadsize = 0;
                if (Int32.TryParse(i.ToString(), out maxThreadsize))
                {
                    if (maxThreadsize > MinThreadSize)
                    {
                        return maxThreadsize;
                    }
                }
                return MinThreadSize * 2;
            }
        }
	}
}
