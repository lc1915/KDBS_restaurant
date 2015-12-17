using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KDBS_restaurant
{
    public partial class InputAllRecipe : Form
    {
        SqlConnection sqlConn;
        DataTable dataTab;
        DataSet ds = new DataSet();

        String databaseConn = "Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True";
        String sql = "select * from AllRecipeList";

        public InputAllRecipe()
        {
            InitializeComponent();
        }

        private void InputAllRecipe_Load(object sender, EventArgs e)
        {
            sqlConn = new SqlConnection(databaseConn);
            try
            {
                //将数据库中的数据绑定到DataGridView控件
                SqlDataAdapter sqlAdap = new SqlDataAdapter(sql, sqlConn); //创建数据适配器对象
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
                dataGridView1.Columns[6].HeaderCell.Value = "备注";
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("连接数据库失败");
            }
            finally
            {
                sqlConn.Close();
            }
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
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source = " + path + ";Extended Properties ='Excel 8.0;HDR=NO;IMEX=1'";//HDR=YES 有两个值:YES/NO,表示第一行是否字段名,默认是YES,第一行是字段名
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            //ds = null;
            strExcel = "select  * from   [sheet1$]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");
            // dataGridView1.DataSource = ds.Tables[0].DefaultView;
            dataGridView1.DataSource = ds.Tables[0];

            //改变datagridview标题的文字
            dataGridView1.Columns[0].HeaderCell.Value = "菜品编号";
            dataGridView1.Columns[1].HeaderCell.Value = "菜品名称";
            dataGridView1.Columns[2].HeaderCell.Value = "单位";
            dataGridView1.Columns[3].HeaderCell.Value = "类别";
            dataGridView1.Columns[4].HeaderCell.Value = "标准";
            dataGridView1.Columns[5].HeaderCell.Value = "价格";
            dataGridView1.Columns[6].HeaderCell.Value = "备注";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table = (DataTable)this.dataGridView1.DataSource;
            table.Columns[0].ColumnName = "RecipePrimaryID";
            table.Columns[1].ColumnName = "Name";
            table.Columns[2].ColumnName = "Unit";
            table.Columns[3].ColumnName = "Class";
            table.Columns[4].ColumnName = "Standard";
            table.Columns[5].ColumnName = "Price";
            table.Columns[6].ColumnName = "Comment";

            /*SqlConnection sqlConnection = new SqlConnection(databaseConn);
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);

            SqlDataAdapter sqlAdap = new SqlDataAdapter(sqlCommand);
            SqlCommandBuilder sqlBuilder = new SqlCommandBuilder(sqlAdap);//必须有  

            sqlConnection.Open();
            //sqlAdap.Fill(table);

            //表中必须存在主键，否则无法更新  
            sqlAdap.Update(table);
            ds.AcceptChanges();

            sqlConnection.Close();*/

            SqlConnection sqlConnection = new SqlConnection(databaseConn);
            sqlConnection.Open();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                string sqlStr = "insert into AllRecipeList (RecipePrimaryID,Name,Unit,Class,Standard,Price,Comment) values ('" + dataGridView1.Rows[i].Cells[0].Value + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + dataGridView1.Rows[i].Cells[3].Value + "','" + dataGridView1.Rows[i].Cells[4].Value + "','" + dataGridView1.Rows[i].Cells[5].Value + "','" + dataGridView1.Rows[i].Cells[6].Value + "')";
                SqlCommand cmd = new SqlCommand(sqlStr, sqlConnection);
                cmd.ExecuteNonQuery();
            }
            sqlConnection.Close();
            MessageBox.Show("大区总菜单初始化成功！");
        }
    }
}
