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
        public static String username;

        public MainForm(String str)
        {
            InitializeComponent();
            username = str;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = username;
            DateTime dt = DateTime.Now;
            toolStripStatusLabel1.Text = dt.ToLongDateString().ToString();
            //全屏
            //this.FormBorderStyle = FormBorderStyle.None; //去掉Form的边框
            //this.WindowState = FormWindowState.Maximized;

            //作用：点击叶子节点：打开相应窗口；点击非叶子节点：panal2（KDBS爷爷图片处）显示该模块的流程图
            //给TreeView中结点赋值（初始化TreeView）
            //根节点命名为node0，node1，node2等，孩子为t00，t01这样依次命名
            TreeNode node0 = new TreeNode("采购管理"); //根节点
                                                        //门店的采购管理具体是什么？跟供应商没关系吧
            treeView1.Nodes.Add(node0);
            TreeNode t01 = new TreeNode("采购计划查询"); // 查看门店的采购计划
            TreeNode t010 = new TreeNode("门店补货预测计划查询"); //查看大区给该门店预测的补货计划
            t01.Nodes.Add(t010);
            TreeNode t02 = new TreeNode("采购订单查询");
            TreeNode t03 = new TreeNode("采购统计表查询");
            //上面几个没写
            TreeNode t04 = new TreeNode("紧急订货");  // 紧急订货单放采购管理模块还是库存管理模块？
            node0.Nodes.Add(t04);
            //TreeNode t040 = new TreeNode("紧急订货单新增");
            TreeNode t041 = new TreeNode("紧急订货单");
            //t04.Nodes.Add(t040);
            t04.Nodes.Add(t041);
            node0.Nodes.Add(t01);//node下的两个子节点。  
            node0.Nodes.Add(t02);
            node0.Nodes.Add(t03);

            TreeNode node1 = new TreeNode("库存管理");  
            treeView1.Nodes.Add(node1);
            TreeNode t10 = new TreeNode("收货");
            node1.Nodes.Add(t10);
            //TreeNode t100 = new TreeNode("收货单新增");
            TreeNode t101 = new TreeNode("收货单");
            //t10.Nodes.Add(t100);
            t10.Nodes.Add(t101);
            /*TreeNode t11 = new TreeNode("入库单");
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
            t12.Nodes.Add(t121);*/
            TreeNode t13 = new TreeNode("取料");
            node1.Nodes.Add(t13);
            /*TreeNode t130 = new TreeNode("紧急订货单新增");
            TreeNode t131 = new TreeNode("紧急订货单查询");
            t13.Nodes.Add(t130);
            t13.Nodes.Add(t131);*/
            /*TreeNode t14 = new TreeNode("补货单");
            node1.Nodes.Add(t14);
            TreeNode t140 = new TreeNode("补货单新增");
            TreeNode t141 = new TreeNode("补货单查询");
            t14.Nodes.Add(t140);
            t14.Nodes.Add(t141);*/
            TreeNode t15 = new TreeNode("库存查询"); 
            node1.Nodes.Add(t15);
            TreeNode t150 = new TreeNode("库存统计报表查询（日）"); // 用于生成紧急订货单
            TreeNode t151 = new TreeNode("库存统计报表查询（周）");
            t15.Nodes.Add(t150);
            t15.Nodes.Add(t151);

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
                                                        // 通过修改datagridview单元格的值，修改数据库表中的数据
            TreeNode t2001 = new TreeNode("等位情况"); //显示顾客等位情况，等位单号，等位人数等
            //TreeNode t2002 = new TreeNode("等位单新增"); //新增等位单，包括等位人数等（在等位情况窗口里新增等位单）
            t200.Nodes.Add(t2000);
            t200.Nodes.Add(t2001);
            //t200.Nodes.Add(t2002);
            //TreeNode t2010 = new TreeNode("点菜单新增");
            TreeNode t2011 = new TreeNode("点菜单查询"); // 这里可以结账（对一个单或多个单）
                                                         // 可以更改各个单据的状态，比如已下达（给生产管理），已生产（分出去的几个做菜单都已完成（这个好像有点麻烦））
            TreeNode t2012 = new TreeNode("结账单查询");
            //t201.Nodes.Add(t2010);  
            t201.Nodes.Add(t2011);
            t201.Nodes.Add(t2012);
            TreeNode t21 = new TreeNode("外卖"); // 1.新增外卖订单（可生成结账单，配送成功则顾客确认结账） 
                                                 // 2.系统根据地点、配送员等计算生成配送单
            node2.Nodes.Add(t21);
            TreeNode t210 = new TreeNode("外卖单导入"); //外卖这三个还没写
            TreeNode t211 = new TreeNode("外卖单查询"); //可生成配送单
            //TreeNode t212 = new TreeNode("配送单查询"); //可进行配送单的送达确认
            t21.Nodes.Add(t210);
            t21.Nodes.Add(t211);
            //t21.Nodes.Add(t212);
            TreeNode t22 = new TreeNode("销售计划查询"); 
            node2.Nodes.Add(t22);
            TreeNode t220 = new TreeNode("销售预测计划查询"); //可按周 按月
            t22.Nodes.Add(t220);
            //TreeNode t23 = new TreeNode("日取货配料计划");
            //node2.Nodes.Add(t23);

            TreeNode node3 = new TreeNode("生产管理"); // 包括菜品预处理和做菜，厨师做好后生成送餐单 
            treeView1.Nodes.Add(node3);
            TreeNode t30 = new TreeNode("店内生产");
            node3.Nodes.Add(t30);
            TreeNode t300 = new TreeNode("预处理单查询"); //可更改预处理单状态，如已完成预处理（根据日取货配料计划来）
            t30.Nodes.Add(t300);
            TreeNode t301 = new TreeNode("主厨区处理"); //可更改主厨处理单状态，如已做完菜 
            t30.Nodes.Add(t301);
            TreeNode t3011 = new TreeNode("主食"); // 这里可以结账（对一个单或多个单）
            // 可以更改各个单据的状态，比如已下达（给生产管理），已生产（分出去的几个做菜单都已完成（这个好像有点麻烦））
            TreeNode t3012 = new TreeNode("辅食（浓汤）");
            TreeNode t3013 = new TreeNode("辅食（沙拉）"); 
            t301.Nodes.Add(t3011);
            t301.Nodes.Add(t3012);
            t301.Nodes.Add(t3013);
            TreeNode t302 = new TreeNode("送餐"); 
            t30.Nodes.Add(t302);
            TreeNode t3021 = new TreeNode("送餐界面"); // 这里可以结账（对一个单或多个单）
            // 可以更改各个单据的状态，比如已下达（给生产管理），已生产（分出去的几个做菜单都已完成（这个好像有点麻烦））
            //TreeNode t3022 = new TreeNode("送餐单查询");
            t302.Nodes.Add(t3021);
            //t302.Nodes.Add(t3022);
            TreeNode t33 = new TreeNode("生产计划查询"); 
            node3.Nodes.Add(t33);
            TreeNode t330 = new TreeNode("日取货配料计划查询"); //不仅是查询，还要做计划
            t33.Nodes.Add(t330);

            TreeNode node4 = new TreeNode("综合查询"); // 查各种计划啊报表啊
            treeView1.Nodes.Add(node4);
            TreeNode t40 = new TreeNode("采购查询"); // 门店采购统计报表？
            node4.Nodes.Add(t40);
            TreeNode t41 = new TreeNode("库存查询");
            node4.Nodes.Add(t41);
            TreeNode t410 = new TreeNode("原材料消耗利用情况统计报表");
            t41.Nodes.Add(t410);
            /*TreeNode t411 = new TreeNode("库存统计报表（日）"); // 日库存统计报表在库存管理下，用于生成紧急订货单
            t41.Nodes.Add(t411);*/
            TreeNode t412 = new TreeNode("库存统计报表（周）");
            t41.Nodes.Add(t412);
            TreeNode t42 = new TreeNode("销售查询");
            node4.Nodes.Add(t42);
            TreeNode t421 = new TreeNode("菜品销售查询");
            t42.Nodes.Add(t421);
            TreeNode t43 = new TreeNode("生产查询");
            node4.Nodes.Add(t43);
            TreeNode t44 = new TreeNode("财务查询");
            node4.Nodes.Add(t44);
            TreeNode t440 = new TreeNode("成本利润核算报表（周）");
            t44.Nodes.Add(t440);
            TreeNode t441 = new TreeNode("成本利润核算报表（月）");
            t44.Nodes.Add(t441);
            TreeNode t442 = new TreeNode("营业利润核算表（日）");
            t44.Nodes.Add(t442);
            TreeNode t443 = new TreeNode("营业利润核算表（周）");
            t44.Nodes.Add(t443);
            
            TreeNode node5 = new TreeNode("系统管理");
            treeView1.Nodes.Add(node5);
            TreeNode t50 = new TreeNode("登录");
            node5.Nodes.Add(t50);
            TreeNode t51 = new TreeNode("系统初始化"); // 在里面输各种基础资料
            node5.Nodes.Add(t51);
            TreeNode t510 = new TreeNode("菜品信息初始化");
            t51.Nodes.Add(t510);
            TreeNode t511 = new TreeNode("菜品标准成本卡初始化");
            t51.Nodes.Add(t511);
            TreeNode t512 = new TreeNode("门店桌号信息初始化");
            t51.Nodes.Add(t512);
            TreeNode t513 = new TreeNode("原材料信息初始化");
            t51.Nodes.Add(t513);
            TreeNode t52 = new TreeNode("权限设置/用户管理"); //这两个可以在一起吧？对用户组、用户的增删查改
            node5.Nodes.Add(t52);
            TreeNode t53 = new TreeNode("备份管理"); // 备份
            node5.Nodes.Add(t53);
            TreeNode t54 = new TreeNode("通知管理"); // 发送各种通知
            node5.Nodes.Add(t54);
            TreeNode t55 = new TreeNode("导入大区总菜单"); // 导入大区总菜单（信息系统课设新增）
            node5.Nodes.Add(t55);

            //treeView1.ExpandAll(); //展开所有节点
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            String treenodeStr = e.Node.Text;

            switch (treenodeStr)
            {
                case "门店补货预测计划查询":
                    SupplymentPlanSearch supplymentPlanSearch = new SupplymentPlanSearch();
                    supplymentPlanSearch.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;
                /*case "紧急订货单新增":
                    UrgentPurchaseAdd0 urgentPurchaseAdd0 = new UrgentPurchaseAdd0(null,null);
                    urgentPurchaseAdd0.Show();
                    this.WindowState = FormWindowState.Minimized;
                    //this.Enabled = false;
                    break;*/
                case "紧急订货单":
                    UrgentPurchaseSearch0 urgentPurchaseSearch0 = new UrgentPurchaseSearch0();
                    urgentPurchaseSearch0.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;
                /*case "收货单新增":
                    ReceiveGoodsAdd receiveGoodsAdd = new ReceiveGoodsAdd();
                    receiveGoodsAdd.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;*/
                case "收货单":
                    ReceiveGoodsAdd receiveGoodAdd = new ReceiveGoodsAdd();
                    receiveGoodAdd.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case "取料":
                    TakeoutMaterial takeoutMaterial = new TakeoutMaterial();
                    takeoutMaterial.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case "桌位情况":
                    TableStatus tableStatus = new TableStatus();
                    tableStatus.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case "等位情况":
                    WaitingStatus waitingStatus = new WaitingStatus();
                    waitingStatus.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;
                /*case "点菜单新增":
                    OrderFoodAdd orderFoodAdd = new OrderFoodAdd();
                    orderFoodAdd.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;*/
                case "点菜单查询":
                    OrderFoodSearch orderFoodSearch = new OrderFoodSearch();
                    orderFoodSearch.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case "结账单查询":
                    BillSearch billSearch = new BillSearch();
                    billSearch.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case "外卖单导入":
                    TakeoutInput takeoutInput = new TakeoutInput();
                    takeoutInput.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case "外卖单查询":
                    TakeoutSearch takeoutSearch = new TakeoutSearch();
                    takeoutSearch.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case "预处理单查询":
                    PreProcessSearch preProcessSearch = new PreProcessSearch();
                    preProcessSearch.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;
                /*case "主厨处理单查询":
                    MainProcessSearch mainProcessSearch = new MainProcessSearch();
                    mainProcessSearch.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;*/
                case "主食":
                    Cook1 cook1 = new Cook1();
                    cook1.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case "辅食（浓汤）":
                    Cook2 cook2 = new Cook2();
                    cook2.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case "辅食（沙拉）":
                    Cook3 cook3 = new Cook3();
                    cook3.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case "送餐界面":
                    ServeFood serveFood = new ServeFood();
                    serveFood.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case "送餐单查询":
                    ServeFoodSearch serveFoodSearch = new ServeFoodSearch();
                    serveFoodSearch.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case "菜品销售查询":
                    SellFoodSearch sellFoodSearch = new SellFoodSearch();
                    sellFoodSearch.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case "登录/退出登录":
                    Login login = new Login();
                    login.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case "权限设置/用户管理":
                    Console.WriteLine("username = " + username);
                    if (username.Equals("administrator"))
                    {
                        PermissionSetting0 permissionSetting0 = new PermissionSetting0(null);
                        permissionSetting0.Show();
                        this.WindowState = FormWindowState.Minimized;
                    }
                    else
                    {
                        MessageBox.Show("您没有相应权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                case "菜品信息初始化":
                    InitialRecipePrimary initialRecipePrimary = new InitialRecipePrimary();
                    initialRecipePrimary.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case "门店桌号信息初始化":
                    InitialTable initialTable = new InitialTable();
                    initialTable.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case "原材料信息初始化":
                    InitialMaterial initialMaterial = new InitialMaterial();
                    initialMaterial.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case "导入大区总菜单":
                    InputAllRecipe inputAllRecipe = new InputAllRecipe();
                    inputAllRecipe.Show();
                    this.WindowState = FormWindowState.Minimized;
                    break;
                /*case "销售管理":
                    splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(22, 155, 233);
                    splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(22, 155, 233);
                    break;*/
                default:
                    //ccc
                    break;
            }
        }

        private void 没啦ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定退出系统吗？", "Confirm Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                Close();
            }
        }
    }
}
