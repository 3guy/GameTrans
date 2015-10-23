using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThrFrd.GameTrans.Infrastructure.Entities;
using ThrFrd.GameTrans.Infrastructure.Entities.EFContext;
using ThrFrd.GameTrans.Infrastructure.Entities.Entity;
using ThrFrd.GameTrans.Infrastructure.Entities.Enum;
using ThrFrd.GameTrans.Tool;

namespace ThrFrd.GameTrans.Admin.Biz
{
    public class ModuleBiz
    {
        /// <summary>
        /// 获取所有模块信息
        /// </summary>
        /// <returns></returns>
        public static List<Module> GetModelList()
        {
            try
            {
                var moduleList = new Module().FindAll().OrderBy(c => c.Level).ToList();
                List<Module> menuList = null;
                if (moduleList != null && moduleList.Count > 0)
                {
                    menuList = new List<Module>();
                    Module firstMenu = null;
                    foreach (var first in moduleList.Where(c => c.ParentID == 0).ToList())
                    {
                        firstMenu = new Module();
                        firstMenu = first;
                        var secondMenuListTemp = moduleList.Where(c => c.ParentID == first.ID).ToList();
                        if (secondMenuListTemp != null && secondMenuListTemp.Count > 0)
                        {
                            var secondMenuList = new List<Module>();
                            foreach (var second in secondMenuListTemp)
                            {
                                var secondeMenu = new Module();
                                secondeMenu = second;
                                var thirdMenuList = moduleList.Where(c => c.ParentID == second.ID).ToList();
                                secondeMenu.ChildrenList = thirdMenuList;
                                secondMenuList.Add(secondeMenu);
                            }
                            firstMenu.ChildrenList = secondMenuList;
                        }
                        menuList.Add(firstMenu);
                    }
                }
                return menuList;
            }
            catch (Exception ex)
            {
                LogHelper.Write(CommonLogger.Web, ex);
            }
            return null;
        }

        public static List<Module> GetModelListByUser(string userName)
        {
            return iCache.Get<List<Module>>(String.Format("ModuleBiz.GetModelListByUser:{0}", userName),
                () =>
                {
                    using (Context ctx = new Context())
                    {
                        var loginUser = new User().Find(System.Web.HttpContext.Current.User.Identity.Name);
                        if (loginUser == null)
                        {
                            return null;
                        }
                        if (loginUser.Status == ItemStatus.Supper)
                        {
                            var data = new Module().FindAll();
                            data.ForEach(c =>
                            {
                                if (!String.IsNullOrEmpty(c.Url))
                                {
                                    c.Url = c.Url.ToLower();
                                }
                            });
                            return data;
                        }
                        else
                        {
                            var data = (from c in ctx.Module
                                        join d in ctx.UserModule
                                        on c.ID equals d.ModuleID
                                        where d.UserName == userName
                                        select new { c }).Distinct()
                                    .ToList()
                                    .Select(e =>
                                    {
                                        var module = new Module().Set(e.c);

                                        module.Url = module.Url == null ? "" : module.Url.ToLower();
                                        return module;
                                    })
                                        .ToList();
                            if (data != null && data.Count > 0)
                            {
                                var list = new List<Module>();
                                foreach (var item in data)
                                {
                                    list.Add(item);
                                    if (item.Level == 2)
                                    {
                                        var thirdlist = new Module().FindAll(c => c.ParentID == item.ID);
                                        list.AddRange(thirdlist);
                                    }
                                }

                                return list;
                            }
                            else
                            {
                                return data;
                            }
                        }
                    }
                }, 600);
        }

        public static List<Module> GetModelListByUserForMenu(string userName)
        {
            try
            {
                using (Context ctx = new Context())
                {
                    var loginUser = new User().Find(System.Web.HttpContext.Current.User.Identity.Name);
                    if (loginUser == null)
                    {
                        return null;
                    }
                    if (loginUser.Status == ItemStatus.Supper)
                    {
                        var data = new Module().FindAll(c => c.IsDisplay == true && (c.Level == 1 || c.Level == 2));
                        data.ForEach(c =>
                        {
                            if (!String.IsNullOrEmpty(c.Url))
                            {
                                c.Url = c.Url.ToLower();
                            }
                        });
                        return data;
                    }
                    else
                    {
                        var data = (from c in ctx.Module
                                    join d in ctx.UserModule
                                    on c.ID equals d.ModuleID
                                    where d.UserName == userName && c.IsDisplay == true
                                     && (c.Level == 1 || c.Level == 2)
                                    select new { c }).Distinct()
                                    .ToList()
                                    .Select(e =>
                                    {
                                        var module = new Module().Set(e.c);

                                        module.Url = module.Url == null ? "" : module.Url.ToLower();
                                        return module;
                                    }).ToList();
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Write(CommonLogger.Web, ex);
            }
            return null;
        }
    }
}
