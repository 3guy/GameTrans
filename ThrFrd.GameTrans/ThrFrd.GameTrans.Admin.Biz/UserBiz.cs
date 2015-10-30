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
    public class UserBiz
    {
        #region 用户基本信息
        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public static List<User> GetUserList()
        {
            try
            {
                var loginUser = new User().Find(System.Web.HttpContext.Current.User.Identity.Name);
                if (loginUser.Status == ItemStatus.Supper)
                {
                    var userList = new User().FindAll(c => c.Status != (int)ItemStatus.Delete);
                    return userList;
                }
                else
                {
                    var userList = new User().FindAll(c => c.Status != (int)ItemStatus.Delete && c.Status != (int)ItemStatus.Supper);
                    return userList;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Write(CommonLogger.Web, ex);
            }
            return null;
        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public static List<User> GetActiveUserList(string key)
        {
            try
            {
                var loginUser = new User().Find(System.Web.HttpContext.Current.User.Identity.Name);
                using (Context ctx = new Context())
                {
                    if (loginUser.Status == ItemStatus.Supper)
                    {
                        var userList = (from c in ctx.User
                                        where (c.Status == (int)ItemStatus.Enable || c.Status == (int)ItemStatus.Supper)
                                        && (c.RealName.Contains(key) || c.UserName.Contains(key))
                                        select new { c.UserName, c.RealName })
                                  .ToList()
                                  .Select(c => new User() { UserName = c.UserName, RealName = c.RealName }).ToList();

                        return userList;
                    }
                    else
                    {
                        var userList = (from c in ctx.User
                                        where c.Status == (int)ItemStatus.Enable
                                         && (c.RealName.Contains(key) || c.UserName.Contains(key))
                                        select new { c.UserName, c.RealName })
                            .ToList()
                            .Select(c => new User() { UserName = c.UserName, RealName = c.RealName }).ToList();

                        return userList;

                    }
                }

            }
            catch (Exception ex)
            {
                LogHelper.Write(CommonLogger.Web, ex);
            }
            return null;
        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public static ItemListBox<User> GetUserListByPage(int page, int pageSize, string key, ref int count)
        {
            try
            {
                using (Context ctx = new Context())
                {
                    var loginUser = new User().Find(System.Web.HttpContext.Current.User.Identity.Name);
                    var expression = from c in ctx.User
                                     where (string.IsNullOrEmpty(key) || (c.UserName.Contains(key) || c.RealName.Contains(key))) && c.Status != (int)ItemStatus.Delete && c.Status != (int)ItemStatus.Supper
                                     orderby c.UserName
                                     select c;
                    if (loginUser.Status == ItemStatus.Supper)
                    {
                        expression = from c in ctx.User
                                     where (string.IsNullOrEmpty(key) || (c.UserName.Contains(key) || c.RealName.Contains(key))) && c.Status != (int)ItemStatus.Delete
                                     orderby c.UserName
                                     select c;
                    }

                    count = expression.Count();
                    var data = expression
                        .Skip(page * pageSize)
                        .Take(pageSize)
                        .ToList().Select(u => new User().Set(u)).ToList();

                    return new ItemListBox<User>(data).BuildPage(count, page, pageSize, new PageParameter() { Style = "admin" });

                }
            }
            catch (Exception ex)
            {
                LogHelper.Write(CommonLogger.Web, ex);
            }
            return null;
        }

        public static User GetUserByUserName(string userName)
        {
            try
            {
                return new User().Find(userName);
            }
            catch (Exception ex)
            {
                LogHelper.Write(CommonLogger.Web, ex);
            }
            return null;
        }

        public static bool UserExsit(string userName)
        {
            bool isTrue = false;
            try
            {
                var user = new User().Find(c => c.UserName == userName);
                if (user != null)
                {
                    isTrue = true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Write(CommonLogger.Web, ex);
            }
            return isTrue;
        }


        public static User AddUser(string userName, string realName, string telePhone, string handPhone, string createUser)
        {
            try
            {
                var user = new User() { UserName = userName, RealName = realName, TelePhone = telePhone, HandPhone = handPhone, Status = ItemStatus.Enable, CreateUser = createUser, CreateTime = DateTime.Now };
                user.Password = AesAlgorithm.Encrypt(userName);
                user = user.PostAdd();
                return user;
            }
            catch (Exception ex)
            {
                LogHelper.Write(CommonLogger.Web, ex);
            }
            return null;
        }

        public static User EditUser(string userName, string realName, string telePhone, string handPhone)
        {
            try
            {
                var user = new User().Find(userName);
                user.RealName = realName;
                user.TelePhone = telePhone;
                user.HandPhone = handPhone;
                //user.Password = AesAlgorithm.Encrypt(userName);
                user = user.PostModify();
                return user;
            }
            catch (Exception ex)
            {
                LogHelper.Write(CommonLogger.Web, ex);
            }
            return null;
        }

        public static User DeleteUser(string userName)
        {
            try
            {
                var user = new User().Find(userName);
                user.Status = ItemStatus.Delete;
                //user.Password = AesAlgorithm.Encrypt(userName);
                user = user.PostModify();
                UserModuleBiz.UserAuthorizeDelete(userName);
                return user;
            }
            catch (Exception ex)
            {
                LogHelper.Write(CommonLogger.Web, ex);
            }
            return null;
        }

        public static User ChangeStatus(string userName, int status)
        {
            try
            {
                var user = new User().Find(userName);
                user.Status = (ItemStatus)status;
                user = user.PostModify();
                if (status == -1)
                {
                    UserModuleBiz.UserAuthorizeDelete(userName);
                }
                return user;
            }
            catch (Exception ex)
            {
                LogHelper.Write(CommonLogger.Web, ex);
            }
            return null;
        }

        public static User UserLogin(string userName, string password, ref int msgId)
        {
            try
            {
                var user = new User().Find(c => c.UserName == userName.Trim());
                if (user != null)
                {
                    if (user.Status == ItemStatus.Enable || user.Status == ItemStatus.Supper)
                    {
                        if (user.Password == AesAlgorithm.Encrypt(password))
                        {
                            msgId = 1;
                        }
                        else
                        {
                            msgId = -3;//密码错误
                        }
                    }
                    else
                    {
                        msgId = -2;//用户名被禁用
                    }
                }
                else
                {
                    msgId = -1;//用户名不存在
                }
                return user;
            }
            catch (Exception ex)
            {
                LogHelper.Write(CommonLogger.Web, ex);
            }
            return null;
        }
        #endregion

        #region 用户授权

        #endregion
    }
}
