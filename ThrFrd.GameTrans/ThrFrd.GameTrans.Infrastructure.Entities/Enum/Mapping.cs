using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Enum
{
    public static class EnumMapping
    {
        public static string Mapping(this ItemStatus itemStatus)
        {
            switch (itemStatus)
            {
                case ItemStatus.Delete:
                    return "已删除";
                case ItemStatus.Disable:
                    return "禁用";
                default:
                case ItemStatus.Enable:
                    return "启用";
                case ItemStatus.Supper:
                    return "超级管理";
            }
        }

        public static string Mapping(this OrderStatus orderStatus)
        {
            switch (orderStatus)
            {
                case OrderStatus.Cancell:
                    return "<span class='label label-primary' style='border-radius:0px;padding:7px;'>订单取消</span>";
                case OrderStatus.Create:
                    return "<span class='label label-info' style='border-radius:0px;padding:7px;'>订单生成</span>";
                case OrderStatus.Recharging:
                    return "<span class='label label-warning' style='border-radius:0px;padding:7px;'>正在充值</span>";
                case OrderStatus.RechargeSuccess:
                    return "<span class='label label-success' style='border-radius:0px;padding:7px;'>充值成功</span>";
                default:
                    return "";
            }
        }
    }
}
