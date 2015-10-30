using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Enum
{
    public enum SystemRole
    {
        /// <summary>
        /// 超级管理员
        /// </summary>
        SupperAdmin = 1,
        /// <summary>
        /// 下单员
        /// </summary>
        OrderPlacer = 2,
        /// <summary>
        /// 充值员
        /// </summary>
        OrderProcessor = 3

    }
}
