using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThrFrd.GameTrans.Infrastructure.Entities
{
    public class ItemListBox<T> where T : class
    {
        public ItemListBox() { }
        public ItemListBox(List<T> list)
        {
            if (list == null || list.Count(c => c != null) == 0)
            {
                this.Data = new List<T>();
            }
            else
            {
                this.Data = list;
            }
        }
        public List<T> Data { get; set; }
        public int ItemTotle { get; set; }
        public int TotlePage { get; set; }
        public int CurrentPage { get; set; }
        public int PrePage { get; set; }
        public int NextPage { get; set; }
        public string PageItemSegment { get; set; }

        private ItemListBox<T> BuildPage(int itemCount, int currentPage, int pageSize)
        {
            if (itemCount <= 0)
            {
                ItemTotle = 0;
                TotlePage = 0;
                CurrentPage = 0;
                PrePage = 0;
                NextPage = 0;
                return this;
            }
            ItemTotle = itemCount;
            this.TotlePage = itemCount % pageSize == 0 ? itemCount / pageSize : itemCount / pageSize + 1;
            if (this.TotlePage > 0)
            {
                this.CurrentPage = this.CurrentPage < 0 ? 0 : this.CurrentPage;
                this.CurrentPage = this.CurrentPage > this.TotlePage - 1 ? this.TotlePage - 1 : currentPage;
            }
            else
            {
                this.CurrentPage = 0;
            }
            this.PrePage = this.CurrentPage == 0 ? 0 : this.CurrentPage - 1;
            this.NextPage = this.CurrentPage == this.TotlePage - 1 ? this.TotlePage - 1 : this.CurrentPage + 1;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemCount"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="hrefisscript"></param>
        /// <param name="scriptfm">如果href是javascript方法，则这里是方法的format串</param>
        /// <param name="param_page">如果href不是javascript方法，则这里是page的查询参数</param>
        /// <returns></returns>
        public ItemListBox<T> BuildPage(int itemCount, int currentPage, int pageSize, PageParameter setting=null)
        {
            if (setting == null)
            {
                setting = new PageParameter() { Style = "Home" };
            }
            string requestPath = System.Web.HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Substring(2) + System.Web.HttpContext.Current.Request.PathInfo;
            BuildPage(itemCount, currentPage, pageSize);
            string urlformat = string.Empty;
            string rawurl = System.Web.HttpContext.Current.Request.RawUrl;
            if (!setting.IsActive)
            {
                if (rawurl.IndexOf('?') >= 0)
                {
                    if (rawurl.IndexOf(setting.Param_page + "=" + currentPage.ToString()) > 0)
                    {
                        urlformat = rawurl.Replace(setting.Param_page + "=" + currentPage.ToString(), setting.Param_page + "={0}");
                    }
                    else
                    {
                        urlformat = rawurl + "&" + setting.Param_page + "={0}";
                    }
                }
                else
                {
                    if (rawurl.IndexOf(setting.Param_page + "=" + currentPage.ToString()) > 0)
                    {
                        urlformat = rawurl.Replace(setting.Param_page + "=" + currentPage.ToString(), setting.Param_page + "={0}");
                    }
                    else
                    {
                        urlformat = rawurl + "?" + setting.Param_page + "={0}";
                    }
                }

            }
            else
            {
                urlformat = setting.Script;
            }
            StringBuilder sb = BuildStyle(currentPage, itemCount, setting, urlformat);
            PageItemSegment = sb.ToString();
            return this;
        }

        private StringBuilder BuildStyle(int currentPage, int itemCount, PageParameter setting, string urlformat)
        {
            StringBuilder sb = new StringBuilder();
            switch (setting.Style.ToLower())
            {
                case "workspace":
                    WorkSpaceStyle(currentPage, setting.IsActive, urlformat, sb);
                    break;
                default:
                case "home":
                    HomeStyle(currentPage, setting.IsActive, urlformat, sb);
                    break;
                case "admin":
                    MoFangGoAdminStyle(currentPage, itemCount, setting.IsActive, urlformat, sb);
                    break;
            }
            return sb;
        }

        private void WorkSpaceStyle(int currentPage, bool isActive, string urlformat, StringBuilder sb)
        {
            sb.Append("<div class=\"widget-title\"><div class=\"dataTables_paginate\">");
            if (isActive)
            {
                sb.Append(string.Format("<a class=\"first ui-button\" href=\"javascript:;\" onclick=\"" + urlformat + "\">首页</a>", 0));
                if (currentPage <= 0)
                {
                    sb.Append("<a class=\"previous ui-button ui-state-disabled\" href=\"javascript:;\">上一页</a>");
                }
                else
                {
                    sb.Append(string.Format("<a class=\"previous ui-button\" href=\"javascript:;\" onclick=\"" + urlformat + "\">上一页</a>", 0));
                }
            }
            else
            {
                sb.Append(string.Format("<a class=\"fg-button ui-button\" href=\"" + urlformat + "\">首页</a>", 0));
                if (currentPage <= 0)
                {
                    sb.Append("<a class=\"previous ui-button ui-state-disabled\" href=\"javascript:;\">上一页</a>");
                }
                else
                {
                    sb.Append(string.Format("<a class=\"previous ui-button\" href=\"" + urlformat + "\">上一页</a>", 0));
                }
            }
            if (currentPage <= 5)
            {
                for (int i = 0, len = TotlePage > 10 ? 10 : TotlePage; i < len; i++)
                {
                    if (currentPage == i)
                    {
                        if (isActive)
                        {
                            sb.Append(string.Format("<span><a class=\"fg-button ui-button ui-state-disabled\" href=\"javascript:;\">{0}</a></span>", i + 1));
                        }
                        else
                        {
                            sb.Append(string.Format("<span><a class=\"fg-button ui-button ui-state-disabled\" href=\"javascript:;\">{0}</a></span>", i + 1));
                        }
                    }
                    else
                    {
                        if (isActive)
                        {
                            sb.Append(string.Format("<span><a class=\"fg-button ui-button\" href=\"javascript:;\" onclick=\"" + urlformat + "\">{1}</a></span>", i, i + 1));
                        }
                        else
                        {
                            sb.Append(string.Format("<span><a class=\"fg-button ui-button\" href=\"" + urlformat + "\">{1}</a></span>", i, i + 1));
                        }
                    }
                }
            }
            else
            {
                if (isActive)
                {
                    sb.Append(string.Format("<a class=\"fg-button ui-button ui-state-disabled\" href=\"javascript:;\" onclick=\"" + urlformat + "\">{1}</a>", 0, 1));
                }
                else
                {
                    sb.Append(string.Format("<a class=\"fg-button ui-button ui-state-disabled\" href=\"" + urlformat + "\">{1}</a>", 0, 1));
                }
                sb.Append("<div>...</div>");
                for (int i = currentPage - 3, len = currentPage + 5 > TotlePage ? TotlePage : currentPage + 5;
                    i < len; i++)
                {
                    if (currentPage == i)
                    {
                        if (isActive)
                        {
                            sb.Append(string.Format("<a class=\"fg-button ui-button\"  href=\"javascript:;\" onclick=\"" + urlformat + "\">{1}</a>", i, i + 1));
                        }
                        else
                        {
                            sb.Append(string.Format("<a class=\"fg-button ui-button\" href=\"" + urlformat + "\">{1}</a>", i, i + 1));
                        }
                    }
                    else
                    {
                        if (isActive)
                        {
                            sb.Append(string.Format("<a class=\"fg-button ui-button ui-state-disabled\" href=\"javascript:;\" onclick=\"" + urlformat + "\">{1}</a>", i, i + 1));
                        }
                        else
                        {
                            sb.Append(string.Format("<a class=\"fg-button ui-button ui-state-disabled\" href=\"" + urlformat + "\">{1}</a>", i, i + 1));
                        }
                    }
                }
            }
            if (isActive)
            {
                if (currentPage >= TotlePage - 1)
                {
                    sb.Append("<a class=\"previous ui-button ui-state-disabled\" href=\"javascript:;\">下一页</a>");
                }
                else
                {
                    sb.Append(string.Format("<a class=\"previous ui-button\" href=\"javascript:;\" onclick=\"" + urlformat + "\">下一页</a>", NextPage));
                }

                sb.Append(string.Format("<a class=\"first ui-button\" href=\"javascript:;\" onclick=\"" + urlformat + "\">末页</a>", TotlePage - 1));
            }
            else
            {
                if (currentPage >= TotlePage - 1)
                {
                    sb.Append("<a class=\"previous ui-button ui-state-disabled\" href=\"javascript:;\">下一页</a>");
                }
                else
                {
                    sb.Append(string.Format("<a class=\"previous ui-button\" href=\"" + urlformat + "\">下一页</a>", NextPage));
                }
                sb.Append(string.Format("<a class=\"fg-button ui-button\" href=\"" + urlformat + "\">末页</a>", TotlePage - 1));
            }
            sb.Append("</div></div>");
        }

        private void HomeStyle(int currentPage, bool isActive, string urlformat, StringBuilder sb)
        {
            sb.Append("<nav><ul class=\"pagination\">");
            if (isActive)
            {
                if (currentPage <= 0)
                {
                    sb.Append("<li class=\"disabled\"><a href=\"javascript:;\">&laquo;</a></li>");
                }
                else
                {
                    sb.Append(string.Format("<li><a href=\"javascript:;\" onclick=\"" + urlformat + "\">&laquo;</a></li>", 0));
                }
            }
            else
            {
                if (currentPage <= 0)
                {
                    sb.Append("<li class=\"disabled\"><a href=\"javascript:;\">&laquo;</a></li>");
                }
                else
                {
                    sb.Append(string.Format("<li><a href=\"" + urlformat + "\">&laquo;</a></li>", 0));
                }
            }
            if (currentPage <= 5)
            {
                for (int i = 0, len = TotlePage > 10 ? 10 : TotlePage; i < len; i++)
                {
                    if (currentPage == i)
                    {
                        sb.Append(string.Format("<li class=\"active\"><a href=\"javascript:;\">{0}</a></li>", i + 1));
                    }
                    else
                    {
                        if (isActive)
                        {
                            sb.Append(string.Format("<li><a href=\"javascript:;\" onclick=\"" + urlformat + "\">{1}</a></li>", i, i + 1));
                        }
                        else
                        {
                            sb.Append(string.Format("<li><a href=\"" + urlformat + "\">{1}</a></li>", i, i + 1));
                        }
                    }
                }
            }
            else
            {
                if (isActive)
                {
                    sb.Append(string.Format("<li><a href=\"javascript:;\" onclick=\"" + urlformat + "\">{1}</a></li>", 0, 1));
                }
                else
                {
                    sb.Append(string.Format("<li><a href=\"" + urlformat + "\">{1}</a></li>", 0, 1));
                }
                sb.Append("<div style=\"float:left;padding:5px 12px; border-right:1px solid #ddd;\">...</div>");
                for (int i = currentPage - 3, len = currentPage + 5 > TotlePage ? TotlePage : currentPage + 5;
                    i < len; i++)
                {
                    if (currentPage == i)
                    {
                        if (isActive)
                        {
                            sb.Append(string.Format("<li class=\"active\"><a href=\"javascript:;\" onclick=\"" + urlformat + "\">{1}</a></li>", i, i + 1));
                        }
                        else
                        {
                            sb.Append(string.Format("<li class=\"active\"><a href=\"" + urlformat + "\">{1}</a></li>", i, i + 1));
                        }
                    }
                    else
                    {
                        if (isActive)
                        {
                            sb.Append(string.Format("<li><a href=\"javascript:;\" onclick=\"" + urlformat + "\">{1}</a></li>", i, i + 1));
                        }
                        else
                        {
                            sb.Append(string.Format("<li><a href=\"" + urlformat + "\">{1}</a></li>", i, i + 1));
                        }
                    }
                }
            }
            if (isActive)
            {
                if (currentPage >= TotlePage - 1)
                {
                    sb.Append("<li class=\"disabled\"><a href=\"javascript:;\">&raquo;</a></li>");
                }
                else
                {
                    sb.Append(string.Format("<li><a href=\"javascript:;\" onclick=\"" + urlformat + "\">&raquo;</a></li>", TotlePage - 1));
                }
            }
            else
            {
                if (currentPage >= TotlePage - 1)
                {
                    sb.Append("<li class=\"disabled\"><a href=\"javascript:;\">&raquo;</a>");
                }
                else
                {
                    sb.Append(string.Format("<li><a href=\"" + urlformat + "\">&raquo;</a>", TotlePage - 1));
                }
            }
            sb.Append("</ul></nav>");
        }

        private void MoFangGoAdminStyle(int currentPage, int itemCount, bool isActive, string urlformat, StringBuilder sb)
        {
            sb.Append("<div class=\"widget-title\" style=\"padding-top:3px;\"><div class=\"dataTables_paginate\">");
            if (isActive)
            {
                sb.Append(string.Format("<a class=\"first ui-button\" href=\"javascript:;\" style=\"margin-right:-5px;\" onclick=\"javascript:;\">共<strong><span style=\"color:red;\">" + itemCount + "</span></strong>条</a>", 0));
                sb.Append(string.Format("<a class=\"first ui-button\" href=\"javascript:;\" onclick=\"" + urlformat + "\">首页</a>", 0));
                if (currentPage <= 0)
                {
                    sb.Append("<a class=\"previous ui-button ui-state-disabled\" href=\"javascript:;\">上一页</a>");
                }
                else
                {
                    sb.Append(string.Format("<a class=\"previous ui-button\" href=\"javascript:;\" onclick=\"" + urlformat + "\">上一页</a>", 0));
                }
            }
            else
            {
                sb.Append(string.Format("<a class=\"first ui-button\" href=\"javascript:;\" style=\"margin-right:-5px;\" onclick=\"javascript:;\">共<strong><span style=\"color:red;\">" + itemCount + "</span></strong>条</a>", 0));
                sb.Append(string.Format("<a class=\"fg-button ui-button\" href=\"" + urlformat + "\">首页</a>", 0));
                if (currentPage <= 0)
                {
                    sb.Append("<a class=\"previous ui-button ui-state-disabled\" href=\"javascript:;\">上一页</a>");
                }
                else
                {
                    sb.Append(string.Format("<a class=\"previous ui-button\" href=\"" + urlformat + "\">上一页</a>", 0));
                }
            }
            if (currentPage <= 5)
            {
                for (int i = 0, len = TotlePage > 10 ? 10 : TotlePage; i < len; i++)
                {
                    if (currentPage == i)
                    {
                        if (isActive)
                        {
                            sb.Append(string.Format("<span><a class=\"fg-button ui-button ui-state-disabled\" href=\"javascript:;\">{0}</a></span>", i + 1));
                        }
                        else
                        {
                            sb.Append(string.Format("<span><a class=\"fg-button ui-button ui-state-disabled\" href=\"javascript:;\">{0}</a></span>", i + 1));
                        }
                    }
                    else
                    {
                        if (isActive)
                        {
                            sb.Append(string.Format("<span><a class=\"fg-button ui-button\" href=\"javascript:;\" onclick=\"" + urlformat + "\">{1}</a></span>", i, i + 1));
                        }
                        else
                        {
                            sb.Append(string.Format("<span><a class=\"fg-button ui-button\" href=\"" + urlformat + "\">{1}</a></span>", i, i + 1));
                        }
                    }
                }
            }
            else
            {
                if (isActive)
                {
                    sb.Append(string.Format("<a class=\"fg-button ui-button ui-state-disabled\" href=\"javascript:;\" onclick=\"" + urlformat + "\">{1}</a>", 0, 1));
                }
                else
                {
                    sb.Append(string.Format("<a class=\"fg-button ui-button ui-state-disabled\" href=\"" + urlformat + "\">{1}</a>", 0, 1));
                }
                sb.Append("<div>...</div>");
                for (int i = currentPage - 3, len = currentPage + 5 > TotlePage ? TotlePage : currentPage + 5;
                    i < len; i++)
                {
                    if (currentPage == i)
                    {
                        if (isActive)
                        {
                            sb.Append(string.Format("<a class=\"fg-button ui-button\"  href=\"javascript:;\" onclick=\"" + urlformat + "\">{1}</a>", i, i + 1));
                        }
                        else
                        {
                            sb.Append(string.Format("<a class=\"fg-button ui-button\" href=\"" + urlformat + "\">{1}</a>", i, i + 1));
                        }
                    }
                    else
                    {
                        if (isActive)
                        {
                            sb.Append(string.Format("<a class=\"fg-button ui-button ui-state-disabled\" href=\"javascript:;\" onclick=\"" + urlformat + "\">{1}</a>", i, i + 1));
                        }
                        else
                        {
                            sb.Append(string.Format("<a class=\"fg-button ui-button ui-state-disabled\" href=\"" + urlformat + "\">{1}</a>", i, i + 1));
                        }
                    }
                }
            }
            if (isActive)
            {
                if (currentPage >= TotlePage - 1)
                {
                    sb.Append("<a class=\"previous ui-button ui-state-disabled\" href=\"javascript:;\">下一页</a>");
                }
                else
                {
                    sb.Append(string.Format("<a class=\"previous ui-button\" href=\"javascript:;\" onclick=\"" + urlformat + "\">下一页</a>", NextPage));
                }

                sb.Append(string.Format("<a class=\"first ui-button\" href=\"javascript:;\" onclick=\"" + urlformat + "\">末页</a>", TotlePage - 1));
            }
            else
            {
                if (currentPage >= TotlePage - 1)
                {
                    sb.Append("<a class=\"previous ui-button ui-state-disabled\" href=\"javascript:;\">下一页</a>");
                }
                else
                {
                    sb.Append(string.Format("<a class=\"previous ui-button\" href=\"" + urlformat + "\">下一页</a>", NextPage));
                }
                sb.Append(string.Format("<a class=\"fg-button ui-button\" href=\"" + urlformat + "\">末页</a>", TotlePage - 1));
            }
            sb.Append("</div></div>");
        }
    }
    public class PageParameter
    {
        /// <summary>
        /// 是否是使用onclick进行翻页
        /// </summary>
        public bool IsActive = false;
        /// <summary>
        /// 如果IsActive为true，则用户点击翻页时执行的函数
        /// </summary>
        public string Script = string.Empty;
        /// <summary>
        /// 用于翻页的Querystring参数，默认为"page"
        /// </summary>
        public string Param_page = "page";
        /// <summary>
        /// home:站点样式
        /// workspace: 用户中心
        /// </summary>
        public string Style = "home";
    }
}
