using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThrFrd.GameTrans.Infrastructure.Component.ShortMessage
{
    public class ShortMessageContext
    {
        /// <summary>
        /// 收件短信地址
        /// </summary>
        public string Recipient { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Way{get;set;}
        /// <summary>
        /// 内容
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 署名
        /// </summary>
        public string Tail { get; set; }
        /// <summary>
        /// 发信账户
        /// </summary>
        public string CredentialUserName { get; set; }
        /// <summary>
        /// 发信密码
        /// </summary>
        public string CredentialPassword { get; set; }
    }
}
