using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThrFrd.GameTrans.Admin.Biz;
using ThrFrd.GameTrans.Tool;
using ThrFrd.GameTrans.Admin.WebLogic;
using EntityFramework.Extensions;
using ThrFrd.GameTrans.Admin.Web.Models;
using ThrFrd.GameTrans.Infrastructure.Entities.EFContext;
using ThrFrd.GameTrans.Infrastructure.Entities.Entity;

namespace ThrFrd.GameTrans.Admin.Web.Controllers
{
    [Auth(Order = 1)]
    [RoleFilter(Order = 2)]
    [ErrorHandle]
    public class SystemController : BaseController
    {
        //
        // GET: /System/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult SystemMenu()
        {
            var allmoduleList = ModuleBiz.GetModelList();
            ViewBag.allmoduleList = allmoduleList;
            return View();
        }

        [HttpPost]
        public ActionResult ModuleSave(int moduleID, string moduleName, int level, string url, int parentID, string icon, int isDisplay, int seqNo, string code)
        {
            try
            {
                if (moduleID == 0)
                {
                    var module = new Module();
                    module.Name = moduleName;
                    module.Level = level;
                    module.Url = url;
                    module.ParentID = parentID;
                    module.Icon = icon;
                    module.SeqNo = seqNo;
                    module.Code = code;
                    module.IsDisplay = isDisplay == 0 ? false : true;
                    module = module.PostAdd();
                    if (module == null)
                    {
                        return Content("0");
                    }
                    else
                    {
                        return Content("1");
                    }

                }
                else
                {
                    var module = new Module().Find(moduleID.ToString());
                    module.Name = moduleName;
                    module.Level = level;
                    module.Url = url;
                    module.ParentID = parentID;
                    module.Icon = icon;
                    module.SeqNo = seqNo;
                    module.Code = code;
                    module.IsDisplay = isDisplay == 0 ? false : true;
                    module = module.PostModify();
                    if (module == null)
                    {
                        return Content("0");
                    }
                    else
                    {
                        return Content("1");
                    }

                }
            }
            catch (Exception ex)
            {

                LogHelper.Write(CommonLogger.Web, ex);
            }

            return Content("0");
        }
        [HttpPost]
        public ActionResult ModuleDelete(int moduleID)
        {
            var module = new Module().Find(moduleID.ToString());
            if (module.Level == 3)
            {
                module.PostDelete();
            }
            else if (module.Level == 2)
            {
                using (Context ctx = new Context())
                    ctx.Module.Where(c => c.ParentID == moduleID).Delete();
                //ctx.Delete<U_Module>()
                //        .WhereSet(c => c.ParentID, WhereOperator.Equal, moduleID)
                //        .End()
                //    
                module.PostDelete();

            }
            else if (module.Level == 1)
            {
                var chidren = new Module().FindAll(c => c.ParentID == moduleID);
                using (Context ctx = new Context())
                {
                    if (chidren != null && chidren.Count > 0)
                    {
                        foreach (var second in chidren)
                        {
                            ctx.Module.Where(c => c.ParentID == second.ID).Delete();
                            //ctx.Delete<U_Module>()
                            //        .WhereSet(c => c.ParentID, WhereOperator.Equal, second.ID)
                            //        .End()
                            //        .Excute();
                        }
                    }
                    ctx.Module.Where(c => c.ParentID == moduleID).Delete();
                    //ctx.Delete<U_Module>()
                    //        .WhereSet(c => c.ParentID, WhereOperator.Equal, moduleID)
                    //        .End()
                    //        .Excute();

                    module.PostDelete();
                }
            }
            return Content("1");
        }

        /// <summary>
        /// 平台资金流水
        /// </summary>
        /// <returns></returns>
        public ActionResult PlatformFinance()
        {
            return View();
        }

    }
}
