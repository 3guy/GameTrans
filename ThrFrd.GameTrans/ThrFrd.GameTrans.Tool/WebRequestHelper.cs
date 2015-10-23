using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ThrFrd.GameTrans.Tool
{
    public class WebRequestHelper
    {
        public static string DoPostRequest(string Url, SortedDictionary<string,string> paramData, Encoding dataEncode)
        {
            string ret = string.Empty;
            Stream stream = null;
            HttpWebResponse response = null;
            StreamReader sr = null;
            try
            {
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(Url);
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                if (paramData.Any())
                {
                    StringBuilder parms = new StringBuilder();
                    foreach (var item in paramData)
                    {
                        parms.Append(String.Format("{0}={1}&", item.Key, item.Value));
                    }
                    //LogHelper.Write(CommonLogger.HuiFu, LogLevel.Trace,"请求参数:"+parms.ToString());
                    byte[] byteArray = dataEncode.GetBytes(parms.ToString().Substring(0, parms.ToString().Length - 1));
                    webReq.ContentLength = byteArray.Length;
                    stream = webReq.GetRequestStream();
                    stream.Write(byteArray, 0, byteArray.Length);
                    stream.Close();
                }
                response = (HttpWebResponse)webReq.GetResponse();
                sr = new StreamReader(response.GetResponseStream(), dataEncode);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                LogHelper.Write(CommonLogger.Application, LogLevel.Error, ex);
                if (stream != null)
                {
                    stream.Close();
                }
                if (sr != null)
                {
                    sr.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                throw ex;
            }
            return ret;
        }
    }
}
