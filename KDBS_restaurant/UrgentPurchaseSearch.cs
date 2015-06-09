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
    public partial class UrgentPurchaseSearch : Form
    {
        static String EmercyOrderPrimaryID = "";
        SqlConnection sqlConn;
        DataTable dataTab;

        String databaseConn = "Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True";
        String sql = "select * from EmercyOrderPrimary";
        

        public UrgentPurchaseSearch()
        {
            InitializeComponent();
        }

        private void UrgentPurchaseSearch_Load(object sender, EventArgs e)
        {
            sqlConn = new SqlConnection(databaseConn);
            try
            {
                //将数据库中的数据绑定到DataGridView控件
                SqlDataAdapter sqlAdap = new SqlDataAdapter(sql, sqlConn); //创建数据适配器对象
                DataSet ds = new DataSet(); //创建数据集对象
                sqlAdap.Fill(ds); //填充数据集

                //DataColumn dc_1 = new DataColumn("审核", System.Type.GetType("System.String"));
                //ds.Tables[0].Columns.Add(dc_1);
                dataGridView1.DataSource = ds.Tables[0]; //绑定到数据表
                DataGridViewCheckBoxColumn cobColumn = new DataGridViewCheckBoxColumn();
                dataGridView1.Columns.Add(cobColumn); //增加审核列
                ds.Dispose(); //释放资源

                //对数据库执行sql语句
                sqlConn.Open();
                //TODO: 执行sql语句

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //列宽设为fill

                //改变datagridview标题的文字
                dataGridView1.Columns[0].HeaderCell.Value = "紧急订货单编号";
                dataGridView1.Columns[1].HeaderCell.Value = "门店编号";
                //TODO: 加一列门店名称
                dataGridView1.Columns[2].HeaderCell.Value = "日期";
                dataGridView1.Columns[3].HeaderCell.Value = "备注";
                dataGridView1.Columns[4].HeaderCell.Value = "是否审核";



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
            }
        }

        //单元格双击事件：跳转到紧急订货单的详细表格（主从表中的从表）
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            object objCellValue = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            EmercyOrderPrimaryID = objCellValue.ToString().Trim();

            MessageBox.Show("点击了" + ((dataGridView1.CurrentCell.RowIndex) + 1) + "行" + EmercyOrderPrimaryID);

            //打开紧急订货单详细查询界面（可以不用紧急订货单新增界面）
            UrgentPurchaseAdd urgentPurchaseAdd = new UrgentPurchaseAdd();
            urgentPurchaseAdd.Show();
            this.WindowState = FormWindowState.Minimized;
            //this.Enabled = false;

        }
    }
}
