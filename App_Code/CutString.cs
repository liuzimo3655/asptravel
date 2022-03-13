using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonLib
{
    public class CutString
    {
        #region ��ȡ�ַ�����������HTML��ʽ
        /// <summary>
        /// ��ȡ�ַ�����������HTML��ʽ
        /// </summary>
        /// <param name="strText">Ҫ��ȡ���ַ���</param>
        /// <param name="len">��ȡ����</param>
        /// <returns></returns>
        public static string CutWithSubstring(string strText, int len)
        {
            if (strText.Length > len)
            {
                return strText.Substring(0, len) + "��";
            }
            else
            {
                return strText;
            }
        }
        #endregion

        #region ���ַ�����ƥ����ַ��滻
        /// <summary>
        /// ���ַ�����ƥ����ַ��滻
        /// </summary>
        /// <param name="ContentStr">Ҫȥ��HTML��ǩ���ַ���</param>
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

        #region ��ȡ�ַ���ȥ����HTML�ַ�
        /// <summary>
        /// ��ȡ�ַ���ȥ����HTML�ַ�
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string CutWithOutHtml(string inputString, int len)
        {
            inputString = LoseHtml(inputString);
            if (inputString.Length > len)
                inputString = inputString.Substring(0, len) + "��";
            return inputString;
        }
        #endregion

        #region sql��ע��������ȡ�ַ����е�Σ���ַ�
        /// <summary>
        /// sql��ע��������ȡ�ַ����е�Σ���ַ�
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrnHtml(string strHtml)
        {
            if (strHtml != string.Empty)
            {
                //�滻������
                strHtml = strHtml.Replace("<", "&lt;");
                strHtml = strHtml.Replace(">", "&rt;");
                //�滻����
                strHtml = strHtml.Replace(((char)34).ToString(), "&quot;");
                strHtml = strHtml.Replace(((char)39).ToString(), "&#39;");
                //�滻�ո�
                strHtml = strHtml.Replace(((char)13).ToString(), "");
                //�滻���з�
                strHtml = strHtml.Replace(((char)10).ToString(), "<BR> ");
                //�滻��
                strHtml = strHtml.Replace(".", "��");
                //�滻����
                strHtml = strHtml.Replace("-", "��");
                //�滻�ٷֺ�
                strHtml = strHtml.Replace("%", "��");
            }
            return strHtml;
        }
        #endregion

        #region ȥ���ַ����е�HTML�ַ�������ȡ���е�Σ���ַ�
        /// <summary>
        /// ȥ���ַ����е�HTML�ַ�������ȡ���е�Σ���ַ�
        /// </summary>
        /// <param name="strHTML"></param>
        /// <returns></returns>
        public static string CutHTML(string strHTML)
        {
            //ȥ��HTML�ַ�
            strHTML = LoseHtml(strHTML);
            //��ȡΣ���ַ�
            strHTML = UrnHtml(strHTML);
            return strHTML;
        }
        #endregion
    }
}
