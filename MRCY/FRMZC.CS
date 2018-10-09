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
    public partial class FrmZc : Form
    {
        public FrmZc()
        {
            InitializeComponent();
        }

        private void  FrmZc_Load(object sender, EventArgs e)
        {
            txtUser.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtUser.Text== "")
            {
                MessageBox.Show("用户名不能为空");
            }
            else if (texPswd.Text == "")
            {
                MessageBox.Show("密码不能为空");

            }
            else 
            {
                
                SqlConnection conn = BaseClass.DBConn.CyCon();
                conn.Open();
                string sUser = txtUser.Text.Trim();
                string sPwd = texPswd.Text.Trim();
                string str="select UserName from tb_user where UserName='"+sUser+"'";
                SqlCommand comm = new SqlCommand(str,conn);
                int i = Convert.ToInt32(comm.ExecuteScalar());
               if(i>0)
                   {
                     MessageBox.Show ("用户已经存在");
                   }
               else 
                {
                     string stri = "insert into tb_user values('"+sUser+"','"+sPwd+"', '"+2+"')";
                     SqlCommand command = new SqlCommand(stri,conn);
                     int z =command.ExecuteNonQuery();
                     conn.Close();
                   if (z> 0)
                 {

                     MessageBox.Show("注册成功");
                 }
                   else
                 {
                    MessageBox.Show("这册失败");
                
                 }
                }


            }
        }


     
    }
   
}