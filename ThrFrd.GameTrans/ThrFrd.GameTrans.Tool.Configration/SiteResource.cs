using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;

namespace GoodMan.Tool.Configration
{
    public class SiteResource
    {
        static int costrank=100;
        static Resource resource = null;
        static SiteResource()
        {
            XElement xe = XElement.Load(AppDomain.CurrentDomain.SetupInformation.ApplicationBase+"/content/config/sitesetting.xml");
            string costrankSet = ConfigurationManager.AppSettings["costrank"];
            if (!string.IsNullOrEmpty(costrankSet))
            {
                Int32.TryParse(costrankSet, out costrank);
            }
            resource = new Resource()
            {
                Host = new Resource_Host()
                {
                    Domain = xe.Element("host").Element("domain").Value,
                    SiteName = xe.Element("host").Element("name").Value
                },
                Scope = xe.Element("host").Element("scope").Elements()
                        .ToDictionary(
                            xn=>{ return xn.Attribute("domain").Value;},
                            xn => { return xn.Attribute("name").Value; }),
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
                            item.keywords = xn.Attribute("keyword").Value;
                            item.description = xn.Attribute("description").Value;
                            return item;
                        })
                },
                Comminession = xe.Element("commission").Elements("item").Select(
                    c => new CommissionItem()
                    {
                        Name = c.Attribute("name").Value,
                        Order = Int32.Parse(c.Attribute("order").Value),
                        point = decimal.Parse(c.Attribute("point").Value),
                        value = c.Attribute("value").Value
                    }).ToList(),
                Articles = new Articles()
                {
                    AboutUs = new ArticleGroup()
                    {
                        Title = xe.Element("articles").Elements("group").FirstOrDefault(c => c.Attribute("key").Value == "4").Attribute("title").Value,
                        Items = xe.Element("articles").Elements("group").FirstOrDefault(c => c.Attribute("key").Value == "4").Elements("item")
                            .Select(c => new ArticleItem()
                            {
                                Id = c.Attribute("id").Value,
                                Title = c.Attribute("name").Value,
                                Content = c.Attribute("file").Value
                            }).ToList()
                    },
                    Cooperation = new ArticleGroup()
                    {
                        Title = xe.Element("articles").Elements("group").FirstOrDefault(c => c.Attribute("key").Value == "3").Attribute("title").Value,
                        Items = xe.Element("articles").Elements("group").FirstOrDefault(c => c.Attribute("key").Value == "3").Elements("item")
                            .Select(c => new ArticleItem()
                            {
                                Id = c.Attribute("id").Value,
                                Title = c.Attribute("name").Value,
                                Content = c.Attribute("file").Value
                            }).ToList()
                    },
                    ShoppingGuide = new ArticleGroup()
                    {
                        Title = xe.Element("articles").Elements("group").FirstOrDefault(c => c.Attribute("key").Value == "2").Attribute("title").Value,
                        Items = xe.Element("articles").Elements("group").FirstOrDefault(c => c.Attribute("key").Value == "2").Elements("item")
                            .Select(c => new ArticleItem()
                            {
                                Id = c.Attribute("id").Value,
                                Title = c.Attribute("name").Value,
                                Content = c.Attribute("file").Value
                            }).ToList()
                    },
                    Delivery = new ArticleGroup()
                    {
                        Title = xe.Element("articles").Elements("group").FirstOrDefault(c => c.Attribute("key").Value == "1").Attribute("title").Value,
                        Items = xe.Element("articles").Elements("group").FirstOrDefault(c => c.Attribute("key").Value == "1").Elements("item")
                            .Select(c => new ArticleItem()
                            {
                                Id = c.Attribute("id").Value,
                                Title = c.Attribute("name").Value,
                                Content = c.Attribute("file").Value
                            }).ToList()
                    }
                },
                Block = xe.Element("advertisement").Elements("block")
                    .Select(c => new BlockGroup()
                    {
                        Key = c.Attribute("key").Value,
                        Items = c.Elements("item").Select(
                            e => new BlockItem()
                            {
                                alt = e.Attribute("alt").Value,
                                description = e.Attribute("description").Value,
                                href = e.Attribute("href").Value,
                                img = e.Attribute("img").Value,
                                price = e.Element("price") == null ? "" : e.Element("price").Value,
                                product = e.Element("product") == null ? "" : e.Element("product").Value,
                            }).ToList()
                    }).ToList()
            };

            #region LoadArticleContent
            resource.Articles.AboutUs.Items.ForEach(
                c => {
                    try
                    {
                        string filePath = c.Content;
                        if (File.Exists(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + filePath))
                        {
                            using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + filePath))
                            {
                                c.Content = sr.ReadToEnd();
                            }
                        }
                    }
                    catch
                    {
                        c.Content = "";
                    }
                });
            resource.Articles.Cooperation.Items.ForEach(
                c =>
                {
                    try
                    {
                        string filePath = c.Content;
                        if (File.Exists(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + filePath))
                        {
                            using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + filePath))
                            {
                                c.Content = sr.ReadToEnd();
                            }
                        }
                    }
                    catch
                    {
                        c.Content = "";
                    }
                });
            resource.Articles.Delivery.Items.ForEach(
                c =>
                {
                    try
                    {
                        string filePath = c.Content;
                        if (File.Exists(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + filePath))
                        {
                            using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + filePath))
                            {
                                c.Content = sr.ReadToEnd();
                            }
                        }
                    }
                    catch
                    {
                        c.Content = "";
                    }
                });
            resource.Articles.ShoppingGuide.Items.ForEach(
                c =>
                {
                    try
                    {
                        string filePath = c.Content;
                        if (File.Exists(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + filePath))
                        {
                            using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + filePath))
                            {
                                c.Content = sr.ReadToEnd();
                            }
                        }
                    }
                    catch
                    {
                        c.Content = "";
                    }
                });
            #endregion
        }

        #region 字段
        public static List<CommissionItem> Commissions
        {
            get
            {
                return resource.Comminession;
            }
        }
        public static string Url
        {
            get
            {
                if (Domain == "127.0.0.1" || Domain == "112.124.48.184")
                {
                    return Domain;
                }
                else
                {
                    return CurrentScope + "." + Domain;
                }
            } 
        }
        public static string Domain
        {
            get
            {
                return resource.Host.Domain;
            }
        }
        public static string SiteName
        {
            get
            {
                return resource.Host.SiteName;
            }
        }
        public static string CurrentScope
        {
            get
            {
                var host = HttpContext.Current.Request.Url.Host;
                if (new Regex("([A-Za-z0-9\\-]+\\.){2}com").IsMatch(host))
                { 
                    return  host.Substring(0, host.IndexOf('.'));
                }
                return "ctbu";
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
                    dict.Clear();
                    dict.Add("title",item.Value.title);
                    dict.Add("keywords", item.Value.keywords);
                    dict.Add("description", item.Value.description);
                    result.Add(item.Key, dict);
                }
                return result;
            }
        }
        public static Articles SiteArticles
        {
            get
            {
                return resource.Articles;
            }
        }
        public static List<BlockGroup> Block
        {
            get
            {
                return resource.Block;
            }
        }
        public static string BussnissQQ { get { return resource.Setting["bussnissQQ"]; } }
        public static string InfoQQ { get { return resource.Setting["infoQQ"]; } }
        public static Dictionary<string, string> DomainScope
        {
            get
            {
                return resource.Scope;
            }
        }
        public static int CostRank
        {
            get
            {
                return costrank;
            }
        }
        #endregion

        #region
        private class Resource
        {
            public Resource_Host Host { get; set; }
            public Dictionary<string, string> Company { get; set; }
            public Dictionary<string, string> Setting { get; set; }
            public Dictionary<string, string> Scope { get; set; }
            public List<CommissionItem> Comminession { get; set; }
            public SEOSettings seo{get;set;}
            public Articles Articles { get; set; }
            public List<BlockGroup> Block { get; set; }
        }
        private class Resource_Host
        {
            /// <summary>
            /// 站点域名 duo500.com
            /// </summary>
            public string Domain { get; set; }
            /// <summary>
            /// 站点名
            /// </summary>
            public string SiteName { get; set; }
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
        public class CommissionItem
        {
            public int Order { get; set; }
            public string Name { get; set; }
            public string value { get; set; }
            public decimal point { get; set; }
        }
        public class Articles
        {
            public ArticleGroup Delivery { get; set; }
            public ArticleGroup ShoppingGuide { get; set; }
            public ArticleGroup Cooperation { get; set; }
            public ArticleGroup AboutUs { get; set; }
        }
        public class ArticleGroup
        {
            public string Title { get; set; }
            public List<ArticleItem> Items { get; set; }
        }
        public class ArticleItem
        {
            public string Title { get; set; }
            public string Id { get; set; }
            public string Content { get; set; }
        }
        public class BlockGroup
        {
            public string Key { get; set; }
            public List<BlockItem> Items { get; set; }
        }
        public class BlockItem
        {
            public string img { get; set; }
            public string href { get; set; }
            public string alt { get; set; }
            public string description { get; set; }
            public string product { get; set; }
            public string price { get; set; }
        }
        #endregion
    }
}
