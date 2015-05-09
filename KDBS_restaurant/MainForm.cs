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
            //this.FormBorderStyle = FormBorderStyle.None; //去掉Form的边框
            //this.WindowState = FormWindowState.Maximized;

            //作用：点击叶子节点：打开相应窗口；点击非叶子节点：panal2（KDBS爷爷图片处）显示该模块的流程图
            //给TreeView中结点赋值（初始化TreeView）
            //根节点命名为node0，node1，node2等，孩子为t00，t01这样依次命名
            TreeNode node0 = new TreeNode("采购管理"); //根节点
            treeView1.Nodes.Add(node0);
            TreeNode t01 = new TreeNode("采购计划单"); // 先生成采购计划，然后根据供应商，一个供应商一个采购订单
            TreeNode t02 = new TreeNode("采购订单查询");
            TreeNode t03 = new TreeNode("采购统计表查询"); //是放在这里还是综合查询下面？
            node0.Nodes.Add(t01);//node下的两个子节点。  
            node0.Nodes.Add(t02);
            node0.Nodes.Add(t03);

            TreeNode node1 = new TreeNode("库存管理");  
            treeView1.Nodes.Add(node1);
            TreeNode t10 = new TreeNode("收货单");
            node1.Nodes.Add(t10);
            TreeNode t100 = new TreeNode("收货单新增");
            TreeNode t101 = new TreeNode("收货单查询");
            t10.Nodes.Add(t100);
            t10.Nodes.Add(t101);
            TreeNode t11 = new TreeNode("入库单");
            node1.Nodes.Add(t11);
            TreeNode t110 = new TreeNode("入库单新增");
            TreeNode t111 = new TreeNode("入库单查询");
            t11.Nodes.Add(t110);
            t11.Nodes.Add(t111);
            TreeNode t12 = new TreeNode("退货单");
            node1.Nodes.Add(t12);
            TreeNode t120 = new TreeNode("退货单新增");
            TreeNode t121 = new TreeNode("退货单查询");
            t12.Nodes.Add(t120);
            t12.Nodes.Add(t121);
            TreeNode t13 = new TreeNode("紧急订货单");
            node1.Nodes.Add(t13);
            TreeNode t130 = new TreeNode("退货单新增");
            TreeNode t131 = new TreeNode("退货单查询");
            t13.Nodes.Add(t130);
            t13.Nodes.Add(t131);
            TreeNode t14 = new TreeNode("补货单");
            node1.Nodes.Add(t14);
            TreeNode t140 = new TreeNode("补货单新增");
            TreeNode t141 = new TreeNode("补货单查询");
            t14.Nodes.Add(t140);
            t14.Nodes.Add(t141);
            TreeNode t15 = new TreeNode("xx计划查询"); // 查询猴子那边制定的一些计划
            node1.Nodes.Add(t15);

            TreeNode node2 = new TreeNode("销售管理");   
            treeView1.Nodes.Add(node2);
            TreeNode t20 = new TreeNode("店内销售"); // 1.新增每一桌的点菜单（点菜单分类传至生产管理模块）
                                                        //2.点菜单查询（简单列表式）（有可能可按桌号和时间查询）（这里可以选择多张点菜单一起结账） 
                                                         //3.点进每一个点菜单会有结账按钮，生成结账单
                                                        //4.包括排号模块（主要实体为桌位）
            node2.Nodes.Add(t20);
            TreeNode t200 = new TreeNode("排队等位");
            TreeNode t201 = new TreeNode("店内点餐");
            t20.Nodes.Add(t200);  
            t20.Nodes.Add(t201);
            TreeNode t2000 = new TreeNode("桌位情况"); //显示各桌位是否空闲，桌位属性等
            TreeNode t2001 = new TreeNode("等位情况"); //显示顾客等位情况，等位单号，等位人数等
            TreeNode t2002 = new TreeNode("等位单新增");
            t200.Nodes.Add(t2000);
            t200.Nodes.Add(t2001);
            t200.Nodes.Add(t2002);
            TreeNode t2010 = new TreeNode("点菜单新增");
            TreeNode t2011 = new TreeNode("点菜单查询"); // 这里可以结账（对一个单或多个单）
                                                         // 可以更改各个单据的状态，比如已下达（给生产管理），已生产（分出去的几个做菜单都已完成（这个好像有点麻烦））
            TreeNode t2012 = new TreeNode("结账单查询");
            t201.Nodes.Add(t2010);  
            t201.Nodes.Add(t2011);
            t201.Nodes.Add(t2012);
            TreeNode t21 = new TreeNode("外卖"); // 1.新增外卖订单（可生成结账单，配送成功则顾客确认结账） 
                                                 // 2.系统根据地点、配送员等计算生成配送单
            node2.Nodes.Add(t21);
            TreeNode t210 = new TreeNode("外卖单新增");
            TreeNode t211 = new TreeNode("外卖单查询"); //可生成配送单
            TreeNode t212 = new TreeNode("配送单查询"); //可进行配送单的送达确认
            TreeNode t22 = new TreeNode("销售预测计划"); // 计划这些放在哪儿？
            node2.Nodes.Add(t22);
            TreeNode t23 = new TreeNode("日取货配料计划");
            node2.Nodes.Add(t23);

            TreeNode node3 = new TreeNode("生产管理"); // 包括菜品预处理和做菜，厨师做好后生成送餐单 
            treeView1.Nodes.Add(node3);
            TreeNode t30 = new TreeNode("预处理单查询"); //可更改预处理单状态，如已完成预处理 
            node3.Nodes.Add(t30);
            TreeNode t31 = new TreeNode("主厨处理单查询"); //可更改主厨处理单状态，如已做完菜 
            node3.Nodes.Add(t31);
            TreeNode t32 = new TreeNode("送餐单查询"); //各部分都处理完的点菜单 
            node3.Nodes.Add(t32);

            TreeNode node4 = new TreeNode("综合查询"); // 查各种计划啊报表啊
            treeView1.Nodes.Add(node4);
            TreeNode t40 = new TreeNode("采购查询");
            node4.Nodes.Add(t40);
            TreeNode t41 = new TreeNode("库存查询");
            node4.Nodes.Add(t41);
            TreeNode t42 = new TreeNode("销售查询");
            node4.Nodes.Add(t42);
            TreeNode t43 = new TreeNode("生产查询"); // 这里面能查后厨的什么还没想好
            node4.Nodes.Add(t43);

            TreeNode node5 = new TreeNode("系统管理");
            treeView1.Nodes.Add(node5);
            TreeNode t50 = new TreeNode("登录");
            node5.Nodes.Add(t50);
            TreeNode t51 = new TreeNode("系统初始化");
            node5.Nodes.Add(t51);
            TreeNode t52 = new TreeNode("权限设置/用户管理"); //这两个可以在一起吧？对用户组、用户的增删查改
            node5.Nodes.Add(t52);
            TreeNode t53 = new TreeNode("基础资料");
            node5.Nodes.Add(t53);
            TreeNode t54 = new TreeNode("通知管理"); // 发送各种通知
            node5.Nodes.Add(t54);

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
                case "桌位情况":
                    InsideSale insideSale = new InsideSale();
                    //insideSale.MdiParent = this;
                    insideSale.Show();
                    //this.Hide();
                    this.WindowState = FormWindowState.Minimized; //最小化
                    break;
                default:
                    //ccc
                    break;
            }
        }
    }
}
