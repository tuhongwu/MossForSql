using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace MossForSql
{
    class Lists : BaseWebService
    {
        private static WSLists.Lists li;
        private const string _URL = @"/_vti_bin/Lists.asmx";

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool InitWebService()
        {
            try
            {

                li = new WSLists.Lists();

                li.Url = baseUrl + _URL;
                li.Timeout = 900000;
                li.Credentials = MyCredential;
                return true;
            }
            catch
            {
                return false;
            }

        }

        public static void SetUrl(string url)
        {
            li.Url = url + _URL;
        }

        public static XmlNode GetList(string ListName)
        {
            try
            {
                
                return li.GetList(ListName);


            }
            catch
            {
                throw new Exception("无法获得列表的文件夹集合。\n");
            }
        }


        public static XmlNode GetListCollection()
        {
            try
            {
                return li.GetListCollection();
            }
            catch
            {
                throw new Exception("无法获得列表的文件夹集合。\n");
            }
        }

        public static XmlNode UpdateList(string ListName, XmlNode listProperties, XmlNode newFields, XmlNode updateFields, XmlNode deleteFields, string listVersion)
        {
            try
            {
                return li.UpdateList(ListName, listProperties, newFields, updateFields, deleteFields, listVersion);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static XmlNode GetListContentTypes(string ListName)
        {
            try
            {
                return Lists.li.GetListContentTypes(ListName, "");
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
                return (XmlNode)null;
            }
        }

        public static XmlNode GetListContentType(string ListName, string contenttypeId)
        {
            try
            {
                return Lists.li.GetListContentType(ListName, contenttypeId);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
                return (XmlNode)null;
            }
        }

        public static XmlNode GetListContent(string strListName, string strContentId)
        {
            //XmlNode xn=li.GetListCollection();
            return li.GetListContentType(strListName, strContentId);

        }
    }
}
