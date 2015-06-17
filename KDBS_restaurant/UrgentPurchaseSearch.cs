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

        DataSet dataset;
        DataView mainView;
        String[] args;
        int[] headmerge;


        List<Int64> changedRowIndex = new List<Int64>();

        SqlConnection conn;


        public UrgentPurchaseSearch(string[] argss)
        {
            InitializeComponent();
            args = new String[7];
            headmerge = new int[3];
            this.args = argss;

        }

        private void UrgentPurchaseSearch_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True");//args[4]
            dataset = new DataSet();
            this.refreshdatagridview();
            dataGridView1.DataSource = mainView;
            headmerge[0] = 0;
            headmerge[1] = 1;
            headmerge[2] = 2;

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //打开紧急订货单详细查询界面（可以不用紧急订货单新增界面）
            UrgentPurchaseAdd urgentPurchaseAdd = new UrgentPurchaseAdd(null);
            urgentPurchaseAdd.Show();
            this.WindowState = FormWindowState.Minimized;
        }



        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            SolidBrush b = new SolidBrush(this.dataGridView1.RowHeadersDefaultCellStyle.ForeColor);
            e.Graphics.DrawString((e.RowIndex + 1).ToString(System.Globalization.CultureInfo.CurrentUICulture), this.dataGridView1.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);

        }

        public void refreshdatagridview()
        {

            //DataTable   dd =dataGridView1.DataSource as DataTable;
            //  dd.Rows.Clear();
            //  dataGridView1.DataSource = dd;
            SqlCommand sqc = new SqlCommand("Select_EmercyOrder", conn);
            SqlDataAdapter sda = new SqlDataAdapter(sqc);
            sqc.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter sqlparme;

            sqlparme = sqc.Parameters.Add("@district", SqlDbType.Char);
            sqlparme.Direction = ParameterDirection.Input;
            sqlparme.Value = "01";


            conn.Open();
            if (dataset.Tables.Count > 0)
                dataset.Tables[0].Rows.Clear();
            sda.Fill(dataset, "EmercyOrder");
            conn.Close();
            mainView = new DataView(dataset.Tables[0]);
            //dataGridView1.Columns.Clear();
            //   dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns[0].DataPropertyName = "EmercyOrderPrimaryID";
            dataGridView1.Columns[1].DataPropertyName = "Date";
            dataGridView1.Columns[2].DataPropertyName = "MaterialID";
            dataGridView1.Columns[3].DataPropertyName = "MaterialName";
            dataGridView1.Columns[4].DataPropertyName = "Number";
            dataGridView1.Columns[5].DataPropertyName = "Unit";
            //dataGridView1.Columns[6].DataPropertyName = "Checked";

            mainView.Sort = "Date DESC";
        }


        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (!changedRowIndex.Contains(dataGridView1.CurrentCell.RowIndex))
                changedRowIndex.Add(dataGridView1.CurrentCell.RowIndex);
            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[6].Value = 1;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (!changedRowIndex.Contains(dataGridView1.CurrentCell.RowIndex))
                changedRowIndex.Add(dataGridView1.CurrentCell.RowIndex);
            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[6].Value = 0;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Boolean i = true;
            conn.Open();
            foreach (int ri in changedRowIndex)
            {
                Console.Write(Convert.ToString(ri));

                DataGridViewRow dgvr = dataGridView1.Rows[ri];

                Boolean r = alter_checked(dgvr.Cells[0].Value.ToString(), dgvr.Cells[4].Value.ToString(), dgvr.Cells[5].Value.ToString(), Convert.ToBoolean(dataGridView1.Rows[ri].Cells[7].Value));
                i = (r == true ? true : false);

                Console.WriteLine(dgvr.Cells[0].Value.ToString() + dgvr.Cells[4].Value.ToString() + dgvr.Cells[5].Value.ToString() + Convert.ToBoolean(dataGridView1.Rows[ri].Cells[7].Value));



            }
            if (i == true)
                MessageBox.Show("保存成功！");
            else
            {
                MessageBox.Show("保存失败！");
            }
            conn.Close();
        }//

        public bool alter_checked(String planid, String ware, String materialid, Boolean checke)
        {
            SqlCommand sqc = new SqlCommand("Alter_ProcurePlan_Checked", conn);
            sqc.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter sqlparme;

            sqlparme = sqc.Parameters.Add("@ProcurePlanPrimaryID", SqlDbType.Char);
            sqlparme.Direction = ParameterDirection.Input;
            sqlparme.Value = planid;

            sqlparme = sqc.Parameters.Add("@WareHouseID", SqlDbType.Char);
            sqlparme.Direction = ParameterDirection.Input;
            sqlparme.Value = ware;

            sqlparme = sqc.Parameters.Add("@material", SqlDbType.Char);
            sqlparme.Direction = ParameterDirection.Input;
            sqlparme.Value = materialid;

            sqlparme = sqc.Parameters.Add("@checked", SqlDbType.Bit);
            sqlparme.Direction = ParameterDirection.Input;
            sqlparme.Value = checke;


            int effect = sqc.ExecuteNonQuery();
            if (effect > 0)
                return true;
            else
                return false;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Console.WriteLine(dataset.Tables[0].Rows.Count);
            if (MessageBox.Show("Will you Delete it?", "Confirm Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                SqlCommand sqc = new SqlCommand("Alter_ProcurePlan_Deleted", conn);
                sqc.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter sqlparme;

                sqlparme = sqc.Parameters.Add("@EmercyOrderPrimaryID", SqlDbType.Char);
                sqlparme.Direction = ParameterDirection.Input;
                sqlparme.Value = dataGridView1.CurrentCell.OwningRow.Cells[0].Value.ToString();

                sqlparme = sqc.Parameters.Add("@WareHouseID", SqlDbType.Char);
                sqlparme.Direction = ParameterDirection.Input;
                sqlparme.Value = dataGridView1.CurrentCell.OwningRow.Cells[4].Value.ToString();

                sqlparme = sqc.Parameters.Add("@material", SqlDbType.Char);
                sqlparme.Direction = ParameterDirection.Input;
                sqlparme.Value = dataGridView1.CurrentCell.OwningRow.Cells[6].Value.ToString();

                conn.Open();
                int effect = sqc.ExecuteNonQuery();
                conn.Close();
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
                this.refreshdatagridview();
                dataGridView1.Refresh();
                dataGridView1.DataSource = mainView;
                Console.WriteLine(dataset.Tables[0].Rows.Count + "**");
                if (effect > 0)
                    MessageBox.Show("删除成功！");
                else
                    MessageBox.Show("删除失败！ 请检查被删除项目是否处于未审核状态！");

            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            MessageBox.Show("点击了" + ((dataGridView1.CurrentCell.RowIndex) + 1) + "行");
            String orderid = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            DataTable ddt = dataset.Tables[0];
            DataView ddv = new DataView(ddt);
            ddv.RowFilter = "EmercyOrderPrimaryID=" + "\'" + orderid + "\'";
            UrgentPurchaseAdd urgentPurchaseAdd = new UrgentPurchaseAdd(ddv.ToTable());
            this.WindowState = FormWindowState.Minimized;
            urgentPurchaseAdd.Show();

        }

        /*static String EmercyOrderPrimaryID = "";
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
    }*/

    }
}