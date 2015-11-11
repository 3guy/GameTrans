using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ThrFrd.GameTrans.Admin.Biz;
using ThrFrd.GameTrans.Admin.Web.Models;
using ThrFrd.GameTrans.Admin.WebLogic;
using ThrFrd.GameTrans.Application.Core;
using ThrFrd.GameTrans.Tool;
using ThrFrd.GameTrans.Infrastructure.Entities.Enum;
using ThrFrd.GameTrans.Infrastructure.Entities.Entity;

namespace ThrFrd.GameTrans.Admin.Web.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string validateCode)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            int msgID = 1;
            if (!ValidateValidateCode(validateCode))
            {
                msgID = -1;
            }
            else
            {
                var user = UserBiz.UserLogin(username, password, ref msgID);
                if (msgID == 1)
                {
                    System.Web.Security.FormsAuthentication.SetAuthCookie(user.UserName, false);
                    //if (user.Status == ItemStatus.Supper)
                    //{
                    //    string checkcode = SupportBiz.SendShortMessageCode(user.UserName, user.HandPhone, CheckCodeType.LoginAdminShortMessageCode);
                    //    if (checkcode == "")
                    //    {
                    //        msgID = -10;
                    //    }
                    //    else
                    //    {
                    //        msgID = 99;
                    //        dic.Add("PHONE", user.HandPhone.Substring(0, 3) + "*****" + user.HandPhone.Substring(8, 3));
                    //    }
                    //}
                    //else
                    //{
                    //    System.Web.Security.FormsAuthentication.SetAuthCookie(user.UserName, false);
                    //}
                }
            }
            dic.Add("RID", msgID.ToString());

            return Content(dic.ToJson());
        }

        public ActionResult LogOut()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult ChangePassword(string password, string newpassword, string confirm)
        {
            string userName = System.Web.HttpContext.Current.User.Identity.Name;
            var user = new User().Find(userName);
            if (user.Password == AesAlgorithm.Encrypt(password))
            {
                if (newpassword == confirm)
                {
                    user.Password = AesAlgorithm.Encrypt(newpassword);
                    user.PostModify();
                    if (user != null)
                    {
                        return Content("1");
                    }
                    else
                    {
                        return Content("0");
                    }
                }
                else
                {
                    return Content("-2");
                }

            }
            else
            {
                return Content("-1");
            }
        }

        /// <summary>
        /// 验证验证码是否正确
        /// </summary>
        /// <returns></returns>
        private bool ValidateValidateCode(string code)
        {
            bool result;

            string token = Request.Params["token"];
            if (token == null)
            {
                result = false;
            }
            else
            {
                token = Cryptogram.CommonEncrypt(token);
                //过虑加密后出现的非法字符
                token = token.Replace("/", "");
                token = token.Replace("+", "");
                token = token.Replace("=", "");
                token = token.Substring(0, 4).ToString();
                token = token.ToLower();
                //过虑0为O,I和1
                token = token.Replace("0", "o");
                token = token.Replace("1", "i");
                string validStr = code.ToLower();
                //o,0转换，i,1转换
                validStr = validStr.Replace("0", "o");
                validStr = validStr.Replace("1", "i");
                if (validStr != token)
                    result = false;
                else
                    result = true;
            }
            return result;
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="t"></param>
        [HttpGet]
        public void validatetoken(string t)
        {
            const int width = 60, height = 25;
            Bitmap aBmp = new Bitmap(width, height);
            Graphics aGrph = Graphics.FromImage(aBmp);
            aGrph.FillRectangle(new SolidBrush(Color.FromArgb(68, 68, 68)), 0, 0, width, height);//255,153,0

            //string adPitch = ssolib.Cryptogram.CommonDecrypt(Request.QueryString["yyMM"].ToString());

            //为了解决浏览器后退，验证码不刷新的问题，对验证码的实现做了修改。
            string adPitch = Cryptogram.CommonEncrypt(t);

            adPitch = adPitch.Replace("/", "");
            adPitch = adPitch.Replace("+", "");
            adPitch = adPitch.Replace("=", "");

            adPitch = adPitch.Substring(0, 4);

            adPitch = adPitch.ToUpper();

            //过虑0为O,I和1
            adPitch = adPitch.Replace("0", "O");
            adPitch = adPitch.Replace("1", "I");
            new VerifyCodeImage().CreateImageOnPage(adPitch, System.Web.HttpContext.Current);

        }

        [HttpPost]
        public ActionResult CheckShortMessageCode(string userName, string checkCode)
        {

            var user = new User().Find(userName);
            if (user.Status == ItemStatus.Supper)
            {
                bool isTrue = SupportBiz.CheckShortMessageCode(user.UserName, user.HandPhone, checkCode, CheckCodeType.LoginAdminShortMessageCode);
                if (isTrue)
                {
                    System.Web.Security.FormsAuthentication.SetAuthCookie(userName, false);
                    return Content("1");
                }
                else
                {
                    return Content("0");
                }
            }
            else
            {
                return Content("-1");//用户名错误
            }

        }

        public ActionResult Resetpassword()
        {
            return View();
        }

    }
}
