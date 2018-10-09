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
    public partial class frmZhangDan : Form
    {
        public frmZhangDan()
        {
            InitializeComponent();
        }

        private void frmZhangDan_Load(object sender, EventArgs e)
        {
            string str = dateTimePicker1.Value.ToShortDateString();
            
            
            SqlConnection conn = BaseClass.DBConn.CyCon();
            conn.Open();
       
            SqlDataAdapter da = new SqlDataAdapter("select guestname,roomname, sum(price)as allprice,datatime,waitername from tb_zhangdan group by guestname,waitername,roomname,datatime having datatime ='"+str+"'", conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #region 导出gridview中的数据
       
        private void button2_Click(object sender, EventArgs e)
        {

            
            int x = dataGridView1.Rows.Count;
            int y = dataGridView1.Columns.Count;
            int i = 1, j = 1;

            Excel.Application excle = new Excel.Application();
            excle.Application.Workbooks.Add(true);
            excle.Visible = true;
            try 
            
            {
            
          
    
          for (i = 1; i <=x+1; i++)
            {

                 for (j = 1; j <= y ; j++)

                {
                
                    if (i == 1)
                    {
                        excle.Cells[i, j] = dataGridView1.Columns[j-1].HeaderText;
                    }

                    else
                    {
                        excle.Cells[i, j] = dataGridView1.Rows[i-2].Cells[j-1].Value.ToString();
                    }
                }



            }
                 
            
            
            }
            catch(Exception)
            {

                MessageBox.Show("导出失败");
            
            }




        }
        #endregion

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}