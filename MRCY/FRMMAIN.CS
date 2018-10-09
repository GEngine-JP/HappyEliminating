using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace MrCy
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        public SqlDataReader sdr;
        public string power;
        public string Names;
        public string Times;
        private void frmMain_Load(object sender, EventArgs e)
        {
                switch (power)
                {
                    case "0": toolStripStatusLabel13.Text = "超级管理员"; break;
                    case "1": toolStripStatusLabel13.Text = "经理"; break;
                    case "牢记密码": toolStripStatusLabel13.Text = "牢记密码"; break;
            //        case "2": toolStripStatusLabel13.Text = "一般用户"; break;
                    default :   toolStripStatusLabel13.Text="一般用户";break;
                }
            toolStripStatusLabel10.Text = Names;
            toolStripStatusLabel16.Text = Times;
         /*   if (power == "2")
            {
                系统维护SToolStripMenuItem.Enabled = false;
                基础信息MToolStripMenuItem.Enabled = false;
            }
         */
            if (power == "0" || power == "牢记密码")
            { 
            
            }
        //    if (power == "牢记密码")
        //    { 
            
         //   }
            else if (power == "1")
            {
                系统维护SToolStripMenuItem.Enabled = false;
            }

            else
            {
                系统维护SToolStripMenuItem.Enabled = false;
                基础信息MToolStripMenuItem.Enabled = false;

            }

            
        }
        private void AddItems(string rzt)
        {
            if (rzt == "使用")
            {
                lvDesk.Items.Add(sdr["RoomName"].ToString(), 1);
            }
            else
            {
                lvDesk.Items.Add(sdr["RoomName"].ToString(), 0);
            }
        }


        private void 开台ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvDesk.SelectedItems.Count != 0)
            {

                string names = lvDesk.SelectedItems[0].SubItems[0].Text;
                frmOpen openroom = new frmOpen();
                openroom.name = names;
                openroom.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选择桌台");
            }
        }

        private void frmMain_Activated(object sender, EventArgs e)
        {
            lvDesk.Items.Clear();
            SqlConnection conn = BaseClass.DBConn.CyCon();
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from tb_Room", conn);
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                string zt = sdr["RoomZT"].ToString().Trim();
                AddItems(zt);
            }
            conn.Close();
        }
        private void 点菜ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvDesk.SelectedItems.Count != 0)
            {
                string names = lvDesk.SelectedItems[0].SubItems[0].Text;
                frmDC dc = new frmDC();
                dc.RName = names;
                dc.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选择桌台");
            }
        }

        private void 消费查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvDesk.SelectedItems.Count != 0)
            {
                string names = lvDesk.SelectedItems[0].SubItems[0].Text;
                frmSerch serch = new frmSerch();
                serch.RName = names;
                serch.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选择桌台");
            }
        }
        private void 结账ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvDesk.SelectedItems.Count != 0)
            {
                string names = lvDesk.SelectedItems[0].SubItems[0].Text;
                frmJZ jz = new frmJZ();
                jz.Rname = names;
                jz.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选择桌台");
            }

        }
        private void lvDesk_DoubleClick(object sender, EventArgs e)
        {
            frmDetails details = new frmDetails();
            details.TableName = lvDesk.SelectedItems[0].SubItems[0].Text;
            details.ShowDialog();
        }
        private void lvDesk_Click(object sender, EventArgs e)
        {
            string names = lvDesk.SelectedItems[0].SubItems[0].Text;
            SqlConnection conn = BaseClass.DBConn.CyCon();
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from tb_Room where RoomName='" + names + "'", conn);
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            string zt = sdr["RoomZT"].ToString().Trim();
            sdr.Close();
            if (zt == "使用")
            {
                this.contextMenuStrip1.Items[0].Enabled = false;
                this.contextMenuStrip1.Items[1].Enabled = true;
                this.contextMenuStrip1.Items[3].Enabled = true;
                this.contextMenuStrip1.Items[5].Enabled = true;
                this.contextMenuStrip1.Items[6].Enabled = true;
            }
            if (zt == "待用")
            {
                this.contextMenuStrip1.Items[0].Enabled = true;
                this.contextMenuStrip1.Items[1].Enabled = false;
                this.contextMenuStrip1.Items[3].Enabled = false;
                this.contextMenuStrip1.Items[5].Enabled = false;
                this.contextMenuStrip1.Items[6].Enabled = false;
            }
            conn.Close();
        }

        private void 取消开台toolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (lvDesk.SelectedItems.Count != 0)
            {
                SqlConnection conn = BaseClass.DBConn.CyCon();
                conn.Open();

                string names = lvDesk.SelectedItems[0].SubItems[0].Text;
                SqlCommand cmd = new SqlCommand("Select * from tb_guestfood where roomname='" + names + "'", conn);
                int i = Convert.ToInt32(cmd.ExecuteScalar());

                if (i > 0)
                {
                    MessageBox.Show("请先结帐!!");

                }
            

               else
              {



                cmd = new SqlCommand("update tb_Room set RoomZT='待用',Num=0 where RoomName='" + names + "'", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("delete from tb_GuestFood where roomname='" + names + "'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                frmMain_Activated(sender, e);
              }
            }
            else
            {
                MessageBox.Show("请选择桌台");

            }
            }
           
        
        private void 桌台信息ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDesk desk = new frmDesk();
            desk.ShowDialog();
        }

        private void 职员信息ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmUser users = new frmUser();
            users.ShowDialog();
        }

        private void 日历ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmCalender calender = new frmCalender();
            calender.ShowDialog();
        }

        private void 记事本ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe");
        }

        private void 计算器ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe");
        }

        private void 权限管理ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
        }

        private void 系统备份ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmBF bf = new frmBF();
            bf.ShowDialog();
        }

        private void 系统恢复ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmHF hf = new frmHF();
            hf.ShowDialog();
        }

        private void 口令设置ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmPwd pwd = new frmPwd();
            pwd.names = Names;
            pwd.ShowDialog();
        }

        private void 锁定系统ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmLock locksystem = new frmLock();
            locksystem.Owner = this;
            locksystem.ShowDialog();
        }

        private void 关于ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.ShowDialog();
        }

        private void 退出系统ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定退出本系统吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void 系统维护SToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 修改用户权限ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmXgQx qx = new frmXgQx();
            qx.ShowDialog();
        }

        private void 注册新用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmZc zc = new FrmZc();
            zc.ShowDialog();
        }

        private void 删除用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSch sch = new FrmSch();
            sch.ShowDialog();
        }

        private void 食品信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFood food = new FrmFood();
            food.ShowDialog();
        }

        private void 查询统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 收入日报ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmZhangDan zhdan = new frmZhangDan();
            zhdan.ShowDialog();
        }
    }
}