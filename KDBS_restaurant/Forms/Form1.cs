using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyNewTools;

namespace KDBS_restaurant
{
    public partial class Form1 : Form
    {
        String databaseConn = "Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True";
        String sql = "select * from dbo.RecipePrimary";
        SqlConnection conn = new SqlConnection("Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True");
        DataSet ds;
        DataSet ds2;
        SqlDataAdapter adpt;
        DBOper odb = new DBOper();

        public Form1()
        {
            InitializeComponent();
        }

        // 初始化DataGridView
       private void iniDataGridViewDW()
        {
            DataGridViewColumn column = new DataGridViewDataComboColumn();
            column.Name = "RecipePrimaryID";
            (column as DataGridViewDataComboColumn).SDisplayField = "RecipePrimaryID,Name,Unit,Class,Standard,Price,Comment";
            (column as DataGridViewDataComboColumn).SDisplayMember = "RecipePrimaryID";
            (column as DataGridViewDataComboColumn).SKeyWords = "RecipePrimaryID";
            
            (column as DataGridViewDataComboColumn).DataSource = createTable();
            dataGridView1.Columns.Add(column);     
        }

       //创建下拉框数据源
       private DataTable createTable()
        {
            ds = new DataSet();
            adpt = new SqlDataAdapter("select * from dbo.AllRecipeList", conn);
            adpt.Fill(ds, "total");
  
            DataTable dt = new DataTable();
            dt = ds.Tables["total"];         
            return dt;
        }


        // 获取在下拉框中选择的数据
        void dataWindow1_AfterSelector(object sender, AfterSelectorEventArgs e)
        {
           ;
            DataGridViewRow row = e.Value as DataGridViewRow;
            DataRowView dataRow = row.DataBoundItem as DataRowView;

            int intRowIndex = this.dataGridView1.CurrentCell.RowIndex;
            this.dataGridView1.Rows[intRowIndex].Cells[0].Value = dataRow["RecipePrimaryID"].ToString().Trim();
            this.dataGridView1.Rows[intRowIndex].Cells[2].Value = dataRow["Name"].ToString().Trim();
            this.dataGridView1.Rows[intRowIndex].Cells[3].Value = dataRow["Unit"].ToString().Trim();
            this.dataGridView1.Rows[intRowIndex].Cells[4].Value = dataRow["Class"].ToString().Trim();
            this.dataGridView1.Rows[intRowIndex].Cells[5].Value = dataRow["Standard"];
            this.dataGridView1.Rows[intRowIndex].Cells[6].Value = dataRow["Price"];
            this.dataGridView1.Rows[intRowIndex].Cells[7].Value = dataRow["Comment"];
        }

        // DataGridView中的下拉框选择事件
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is dataCombobox)
            {
                (e.Control as dataCombobox).AfterSelector -= new AfterSelectorEventHandler(SalePageAddOrEditForm_AfterSelector);
                (e.Control as dataCombobox).AfterSelector += new AfterSelectorEventHandler(SalePageAddOrEditForm_AfterSelector);
            }
        }


        // DataGridView中dataCombo选择后事件处理（获取在下拉框中选择的数据）
        void SalePageAddOrEditForm_AfterSelector(object sender, AfterSelectorEventArgs e)
        {
            DataGridViewRow row = e.Value as DataGridViewRow;
            DataRowView dataRow = row.DataBoundItem as DataRowView;
            this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = dataRow["RecipePrimaryID"].ToString().Trim();
            this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = dataRow["RecipePrimaryID"].ToString().Trim();
            this.dataGridView1.Rows[e.RowIndex].Cells[2].Value = dataRow["Name"].ToString().Trim();
            this.dataGridView1.Rows[e.RowIndex].Cells[3].Value = dataRow["Unit"].ToString().Trim();
            this.dataGridView1.Rows[e.RowIndex].Cells[4].Value = dataRow["Class"].ToString().Trim();
            this.dataGridView1.Rows[e.RowIndex].Cells[5].Value = dataRow["Standard"].ToString().Trim();
            this.dataGridView1.Rows[e.RowIndex].Cells[6].Value = dataRow["Price"].ToString().Trim();
            this.dataGridView1.Rows[e.RowIndex].Cells[7].Value = dataRow["Comment"].ToString().Trim();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            iniDataGridViewDW();
            ds2 = new DataSet();  // 创建数据集          
            adpt = new SqlDataAdapter("select * from dbo.RecipePrimary", conn);
            adpt.Fill(ds2, "titless");
            dataGridView1.DataSource = ds2.Tables["titless"];
            //隐藏
            dataGridView1.Columns[1].Visible = false;
            this.dataGridView1.Columns[0].HeaderCell.Value = "菜品编号";
            this.dataGridView1.Columns[2].HeaderCell.Value = "菜品名称";
            this.dataGridView1.Columns[3].HeaderCell.Value = "单位";
            this.dataGridView1.Columns[4].HeaderCell.Value = "类别";
            this.dataGridView1.Columns[5].HeaderCell.Value = "标准";
            this.dataGridView1.Columns[6].HeaderCell.Value = "价格";
            this.dataGridView1.Columns[7].HeaderCell.Value = "备注";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //列宽设为fill

            //使下拉框列显示内容
            for (int i = 0; i < dataGridView1.RowCount;i++ )
            {
                this.dataGridView1.Rows[i].Cells[0].Value  = this.dataGridView1.Rows[i].Cells[1].Value;
            }

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table = (DataTable)this.dataGridView1.DataSource;

            SqlConnection sqlConnection = new SqlConnection(databaseConn);
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);

            SqlDataAdapter sqlAdap = new SqlDataAdapter(sqlCommand);
            SqlCommandBuilder sqlBuilder = new SqlCommandBuilder(sqlAdap);//必须有  

            sqlConnection.Open();
            //sqlAdap.Fill(table);

            //表中必须存在主键，否则无法更新  
            sqlAdap.Update(table);
            ds.AcceptChanges();

            sqlConnection.Close();

            MessageBox.Show("菜品主要信息初始化成功！");
        }

    }
}
