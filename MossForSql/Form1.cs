using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using WindowsMin.User.Common;
using System.Configuration;

namespace MossForSql
{
    public partial class Moss2Sql : Form
    {

        //Webs.Webs wb = new Webs.Webs();
        //SiteData.SiteData sd = new SiteData.SiteData();
        //System.Net.NetworkCredential nc = new System.Net.NetworkCredential();

        string strDemoKey;
        public Moss2Sql()
        {
            InitializeComponent();
        }

        #region 初始化

        Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        TreeNode TNParentWebNode;
        TreeNode TNSelectedNode;
        TreeNode TNParentListNode;
        private TreeNode TNmyNode;
        /// <summary>
        /// 选中LIst返回的XML
        /// </summary>
        XmlNode xlLists = null;
        string ListVersion = string.Empty;
        string ListName = string.Empty;
        string ListDescription = string.Empty;
        string ListGuid = string.Empty;
        string SelectListID = "";
        ListViewColumnSorter lvwColumnSorter;
        string[] strArry;
        private void Form1_Load(object sender, EventArgs e)
        {
            strDemoKey = config.AppSettings.Settings["DemoKey"].Value.ToString();
            if (!string.IsNullOrEmpty(strDemoKey))
                strArry = strDemoKey.Split(',');
            bool ischkek = config.AppSettings.Settings["Ischeck"].Value.ToString().Equals("true") ? true : false;
            cbISChekd.CheckState = ischkek ? CheckState.Checked : CheckState.Unchecked;
            cmbTitle.DropDownStyle = ComboBoxStyle.DropDown;
            cmbTitle.FlatStyle = FlatStyle.Flat; //设置外观
        }
        #endregion

        public void SPSTreeViewLoad()
        {
            MyPermissions.InitWebService();
            Lists.InitWebService();
            AddWebListNode();
        }

        #region 加载数据


        //加载数据
        private void btnInit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.treeWebAndList.Nodes.Clear();
                this.lvPressions.Items.Clear();
                txtresult.Text = "";

                string url = this.txtUrl.Text.Trim();
                string UserName = this.txtuser.Text.Trim();
                string Password = this.txtpwd.Text.Trim();

                BaseWebService.InitWebService(UserName, Password, url);
                MyPermissions.InitWebService();

                SPSTreeViewLoad();
                InitLvCoumnsName();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void InitLvCoumnsName()
        {
            lvPressions.Columns.Clear();
            this.lvPressions.Columns.Add("DisplayName", 150, HorizontalAlignment.Right);
            this.lvPressions.Columns.Add("Name", 100, HorizontalAlignment.Left);

            this.lvPressions.Columns.Add("ColName", 180, HorizontalAlignment.Left);
            this.lvPressions.Columns.Add("RowOrdinal", 100, HorizontalAlignment.Left);
        }

        private void AddWebListNode()
        {
            StringBuilder sb = new StringBuilder();

            XmlNode websListColloction = MyPermissions.GetWebsListColloction();
            if (websListColloction == null)
                return;
            this.treeWebAndList.Nodes.Clear();
            XmlNodeList childNodes1 = websListColloction.ChildNodes;
            for (int index1 = 0; index1 < childNodes1.Count; ++index1)
            {

                XmlNode xmlNode = childNodes1.Item(index1);
                XmlAttributeCollection attributes = xmlNode.Attributes;

                sb.Append(index1 + attributes.GetNamedItem("Title").Value + "-" + attributes.GetNamedItem("Url").Value + Environment.NewLine);

                this.treeWebAndList.Nodes.Add(new TreeNode(attributes.GetNamedItem("Title").Value, 0, 0)
                {
                    Tag = (object)attributes.GetNamedItem("Url").Value
                });
                XmlNodeList childNodes2 = xmlNode.ChildNodes;

                for (int index2 = 0; index2 < childNodes2.Count; ++index2)
                {
                    sb.Append(index1 + "-" + index2 + childNodes2.Item(index2).Attributes.GetNamedItem("Title").Value + Environment.NewLine);
                    this.treeWebAndList.Nodes[index1].Nodes.Add(new TreeNode(childNodes2.Item(index2).Attributes.GetNamedItem("Title").Value, 1, 1)
                    {
                        Nodes = { " " }
                    });
                }
            }

        }
        #endregion

        //生成sql
        private void btnSC_Click(object sender, EventArgs e)
        {
            bool flag = false;
            foreach (ListViewItem lv in this.lvPressions.Items)
            {
                if (lv.Checked != true)
                    continue;

                if (lv.SubItems[3].Text.ToString() != "0" && !string.IsNullOrEmpty(lv.SubItems[3].Text))
                {
                    flag = true;
                }
            }

            StringBuilder stb = new StringBuilder();

            stb.Append("Select ");

            if (!flag)
            {
                foreach (ListViewItem lv in this.lvPressions.Items)
                {
                    if (lv.Checked != true)
                        continue;

                    stb.Append(lv.SubItems[2].Text.ToString());
                    stb.Append(" as ");
                    stb.Append(lv.SubItems[1].Text.ToString());
                    stb.Append(" , ");
                }
            }
            else
            {
                foreach (ListViewItem lv in this.lvPressions.Items)
                {
                    if (lv.Checked != true)
                        continue;

                    if (!string.IsNullOrEmpty(lv.SubItems[3].Text))
                    {
                        stb.Append(Convert.ToChar(Convert.ToInt32(lv.SubItems[3].Text) + 97));
                        stb.Append(".");
                    }

                    stb.Append(lv.SubItems[2].Text.ToString());
                    stb.Append(" as ");
                    stb.Append(lv.SubItems[1].Text.ToString());
                    stb.Append(" , ");
                }
            }
            stb.Remove(stb.Length - 2, 1);

            stb.Append("FROM [dbo].[AllUserData] where tp_listid='" + SelectListID + "'");

            this.txtresult.Text = stb.ToString();
        }

        public void setListID(XmlNode xlLists)
        {
            XmlNodeList childNodes = xlLists.ChildNodes;
            this.SelectListID = xlLists.Attributes.GetNamedItem("ID").Value;
        }

        private void treeWebAndList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                this.lvPressions.Items.Clear();
                TNParentWebNode = this.treeWebAndList.SelectedNode;
                TNSelectedNode = this.treeWebAndList.SelectedNode;
                TNParentListNode = this.treeWebAndList.SelectedNode;
                while (TNParentWebNode.Level != 0)
                {
                    TNParentWebNode = TNParentWebNode.Parent;

                    if (TNParentWebNode.Level == 1)
                    {
                        TNParentListNode = TNParentWebNode;
                    }
                }
                MyPermissions.SetUrl(TNParentWebNode.Tag.ToString());
                Lists.SetUrl(TNParentWebNode.Tag.ToString());
                //toolStripStatusLabel1.Text = TNParentWebNode.Tag.ToString();
                txtWebUrl.Text = TNParentWebNode.Tag.ToString();
                switch (TNSelectedNode.Level)
                {
                    case 1:
                        {
                            xlLists = Lists.GetList(TNSelectedNode.Text);
                            ListVersion = xlLists.Attributes["Version"].Value;
                            ListName = xlLists.Attributes["Title"].Value;
                            ListDescription = xlLists.Attributes["Description"].Value;
                            ListGuid = xlLists.Attributes["Name"].Value;
                            break;
                        }
                    case 2:
                        //MessageBox.Show(this.TNSelectedNode.Parent.Text + "--" + this.TNSelectedNode.Tag.ToString());
                        this.xlLists = Lists.GetListContentType(this.TNSelectedNode.Parent.Text, this.TNSelectedNode.Tag.ToString());
                        //MessageBox.Show(xlLists.InnerXml);
                        break;
                }

                if (xlLists != null)
                {
                    XmlNodeList childNodes1 = this.xlLists.ChildNodes;
                    XmlAttributeCollection attributes1 = this.xlLists.Attributes;
                    if (this.TNSelectedNode.Level == 2)
                        this.setListID(Lists.GetList(this.TNSelectedNode.Parent.Text));
                    else if (this.TNSelectedNode.Level == 1)
                        this.setListID(this.xlLists);
                    for (int index1 = 0; index1 < childNodes1.Count; ++index1)
                    {
                        XmlNode xmlNode1 = childNodes1.Item(index1);
                        attributes1 = xmlNode1.Attributes;
                        if (xmlNode1.ChildNodes.Count != 0)
                        {
                            XmlNodeList childNodes2 = xmlNode1.ChildNodes;
                            for (int index2 = 0; index2 < childNodes2.Count; ++index2)
                            {
                                string text1 = "";
                                XmlNode xmlNode2 = childNodes2.Item(index2);
                                if (!(xmlNode2.Name != "Field"))
                                {
                                    XmlAttributeCollection attributes2 = xmlNode2.Attributes;
                                    string str1 = attributes2.GetNamedItem("Type").Value;
                                    if (!(str1 == "Computed") && !(str1 == "File") && !(str1 == "Lookup"))
                                    {
                                        string text2 = attributes2.GetNamedItem("Name").Value;
                                        string text3 = attributes2.GetNamedItem("ColName").Value;
                                        string str2 = attributes2.GetNamedItem("DisplayName").Value;
                                        if (attributes2.GetNamedItem("RowOrdinal") != null)
                                            text1 = attributes2.GetNamedItem("RowOrdinal").Value;
                                        ListViewItem listViewItem = new ListViewItem();
                                        listViewItem.SubItems[0].Text = str2;
                                        listViewItem.SubItems.Add(text2);
                                        listViewItem.SubItems.Add(text3);
                                        listViewItem.SubItems.Add(text1);
                                        this.lvPressions.Items.Add(listViewItem);
                                    }
                                }
                            }

                            // 创建一个ListView排序类的对象，并设置listView1的排序器

                            lvwColumnSorter = new ListViewColumnSorter();
                            this.lvPressions.ListViewItemSorter = lvwColumnSorter;


                            lvwColumnSorter.SortColumn = 1;
                            lvwColumnSorter.Order = SortOrder.Ascending;
                            this.lvPressions.Sort();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lvPressions_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // 检查点击的列是不是现在的排序列.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // 重新设置此列的排序方法.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // 设置排序列，默认为正向排序
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }
            // 用新的排序方法对ListView排序
            this.lvPressions.Sort();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            //nc.UserName = txtuser.Text;
            //nc.Password = txtpwd.Text;
            //nc.Domain = txtyu.Text;
            //sd.Credentials = nc;
            //wb.Credentials = nc;
            //wb.Url = "http://192.168.9.133/xmgl/_vti_bin/Webs.asmx";
            //XmlNode xn = null;
            //xn=wb.GetContentTypes();

            try
            {
                this.lvPressions.Items.Clear();
                this.lvPressions.Columns.Clear();
                string url = this.txtUrl.Text.Trim();
                string UserName = this.txtuser.Text.Trim();
                string Password = this.txtpwd.Text.Trim();

                BaseWebService.InitWebService(UserName, Password, url);
                InitLvCoumnsName();
                string weburl = txtWebUrl.Text.Trim();
                string name = this.cmbTitle.Text;//txtname.Text.Trim();

                //XmlNode xmlNode = Lists.GetListContent(name, "0x0100740A6F1D864EA34C8806B773FBDBF66900EE73E01386AC314D98D4EF9671938633");

                //string aa = cmbTitle.SelectedValue.ToString() ;
                if (string.IsNullOrEmpty(name)) { MessageBox.Show("列表名称不能为空"); return; }
                Lists.InitWebService();
                // MyPermissions.SetUrl(weburl);
                Lists.SetUrl(weburl);
                //toolStripStatusLabel1.Text = TNParentWebNode.Tag.ToString();
                //switch (TNSelectedNode.Level)
                //{
                //    case 1:
                //        {
                //            xlLists = Lists.GetList(TNSelectedNode.Text);
                //            ListVersion = xlLists.Attributes["Version"].Value;
                //            ListName = xlLists.Attributes["Title"].Value;
                //            ListDescription = xlLists.Attributes["Description"].Value;
                //            ListGuid = xlLists.Attributes["Name"].Value;
                //            break;
                //        }
                //}

                XmlNode xlweblist = Lists.GetListCollection();
                xlLists = Lists.GetList(name);
                ListVersion = xlLists.Attributes["Version"].Value;
                ListName = xlLists.Attributes["Title"].Value;
                ListDescription = xlLists.Attributes["Description"].Value;
                ListGuid = xlLists.Attributes["Name"].Value;

                if (xlLists != null)
                {
                    XmlNodeList xlList = xlLists.ChildNodes;

                    XmlAttributeCollection xmlAttr = xlLists.Attributes;     //接点属性   
                    SelectListID = xmlAttr.GetNamedItem("ID").Value;

                    for (int i = 0; i < xlList.Count; i++)
                    {
                        XmlNode xlFields = xlList.Item(i);              //节点对象
                        xmlAttr = xlFields.Attributes;     //接点属性           


                        if (xlFields.ChildNodes.Count != 0)
                        {
                            XmlNodeList xlField = xlFields.ChildNodes;


                            for (int j = 0; j < xlField.Count; j++)
                            {

                                string Name = "";
                                string ColName = "";
                                string DisplayName = "";
                                string Type = "";
                                string RowOrdinal = "";

                                XmlNode xlFieldItem = xlField.Item(j);              //节点对象

                                if (xlFieldItem.Name != "Field")
                                    continue;


                                xmlAttr = xlFieldItem.Attributes;                  //接点属性    

                                //排除计算列
                                Type = xmlAttr.GetNamedItem("Type").Value;
                                if (Type == "Computed" || Type == "File" || Type == "Lookup")
                                    continue;

                                Name = xmlAttr.GetNamedItem("Name").Value;
                                ColName = xmlAttr.GetNamedItem("ColName").Value;
                                DisplayName = xmlAttr.GetNamedItem("DisplayName").Value;

                                if (xmlAttr.GetNamedItem("RowOrdinal") != null)
                                    RowOrdinal = xmlAttr.GetNamedItem("RowOrdinal").Value;


                                ListViewItem List = new ListViewItem();

                                List.SubItems[0].Text = DisplayName;

                                List.SubItems.Add(Name);
                                List.SubItems.Add(ColName);
                                List.SubItems.Add(RowOrdinal);
                                this.lvPressions.Items.Add(List);

                            }

                            // 创建一个ListView排序类的对象，并设置listView1的排序器

                            lvwColumnSorter = new ListViewColumnSorter();
                            this.lvPressions.ListViewItemSorter = lvwColumnSorter;


                            lvwColumnSorter.SortColumn = 1;
                            lvwColumnSorter.Order = SortOrder.Ascending;

                            this.lvPressions.Sort();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //string[] strArry = new string[9] { "任务", "日历", "网站资产", "微源", "文档", "母版页样式库", "组合外观", "链接", "通知" };
        //if (strArry.Contains("John")) 

        private void btnJZ_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string url = this.txtUrl.Text.Trim();
                string UserName = this.txtuser.Text.Trim();
                string Password = this.txtpwd.Text.Trim();

                BaseWebService.InitWebService(UserName, Password, url);
                InitLvCoumnsName();

                string weburl = txtWebUrl.Text.Trim();
                Lists.InitWebService();
                Lists.SetUrl(weburl);
                XmlNode xlweblist = Lists.GetListCollection();
                cmbTitle.Items.Clear();
                if (xlweblist != null)
                {

                    treeList.Nodes.Clear();

                    XmlNodeList xlList = xlweblist.ChildNodes;
                    XmlAttributeCollection xmlAttr = xlweblist.Attributes;
                    for (int i = 0; i < xlList.Count; i++)
                    {
                        XmlNode xlFieldItem = xlList.Item(i);
                        xmlAttr = xlFieldItem.Attributes;  //接点属性   
                        string strTitle = xmlAttr.GetNamedItem("Title").Value.Trim();    //

                        if (cbISChekd.Checked)
                        {
                            if (!strArry.Contains(strTitle))
                            {
                                cmbTitle.Items.Add(strTitle);
                                TreeNode tnlist = new TreeNode();
                                tnlist.Text = strTitle;
                                treeList.Nodes.Add(tnlist);

                                XmlNodeList childNodes = Lists.GetListContentTypes(strTitle).ChildNodes;
                                for (int index = 0; index < childNodes.Count; ++index)
                                {
                                    XmlAttributeCollection attributes = childNodes.Item(index).Attributes;
                                    string text = attributes.GetNamedItem("Name").Value;
                                    string str = attributes.GetNamedItem("ID").Value;
                                    TreeNode tnlistContent = new TreeNode();
                                    tnlistContent.Text = text;
                                    tnlistContent.Tag = str;
                                    tnlist.Nodes.Add(tnlistContent);
                                }
                            }
                        }
                        else
                        {
                            cmbTitle.Items.Add(strTitle);
                            TreeNode tnlist = new TreeNode();
                            tnlist.Text = strTitle;
                            treeList.Nodes.Add(tnlist);

                            XmlNodeList childNodes = Lists.GetListContentTypes(strTitle).ChildNodes;
                            for (int index = 0; index < childNodes.Count; ++index)
                            {
                                XmlAttributeCollection attributes = childNodes.Item(index).Attributes;
                                string text = attributes.GetNamedItem("Name").Value;
                                string str = attributes.GetNamedItem("ID").Value;
                                TreeNode tnlistContent = new TreeNode();
                                tnlistContent.Text = text;
                                tnlistContent.Tag = str;
                                tnlist.Nodes.Add(tnlistContent);
                            }

                        }

                    }
                }
                cmbTitle.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void treeWebAndList_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            this.TNmyNode = e.Node;
            this.treeWebAndList.SelectedNode = this.TNmyNode;
            if (this.TNmyNode.Level != 1 || this.TNmyNode.Nodes.Count == 0)
                return;
            this.TNmyNode.Nodes.Clear();
            MyPermissions.SetUrl(this.TNmyNode.Parent.Tag.ToString());
            XmlNodeList childNodes = Lists.GetListContentTypes(this.TNSelectedNode.Text).ChildNodes;
            for (int index = 0; index < childNodes.Count; ++index)
            {
                XmlAttributeCollection attributes = childNodes.Item(index).Attributes;
                string text = attributes.GetNamedItem("Name").Value;
                string str = attributes.GetNamedItem("ID").Value;
                this.TNmyNode.Nodes.Add(new TreeNode(text, 4, 4)
                {
                    Tag = (object)str
                });
            }
        }

        private void treeList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                this.lvPressions.Items.Clear();
                //TNParentWebNode = this.treeWebAndList.SelectedNode;
                //TNSelectedNode = this.treeWebAndList.SelectedNode;
                TNSelectedNode = this.treeList.SelectedNode;


                //while (TNParentWebNode.Level != 0)
                //{
                //    TNParentWebNode = TNParentWebNode.Parent;

                //    if (TNParentWebNode.Level == 1)
                //    {
                //        TNParentListNode = TNParentWebNode;
                //    }
                //}
                //MyPermissions.SetUrl(TNParentWebNode.Tag.ToString());
                //Lists.SetUrl(TNParentWebNode.Tag.ToString());
                //toolStripStatusLabel1.Text = TNParentWebNode.Tag.ToString();
                switch (TNSelectedNode.Level)
                {
                    case 0:
                        {
                            xlLists = Lists.GetList(TNSelectedNode.Text);
                            ListVersion = xlLists.Attributes["Version"].Value;
                            ListName = xlLists.Attributes["Title"].Value;
                            ListDescription = xlLists.Attributes["Description"].Value;
                            ListGuid = xlLists.Attributes["Name"].Value;
                            break;
                        }
                    case 1:
                        //MessageBox.Show(this.TNSelectedNode.Parent.Text + "--" + this.TNSelectedNode.Tag.ToString());
                        this.xlLists = Lists.GetListContentType(this.TNSelectedNode.Parent.Text, this.TNSelectedNode.Tag.ToString());
                        //MessageBox.Show(xlLists.InnerXml);
                        break;
                }

                if (xlLists != null)
                {
                    XmlNodeList childNodes1 = this.xlLists.ChildNodes;
                    XmlAttributeCollection attributes1 = this.xlLists.Attributes;
                    if (this.TNSelectedNode.Level == 2)
                        this.setListID(Lists.GetList(this.TNSelectedNode.Parent.Text));
                    else if (this.TNSelectedNode.Level == 1)
                        this.setListID(this.xlLists);
                    for (int index1 = 0; index1 < childNodes1.Count; ++index1)
                    {
                        XmlNode xmlNode1 = childNodes1.Item(index1);
                        attributes1 = xmlNode1.Attributes;
                        if (xmlNode1.ChildNodes.Count != 0)
                        {
                            XmlNodeList childNodes2 = xmlNode1.ChildNodes;
                            for (int index2 = 0; index2 < childNodes2.Count; ++index2)
                            {
                                string text1 = "";
                                XmlNode xmlNode2 = childNodes2.Item(index2);
                                if (!(xmlNode2.Name != "Field"))
                                {
                                    XmlAttributeCollection attributes2 = xmlNode2.Attributes;
                                    string str1 = attributes2.GetNamedItem("Type").Value;
                                    if (!(str1 == "Computed") && !(str1 == "File") && !(str1 == "Lookup"))
                                    {
                                        string text2 = attributes2.GetNamedItem("Name").Value;
                                        string text3 = attributes2.GetNamedItem("ColName").Value;
                                        string str2 = attributes2.GetNamedItem("DisplayName").Value;
                                        if (attributes2.GetNamedItem("RowOrdinal") != null)
                                            text1 = attributes2.GetNamedItem("RowOrdinal").Value;
                                        ListViewItem listViewItem = new ListViewItem();
                                        listViewItem.SubItems[0].Text = str2;
                                        listViewItem.SubItems.Add(text2);
                                        listViewItem.SubItems.Add(text3);
                                        listViewItem.SubItems.Add(text1);
                                        this.lvPressions.Items.Add(listViewItem);
                                    }
                                }
                            }

                            // 创建一个ListView排序类的对象，并设置listView1的排序器

                            lvwColumnSorter = new ListViewColumnSorter();
                            this.lvPressions.ListViewItemSorter = lvwColumnSorter;


                            lvwColumnSorter.SortColumn = 1;
                            lvwColumnSorter.Order = SortOrder.Ascending;
                            this.lvPressions.Sort();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        public DataTable listViewToDataTable(ListView lv)
        {
            DataTable dt = new DataTable();
            int i, j;
            DataRow dr;
            dt.Clear();
            dt.Columns.Clear();
            //生成DataTable列头
            for (i = 0; i < lv.Columns.Count; i++)
            {
                dt.Columns.Add(lv.Columns[i].Text.Trim(), typeof(String));
            }
            //每行内容
            for (i = 0; i < lv.Items.Count; i++)
            {
                dr = dt.NewRow();
                for (j = 0; j < lv.Columns.Count; j++)
                {
                    dr[j] = lv.Items[i].SubItems[j].Text.Trim();
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            SelectAll(lvPressions, true);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            unSelectAll(lvPressions);
        }

        private void btnNO_Click(object sender, EventArgs e)
        {
            SelectAll(lvPressions, false);
        }

        //全选方法一
        private void SelectAll(ListView lv)
        {
            for (int i = 0; i < lv.CheckedItems.Count; i++)
            {
                //lv.Items[i].Selected = true;
                lv.CheckedItems[i].Checked = true;
            }
        }
        //全选方法一和全不选
        private void SelectAll(ListView lv, bool b)
        {
            for (int i = 0; i < lv.Items.Count; i++)
            {
                lv.Items[i].Checked = b;
                //lv.CheckedItems[].Checked = b;
            }
        }
        //反选
        private void unSelectAll(ListView lv)
        {
            bool b;
            for (int i = 0; i < lv.Items.Count; i++)
            {
                b = !lv.Items[i].Checked;
                lv.Items[i].Checked = b;
            }
        }
    }
}
