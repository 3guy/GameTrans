using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GoodMan.Tool.Framework
{
    public static class StringPlus
    {
        #region 删除最后一个字符之后的字符
        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(this string str, string strchar)
        {
            return str.Substring(0, str.LastIndexOf(strchar));
        }

        #endregion
        /// <summary>
        /// 分裂字符串
        /// </summary>
        /// <param name="input">原字符串</param>
        /// <param name="splstr">分割标签</param>
        /// <param name="removeempty">是否去除空白段</param>
        /// <returns></returns>
        public static IEnumerable<string> SplitString(this string input, char splstr, bool removeempty)
        {
            if (!String.IsNullOrEmpty(input))
            {
                return input.Split(splstr).Where(c =>
                {
                    if (removeempty)
                    {
                        if (!String.IsNullOrEmpty(c))
                        {
                            return true;
                        }
                        return false;
                    }
                    return true;
                });

            }
            return null;
        }

        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSBC(this string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        /// <summary>
        ///  转半角的函数(SBC case)
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public static string ToDBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        #region 去除字符串中的所有网页标签
        /// <summary>
        /// 删除指定标签及其所有子节点
        /// </summary>
        /// <param name="str"></param>
        /// <param name="labelName"></param>
        /// <returns></returns>
        public static string NoHTMLSegment(this string str, string labelName)
        {
            if (String.IsNullOrEmpty(str))
            {
                return str;
            }
            Regex regex = new Regex("<(" + labelName + ").*?>.*</\\1>", RegexOptions.IgnoreCase);
            return regex.Replace(str, "");
        }
        /// <summary>
        /// 替换掉特定标签
        /// </summary>
        /// <param name="str"></param>
        /// <param name="labelName"></param>
        /// <returns></returns>
        public static string NoHTMLLabel(this string str, string labelName)
        {
            if (String.IsNullOrEmpty(str))
            {
                return str;
            }
            Regex regex = new Regex("</?" + labelName + ".*?>", RegexOptions.IgnoreCase);
            return regex.Replace(str, "");
        }
        /// <summary>
        /// 去除字符串中的所有网页标签
        /// </summary>
        /// <param name="str">需要处理的字符串</param>
        /// <returns></returns>
        public static string NoHTMLLabel(this string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return str;
            }
            Regex regex = new Regex("</?.*?>", RegexOptions.IgnoreCase);
            string s = regex.Replace(str, "");
            string s1 = s.Replace("\n", "").Replace("\r", "").Trim();
            return s1;
        }
        /// <summary>
        /// 删除html标记。并返回指定长度的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="charLength"></param>
        /// <returns></returns>
        public static string NoHTMLLabel(this string str, int charLength)
        {
            string result = str.NoHTMLLabel();
            if (result.Length <= charLength)
                return result;
            return result.Substring(0, charLength);
        }
        #endregion
        /// <summary>
        /// 删除空白
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string NoSpace(this string str)
        {
            return str.Replace(" ", "");
        }
        /// <summary>
        /// 截取字符串中的一段
        /// </summary>
        /// <param name="source"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string PieceOfString(this string source, int startIndex, int length, string overflow)
        {
            string piece = source.Length > 20 ? source.Substring(0, 20) : source;
            if (new Regex("^[A-Za-z0-9\\s\\.]+$").IsMatch(piece))
            {
                length *= 2;
            }
            if (source.Length < startIndex)
            {
                return String.Empty;
            }
            if (source.Length < startIndex + length)
            {
                return source.Substring(startIndex);
            }
            return source.Substring(startIndex, length) + overflow;
        }
        /// <summary>
        /// 查看字符串中是否包含某些字符
        /// </summary>
        /// <param name="str">目标字符串</param>
        /// <param name="regexp">匹配正则表达式</param>
        /// <returns></returns>
        public static bool Contains(this string str, string regexp, RegexOptions option)
        {
            Regex reg = new Regex(regexp, option);
            return reg.IsMatch(str);
        }

        public static bool AllNotNullOrEmpty(params string[] strings)
        {
            foreach (string s in strings)
            {
                if (String.IsNullOrEmpty(s))
                {
                    return false;
                }
            }
            return true;
        }

        public static string RemoveParameter(this string url, string paramName)
        {
            string regex = String.Format("[\\?&]{0}=.*?(?=[&])|[\\?&]{0}=.*?$", paramName);
            url = new Regex(regex, RegexOptions.IgnoreCase).Replace(url, "");
            return url;
        }
    }
}
