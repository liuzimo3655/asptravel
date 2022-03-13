using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonLib
{
    public class CutString
    {
        #region 截取字符串，保留其HTML格式
        /// <summary>
        /// 截取字符串，保留其HTML格式
        /// </summary>
        /// <param name="strText">要截取的字符串</param>
        /// <param name="len">截取长度</param>
        /// <returns></returns>
        public static string CutWithSubstring(string strText, int len)
        {
            if (strText.Length > len)
            {
                return strText.Substring(0, len) + "…";
            }
            else
            {
                return strText;
            }
        }
        #endregion

        #region 将字符串中匹配的字符替换
        /// <summary>
        /// 将字符串中匹配的字符替换
        /// </summary>
        /// <param name="ContentStr">要去除HTML标签的字符串</param>
        /// <returns></returns>
        private static string LoseHtml(string ContentStr)
        {
            string ClsTempLoseStr = "";
            System.Text.RegularExpressions.Regex regEx = new System.Text.RegularExpressions.Regex("<\\/*[^<>]*>");
            ClsTempLoseStr = regEx.Replace(ContentStr, "");
            ClsTempLoseStr = ClsTempLoseStr.Replace("&nbsp;", "").Replace("<BR>", "").Replace("<BR />", "").Replace("\r", "").Replace("\n", "").Replace(" ","");
            return ClsTempLoseStr;
        }
        #endregion

        #region 截取字符串去掉其HTML字符
        /// <summary>
        /// 截取字符串去掉其HTML字符
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string CutWithOutHtml(string inputString, int len)
        {
            inputString = LoseHtml(inputString);
            if (inputString.Length > len)
                inputString = inputString.Substring(0, len) + "…";
            return inputString;
        }
        #endregion

        #region sql防注函数，截取字符串中的危险字符
        /// <summary>
        /// sql防注函数，截取字符串中的危险字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrnHtml(string strHtml)
        {
            if (strHtml != string.Empty)
            {
                //替换尖括号
                strHtml = strHtml.Replace("<", "&lt;");
                strHtml = strHtml.Replace(">", "&rt;");
                //替换引号
                strHtml = strHtml.Replace(((char)34).ToString(), "&quot;");
                strHtml = strHtml.Replace(((char)39).ToString(), "&#39;");
                //替换空格
                strHtml = strHtml.Replace(((char)13).ToString(), "");
                //替换换行符
                strHtml = strHtml.Replace(((char)10).ToString(), "<BR> ");
                //替换点
                strHtml = strHtml.Replace(".", "．");
                //替换横线
                strHtml = strHtml.Replace("-", "－");
                //替换百分号
                strHtml = strHtml.Replace("%", "％");
            }
            return strHtml;
        }
        #endregion

        #region 去除字符串中的HTML字符，并截取其中的危险字符
        /// <summary>
        /// 去除字符串中的HTML字符，并截取其中的危险字符
        /// </summary>
        /// <param name="strHTML"></param>
        /// <returns></returns>
        public static string CutHTML(string strHTML)
        {
            //去除HTML字符
            strHTML = LoseHtml(strHTML);
            //截取危险字符
            strHTML = UrnHtml(strHTML);
            return strHTML;
        }
        #endregion
    }
}
