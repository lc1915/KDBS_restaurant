using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KDBS_restaurant
{
    public partial class InitialRecipeDetail : Form
    {
        SqlConnection sqlConn;
        DataTable dataTab;
        DataSet ds = new DataSet();
        static String recipePrimaryID = "";

        String databaseConn = "Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True";
        //String sql = "select * from RecipeDetail where RecipeDetail.RecipePrimaryID = RecipePrimary.RecipePrimaryID";
        //String sql = "select * from RecipeDetail where RecipePrimaryID=" + recipePrimaryID;
        

        List<Int64> changedRowIndex = new List<Int64>();

        public InitialRecipeDetail(String str)
        {
            InitializeComponent();
            recipePrimaryID = str;
            Console.WriteLine("recipePrimaryID = " + recipePrimaryID);
        }

        private void InitialRecipeDetail_Load(object sender, EventArgs e)
        {
            
            sqlConn = new SqlConnection(databaseConn);
            try
            {
                //将数据库中的数据绑定到DataGridView控件
                SqlDataAdapter sqlAdap = new SqlDataAdapter("select * from RecipeDetail where RecipePrimaryID=" + recipePrimaryID, sqlConn); //创建数据适配器对象
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
                dataGridView1.Columns[1].HeaderCell.Value = "原材料编号";
                //dataGridView1.Columns[2].HeaderCell.Value = "原材料名称";
                dataGridView1.Columns[2].HeaderCell.Value = "单位";
                dataGridView1.Columns[3].HeaderCell.Value = "数量";
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("连接数据库失败");
            }
            finally
            {
                sqlConn.Close();
            }
            /*SqlCommand sqc = new SqlCommand("Select_RecipeDetail", sqlConn);
            SqlDataAdapter sda = new SqlDataAdapter(sqc);
            sqc.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter sqlparme;

            sqlparme = sqc.Parameters.Add("@district", SqlDbType.Char);
            sqlparme.Direction = ParameterDirection.Input;
            sqlparme.Value = "01";


            sqlConn.Open();
            if (ds.Tables.Count > 0)
                ds.Tables[0].Rows.Clear();
            sda.Fill(ds, "RecipeDetail");
            sqlConn.Close();
            DataView mainView = new DataView(ds.Tables[0]);
            //dataGridView1.Columns.Clear();
            //   dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns[0].DataPropertyName = "RecipePrimaryID";
            dataGridView1.Columns[1].DataPropertyName = "MaterialID";
            dataGridView1.Columns[2].DataPropertyName = "MaterialName";
            dataGridView1.Columns[3].DataPropertyName = "Unit";
            dataGridView1.Columns[4].DataPropertyName = "Number";*/
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table = (DataTable)this.dataGridView1.DataSource;

            SqlConnection sqlConnection = new SqlConnection(databaseConn);
            SqlCommand sqlCommand = new SqlCommand("select * from RecipeDetail where RecipePrimaryID=" + recipePrimaryID, sqlConnection);

            SqlDataAdapter sqlAdap = new SqlDataAdapter(sqlCommand);
            SqlCommandBuilder sqlBuilder = new SqlCommandBuilder(sqlAdap);//必须有  

            sqlConnection.Open();
            //sqlAdap.Fill(table);

            //表中必须存在主键，否则无法更新  
            sqlAdap.Update(table);
            ds.AcceptChanges();

            sqlConnection.Close();

            MessageBox.Show("菜品详细信息初始化成功！");

           /* Boolean i = true;
            sqlConn.Open();
            foreach (int ri in changedRowIndex)
            {
                Console.Write(Convert.ToString(ri));

                DataGridViewRow dgvr = dataGridView1.Rows[ri];

                Boolean r = alter_checked(dgvr.Cells[0].Value.ToString(), dgvr.Cells[1].Value.ToString(), dgvr.Cells[4].Value.ToString());
                i = (r == true ? true : false);

                Console.WriteLine(dgvr.Cells[0].Value.ToString() + dgvr.Cells[1].Value.ToString() + dgvr.Cells[4].Value.ToString());
            }
            if (i == true)
                MessageBox.Show("保存成功！");
            else
            {
                MessageBox.Show("保存失败！");
            }
            sqlConn.Close();*/
        }

        public bool alter_checked(String planid, String materialid, String number)
        {
            SqlCommand sqc = new SqlCommand("Alter_RecipeDetail_Checked", sqlConn);
            sqc.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter sqlparme;

            sqlparme = sqc.Parameters.Add("@RecipePrimaryID", SqlDbType.Char);
            sqlparme.Direction = ParameterDirection.Input;
            sqlparme.Value = planid;

            sqlparme = sqc.Parameters.Add("@material", SqlDbType.Char);
            sqlparme.Direction = ParameterDirection.Input;
            sqlparme.Value = materialid;


            int effect = sqc.ExecuteNonQuery();
            if (effect > 0)
                return true;
            else
                return false;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table = (DataTable)this.dataGridView1.DataSource;

            table.Rows[dataGridView1.CurrentCell.RowIndex].Delete();

            SqlConnection sqlConnection = new SqlConnection(databaseConn);
            SqlCommand sqlCommand = new SqlCommand("select * from RecipeDetail where RecipePrimaryID=" + recipePrimaryID, sqlConnection);

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
    }
}
