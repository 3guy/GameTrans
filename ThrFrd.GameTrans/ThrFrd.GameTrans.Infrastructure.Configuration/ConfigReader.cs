using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using ThrFrd.GameTrans.Infrastructure.Configuration.WorkPool;

namespace ThrFrd.GameTrans.Infrastructure.Configuration
{
    public class ConfigReader
    {
        public static WorkPoolSection WorkPool
        {
            get
            {
                return ConfigurationManager.GetSection("workpool") as WorkPoolSection;
            }
        }
    }
}
