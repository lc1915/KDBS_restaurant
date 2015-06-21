using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KDBS_restaurant
{
    public partial class ReceiveGoodsAdd : Form
    {
        public ReceiveGoodsAdd()
        {
            InitializeComponent();
        }

        DataSet ds = new DataSet();
        /*SqlDataAdapter da = new SqlDataAdapter();
        SqlCommandBuilder cb = new SqlCommandBuilder();
        SqlCommand cmd = new SqlCommand();
        private string sql = "select * from TEST_Table";
        private string connString = "Data Source=.;User ID=sa;Password=123;Initial Catalog=Test;";*/

        private void ReceiveGoodsAdd_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = MainForm.username;
            DateTime dt = DateTime.Now;
            toolStripStatusLabel1.Text = dt.ToLongDateString().ToString();
            
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=B;Integrated Security=True");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExcelToDataGridView(dataGridView1);
        }

        public void ExcelToDataGridView(DataGridView dgv)
        {
            String path;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Excel文件";
            ofd.FileName = "";
            ofd.Filter = "Excel文件(*.xls)|*.xls";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path = ofd.FileName;
            }
            else
            {
                path = " ";
            }
            
            //根据路径打开一个Excel文件并将数据填充到DataSet中
            //这一块还有bug，需要调
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source = " + path + ";Extended Properties ='Excel 8.0;HDR=NO;IMEX=1'";//HDR=YES 有两个值:YES/NO,表示第一行是否字段名,默认是YES,第一行是字段名
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "select  * from   [sheet1$]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }

    }
}
