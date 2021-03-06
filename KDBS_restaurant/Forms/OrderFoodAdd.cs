﻿using System;
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
    public partial class OrderFoodAdd : Form
    {
        SqlConnection sqlConn;
        DataTable dataTab;
        DataSet ds = new DataSet();
        static String orderPrimaryID = "";
        static String tableID = "";
        static String waiterID = "";
        static String totalPrice = "";

        String databaseConn = "Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True";

        public OrderFoodAdd(String str0, String str1, String str2, String str3)
        {
            InitializeComponent();
            orderPrimaryID = str0;
            tableID = str1;
            waiterID = str2;
            totalPrice = str3;
            Console.WriteLine("orderPrimaryID = " + orderPrimaryID);
        }

        private void OrderFoodAdd_Load(object sender, EventArgs e)
        {
            textBox1.Text = tableID;
            textBox2.Text = waiterID;
            DateTime dt = DateTime.Now;
            textBox3.Text = dt.ToLongTimeString().ToString();
            toolStripStatusLabel1.Text = dt.ToLongDateString().ToString();
            toolStripStatusLabel3.Text = MainForm.username;

            String sqlStr = "select * from OrderDetail0 where OrderPrimaryID='" + orderPrimaryID+"'";
            
            sqlConn = new SqlConnection(databaseConn);
            try
            {
                //将数据库中的数据绑定到DataGridView控件
                //SqlDataAdapter sqlAdap = new SqlDataAdapter("select * from OrderDetail", sqlConn); //创建数据适配器对象
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
                dataGridView1.Columns[0].HeaderCell.Value = "点菜单编号";
                dataGridView1.Columns[1].HeaderCell.Value = "菜品编号";
                //dataGridView1.Columns[1] = new DataGridViewComboBoxCell();
                //dataGridView1.Columns[2].HeaderCell.Value = "原材料名称";
                dataGridView1.Columns[2].HeaderCell.Value = "数量";
                dataGridView1.Columns[3].HeaderCell.Value = "价格";

                /*dataGridView1.Columns[0].DataPropertyName = "OrderPrimaryID";
                dataGridView1.Columns[1].DataPropertyName = "RecipePrimaryID";
                dataGridView1.Columns[2].DataPropertyName = "Price";
                dataGridView1.Columns[3].DataPropertyName = "Number";*/
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

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table = (DataTable)this.dataGridView1.DataSource;

            SqlConnection sqlConnection = new SqlConnection(databaseConn);
            SqlCommand sqlCommand = new SqlCommand("select * from OrderDetail0 where OrderPrimaryID='" + orderPrimaryID + "'", sqlConnection);

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

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table = (DataTable)this.dataGridView1.DataSource;

            table.Rows[dataGridView1.CurrentCell.RowIndex].Delete();

            SqlConnection sqlConnection = new SqlConnection(databaseConn);
            SqlCommand sqlCommand = new SqlCommand("select * from OrderDetail0 where OrderPrimaryID='" + orderPrimaryID + "'", sqlConnection);

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

        //在datagridview每一行最左端显示行号
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dataGridView1.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dataGridView1.RowHeadersDefaultCellStyle.Font, rectangle,
                dataGridView1.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        //数据类型验证
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.Cancel = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InputMoney inputMoney = new InputMoney(orderPrimaryID, tableID, waiterID, totalPrice);
            inputMoney.Show();
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
