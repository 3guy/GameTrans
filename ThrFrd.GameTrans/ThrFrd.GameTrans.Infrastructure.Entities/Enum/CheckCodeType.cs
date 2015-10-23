using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Enum
{
    public enum CheckCodeType
    {
        /// <summary>
        /// 注册手机号验证
        /// </summary>
        RegisterShortMessageCode = 1,
        /// <summary>
        /// 重置密码短信验证码
        /// </summary>
        ResetPasswordShortMessageCode = 2,
        /// <summary>
        /// 设置绑定手机号短信验证码
        /// </summary>
        SetMobileShortMessageCode = 3,
        /// <summary>
        /// 更改绑定手机号
        /// </summary>
        ResetPhoneShortMessageCode = 4,
        /// <summary>
        /// 登陆后台
        /// </summary>
        LoginAdminShortMessageCode = 5
    }
}
