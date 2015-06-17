using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KDBS_restaurant
{
    public partial class InputMoney : Form
    {

        static String orderPrimaryID = "";
        static String tableID = "";
        static String waiterID = "";
        static String totalPrice = "";

        public InputMoney(String str0, String str1, String str2, String str3)
        {
            InitializeComponent();
            orderPrimaryID = str0;
            tableID = str1;
            waiterID = str2;
            totalPrice = str3;
        }

        private void InputMoney_Load(object sender, EventArgs e)
        {
            textBox1.Text = totalPrice;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String inputMoney = textBox2.Text.ToString();
            BillDetail billDetail = new BillDetail(orderPrimaryID, tableID, waiterID, totalPrice, inputMoney);
            billDetail.Show();
            this.Close();
        }
    }
}
