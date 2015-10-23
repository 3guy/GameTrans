using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ThrFrd.GameTrans.Infrastructure.Component.WorkPool;
using ThrFrd.GameTrans.Infrastructure.Component.WorkPool.WorkHandle.ShortMessage;
using ThrFrd.GameTrans.Infrastructure.Entities.Entity;
using ThrFrd.GameTrans.Infrastructure.Entities.Enum;
using ThrFrd.GameTrans.Tool;

namespace ThrFrd.GameTrans.Application.Core
{
    public class SupportBiz
    {
        static int Sequence = 0;
        private static Random random = new Random(Guid.NewGuid().GetHashCode());
        public static int GetRandom(int minVal, int maxVal)
        {
            return random.Next(minVal, maxVal);
        }
        private static CheckCodeStream GetCheckCode(string username,string phone, CheckCodeType checkCodeType, int codeLength = 6, bool isRefrash = true)
        {
            var code = new CheckCodeStream().Find(c => c.UserName == username
             && c.Type == (int)checkCodeType);
            if (!isRefrash)
            {
                return code;
            }

            var newcode = GetRandomCode(codeLength);

            if (code == null)
            {
                //第一次获取此类型验证码
                //添加此类型验证码
                code = new CheckCodeStream()
                {
                    Code = newcode,
                    ExpireDate = DateTime.Now.Add(new TimeSpan(0, 5, 0)),
                    RecordDate = DateTime.Now,
                    Type = checkCodeType,
                    UserName = username,
                    Mobile = phone
                }.PostAdd();
            }
            else
            {
                code.Code = newcode;
                code.ExpireDate = DateTime.Now.Add(new TimeSpan(0, 5, 0));
                code.RecordDate = DateTime.Now;
                code.Mobile = phone;
                code = code.PostModify();
                //覆盖之前验证码
            }
            return code;
        }
        private static string GetRandomCode(int length)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(GetRandom(0, 9).ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取30位的随机字符串，含数字和字母
        /// </summary>
        /// <returns></returns>
        public static string GetRandomChar()
        {
            string result = "";
            try
            {
                string all = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
                string[] allChar = all.Split(',');

                Random rand = new Random();
                for (int i = 0; i < 30; i++)
                {
                    result += allChar[rand.Next(60)];
                }
            }
            catch (Exception ex)
            {
                LogHelper.Write(CommonLogger.Application, LogLevel.Error, ex);
            }
            return result;
        }


        #region 手机验证码
        /// <summary>
        /// 发送手机验证码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="mobile"></param>
        /// <returns>返回正确的验证码</returns>
        public static string SendShortMessageCode(string username, string mobile, CheckCodeType checkCodeType)
        {
            var entity = GetCheckCode(username, mobile, checkCodeType);
            string reason = string.Empty;
            switch(checkCodeType)
            {
                case CheckCodeType.RegisterShortMessageCode:
                    reason = "注册新用户";
                    break;
                case CheckCodeType.ResetPasswordShortMessageCode:
                    reason = "找回密码";
                    break;
                case CheckCodeType.SetMobileShortMessageCode:
                    reason = "绑定手机";
                    break;
                case CheckCodeType.ResetPhoneShortMessageCode:
                    reason = "更改绑定手机号";
                    break;
                case CheckCodeType.LoginAdminShortMessageCode:
                    reason = "登陆后台";
                    break;
            }
            WorkPool.Append<ValidateCodeMessageModel>(WorkType.shortmessage_for_validateCode,
                        new ValidateCodeMessageModel()
                        {
                            Code = entity.Code,
                            Reason = reason,
                            Recipient = mobile
                        });
            return entity.Code;
        }

        
        public static bool CheckShortMessageCode(string username, string mobile, string mobileCode, CheckCodeType checkCodeType)
        {
            var code = new CheckCodeStream().Find(c => c.UserName == username && c.Mobile == mobile
             && c.Type == (int)checkCodeType);
            if (code == null || code.ExpireDate < DateTime.Now || code.Code != mobileCode)
            {
                return false;
            }
            return true;
        }
        #endregion

       
        #region 人民币转换
        public static string MoneyToChinese(decimal amount)
        {
            string strAmount = Round(amount, 2).ToString();
            string functionReturnValue = null;
            bool IsNegative = false; // 是否是负数
            if (strAmount.Trim().Substring(0, 1) == "-")
            {
                // 是负数则先转为正数
                strAmount = strAmount.Trim().Remove(0, 1);
                IsNegative = true;
            }
            string strLower = null;
            string strUpart = null;
            string strUpper = null;
            int iTemp = 0;
            // 保留两位小数123.489→123.49　　123.4→123.4

            if (strAmount.IndexOf(".") > 0)
            {
                if (strAmount.IndexOf(".") == strAmount.Length - 2)
                {
                    strAmount = strAmount + "0";
                }
            }
            else
            {
                strAmount = strAmount + ".00";
            }
            strLower = strAmount;
            iTemp = 1;
            strUpper = "";
            while (iTemp <= strLower.Length)
            {
                switch (strLower.Substring(strLower.Length - iTemp, 1))
                {
                    case ".":
                        strUpart = "元";
                        break;
                    case "0":
                        strUpart = "零";
                        break;
                    case "1":
                        strUpart = "壹";
                        break;
                    case "2":
                        strUpart = "贰";
                        break;
                    case "3":
                        strUpart = "叁";
                        break;
                    case "4":
                        strUpart = "肆";
                        break;
                    case "5":
                        strUpart = "伍";
                        break;
                    case "6":
                        strUpart = "陆";
                        break;
                    case "7":
                        strUpart = "柒";
                        break;
                    case "8":
                        strUpart = "捌";
                        break;
                    case "9":
                        strUpart = "玖";
                        break;
                }

                switch (iTemp)
                {
                    case 1:
                        strUpart = strUpart + "分";
                        break;
                    case 2:
                        strUpart = strUpart + "角";
                        break;
                    case 3:
                        strUpart = strUpart + "";
                        break;
                    case 4:
                        strUpart = strUpart + "";
                        break;
                    case 5:
                        strUpart = strUpart + "拾";
                        break;
                    case 6:
                        strUpart = strUpart + "佰";
                        break;
                    case 7:
                        strUpart = strUpart + "仟";
                        break;
                    case 8:
                        strUpart = strUpart + "万";
                        break;
                    case 9:
                        strUpart = strUpart + "拾";
                        break;
                    case 10:
                        strUpart = strUpart + "佰";
                        break;
                    case 11:
                        strUpart = strUpart + "仟";
                        break;
                    case 12:
                        strUpart = strUpart + "亿";
                        break;
                    case 13:
                        strUpart = strUpart + "拾";
                        break;
                    case 14:
                        strUpart = strUpart + "佰";
                        break;
                    case 15:
                        strUpart = strUpart + "仟";
                        break;
                    case 16:
                        strUpart = strUpart + "万";
                        break;
                    default:
                        strUpart = strUpart + "";
                        break;
                }
                strUpper = strUpart + strUpper;
                iTemp = iTemp + 1;
            }

            strUpper = strUpper.Replace("零拾", "零");
            strUpper = strUpper.Replace("零佰", "零");
            strUpper = strUpper.Replace("零仟", "零");
            strUpper = strUpper.Replace("零零零", "零");
            strUpper = strUpper.Replace("零零", "零");
            strUpper = strUpper.Replace("零角零分", "整");
            strUpper = strUpper.Replace("零分", "整");
            strUpper = strUpper.Replace("零角", "零");
            strUpper = strUpper.Replace("零亿零万零元", "亿元");
            strUpper = strUpper.Replace("亿零万零元", "亿元");
            strUpper = strUpper.Replace("零亿零万", "亿");
            strUpper = strUpper.Replace("零万零元", "万元");
            strUpper = strUpper.Replace("零亿", "亿");
            strUpper = strUpper.Replace("零万", "万");
            strUpper = strUpper.Replace("零元", "元");
            strUpper = strUpper.Replace("零零", "零");

            // 对壹元以下的金额的处理

            if (strUpper.Substring(0, 1) == "元")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }

            if (strUpper.Substring(0, 1) == "零")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "角")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "分")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "整")
            {
                strUpper = "零元整";
            }
            functionReturnValue = strUpper;

            if (IsNegative == true)
            {
                return "负" + functionReturnValue;
            }
            else
            {
                return functionReturnValue;
            }

        }
        ///<summary>
        /// 实现数据的四舍五入法
        ///</summary>
        ///<param name="v">要进行处理的数据</param>
        ///<param name="x">保留的小数位数</param>
        ///<returns>四舍五入后的结果</returns>
        public static decimal Round(decimal v = 0m, int x = 2)
        {
            bool isNegative = false;
            //如果是负数
            if (v < 0)
            {
                isNegative = true;
                v = -v;
            }

            int IValue = 1;
            for (int i = 1; i <= x; i++)
            {
                IValue = IValue * 10;
            }
            decimal Int = Math.Floor(v * IValue + 0.5m);
            v = Int / IValue;

            if (isNegative)
            {
                v = -v;
            }

            return v;
        }
        #endregion

       
        public static string CodeGeneration(int length, Func<string, bool> check)
        {
            string nCode = string.Empty;
            do
            {
                StringBuilder sb = new StringBuilder();
                int s = 0;
                for (int i = 0; i < length; i++)
                {
                    s = GetRandom(0, 10);
                    if (s % 2 == 0)
                    {
                        s = GetRandom(97, 122);
                        sb.Append((char)s);
                    }
                    else
                    {
                        sb.Append(GetRandom(0, 9));
                    }
                }
                nCode = sb.ToString();
            }
            while (check.Invoke(nCode));
            return nCode.ToUpper();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public static string GenerateOrderCode(string orderType)
        {
            int no = Interlocked.Increment(ref Sequence) % 999;
            return String.Format("{0}{1}{2}", orderType,
                DateTime.Now.ToString("yyMMddHHmmssfff"),
                no.ToString().PadLeft(3, '0'));
        }

       
    }
}
