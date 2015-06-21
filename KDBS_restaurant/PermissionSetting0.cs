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
    public partial class PermissionSetting0 : Form
    {
        SqlConnection sqlConn;
        DataTable dataTab;
        DataSet ds = new DataSet();
        String addDepart;
        String strNode;
        int nodeInt;

        String databaseConn = "Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True";

        public PermissionSetting0(String str)
        {
            InitializeComponent();
            addDepart = str;
        }

        private void PermissionSetting0_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = MainForm.username;
            DateTime dt = DateTime.Now;
            toolStripStatusLabel1.Text = dt.ToLongDateString().ToString();
            
            TreeNode tn = treeView1.Nodes.Add("用户组");

                sqlConn = new SqlConnection(databaseConn);
                //将数据库中的数据绑定到DataGridView控件
            /*SqlDataAdapter sqlAdap = new SqlDataAdapter("select Name from Department", sqlConn); //创建数据适配器对象

                DataSet ds = new DataSet(); //创建数据集对象
                sqlAdap.Fill(ds); //填充数据集*/

                sqlConn.Open();
                SqlCommand cmd = new SqlCommand("select Name from Department", sqlConn);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    tn.Nodes.Add(sdr["Name"].ToString());
                }
            sdr.Close();
            sqlConn.Close();
            

            if (addDepart != null)
            {
                tn.Nodes.Add(addDepart);
            }

            treeView1.ExpandAll();
        }

        //删除用户
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table = (DataTable)this.dataGridView1.DataSource;

            table.Rows[dataGridView1.CurrentCell.RowIndex].Delete();

            SqlConnection sqlConnection = new SqlConnection(databaseConn);
            SqlCommand sqlCommand = new SqlCommand("select * from Employee", sqlConnection);

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

        //保存
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table = (DataTable)this.dataGridView1.DataSource;

            SqlConnection sqlConnection = new SqlConnection(databaseConn);
            SqlCommand sqlCommand = new SqlCommand("select * from Employee", sqlConnection);

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

        //新增用户组
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddDepartment addDepartment = new AddDepartment();
            addDepartment.Show();
            this.Close();
        }

        //根据treeview中选择的用户组，在右边datagridview中显示对应用户组的员工信息
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            strNode = e.Node.Text;
            nodeInt = e.Node.Index;
            Console.Write("nodeInt = " + nodeInt);

            if (strNode == "门店经理")
            {
                String sqlStr = "select * from Employee where DepartmentID='DE01'";

                sqlConn = new SqlConnection(databaseConn);
                try
                {
                    //将数据库中的数据绑定到DataGridView控件
                    SqlDataAdapter sqlAdap = new SqlDataAdapter(sqlStr, sqlConn); //创建数据适配器对象

                    DataSet ds = new DataSet(); //创建数据集对象
                    sqlAdap.Fill(ds); //填充数据集

                    dataGridView1.DataSource = ds.Tables[0]; //绑定到数据表
                    ds.Dispose(); //释放资源

                    //对数据库执行sql语句
                    sqlConn.Open();
                    //TODO: 执行sql语句

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //列宽设为fill

                    //改变datagridview标题的文字
                    /*dataGridView1.Columns[0].HeaderCell.Value = "点菜单编号";
                    dataGridView1.Columns[1].HeaderCell.Value = "菜品编号";
                    dataGridView1.Columns[2].HeaderCell.Value = "数量";
                    dataGridView1.Columns[3].HeaderCell.Value = "价格";*/
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
            else if (strNode == "门店员工")
            {
                String sqlStr = "select * from Employee where DepartmentID='DE02'";

                sqlConn = new SqlConnection(databaseConn);
                try
                {
                    //将数据库中的数据绑定到DataGridView控件
                    SqlDataAdapter sqlAdap = new SqlDataAdapter(sqlStr, sqlConn); //创建数据适配器对象

                    DataSet ds = new DataSet(); //创建数据集对象
                    sqlAdap.Fill(ds); //填充数据集

                    dataGridView1.DataSource = ds.Tables[0]; //绑定到数据表
                    ds.Dispose(); //释放资源

                    //对数据库执行sql语句
                    sqlConn.Open();
                    //TODO: 执行sql语句

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //列宽设为fill

                    //改变datagridview标题的文字
                    /*dataGridView1.Columns[0].HeaderCell.Value = "点菜单编号";
                    dataGridView1.Columns[1].HeaderCell.Value = "菜品编号";
                    dataGridView1.Columns[2].HeaderCell.Value = "数量";
                    dataGridView1.Columns[3].HeaderCell.Value = "价格";*/
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
        }

        
        //移除用户组
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            
            //treeView1.Nodes.RemoveAt(nodeInt);
            treeView1.Nodes.Remove(treeView1.SelectedNode);
        }

        //双击datagridview行，设置员工的用户名和密码
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //MessageBox.Show("点击了" + ((dataGridView1.CurrentCell.RowIndex) + 1) + "行");
            String employeeID = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            String departmentId = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value.ToString();
            UsernamePassword usernamePassword = new UsernamePassword(employeeID, departmentId);
            usernamePassword.Show();
            this.WindowState = FormWindowState.Minimized;

        }
    }
}
