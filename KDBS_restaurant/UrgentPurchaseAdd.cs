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
    public partial class UrgentPurchaseAdd : Form
    {
        String databaseConn = "Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True";
        String sql = "select * from EmercyOrderDetail join EmercyOrderPrimary on EmercyOrderDetail.EmercyOrderPrimaryID=EmercyOrderPrimary,EmercyOrderPrimaryID";
        //String sql = "select MaterialID, Name, Unit from Material";
        SqlConnection sqlConn;
        DataSet ds = new DataSet();
        List<Int64> changedRowIndex = new List<Int64>();
        //List<KeyValue> material = new List<KeyValue>();


        public UrgentPurchaseAdd(DataTable dt)
        {
            InitializeComponent();
        }

        private void UrgentPurchaseAdd_Load(object sender, EventArgs e)
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
            }
            

            // TODO: 这行代码将数据加载到表“kDBSDataSet.Material”中。您可以根据需要移动或删除它。
            //this.materialTableAdapter.Fill(this.kDBSDataSet.Material);
            // TODO: 这行代码将数据加载到表“kDBSDataSet.MaterialListID”中。您可以根据需要移动或删除它。
            //this.materialListIDTableAdapter.Fill(this.kDBSDataSet.MaterialListID);
            // TODO: 这行代码将数据加载到表“kDBSDataSet.EmercyOrderDetail”中。您可以根据需要移动或删除它。
            //sthis.emercyOrderDetailTableAdapter.Fill(this.kDBSDataSet.EmercyOrderDetail);

        
        }

        //保存到数据库
        private void toolStripButton3_Click(object sender, EventArgs e)
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
    }
}
