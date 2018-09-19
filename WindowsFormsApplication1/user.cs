using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsApplication1
{
    public class user
    {
        /// <summary>用户名</summary>
        static string username = string.Empty;

        /// <summary>获取设置用户名</summary>
        public static string UserName
        {
            get { return username; }
            set { username = value; }
        }

        /// <summary>密码</summary>
        static string password = string.Empty;

        /// <summary>获取设置密码</summary>
        public static string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
