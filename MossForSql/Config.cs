using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Windows.Forms;

namespace EnterpriseUsersManagement
{
    class Config
    {
        Configuration configuration = null;
        CustomDataSection.CustomDataSection customDataSection = null;  //部门机构Config架构

        // Office2003AddInsMenu.CustomMenuSection MenuDataSetions = null;
        ExeConfigurationFileMap FileMap = new ExeConfigurationFileMap();
        /// <summary>
        /// 部门机构Config架构初始化
        /// </summary>
        public Config()
        {

            FileMap.ExeConfigFilename = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "web.config";
            configuration = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None);
            customDataSection = configuration.GetSection("MyCustomDataSection") as CustomDataSection.CustomDataSection;
        }


 

        ///// <summary>
        ///// 得到RootOU
        ///// </summary>
        //public string GetRootOU()
        //{
        //    try
        //    {
        //        return customDataSection.ApplicationSetting["ADRootOU"].id;

        //    }
        //    catch (Exception ex)
        //    {
        //       MessageBox.Show(ex.Message);
        //    }
             
        //}

        //public string GetADSPSConfig()
        //{
        //    MyADHelper.ADPath = customDataSection.ApplicationSetting["ADPath"].id;

        //    MyADHelper.ADServer = customDataSection.ApplicationSetting["ADDomain"].id;
        //    MyADHelper.ADRootOU = customDataSection.ApplicationSetting["ADRootOU"].id;
        //    MyADHelper.ADRootOUPath = customDataSection.ApplicationSetting["ADRootOU"].department;


        //    MyADHelper.ADOCSPool = customDataSection.ApplicationSetting["OCS"].id;
        //    MyADHelper.ADExchangePool = customDataSection.ApplicationSetting["ExchangePool"].id;
        //    MyADHelper.ADExchange = customDataSection.ApplicationSetting["Exchange"].id;
        //}



        /// <summary>
        /// 设置config文件
        /// </summary>
        /// <param name="url"></param>
        public void SetADSPSConfig(string ADRootOU, string ADRootOUDisName, string ADPath, string ADDomain, string OCS, string ExchangePool, string Exchange)
        {
            customDataSection.ApplicationSetting["ADRootOU"].id = ADRootOU;
            customDataSection.ApplicationSetting["ADRootOU"].department = ADRootOUDisName;
            customDataSection.ApplicationSetting["ADPath"].id = ADPath;
            customDataSection.ApplicationSetting["ADDomain"].id = ADDomain;


            customDataSection.ApplicationSetting["OCS"].id = OCS;
            customDataSection.ApplicationSetting["ExchangePool"].id = ExchangePool;
            customDataSection.ApplicationSetting["Exchange"].id = Exchange;
          //  configuration.SaveAs();
        }



      



    }
}
