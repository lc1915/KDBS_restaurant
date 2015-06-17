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
    public partial class UrgentPurchaseAdd : Form
    {
        /*String databaseConn = "Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True";
        String sql = "select * from EmercyOrderDetail join EmercyOrderPrimary on EmercyOrderDetail.EmercyOrderPrimaryID=EmercyOrderPrimary,EmercyOrderPrimaryID";
        //String sql = "select MaterialID, Name, Unit from Material";
        SqlConnection sqlConn;
        DataSet ds = new DataSet();
        List<Int64> changedRowIndex = new List<Int64>();
        //List<KeyValue> material = new List<KeyValue>();*/
        DataSet ds = new DataSet();
        DataTable maintable = new DataTable();
        SqlConnection conn;
        SqlDataAdapter sda;
        SqlDataAdapter sda2;
        List<KeyValue> warehouse = new List<KeyValue>();
        List<KeyValue> material = new List<KeyValue>();


        public UrgentPurchaseAdd(DataTable dt)
        {
            InitializeComponent();
            if (dt == null)
            {
                MessageBox.Show("参数是空");
            }
            else
            {
                MessageBox.Show("参数正常");
                maintable = dt;
                DataRow dr = maintable.Rows[0];
                textBox2.Text = dr[0].ToString();
                textBox1.Text = dr[2].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dr[1].ToString());

            }
        }

        private void UrgentPurchaseAdd_Load(object sender, EventArgs e)
        {
            /*sqlConn = new SqlConnection(databaseConn);
            try
            {
                //将数据库中的数据绑定到DataGridView控件
                SqlDataAdapter sqlAdap = new SqlDataAdapter(sql, sqlConn); //创建数据适配器对象
                DataSet ds = new DataSet(); //创建数据集对象
                sqlAdap.Fill(ds); //填充数据集

                
                //DataColumn dc_1 = new DataColumn("审核", System.Type.GetType("System.String"));
                //ds.Tables[0].Columns.Add(dc_1);
                dataGridView1.DataSource = ds.Tables[0]; //绑定到数据表
                ds.Dispose(); //释放资源

                //对数据库执行sql语句
                sqlConn.Open();
                //TODO: 执行sql语句

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //列宽设为fill

                //改变datagridview标题的文字


                // TODO: 这行代码将数据加载到表“kDBSDataSet.EmercyOrderPrimary”中。您可以根据需要移动或删除它。
                //this.emercyOrderPrimaryTableAdapter.Fill(this.kDBSDataSet.EmercyOrderPrimary);
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("连接数据库失败");
            }
            finally
            {
                sqlConn.Close();
            }*/
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns[1].DataPropertyName = "MaterialName";
            dataGridView1.DataSource = maintable.DefaultView;
            fillgridview();
            conn = new SqlConnection("Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True");//args[4]
            getList();
        }

        public void fillgridview()
        {
            DataGridViewComboBoxCell ddc;
            DataGridViewRow dgvr;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                dgvr = dataGridView1.Rows[i];
                ddc = dgvr.Cells[0] as DataGridViewComboBoxCell;
                ddc.Items.Add("aaaaa");
                //ddc.Items.Add(material[0].getvalue("RD01001"));
            }
        }

        public void getList()
        {
            SqlCommand sqc = new SqlCommand("Select_MaterialList", conn);

            sda = new SqlDataAdapter(sqc);

            sqc.CommandType = System.Data.CommandType.StoredProcedure;

            conn.Open();

            sda.Fill(ds, "Material");
            // 

            /*SqlCommand sqc2 = new SqlCommand("Select_WareHouseList", conn);
            sda2 = new SqlDataAdapter(sqc2);
            sqc2.CommandType = CommandType.StoredProcedure;
            SqlParameter sqlparme;
            sqlparme = sqc2.Parameters.Add("@districtid", SqlDbType.Char);
            sqlparme.Direction = ParameterDirection.Input;
            sqlparme.Value = "01";

            sda2.Fill(ds, "Material");*/

            conn.Close();

            /*foreach (DataRow dr in ds.Tables["WareHouse"].Rows)
            {
                warehouse.Add(new KeyValue(dr[0].ToString(), dr[1].ToString()));
                Console.Write(dr[0].ToString() + dr[1].ToString());
            }*/

            foreach (DataRow dr in ds.Tables["Material"].Rows)
            {
                material.Add(new KeyValue(dr[0].ToString(), dr[1].ToString()));
                Console.Write(" " + dr[0].ToString() + " " + dr[1].ToString());
            }
        }

        //保存到数据库
        /*private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //dataGridView1.AllowUserToAddRows = false;
            //dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 2); //删除最后一行

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

            MessageBox.Show("保存成功！");
        }

        public bool alter_checked(String planid, String ware, String materialid)
        {
            SqlCommand sqc = new SqlCommand("update EmercyOrderDetail.EmercyOrderPrimaryID, EmercyOrderDetail.MaterialID, EmercyOrderDetail.Number from EmercyOrderDetail, Material where EmercyOrderDetail.MaterialID=Material.MaterialID", sqlConn);
            sqc.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter sqlparme;

            sqlparme = sqc.Parameters.Add("@EmercyOrderPrimaryID", SqlDbType.Char);
            sqlparme.Direction = ParameterDirection.Input;
            sqlparme.Value = planid;

            sqlparme = sqc.Parameters.Add("@MaterialID", SqlDbType.Char);
            sqlparme.Direction = ParameterDirection.Input;
            sqlparme.Value = ware;

            sqlparme = sqc.Parameters.Add("@Number", SqlDbType.Char);
            sqlparme.Direction = ParameterDirection.Input;
            sqlparme.Value = materialid;


            int effect = sqc.ExecuteNonQuery();
            if (effect > 0)
                return true;
            else
                return false;
        }

        //删除记录
        private void toolStripButton7_Click(object sender, EventArgs e)
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

            //testB表中必须存在主键，否则无法更新  
            sqlAdap.Update(table);
            ds.AcceptChanges();

            sqlConnection.Close();
            MessageBox.Show("删除成功！");
        }*/
    }

    public class KeyValue
    {
        public KeyValue()
        { }

        public KeyValue(String k, String v)
        {
            key = k;
            value = v;
        }
        public void setvalue(String k, String v)
        {
            key = k;
            value = v;
        }
        public String getvalue(String k)
        {
            return value;
        }
        String key;
        String value;
    }
}
