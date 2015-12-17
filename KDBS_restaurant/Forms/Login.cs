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
    public partial class Login : Form
    {
        public static String username;
        public static String password;

        SqlConnection sqlConn;
        DataTable dataTab;
        DataSet ds = new DataSet();

        String databaseConn = "Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True";

        public Login()
        {
            InitializeComponent();
        }

        //登录按钮
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("用户名不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    String name = textBox1.Text;
                    String pwd = textBox2.Text;
                    sqlConn = new SqlConnection(databaseConn);
                    sqlConn.Open();
                    SqlCommand cmd = new SqlCommand("select Username,Password from Login0 where Username = '" + name + "'and Password = '" + pwd + "'", sqlConn);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    sdr.Read();
                    if (sdr.HasRows)
                    {
                        sqlConn.Close();
                        
                        username = name;
                        password = pwd;

                        MainForm mainForm = new MainForm(username);
                        mainForm.Show();
                        this.Hide();
                        //this.WindowState = FormWindowState.Minimized;
                    }
                    else
                    {
                        textBox1.Text = ""; //清空用户名和密码
                        textBox2.Text = "";
                        MessageBox.Show("用户名或密码错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
