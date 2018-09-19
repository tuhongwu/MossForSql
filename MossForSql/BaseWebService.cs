using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace MossForSql
{
    class BaseWebService
    {
        public static string baseUrl = null;
        public static string WsUserName = null;
        public static string WsPassWord = null;

        public static bool wsConnectflag = true;

        protected static System.Net.NetworkCredential MyCredential;

        public static void InitWebService(string UserName, string PassWord, string Url)
        {

            baseUrl = Url;

            WsUserName = UserName;
            WsPassWord = PassWord;

            MyCredential = new System.Net.NetworkCredential(WsUserName, WsPassWord);
        }
    }
}
