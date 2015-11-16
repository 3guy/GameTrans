using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Enum
{
    public enum OrderStatus
    {
        /// <summary>
        /// 订单取消
        /// </summary>
        Cancell = -1,
        /// <summary>
        /// 订单生成
        /// </summary>
        Create = 1,
        /// <summary>
        /// 正在充值
        /// </summary>
        Recharging = 2,
        /// <summary>
        /// 充值成功
        /// </summary>
        RechargeSuccess = 3


    }
}
