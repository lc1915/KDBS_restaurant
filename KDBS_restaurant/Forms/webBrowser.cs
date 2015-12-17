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
    public partial class webBrowser : Form
    {
        public webBrowser()
        {
            InitializeComponent();
        }

        private void webBrowser_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("file:///E:/takeoutWebsite.html");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("file:///E:/takeoutWebsite1.html");
        }
    }
}
