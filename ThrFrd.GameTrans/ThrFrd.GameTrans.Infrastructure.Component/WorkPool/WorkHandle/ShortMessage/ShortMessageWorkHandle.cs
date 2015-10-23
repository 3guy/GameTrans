using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using ThrFrd.GameTrans.Tool;
using ThrFrd.GameTrans.Infrastructure.Configuration;
using ThrFrd.GameTrans.Infrastructure.Component.ShortMessage;

namespace ThrFrd.GameTrans.Infrastructure.Component.WorkPool.WorkHandle.ShortMessage
{
    internal abstract class BaseShortMessageHandle<T> : IWorkHandle<T>
    {
        string partition = string.Empty;
        /// <summary>
        /// 联通号段
        /// </summary>
        static List<string> unicomCode = new List<string>() { 
            "130","131","132","145","155","156","185","186","176"
        };
        /// <summary>
        /// 电信号段
        /// </summary>
        static List<string> telecomCode = new List<string>() { 
            "133","153","180","181","189","170","177"
        };
        protected virtual void Prepare(ShortMessageContext ctx, string partition = "common")
        {
            this.partition = partition;
            var accountItem = ConfigReader.WorkPool.ShortMessageQueue.Pool[partition].AccountList.RandomGet();
            ctx.CredentialPassword = accountItem.Password;
            ctx.CredentialUserName = accountItem.UserName;
            ctx.Tail = accountItem.Tail;
        }
        protected void SendShortMessageCallBack(ShortMessageContext context)
        {
            if (ConfigReader.WorkPool.ShortMessageQueue.Pool[this.partition].Enable)
            {
                new ShortMessageHandle(context).Send();
            }
            else
            {
                LogHelper.Write(CommonLogger.Application, LogLevel.Info, "短信发送池被禁用，发送短信失败。ShortMessageContext:" + context.ToJson());
            }
        }

        protected bool IsUniconPhone(string phoneCode)
        {
            string codeStart = phoneCode.Trim().Substring(0, 3);
            return unicomCode.Contains(codeStart);
        }
        protected bool IsTelecomPhone(string phoneCode)
        {
            string codeStart = phoneCode.Trim().Substring(0, 3);
            return telecomCode.Contains(codeStart);
        }

        #region IWorkHandle<T> 成员

        public abstract WaitWorkItem Process(T context);

        #endregion
    }

    internal class ValidateCodeShortMessageHandle<T> : BaseShortMessageHandle<T>
    {
        public override WaitWorkItem Process(T context)
        {
            ValidateCodeMessageModel shortmessagectx = context as ValidateCodeMessageModel;
            WaitWorkItem data = new WaitWorkItem();
            var ctx = new ShortMessageContext();
            ctx.Way = 0;
            ctx.Recipient = shortmessagectx.Recipient;
            Prepare(ctx);
            ctx.Body = String.Format("您的验证码为{0}，您正在{1}，请不要将本验证码告诉他人",
                shortmessagectx.Code, shortmessagectx.Reason);
            data.Context = ctx;

            #region 赋值Action
            data.Action = o => { SendShortMessageCallBack(o as ShortMessageContext); };
            #endregion

            return data;
        }
    }

}
