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
    public partial class InsideSale : Form
    {
        public InsideSale()
        {
            InitializeComponent();
        }

        private void InsideSale_Load(object sender, EventArgs e)
        {

            //给TreeView中结点赋值（初始化TreeView）
            //根节点命名为node0，node1，node2等，孩子为t00，t01这样依次命名
            TreeNode node0 = new TreeNode("店内点餐"); //根节点
            treeView1.Nodes.Add(node0);
            TreeNode t01 = new TreeNode("点菜单新增");
            TreeNode t02 = new TreeNode("点菜单查询"); // 这里可以结账（对一个单或多个单）
            TreeNode t03 = new TreeNode("结账单查询");
            node0.Nodes.Add(t01);//node下的两个子节点。  
            node0.Nodes.Add(t02);
            node0.Nodes.Add(t03);
        }

    }
}
