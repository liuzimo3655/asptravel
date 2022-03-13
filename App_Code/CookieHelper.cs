using System;
using System.Web;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    public class CookieHelper
    {
        #region 添加一个新的Cookie到HttpCookes集合
        /// <summary>
        /// 添加一个新的Cookie到HttpCookes集合
        /// </summary>
        /// <param name="strCookName">Cookie的名称</param>
        /// <param name="strCookValue">Cookie的值</param>
        public static void AddCookies(string strCookName, string strCookValue)
        {
            if (System.Web.HttpContext.Current.Request.Browser.Cookies == true)
            {
                HttpCookie Cookies = new HttpCookie(strCookName, strCookValue);
                System.Web.HttpContext.Current.Response.AppendCookie(Cookies);
            }
            else
            {
                System.Web.HttpContext.Current.Session[strCookName] = strCookValue;
            }
        }
        #endregion

        #region 添加一个Cookie到HttpCookes集合并设置其过期时间
        /// <summary>
        /// 添加一个Cookie到HttpCookes集合并设置其过期时间
        /// </summary>
        /// <param name="strCookName"></param>
        /// <param name="strCookValue"></param>
        /// <param name="dtExpires"></param>
        public static void AddCookies(string strCookName, string strCookValue, DateTime dtExpires)
        {
            if (System.Web.HttpContext.Current.Request.Browser.Cookies == true)
            {
                HttpCookie myCookies = new HttpCookie(strCookName);
                myCookies.Value = strCookValue;
                myCookies.Expires = dtExpires;
                System.Web.HttpContext.Current.Response.Cookies.Add(myCookies);
            }
            else
            {
                System.Web.HttpContext.Current.Session[strCookName] = strCookValue;
            }
        }
        #endregion

        #region 删除指定的Cookie
        /// <summary>
        /// 删除指定的Cookie
        /// </summary>
        /// <param name="strCookName">Cookie名称</param>
        public static void DelCookies(string strCookName)
        {
            if (System.Web.HttpContext.Current.Request.Browser.Cookies == true)
            {
                if (System.Web.HttpContext.Current.Request.Cookies[strCookName] != null)
                {
                    HttpCookie myCookie = new HttpCookie(strCookName);
                    myCookie.Expires = DateTime.Now.AddDays(-1d);
                    System.Web.HttpContext.Current.Response.Cookies.Add(myCookie);
                }
            }
            else
            {
                System.Web.HttpContext.Current.Session.Contents.Remove(strCookName);
            }
        }
        #endregion

        #region 获取指定Cookie的值
        /// <summary>
        /// 获取指定Cookie的值
        /// </summary>
        /// <param name="strCookName">Cookie名称</param>
        /// <returns></returns>
        public static string GetCookieValue(string strCookName)
        {
            //判断客户端浏览器的Cookie是否可用
            if (System.Web.HttpContext.Current.Request.Browser.Cookies == true)
            {
                if (HttpContext.Current.Request.Cookies[strCookName] != null)
                {
                    return HttpContext.Current.Request.Cookies[strCookName].Value;
                }
                else
                {
                    return String.Empty;
                }
            }
            else
            {
                if (HttpContext.Current.Session[strCookName] != null)
                {
                    return System.Web.HttpContext.Current.Session[strCookName].ToString();
                }
                else
                {
                    return String.Empty;
                }
            }
        }
        #endregion
    }
}
