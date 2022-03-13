using System;
using System.Web;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    public class CookieHelper
    {
        #region ���һ���µ�Cookie��HttpCookes����
        /// <summary>
        /// ���һ���µ�Cookie��HttpCookes����
        /// </summary>
        /// <param name="strCookName">Cookie������</param>
        /// <param name="strCookValue">Cookie��ֵ</param>
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

        #region ���һ��Cookie��HttpCookes���ϲ����������ʱ��
        /// <summary>
        /// ���һ��Cookie��HttpCookes���ϲ����������ʱ��
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

        #region ɾ��ָ����Cookie
        /// <summary>
        /// ɾ��ָ����Cookie
        /// </summary>
        /// <param name="strCookName">Cookie����</param>
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

        #region ��ȡָ��Cookie��ֵ
        /// <summary>
        /// ��ȡָ��Cookie��ֵ
        /// </summary>
        /// <param name="strCookName">Cookie����</param>
        /// <returns></returns>
        public static string GetCookieValue(string strCookName)
        {
            //�жϿͻ����������Cookie�Ƿ����
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
