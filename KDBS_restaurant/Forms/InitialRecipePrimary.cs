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
    public partial class InitialRecipePrimary : Form
    {
        String databaseConn = "Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True";
        String sql = "select * from dbo.Table_2";
        SqlConnection conn = new SqlConnection("Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True");
        DataSet ds;
        DataSet ds2;
        SqlDataAdapter adpt;
        DBOper odb = new DBOper();

        public InitialRecipePrimary()
        {
            InitializeComponent();
        }

        // 初始化DataGridView
        private void iniDataGridViewDW()
        {
            DataGridViewColumn column = new DataGridViewDataComboColumn();
            column.Name = "title_id";
            (column as DataGridViewDataComboColumn).SDisplayField = "title_id,title,type,price,ytd_sales,notes,pubdate";
            (column as DataGridViewDataComboColumn).SDisplayMember = "title_id";
            (column as DataGridViewDataComboColumn).SKeyWords = "title_id";

            (column as DataGridViewDataComboColumn).DataSource = createTable();
            dataGridView1.Columns.Add(column);
        }

        //创建下拉框数据源
        private DataTable createTable()
        {
            ds = new DataSet();
            adpt = new SqlDataAdapter("select * from dbo.Table_2", conn);
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
            this.dataGridView1.Rows[intRowIndex].Cells[0].Value = dataRow["title_id"].ToString().Trim();
            this.dataGridView1.Rows[intRowIndex].Cells[2].Value = dataRow["title"].ToString().Trim();
            this.dataGridView1.Rows[intRowIndex].Cells[3].Value = dataRow["type"].ToString().Trim();
            this.dataGridView1.Rows[intRowIndex].Cells[4].Value = dataRow["price"].ToString().Trim();
            this.dataGridView1.Rows[intRowIndex].Cells[5].Value = dataRow["ytd_sales"];
            this.dataGridView1.Rows[intRowIndex].Cells[6].Value = dataRow["notes"];
            this.dataGridView1.Rows[intRowIndex].Cells[7].Value = dataRow["pubdate"];
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
            this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = dataRow["title_id"].ToString().Trim();
            this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = dataRow["title_id"].ToString().Trim();
            this.dataGridView1.Rows[e.RowIndex].Cells[2].Value = dataRow["title"].ToString().Trim();
            this.dataGridView1.Rows[e.RowIndex].Cells[3].Value = dataRow["type"].ToString().Trim();
            this.dataGridView1.Rows[e.RowIndex].Cells[4].Value = dataRow["price"].ToString().Trim();
            this.dataGridView1.Rows[e.RowIndex].Cells[5].Value = dataRow["ytd_sales"].ToString().Trim(); ;
            this.dataGridView1.Rows[e.RowIndex].Cells[6].Value = dataRow["notes"].ToString().Trim(); ;
            this.dataGridView1.Rows[e.RowIndex].Cells[7].Value = dataRow["pubdate"].ToString().Trim(); ;
        }

        private void InitialRecipePrimary_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = MainForm.username;
            DateTime dt = DateTime.Now;
            toolStripStatusLabel1.Text = dt.ToLongDateString().ToString();
            
            /*sqlConn = new SqlConnection(databaseConn);
            try
            {
                //将数据库中的数据绑定到DataGridView控件
                SqlDataAdapter sqlAdap = new SqlDataAdapter(sql, sqlConn); //创建数据适配器对象
                DataSet ds = new DataSet(); //创建数据集对象
                sqlAdap.Fill(ds); //填充数据集

                dataGridView1.DataSource = ds.Tables[0]; //绑定到数据表
                ds.Dispose(); //释放资源

                //对数据库执行sql语句
                sqlConn.Open();
                //TODO: 执行sql语句

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //列宽设为fill

                //改变datagridview标题的文字
                dataGridView1.Columns[0].HeaderCell.Value = "菜品编号";
                dataGridView1.Columns[1].HeaderCell.Value = "菜品名称";
                dataGridView1.Columns[2].HeaderCell.Value = "单位";
                dataGridView1.Columns[3].HeaderCell.Value = "类别";
                dataGridView1.Columns[4].HeaderCell.Value = "标准";
                dataGridView1.Columns[5].HeaderCell.Value = "价格";
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("连接数据库失败");
            }
            finally
            {
                sqlConn.Close();
            }*/

            iniDataGridViewDW();
            ds2 = new DataSet();  // 创建数据集          
            adpt = new SqlDataAdapter("select * from dbo.Table_1", conn);
            adpt.Fill(ds2, "titless");
            dataGridView1.DataSource = ds2.Tables["titless"];
            //隐藏
            dataGridView1.Columns[1].Visible = false;
            this.dataGridView1.Columns[0].HeaderCell.Value = "图书编号";
            this.dataGridView1.Columns[2].HeaderCell.Value = "图书名称";
            this.dataGridView1.Columns[3].HeaderCell.Value = "图书种类";
            this.dataGridView1.Columns[4].HeaderCell.Value = "图书价格";
            this.dataGridView1.Columns[5].HeaderCell.Value = "累计销量";
            this.dataGridView1.Columns[6].HeaderCell.Value = "信息备注";
            this.dataGridView1.Columns[7].HeaderCell.Value = "出版日期";

            //使下拉框列显示内容
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                this.dataGridView1.Rows[i].Cells[0].Value = this.dataGridView1.Rows[i].Cells[1].Value;
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

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table = (DataTable)this.dataGridView1.DataSource;

            table.Rows[dataGridView1.CurrentCell.RowIndex].Delete();

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
            MessageBox.Show("删除成功！");
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            /*MessageBox.Show("点击了" + ((dataGridView1.CurrentCell.RowIndex) + 1) + "行");
            String recipePrimaryID = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            InitialRecipeDetail initialRecipeDetail = new InitialRecipeDetail(recipePrimaryID);
            initialRecipeDetail.Show();
            this.WindowState = FormWindowState.Minimized;*/
            
        }

    }
}
