using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using ThrFrd.GameTrans.Tool;
using System.Text.RegularExpressions;
using ThrFrd.GameTrans.Infrastructure.Component.ShortMessageFundamental;

namespace ThrFrd.GameTrans.Infrastructure.Component.ShortMessage
{
    public class ShortMessageHelper
    {
        static Random random = new Random();
        SmsPortTypeClient client = new SmsPortTypeClient();
        const string SpCode = "";
        const string LoginName = "";
        const string Password = "";
        static ShortMessageHelper instance = null;
        private ShortMessageHelper(SmsPortTypeClient smsClient) {
            this.client = smsClient;
        }
        static ShortMessageHelper() 
        {
            instance = new ShortMessageHelper(new SmsPortTypeClient());
        }
        public static ShortMessageHelper Instance
        {
            get
            {
                return instance;
            }
        }

        public void Send(string message, int way = 0, params string[] receive)
        {
            

        }
        private static string doPostRequest(string url, byte[] bData)
        {
            System.Net.HttpWebRequest hwRequest;
            System.Net.HttpWebResponse hwResponse;

            string strResult = string.Empty;
            try
            {
                hwRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                hwRequest.Timeout = 10000;
                hwRequest.Method = "POST";
                hwRequest.ContentType = "application/x-www-form-urlencoded";
                hwRequest.ContentLength = bData.Length;

                System.IO.Stream smWrite = hwRequest.GetRequestStream();
                smWrite.Write(bData, 0, bData.Length);
                smWrite.Close();
            }
            catch (System.Exception err)
            {
                LogHelper.Write(CommonLogger.Application, LogLevel.Error, err);
                return strResult;
            }

            //get response
            try
            {
                hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                StreamReader srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.GetEncoding("GBK"));
                strResult = srReader.ReadToEnd();
                srReader.Close();
                hwResponse.Close();
            }
            catch (System.Exception err)
            {
                LogHelper.Write(CommonLogger.Application, LogLevel.Error, err);
            }
            return strResult;
        }

        public int QueryRemain()
        {
            //byte[] msgbyt = Encoding.GetEncoding("GBK").GetBytes(string.Format("uid={0}&pwd={1}", uid, pwd));
            //String postReturn = doPostRequest(queryurl, msgbyt);
            //if (postReturn.Contains("stat=100"))
            //{
            //    return Int32.Parse(postReturn.Substring(postReturn.LastIndexOf("=") + 1));
            //}
            return 1;
        }

        public Reply[] Receive(out string lastId)
        {
            //Dictionary<string, string> param = new Dictionary<string, string>();
            //param.Add("SpCode", SpCode);
            //param.Add("LoginName", LoginName);
            //param.Add("Password", Password);
            //var result = WebRequestHelper.DoPostRequest(receiveurl, param, Encoding.GetEncoding("GB2312"));
            string confirm_time = string.Empty;
            Reply[] replys = null;
            var result = client.Reply(SpCode, LoginName, Password, string.Empty,
                out confirm_time, out lastId, out replys);
            if (result == "0")
            {
                return replys;
            }
            return null;
        }
        public void ReplyConfirm(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                client.ReplyConfirm(SpCode, LoginName, Password, "", id);
            }
        }
    }
}
