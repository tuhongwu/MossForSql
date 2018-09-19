using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace WindowsMin.User.Common
{
    class ListViewColumnSorter : IComparer
    {
        private int ColumnToSort;// ָ�������ĸ�������      
        private SortOrder OrderOfSort;// ָ������ķ�ʽ               
        private CaseInsensitiveComparer ObjectCompare;// ����CaseInsensitiveComparer�����
        public ListViewColumnSorter()// ���캯��
        {
            ColumnToSort = 0;// Ĭ�ϰ���һ������            
            OrderOfSort = SortOrder.None;// ����ʽΪ������            
            ObjectCompare = new CaseInsensitiveComparer();// ��ʼ��CaseInsensitiveComparer�����
        }
        // ��дIComparer�ӿ�.        
        // <returns>�ȽϵĽ��.�����ȷ���0�����x����y����1�����xС��y����-1</returns>
        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;
            // ���Ƚ϶���ת��ΪListViewItem����
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;
            // �Ƚ�
            compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);
            // ��������ıȽϽ��������ȷ�ıȽϽ��
            if (OrderOfSort == SortOrder.Ascending)
            {   // ��Ϊ��������������ֱ�ӷ��ؽ��
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {  // ����Ƿ�����������Ҫȡ��ֵ�ٷ���
                return (-compareResult);
            }
            else
            {
                // �����ȷ���0
                return 0;
            }
        }
        /// ��ȡ�����ð�����һ������.        
        public int SortColumn
        {
            set
            {
                ColumnToSort = value;
            }
            get
            {
                return ColumnToSort;
            }
        }
        /// ��ȡ����������ʽ.    
        public SortOrder Order
        {
            set
            {
                OrderOfSort = value;
            }
            get
            {
                return OrderOfSort;
            }
        }
    }
}
