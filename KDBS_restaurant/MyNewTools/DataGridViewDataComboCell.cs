using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;

namespace MyNewTools
{
    public partial class DataGridViewDataComboCell : DataGridViewTextBoxCell
    {
        public override void InitializeEditingControl(int rowIndex, object
              initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            DataGridViewDataComboEditingControl datacomboControl =
                DataGridView.EditingControl as DataGridViewDataComboEditingControl;
            datacomboControl.PopupGridAutoSize = true;
            DataGridViewDataComboColumn datacomboColumn =
                (DataGridViewDataComboColumn)OwningColumn;

            datacomboControl.sDisplayMember = datacomboColumn.SDisplayMember;//以下3句必须放在datasource设置前面
            datacomboControl.sDisplayField = datacomboColumn.SDisplayField;
            datacomboControl.sKeyWords = datacomboColumn.SKeyWords;
            datacomboControl.Text = (string)this.Value;

            datacomboControl.DataSource = datacomboColumn.DataSource;

            

            datacomboControl.RowFilterVisible = true;  //此句必须放在datasource设置后面
        }



        [Browsable(true)]
        public override Type EditType
        {
            get
            {
                return typeof(DataGridViewDataComboEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                return typeof(string);
            }
        }
        private DataGridViewDataComboEditingControl EditingDataWindow
        {
            get
            {
                DataGridViewDataComboEditingControl datacomboControl =
                    DataGridView.EditingControl as DataGridViewDataComboEditingControl;

                return datacomboControl;
            }
        }


    }
}