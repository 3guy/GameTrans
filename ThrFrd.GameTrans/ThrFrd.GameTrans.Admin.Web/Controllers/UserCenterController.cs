using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThrFrd.GameTrans.Admin.Biz;
using ThrFrd.GameTrans.Admin.WebLogic;
using ThrFrd.GameTrans.Tool;
using ThrFrd.GameTrans.Infrastructure.Entities.Entity;

namespace ThrFrd.GameTrans.Admin.Web.Controllers
{
    [Auth(Order = 1)]
    [RoleFilter(Order = 2)]
    [ErrorHandle]
    public class UserCenterController : BaseController
    {
        //
        // GET: /UserCenter/

        public ActionResult Index(int page=0, int pageSize=12, string key="")
        {
            // 获取用户列表
            int count = 0;
            var userList = UserBiz.GetUserListByPage(page, pageSize, key, ref count);
            ViewBag.userlist = userList;
            return View();
        }

        [HttpPost]
        public ActionResult UserSave(string userName, string realName, string telePhone, string handPhone, int optype)
        {
            return Content("1");
        }


        [HttpPost]
        public ActionResult UserChangeStatus(string userName, int status)
        {
            return Content("1");

        }

        /// <summary>
        /// 用户授权
        /// </summary>
        /// <returns></returns>
        public ActionResult UserAuthorize(string userName)
        {
            ViewBag.userModule = UserModuleBiz.GetUserModuleByUserName(userName);
            ViewBag.User = new User().Find(userName);
            return View();
        }

        [HttpPost]
        public int UserModuleAuthorize(string userName, string moduleIds = "")
        {
            if (moduleIds == "")
            {
                if (UserModuleBiz.UserAuthorizeDelete(userName))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            if (UserModuleBiz.UserAuthorizeDelete(userName))
            {
                UserModuleBiz.UserAuthorize(userName, moduleIds);
                return 1;
            }
            return 0;
        }

        public ActionResult GetActiveUser(string key = "")
        {
            return Content("1");
        } 
    }
}
