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
    public partial class UrgentPurchaseAdd : Form
    {
        public UrgentPurchaseAdd()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
    }
}
