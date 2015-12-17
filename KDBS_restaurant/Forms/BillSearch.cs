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
    public partial class BillSearch : Form
    {
        SqlConnection sqlConn;
        DataTable dataTab;
        DataSet ds = new DataSet();

        String databaseConn = "Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True";
        String sql = "select * from OrderPrimary";

        public BillSearch()
        {
            InitializeComponent();
        }

        private void BillSearch_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel8.Text = MainForm.username;
            DateTime dt = DateTime.Now;
            toolStripStatusLabel6.Text = dt.ToLongDateString().ToString();

            sqlConn = new SqlConnection(databaseConn);
            try
            {
                //将数据库中的数据绑定到DataGridView控件
                SqlDataAdapter sqlAdap = new SqlDataAdapter(sql, sqlConn); //创建数据适配器对象
                DataSet ds = new DataSet(); //创建数据集对象
                sqlAdap.Fill(ds); //填充数据集

                dataGridView2.DataSource = ds.Tables[0]; //绑定到数据表
                ds.Dispose(); //释放资源

                //对数据库执行sql语句
                sqlConn.Open();
                //TODO: 执行sql语句

                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //列宽设为fill

                //改变datagridview标题的文字
                dataGridView2.Columns[1].HeaderCell.Value = "点菜单编号";
                dataGridView2.Columns[2].HeaderCell.Value = "门店编号";
                dataGridView2.Columns[3].HeaderCell.Value = "时间";
                dataGridView2.Columns[4].HeaderCell.Value = "桌号";
                dataGridView2.Columns[5].HeaderCell.Value = "服务员编号";
                dataGridView2.Columns[6].HeaderCell.Value = "总价";
                dataGridView2.Columns[7].HeaderCell.Value = "备注";
                /*dataGridView1.Columns[0].DataPropertyName = "OrderPrimaryID";
                dataGridView1.Columns[1].DataPropertyName = "StoreID";
                dataGridView1.Columns[2].DataPropertyName = "Time";
                dataGridView1.Columns[3].DataPropertyName = "TableID";
                dataGridView1.Columns[4].DataPropertyName = "WaiterID";
                dataGridView1.Columns[5].DataPropertyName = "TotalPrice";
                dataGridView1.Columns[6].DataPropertyName = "Comment";
                dataGridView1.Columns[7].DataPropertyName = "Checked";*/

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

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value = 1;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value = 0;
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            MessageBox.Show("点击了" + ((dataGridView2.CurrentCell.RowIndex) + 1) + "行");
            String orderPrimaryID = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[1].Value.ToString();
            String tableID = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[4].Value.ToString();
            String waiterID = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[5].Value.ToString();
            String totalPrice = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[6].Value.ToString();
            BillDetail billDetail = new BillDetail(orderPrimaryID, tableID, waiterID, totalPrice, totalPrice);
            billDetail.Show();
            this.WindowState = FormWindowState.Minimized;
        }

        //在datagridview每一行最左端显示行号
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dataGridView2.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dataGridView2.RowHeadersDefaultCellStyle.Font, rectangle,
                dataGridView2.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        //数据类型验证
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.Cancel = true;
        }

    }
}
