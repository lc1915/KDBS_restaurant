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
    public partial class DataGridViewDataComboColumn : DataGridViewColumn
    {
        private object m_dataSoruce = null;
        private string sDisplayMember = "";
        private string sDisplayField = "";
        private string sKeyWords = "";


        public string SDisplayField
        {
            get { return sDisplayField; }
            set { sDisplayField = value; }
        }

        public string SDisplayMember
        {
            get { return sDisplayMember; }
            set { sDisplayMember = value; }
        }

        public string SKeyWords
        {
            get { return sKeyWords; }
            set { sKeyWords = value; }
        }


        public DataGridViewDataComboColumn()
            : base(new DataGridViewDataComboCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewDataComboCell)))
                {
                    throw new InvalidCastException("不是DataGridViewDataComboCell");
                }

                base.CellTemplate = value;
            }
        }
        
        private DataGridViewDataComboCell ComboBoxCellTemplate
        {
            get
            {
                return (DataGridViewDataComboCell)this.CellTemplate;
            }
        }
      
        public Object DataSource
        {
            get
            {
                return m_dataSoruce;

            }
            set
            {
                if (ComboBoxCellTemplate != value)
                {

                    m_dataSoruce = value;
                }
            }
        }

   

    }
}