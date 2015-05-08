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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //全屏
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;


            //给TreeView中结点赋值（初始化TreeView）
            //根节点命名为node0，node1，node2等，孩子为t00，t01这样依次命名
            TreeNode node0 = new TreeNode("采购管理");
            treeView1.Nodes.Add(node0);
            TreeNode t01 = new TreeNode("采购计划表");
            TreeNode t02 = new TreeNode("采购订单");
            node0.Nodes.Add(t01);//node下的两个子节点。  
            node0.Nodes.Add(t02);

            TreeNode node1 = new TreeNode("库存管理"); //作为根节点。  
            treeView1.Nodes.Add(node1);
            TreeNode t10 = new TreeNode("basilone");
            node1.Nodes.Add(t10);
            TreeNode t11 = new TreeNode("basiltwo");
            node1.Nodes.Add(t11);

            TreeNode node2 = new TreeNode("销售管理"); //作为根节点。  
            treeView1.Nodes.Add(node2);
            TreeNode t20 = new TreeNode("basilone");
            node1.Nodes.Add(t20);
            TreeNode t21 = new TreeNode("basiltwo");
            node1.Nodes.Add(t21);

            TreeNode node3 = new TreeNode("综合查询"); //作为根节点。  
            treeView1.Nodes.Add(node3);
            TreeNode t30 = new TreeNode("basilone");
            node1.Nodes.Add(t30);
            TreeNode t31 = new TreeNode("basiltwo");
            node1.Nodes.Add(t31);

            TreeNode node4 = new TreeNode("系统管理"); //作为根节点。  
            treeView1.Nodes.Add(node4);
            TreeNode t40 = new TreeNode("登录");
            node1.Nodes.Add(t40);
            TreeNode t41 = new TreeNode("权限设置");
            node1.Nodes.Add(t41);

            treeView1.ExpandAll(); //展开所有节点
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            String treenodeStr = e.Node.Text;

            switch (treenodeStr)
            {
                case "采购管理":
                    //aaa
                    break;
                case "库存管理":
                    //bbb
                    break;
                case "销售管理":
                    splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(22, 155, 233);
                    splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(22, 155, 233);
                    break;
                //aaa
                default:
                    //ccc
                    break;
            }
        }
    }
}
