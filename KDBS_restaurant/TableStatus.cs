﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KDBS_restaurant
{
    public partial class TableStatus : Form
    {
        public TableStatus()
        {
            InitializeComponent();
        }

        private void TableStatus_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = MainForm.username;
            DateTime dt = DateTime.Now;
            toolStripStatusLabel1.Text = dt.ToLongDateString().ToString();
        }
    }
}
