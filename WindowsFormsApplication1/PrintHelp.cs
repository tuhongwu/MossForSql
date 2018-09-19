using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;
using OfficeWordAddInsDom;
using Excel = Microsoft.Office.Interop.Excel;
using OfficeExcelAddInsDom;
using System.IO;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{


    public class PrintHelp
    {

        public Excel.Application excelApp = null;
        public OfficeExcelAddInsDom.ExcelAddInsDom exceldom = null;

        public void PrintExcel(DataTable table, string ExcelName, string xianShiSheet, string startcell, int insertrow)
        {
            if (table == null) return;
            //if (ds == null || ds.Tables[0].Rows.Count < 0) return;
            //打开Excel模板
            string path = "excel1.xlt";//Application.StartupPath + @"\" + "excel1.xlt";//GetTemplatePath(ExcelName);
            OpenExcel(path, true);
            //string Field = exceldom.GetCellValue("DataField").ToString();
            //string[] fields = Field.ToString().Split(',');
            //DataTable NEWDT = ds.Tables[0].Copy();
            ////exceldom.ShowExcelApplication(true);
            ////添加单位名称和编制人
            //if (fields == null || fields.Length < 1)
            //{
            //    MessageBox.Show("读取模板出错！\n\r请检查模板是否正确！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //DataTable dtData = SetDataTableColumns(NEWDT, fields);
            exceldom.UpdatingScreen(false);//屏蔽更新
            //***********************拷贝公式**************************
            //DataTable GsDT = exceldom.GetRangeDataTable(GongShiSheet, GongShiFanWei);
            //exceldom.SetRangeDataTable(DateSheet, startcell, dtData);
            //for (int t = 1; t < dtData.Rows.Count; t++)
            //{
            //    GsDT.ImportRow(GsDT.Rows[0]);
            //}
            //exceldom.SetRangeDataTable(GongShiSheet, startcell, GsDT);
            //********************************************************

            DataTable dtData = table;

            for (int i = 1; i < dtData.Rows.Count; i++)
            {
                exceldom.InsertRow(xianShiSheet, insertrow);
            }
            exceldom.style.SetBorders(BordersLineStyle.xlContinuous, BordersBorderWeight.xlThin);
            exceldom.SetRangeDataTable(xianShiSheet, startcell, dtData);

            exceldom.UpdatingScreen(true);//屏蔽更新
        }



        /// <summary>对Table的列按FieldsIndex的顺序排序同时只保留排序的列</summary>
        public DataTable SetDataTableColumns(DataTable dt, string[] FieldsIndex)
        {
            DataTable NewDT = new DataTable();
            for (int i = 0; i < FieldsIndex.Length; i++)
            {
                DataColumn dc = new DataColumn(FieldsIndex[i]);
                NewDT.Columns.Add(dc);
            }
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow NewDr = NewDT.NewRow();
                    for (int i = 0; i < FieldsIndex.Length; i++)
                    {
                        NewDr[FieldsIndex[i]] = dr[FieldsIndex[i]];
                    }
                    NewDT.Rows.Add(NewDr);
                }
                return NewDT;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "提示信息！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

        }
        /// <summary>打开一个Excel模板</param>
        /// <param name="tempRelativelyPath">Excel的相对路径</param>
        /// <param name="visible">是否显示</param>
        public void OpenExcel(string tempRelativelyPath, bool visible)
        {
            try
            {
                string absolutePath = AppDomain.CurrentDomain.BaseDirectory + tempRelativelyPath;
                if (!System.IO.File.Exists(absolutePath))
                {
                    //FormAssistant.ShowMessage("模板的路径不正确，请确认！");
                    return;
                }
                if (excelApp == null || exceldom == null)
                {
                    excelApp = new Microsoft.Office.Interop.Excel.Application();
                    excelApp.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMaximized;
                    exceldom = OfficeExcelAddInsDom.ExcelAddInsDomFactory.CreateOfficeExcelAddInsDom(excelApp);
                }
                exceldom.AddDocument(absolutePath);
                excelApp.Visible = visible;
            }
            catch
            {
                //throw new COMException("请检查本机是否已安装了Microsoft Office程序！");
            }
        }

        public void Print_创新工作室申报书(DataTable dtt, bool IsWordVisible)
        {
            try
            {
                if (dtt == null) return;
                string path = Application.StartupPath + @"\" + "demo.dot";//+ Const.CXGZSSBS;
                if (!File.Exists(path))
                {
                    //CommonAssitant.ShowMessage("模板的路径不正确，请确认！");
                    return;
                }
                Word.Application wordApp = new Word.Application();
                wordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize;

                WordAddInsDom worddom = WordAddInsDomFactory.CreateOfficeWordAddInsDom(wordApp);
                worddom.AddDocument(path);
                wordApp.Visible = IsWordVisible;

                #region 数据输出
                DataTable dt = dtt;// DatasetGroupBy.DTReCreateIndex(dtt, new string[] {"ID","kj5005b", "kj5001b", "kj5004b",
                //"kj5006b1", "kj5007b", "kj5008b", "kj5009b", "kj5010b","kj5003b", "kj5012b", "kj5014b", "kj5015b" ,"kj5016b",
                //"kj5017b","kj5018b","kj5019b","kj5020b","kj5021b","kj5022b", "kj5023b", "kj5024b"});

                //dt.Columns.Add("zjxm", typeof(string));
                //foreach (DataRow dr in dt.Rows)
                //{
                //    dr["zjxm"] = dr["kj5005b"];
                //}

                //叛断是不是第一页,若是第一页，直接打印，否则复制
                bool isFirst = true;
                foreach (DataRow dr in dt.Rows)
                {
                    if (isFirst)
                        isFirst = false;
                    else
                    {
                        worddom.CopyTableAndPaste(1);
                    }
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        DataColumn dc = dt.Columns[i];
                        worddom.SetBookMarkValueNoEditRange(dc.ColumnName, dr[dc].ToString().Replace(@"\r\n", "\r\n"));
                    }
                    //string id = dr["ID"].ToString();
                    //string[] strs1 = new string[] { "select", "", id.ToString(), "", "", "", "", "", "", "", "" };
                    //DataSet ds1 = null;//OperateSql.ExecuteProcByDs("up_Get工作室人员信息", strs1);
                    //if (ds1 != null)
                    //{
                    //    DataTable dt1 = null;// DatasetGroupBy.DTReCreateIndex(ds1.Tables[0], new string[] { "kj5002b", "kj5003b",
                    //    //"kj5004b", "kj5005b", "kj5006b", "kj5007b", "kj5008b","kj5009b"});
                    //    int temp = 0;
                    //    foreach (DataRow dr1 in dt1.Rows)
                    //    {
                    //        for (int x = 0; x < dt1.Columns.Count; x++)
                    //        {
                    //            DataColumn dc1 = dt1.Columns[x];
                    //            string lable = dc1.ColumnName + (temp + 1).ToString();
                    //            worddom.SetBookMarkValueNoEditRange(lable, dr1[dc1].ToString().Replace(@"\r\n", "\r\n"));
                    //        }
                    //        temp++;
                    //        if (temp == 3) break;
                    //    }
                    //}


                }
                #endregion
            }
            catch (Exception ex)
            {
                // CommonAssitant.ShowMessage(ex.Message);
            }
        }
        /// <summary>打印项目鉴定表 </summary>
        public void Print_项目鉴定表(string TemplatePath, DataTable dt, bool IsWordVisible)
        {
            try
            {
                // if (DataOperator.IsDTNull(dt)) return;
                if (dt == null) return;
                string path = Application.StartupPath + @"\" + TemplatePath;
                if (!File.Exists(path))
                {
                    //CommonAssitant.ShowMessage("模板的路径不正确，请确认！");

                    return;
                }
                Word.Application wordApp = new Word.Application();
                wordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize;
                WordAddInsDom worddom = WordAddInsDomFactory.CreateOfficeWordAddInsDom(wordApp);
                worddom.AddDocument(path);
                wordApp.Visible = IsWordVisible;
                #region 数据输出
                //叛断是不是第一页,若是第一页，直接打印，否则复制
                bool isFirst = true;
                string ProjectNames = "";
                foreach (DataRow dr in dt.Rows)
                {
                    if (ProjectNames.Equals(""))
                        ProjectNames = dr["kj5005b"].ToString();
                    else
                        ProjectNames += Environment.NewLine + dr["kj5005b"].ToString();
                    if (isFirst)
                        isFirst = false;
                    else
                    {
                        worddom.CopyTableAndPaste(1);
                    }
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        DataColumn dc = dt.Columns[i];
                        worddom.SetBookMarkValueNoEditRange(dc.ColumnName, dr[dc].ToString().Replace(@"\r\n", "\r\n"));
                    }
                }
                worddom.DeleteTable(1);
                worddom.SetBookMarkValueNoEditRange("ProjectNames", ProjectNames.ToString().Replace(@"\r\n", "\r\n"));
                #endregion
            }
            catch (Exception ex)
            {
                //CommonAssitant.ShowMessage(ex.Message);
            }
        }

    }
}
