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
        /// ��ʼ��
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
        /// �����վ��ַ
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
                throw new Exception("�޷������վ�б�����м��ϡ�\nϵͳ��ʾ:" + ex.Message);
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
                throw new Exception("�޷�����б���ļ��м��ϡ�\n");
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
                throw new Exception("�޷������վ���û�Ȩ�޼��ϡ�\n");
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
                throw new Exception("�޷�����ļ��е��û�Ȩ�޼��ϡ�\n");
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
                throw new Exception("�޷�����б���û�Ȩ�޼��ϡ�\n");
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
                throw new Exception("�޷������վ��Ȩ�޽�ɫ���ϡ�\n");
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
                throw new Exception("�޷������ļ��е�Ȩ�޽�ɫ��\n");
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
                throw new Exception("�޷�ɾ���ļ��е�Ȩ�޽�ɫ��\n");
            }

        }

        //����б�Ľ�ɫ
        public static void UpdateUserRoleForList(string objectName, string ListName, string[] RoleName)
        {
            try
            {
                myper.UpdateUserRoleForList(objectName, ListName, RoleName);
            }
            catch
            {
                throw new Exception("�޷������б��Ȩ�޽�ɫ��\n");
            }

        }

        //ɾ���б�Ľ�ɫ
        public static void RemoveUserRoleForList(string objectName, string ListName, string RoleName)
        {
            try
            {
                myper.RemoveUserRoleForList(objectName, ListName, RoleName);
            }
            catch
            {
                throw new Exception("�޷�ɾ���б��Ȩ�޽�ɫ��\n");
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
                throw new Exception("�޷�������վ��Ȩ�޽�ɫ��\n");
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
                throw new Exception("�޷�ɾ����վ��Ȩ�޽�ɫ��\n");
            }

        }


        /// <summary>
        ///Ϊ��վ����û�
        /// </summary>
        /// <param name="UserName">�û�/�û�����</param>
        /// <param name="UserType">����,����CrossGroup,User��Group</param>
        /// <param name="ListName">�б���</param>
        /// <param name="FolderID">�ļ���ID</param>  
        public static void AddUserRoleForFolder(string UserName, string UserType, string ListName, object FolderID, string[] RoleName)
        {

            try
            {
                myper.AddUserRoleForFolder(UserName, UserType, ListName, FolderID, RoleName);
            }
            catch
            {
                throw new Exception("�޷�����ļ��е�Ȩ�޽�ɫ��\n\n");
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
                throw new Exception("�޷������վ��Ȩ�޽�ɫ��\n\nϵͳ��ʾ:" + ex.Message);
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
                throw new Exception("�޷�����б��Ȩ�޽�ɫ��\n\nϵͳ��ʾ:" + ex.Message);
            }
        }




        /// <summary>
        ///Ϊ��վɾ���û�
        /// </summary>
        /// <param name="UserName">�û�/�û�����</param>
        /// <param name="ListName">�б���</param>
        /// <param name="FolderID">�ļ���ID</param>  
        public static void ClearUserRoleForFolder(string ListName, object FolderID)
        {
            try
            {
                myper.ClearUserRoleForFolder(ListName, FolderID);
            }
            catch (Exception ex)
            {
                throw new Exception("�޷�ɾ���ļ��е��û�Ȩ�ޡ�\n\nϵͳ��ʾ:" + ex.Message);
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
                throw new Exception("�޷�ɾ����վ���û�Ȩ�ޡ�\n\nϵͳ��ʾ:" + ex.Message);
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
                throw new Exception("�޷�ɾ���б���û�Ȩ�ޡ�\n\nϵͳ��ʾ:" + ex.Message);
            }
        }


        /// <summary>
        ///Ϊ��վɾ���û�
        /// </summary>
        /// <param name="UserName">�û�/�û�����</param>
        /// <param name="ListName">�б���</param>
        /// <param name="FolderID">�ļ���ID</param>  
        public static void DeleteUserRoleForFolder(string UserName, string ListName, object FolderID)
        {
            try
            {
                myper.DeleteUserRoleForFolder(UserName, ListName, FolderID);
            }
            catch (Exception ex)
            {
                throw new Exception("�޷�ɾ���ļ��е��û�Ȩ�ޡ�\n\nϵͳ��ʾ:" + ex.Message);
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
                throw new Exception("�޷�ɾ����վ���û�Ȩ�ޡ�\n\nϵͳ��ʾ:" + ex.Message);
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
                throw new Exception("�޷�ɾ���б���û�Ȩ�ޡ�\n\nϵͳ��ʾ:" + ex.Message);
            }
        }


        //12_18�ո���
        //-----------------------------------------------------------------------

        /// <summary>
        /// �½��ļ���
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
                throw new Exception("�����ļ���ʧ�ܡ�\n\nϵͳ��ʾ:" + ex.Message);
            }
        }
        /// <summary>
        /// ɾ���ļ���
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
        /// �����վ�Ƿ��Ǽ̳�
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
        /// ����б��Ƿ��Ǽ̳�
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
        /// ����ļ����Ƿ��Ǽ̳�
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
        /// �̳л��ߴ����վ�ĸ�Ȩ��
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
        ///  �̳л��ߴ���б�ĸ�Ȩ��
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
        /// �̳л��ߴ���ļ��еĸ�Ȩ��
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
