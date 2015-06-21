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
    public partial class AddDepartment : Form
    {
        public AddDepartment()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String DeName = textBox2.Text;
            PermissionSetting0 permissionSetting0 = new PermissionSetting0(DeName);
            permissionSetting0.Show();
            this.Close();
        }
    }
}
