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
    public partial class FrmSch : Form
    {
        public FrmSch()
        {
            InitializeComponent();
        }

        

        private void FrmSch_Load(object sender, EventArgs e)
        {  


            //清空comboBox中的内容
            comboBox1.Items.Clear();
            SqlConnection conn = BaseClass.DBConn.CyCon();
            conn.Open();
            SqlCommand comm = new SqlCommand("select * from tb_User", conn);
            SqlDataReader dr = comm.ExecuteReader();
        
            while (dr.Read())
            { 
                
              comboBox1.Items.Add(dr[1].ToString().Trim());
            
            }
            comboBox1.SelectedIndex = 0;
            dr.Close();
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = comboBox1.SelectedItem.ToString();
            SqlConnection conn = BaseClass.DBConn.CyCon();
            conn.Open();

            
          SqlCommand comm = new SqlCommand("delete from tb_User where UserName='"+str+"'",conn);
            if (comm.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("删除成功");


            }
            else
            {

                MessageBox.Show("删除失败");
            }
         
            conn.Close();

            // 重新读取数据库中用户名

            this.FrmSch_Load(sender,e);
        }

    }
}