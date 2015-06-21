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
    public partial class UsernamePassword : Form
    {
        SqlConnection sqlConn;
        DataTable dataTab;
        DataSet ds = new DataSet();

        String databaseConn = "Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True";

        public UsernamePassword(String str, String str0)
        {
            InitializeComponent();
            textBox1.Text = str;
            textBox2.Text = str0;
        }

        private void UsernamePassword_Load(object sender, EventArgs e)
        {

        }

        //保存
        private void button1_Click(object sender, EventArgs e)
        {
            sqlConn = new SqlConnection(databaseConn);
            sqlConn.Open();
            String a1 = textBox1.Text;
            String a2 = textBox2.Text;
            String a3 = textBox3.Text;
            String a4 = textBox4.Text;
            String strSql = "insert into Login0(EmployeeId,Username,Password,DepartmentId) values('"+a1+"','"+a3+"','"+a4+"','"+a2+"')";
            SqlCommand cmd = new SqlCommand(strSql, sqlConn);
            int i = (int)cmd.ExecuteNonQuery();
            sqlConn.Close();

            this.Close();
            MessageBox.Show("保存成功！");
        }
    }
}
