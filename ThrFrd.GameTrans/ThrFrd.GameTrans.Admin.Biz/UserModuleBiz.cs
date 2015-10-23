using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ThrFrd.GameTrans.Infrastructure.Entities.EFContext;
using ThrFrd.GameTrans.Infrastructure.Entities.Entity;
using ThrFrd.GameTrans.Tool;
using EntityFramework.Extensions;

namespace ThrFrd.GameTrans.Admin.Biz
{
    public class UserModuleBiz
    {
        public static List<Module> GetUserModuleByUserName(string userName)
        {
            try
            {
                using (Context ctx = new Context())
                {
                    var data = (from c in ctx.Module
                                join di in ctx.UserModule.Where(f => f.UserName == userName)
                                 on c.ID equals di.ModuleID into dt
                                from d in dt.DefaultIfEmpty()
                                where (c.Level == 1 || c.Level == 2)
                                select new { c, d })
                                    .ToList()
                                    .Select(u =>
                                    {
                                        var module = new Module();
                                        module.Set(u.c);
                                        if (u.d == null || string.IsNullOrEmpty(u.d.UserName))
                                        {
                                            module.IsSelect = false;
                                        }
                                        else
                                        {
                                            module.IsSelect = true;
                                        }
                                        return module;
                                    })
                                    .ToList();

                    return data;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Write(CommonLogger.Web, ex);
            }
            return null;
        }

        public static bool UserAuthorizeDelete(string userName)
        {
            bool isTrue = true;
            try
            {
                using (Context ctx = new Context())
                {
                    int count = ctx.UserModule.Count(c => c.UserName == userName);
                    if (count == 0)
                    {
                        return true;
                    }
                    int row = ctx.UserModule.Where(c => c.UserName == userName).Delete();

                    if (row > 0)
                    {
                        isTrue = true;
                    }
                    else
                    {
                        isTrue = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Write(CommonLogger.Web, ex);
                isTrue = false;
            }
            return isTrue;
        }

        public static bool UserAuthorize(string userName, string moduleIDs)
        {
            bool isTrue = true;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var moduleId in moduleIDs.Split(','))
                    {
                        var roleModule = new UserModule()
                        {
                            UserName = userName,
                            ModuleID = Convert.ToInt32(moduleId)
                        };
                        roleModule = roleModule.PostAdd();
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Write(CommonLogger.Web, ex);
                isTrue = false;
            }
            return isTrue;
        }

    }
}
