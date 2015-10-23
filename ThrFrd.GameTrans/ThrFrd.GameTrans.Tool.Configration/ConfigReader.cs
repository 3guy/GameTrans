using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using GoodMan.Tool.Configration.WorkPool;

namespace GoodMan.Tool.Configration
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
