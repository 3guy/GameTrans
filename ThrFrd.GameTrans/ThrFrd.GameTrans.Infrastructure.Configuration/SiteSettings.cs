using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Configuration;

namespace ThrFrd.GameTrans.Infrastructure.Configuration
{
    public class SiteSettings
    {
        static Resource resource = null;
        static SiteSettings()
        {
            XElement xe = XElement.Load(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/content/config/sitesetting.xml");

            resource = new Resource()
            {
                Host = new Resource_Host()
                {
#if DEBUG
                    Domain_Portal = xe.Element("host").Element("domain")
                        .Element("portal").Element("test").Value,
                    Domain_Admin = xe.Element("host").Element("domain")
                        .Element("admin").Element("test").Value,
#else
                    Domain_Portal = xe.Element("host").Element("domain")
                        .Element("portal").Element("standard").Value,
                    Domain_Admin = xe.Element("host").Element("domain")
                        .Element("admin").Element("standard").Value,
#endif
                    IP_Admin = xe.Element("host").Element("domain")
                        .Element("admin").Element("ip").Value,
                    IP_Portal = xe.Element("host").Element("domain")
                        .Element("portal").Element("ip").Value,
                    SiteName = xe.Element("host").Element("name").Value
                },
                Setting = xe.Element("host").Element("setting").Elements()
                        .ToDictionary(
                            xn => { return xn.Name.ToString(); },
                            xn => { return xn.Value; }),
                Company = xe.Element("host").Element("company").Elements()
                    .ToDictionary(
                        xn => { return xn.Name.ToString(); },
                        xn => { return xn.Value; }),
                seo = new SEOSettings()
                {
                    Default = new SEOSettingItem()
                    {
                        title = xe.Element("seo").Element("default").Attribute("title").Value,
                        keywords = xe.Element("seo").Element("default").Attribute("keyword").Value,
                        description = xe.Element("seo").Element("default").Attribute("description").Value
                    },
                    PageSet = xe.Element("seo").Elements("item")
                    .ToDictionary(
                        xn =>
                        {
                            return xn.Attribute("name").Value;
                        },
                        xn =>
                        {
                            SEOSettingItem item = new SEOSettingItem();
                            item.title = xn.Attribute("title").Value;
                            if (xn.Attribute("keyword") != null)
                            {
                                item.keywords = xn.Attribute("keyword").Value;
                            }
                            else
                            {
                                item.keywords = string.Empty;
                            }
                            if (xn.Attribute("description") != null)
                            {
                                item.description = xn.Attribute("description").Value;
                            }
                            else
                            {
                                item.description = string.Empty;
                            }
                            return item;
                        })
                }
            };
        }

        #region 字段
        
        public static string RequestType
        {
            get
            {
                return resource.Setting["requestType"];
            }
        }
        public static bool EnableTrace
        {
            get
            {
                bool isTrace =false;
                string trace = resource.Setting["trace"];
                if (String.IsNullOrEmpty(trace))
                {
                    return false;
                }
                Boolean.TryParse(trace, out isTrace);
                return isTrace;
            }
        }
        /// <summary>
        /// 返回域名
        /// </summary>
        public static string Domain_Portal
        {
            get
            {
                return resource.Host.Domain_Portal.Substring(resource.Host.Domain_Portal.IndexOf('.') + 1);
            }
        }
        public static string Domain_Admin
        {
            get
            {
                return resource.Host.Domain_Admin.Substring(resource.Host.Domain_Portal.IndexOf('.') + 1);
            }
        }
        /// <summary>
        /// 返回完整url
        /// </summary>
        public static string Host_Portal
        {
            get
            {
                return resource.Host.Domain_Portal;
            }
        }
        public static string IP_Portal
        {
            get
            {
                return resource.Host.IP_Portal;
            }
        }
        public static string IP_Admin
        {
            get
            {
                return resource.Host.IP_Admin;
            }
        }
        public static string Host_Admin
        {
            get
            {
                return resource.Host.Domain_Admin;
            }
        }
        public static string SiteName
        {
            get
            {
                return resource.Host.SiteName;
            }
        }
        public static string CompanyName
        {
            get
            {
                return resource.Company["name"];
            }
        }
        public static string CompanyAddress
        {
            get
            {
                return resource.Company["address"];
            }
        }
        public static string CompanyPhone
        {
            get
            {
                return resource.Company["phone"];
            }
        }
        public static string CompanyCSEmail
        {
            get
            {
                return resource.Company["CSEmail"];
            }
        }
        public static string CompanyBussinessEmail
        {
            get
            {
                return resource.Company["bussinessEmail"];
            }
        }
        public static object CompanyHotline
        {
            get
            {
                return resource.Company["hotline"];
            }
        }
        public static object CompanyFax
        {
            get
            {
                return resource.Company["fax"];
            }
        }
        public static object CompanyICP
        {
            get
            {
                return resource.Company["icp"];
            }
        }

        public static string Logo
        {
            get
            {
                return resource.Setting["logo"];
            }
        }

        public static Dictionary<string, Dictionary<string, string>> SeoSettings 
        {
            get {
                Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>();
                Dictionary<string,string> dict = new Dictionary<string,string>();
                dict.Add("title",resource.seo.Default.title);
                dict.Add("keywords", resource.seo.Default.keywords);
                dict.Add("description", resource.seo.Default.description);
                result.Add("default", dict);
                foreach (var item in resource.seo.PageSet)
                {
                    dict = new Dictionary<string, string>();
                    dict.Add("title",item.Value.title);
                    dict.Add("keywords", item.Value.keywords);
                    dict.Add("description", item.Value.description);
                    result.Add(item.Key, dict);
                }
                return result;
            }
        }

        #endregion

        #region
        private class Resource
        {
            public Resource_Host Host { get; set; }
            public Dictionary<string, string> Company { get; set; }
            public Dictionary<string, string> Setting { get; set; }
            public SEOSettings seo{get;set;}
        }
        private class Resource_Host
        {
            /// <summary>
            /// 前台域名
            /// </summary>
            public string Domain_Portal { get; set; }
            /// <summary>
            /// 后台域名
            /// </summary>
            public string Domain_Admin { get; set; }
            /// <summary>
            /// 站点名
            /// </summary>
            public string SiteName { get; set; }
            public string IP_Portal { get; set; }
            public string IP_Admin { get; set; }
        }
        private class SEOSettings
        {
            public SEOSettingItem Default { get; set; }
            public Dictionary<string, SEOSettingItem> PageSet { get; set; }
        }
        private class SEOSettingItem
        {
            public string title { get; set; }
            public string keywords { get; set; }
            public string description { get; set; }
        }
        #endregion
    }
}
