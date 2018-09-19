using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace MossForSql
{
    class MyPermissions : BaseWebService
    {

        private static MyCustomPermissionsWebService.Service myper;
        private const string _URL = @"/_vti_bin/Service.asmx";



        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static void InitWebService()
        {
            try
            {

                myper = new MyCustomPermissionsWebService.Service();

                myper.Url = baseUrl + _URL;

                myper.Credentials = MyCredential;

            }
            catch
            {

            }

        }

        public static void SetUrl(string url)
        {
            myper.Url = baseUrl + GetListUrl(url) + _URL;
        }



        /// <summary>
        /// 获得主站地址
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public static String GetListUrl(String baseUrl)
        {
            if (baseUrl.IndexOf('/', 7) != -1)
            {
                baseUrl = baseUrl.Remove(0, baseUrl.IndexOf('/', 7));
            }
            else
                baseUrl = "";

            return baseUrl;
        }


        public static XmlNode GetWebsListColloction()
        {
            try
            {
                return myper.GetWebsListColloction();
            }
            catch (Exception ex)
            {
                throw new Exception("无法获得网站列表的所有集合。\n系统提示:" + ex.Message);
            }
        }


        public static XmlNode GetFolders(string ListName)
        {
            try
            {
                return myper.GetFolders(ListName);
            }
            catch
            {
                throw new Exception("无法获得列表的文件夹集合。\n");
            }
        }

        public static XmlNode GetWebPermissions()
        {
            try
            {
                return myper.GetWebPermissions();
            }
            catch
            {
                throw new Exception("无法获得网站的用户权限集合。\n");
            }
        }

        public static XmlNode GetFolderPermissions(String ListName, object FolderID)
        {
            try
            {
                return myper.GetFolderPermissions(ListName, FolderID);
            }
            catch
            {
                throw new Exception("无法获得文件夹的用户权限集合。\n");
            }

        }
        public static XmlNode GetListPermissions(String ListName)
        {
            try
            {
                return myper.GetListPermissions(ListName);
            }
            catch
            {
                throw new Exception("无法获得列表的用户权限集合。\n");
            }
        }



        public static XmlNode GetRoleCollectionFromWeb()
        {
            try
            {
                return myper.GetRoleCollectionFromWeb();
            }
            catch
            {
                throw new Exception("无法获得网站的权限角色集合。\n");
            }

        }

        public static void UpdateUserRoleForFolder(string objectName, string ListName, object FolderID, String[] RoleName)
        {
            try
            {
                myper.UpdateUserRoleForFolder(objectName, ListName, FolderID, RoleName);
            }
            catch
            {
                throw new Exception("无法更新文件夹的权限角色。\n");
            }


        }

        public static void RemoveUserRoleForFolder(string objectName, string ListName, object FolderID, string RoleName)
        {
            try
            {
                myper.RemoveUserRoleForFolder(objectName, ListName, FolderID, RoleName);
            }
            catch
            {
                throw new Exception("无法删除文件夹的权限角色。\n");
            }

        }

        //添加列表的角色
        public static void UpdateUserRoleForList(string objectName, string ListName, string[] RoleName)
        {
            try
            {
                myper.UpdateUserRoleForList(objectName, ListName, RoleName);
            }
            catch
            {
                throw new Exception("无法更新列表的权限角色。\n");
            }

        }

        //删除列表的角色
        public static void RemoveUserRoleForList(string objectName, string ListName, string RoleName)
        {
            try
            {
                myper.RemoveUserRoleForList(objectName, ListName, RoleName);
            }
            catch
            {
                throw new Exception("无法删除列表的权限角色。\n");
            }

        }

        public static void UpdateUserRoleForWeb(string objectName, string[] RoleName)
        {
            try
            {
                myper.UpdateUserRoleForWeb(objectName, RoleName);
            }
            catch
            {
                throw new Exception("无法更新网站的权限角色。\n");
            }


        }

        public static void RemoveUserRoleForWeb(string objectName, string RoleName)
        {
            try
            {
                myper.RemoveUserRoleForWeb(objectName, RoleName);
            }
            catch
            {
                throw new Exception("无法删除网站的权限角色。\n");
            }

        }


        /// <summary>
        ///为网站添加用户
        /// </summary>
        /// <param name="UserName">用户/用户组名</param>
        /// <param name="UserType">类型,包括CrossGroup,User，Group</param>
        /// <param name="ListName">列表名</param>
        /// <param name="FolderID">文件夹ID</param>  
        public static void AddUserRoleForFolder(string UserName, string UserType, string ListName, object FolderID, string[] RoleName)
        {

            try
            {
                myper.AddUserRoleForFolder(UserName, UserType, ListName, FolderID, RoleName);
            }
            catch
            {
                throw new Exception("无法添加文件夹的权限角色。\n\n");
            }
        }


        public static void AddUserRoleForWeb(string UserName, string UserType, string[] RoleName)
        {

            try
            {
                myper.AddUserRoleForWeb(UserName, UserType, RoleName);
            }
            catch (Exception ex)
            {
                throw new Exception("无法添加网站的权限角色。\n\n系统提示:" + ex.Message);
            }

        }


        public static void AddUserRoleForList(string UserName, string UserType, string ListName, string[] RoleName)
        {
            try
            {
                myper.AddUserRoleForList(UserName, UserType, ListName, RoleName);
            }
            catch (Exception ex)
            {
                throw new Exception("无法添加列表的权限角色。\n\n系统提示:" + ex.Message);
            }
        }




        /// <summary>
        ///为网站删除用户
        /// </summary>
        /// <param name="UserName">用户/用户组名</param>
        /// <param name="ListName">列表名</param>
        /// <param name="FolderID">文件夹ID</param>  
        public static void ClearUserRoleForFolder(string ListName, object FolderID)
        {
            try
            {
                myper.ClearUserRoleForFolder(ListName, FolderID);
            }
            catch (Exception ex)
            {
                throw new Exception("无法删除文件夹的用户权限。\n\n系统提示:" + ex.Message);
            }
        }


        public static void ClearUserRoleForWeb()
        {
            try
            {
                myper.ClearUserRoleForWeb();
            }
            catch (Exception ex)
            {
                throw new Exception("无法删除网站的用户权限。\n\n系统提示:" + ex.Message);
            }
        }


        public static void ClearUserRoleForList(string ListName)
        {
            try
            {
                myper.ClearUserRoleForList(ListName);
            }
            catch (Exception ex)
            {
                throw new Exception("无法删除列表的用户权限。\n\n系统提示:" + ex.Message);
            }
        }


        /// <summary>
        ///为网站删除用户
        /// </summary>
        /// <param name="UserName">用户/用户组名</param>
        /// <param name="ListName">列表名</param>
        /// <param name="FolderID">文件夹ID</param>  
        public static void DeleteUserRoleForFolder(string UserName, string ListName, object FolderID)
        {
            try
            {
                myper.DeleteUserRoleForFolder(UserName, ListName, FolderID);
            }
            catch (Exception ex)
            {
                throw new Exception("无法删除文件夹的用户权限。\n\n系统提示:" + ex.Message);
            }
        }


        public static void DeleteUserRoleForWeb(string UserName)
        {
            try
            {
                myper.DeleteUserRoleForWeb(UserName);
            }
            catch (Exception ex)
            {
                throw new Exception("无法删除网站的用户权限。\n\n系统提示:" + ex.Message);
            }
        }


        public static void DeleteUserRoleForList(string UserName, string ListName)
        {
            try
            {
                myper.DeleteUserRoleForList(UserName, ListName);
            }
            catch (Exception ex)
            {
                throw new Exception("无法删除列表的用户权限。\n\n系统提示:" + ex.Message);
            }
        }


        //12_18日更新
        //-----------------------------------------------------------------------

        /// <summary>
        /// 新建文件夹
        /// </summary>
        /// <param name="ListName"></param>
        /// <param name="CreateFolderName"></param>
        public static int CreateFolder(string ListName, string CreateFolderName)
        {

            try
            {
                return myper.CreateFolder(ListName, CreateFolderName);
            }
            catch (Exception ex)
            {
                throw new Exception("创建文件夹失败。\n\n系统提示:" + ex.Message);
            }
        }
        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="ListName"></param>
        /// <param name="CreateFolderName"></param>
        public static void DeleteFolder(string ListName, object FolderID)
        {

            try
            {
                myper.DeleteFolder(ListName, FolderID);
            }
            catch
            {

            }
        }

        /// <summary>
        /// 获得网站是否是继承
        /// </summary>
        /// <param name="ListName"></param>
        /// <param name="CreateFolderName"></param>
        public static bool HasUniqueRoleAssignmentsForWeb()
        {

            try
            {
                return myper.HasUniqueRoleAssignmentsForWeb();
            }
            catch
            {
                return true;
            }
        }

        /// <summary>
        /// 获得列表是否是继承
        /// </summary>
        /// <param name="ListName"></param>
        /// <param name="CreateFolderName"></param>
        public static bool HasUniqueRoleAssignmentsForList(string ListName)
        {

            try
            {
                return myper.HasUniqueRoleAssignmentsForList(ListName);
            }
            catch
            {
                return true;
            }
        }

        /// <summary>
        /// 获得文件夹是否是继承
        /// </summary>
        /// <param name="ListName"></param>
        /// <param name="CreateFolderName"></param>
        public static bool HasUniqueRoleAssignmentsForFolder(string ListName, object FolderID)
        {

            try
            {
                return myper.HasUniqueRoleAssignmentsForFolder(ListName, FolderID);
            }
            catch
            {
                return true;
            }
        }

        /// <summary>
        /// 继承或者打断网站的父权限
        /// </summary>
        /// <param name="ListName"></param>
        /// <param name="CreateFolderName"></param>
        public static void BreakORResetRoleInheritanceForWeb(bool breakRole)
        {

            try
            {
                myper.BreakORResetRoleInheritanceForWeb(breakRole);
            }
            catch
            {

            }
        }

        /// <summary>
        ///  继承或者打断列表的父权限
        /// </summary>
        /// <param name="ListName"></param>
        /// <param name="CreateFolderName"></param>
        public static void BreakORResetRoleInheritanceForList(string ListName, bool breakRole)
        {
            try
            {
                myper.BreakORResetRoleInheritanceForList(ListName, breakRole);
            }
            catch
            {

            }
        }


        /// <summary>
        /// 继承或者打断文件夹的父权限
        /// </summary>
        /// <param name="ListName"></param>
        /// <param name="CreateFolderName"></param>
        public static void BreakORResetRoleInheritanceForFolder(string ListName, object FolderID, bool breakRole)
        {

            try
            {
                myper.BreakORResetRoleInheritanceForFolder(ListName, FolderID, breakRole);
            }
            catch
            {
            }
        }


    }

}
