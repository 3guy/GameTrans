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
    }
}
