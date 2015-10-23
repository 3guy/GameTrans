using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ThrFrd.GameTrans.Infrastructure.Configuration
{
    public static class OperationTraceSettings
    {
        public static Dictionary<string, TracelItem> Dict { get; set; }
        static OperationTraceSettings()
        {
            XElement xe = XElement.Load(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/content/config/System-OperationTrace.xml");
            Dict = xe.Elements("item").ToDictionary(
                xn => xn.Attribute("url").Value,
                xn => new TracelItem()
                {
                    Level = xn.Attribute("level").Value,
                    Log = bool.Parse(xn.Attribute("log").Value),
                    Method = xn.Attribute("method").Value,
                    Url = xn.Attribute("url").Value,
                    Format = new TraceFormat()
                    {
                        Templete = xn.Element("templete").Value,
                        paramList = xn.Element("parameter").Elements("add").Select(c => c.Value).ToList()
                    },
                    OperateType = int.Parse(xn.Element("operateType").Value)
                });
        }
    }
    public class TracelItem
    {
        public string Url { get; set; }
        public string Method { get; set; }
        public string Level { get; set; }
        public bool Log { get; set; }
        public int OperateType { get; set; }
        public TraceFormat Format { get; set; }
    }
    public class TraceFormat
    {
        public string Templete { get; set; }
        public List<string> paramList { get; set; }
    }
}
