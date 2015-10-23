using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThrFrd.GameTrans.Infrastructure.Component.WorkPool.WorkHandle.ShortMessage
{
    public class ValidateCodeMessageModel
    {
        /// <summary>
        /// 收件人
        /// </summary>
        public string Recipient { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 验证码发送原由
        /// （超级管理员）登录管理后台
        /// 手机认证
        /// </summary>
        public string Reason { get; set; }
    }

}
