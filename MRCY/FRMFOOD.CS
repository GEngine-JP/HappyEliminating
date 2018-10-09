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
    public partial class FrmFood : Form
    {
        public FrmFood()
        {
            InitializeComponent();
        }

        private void FrmFood_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foodname.Text = "";
            txtprice.Text = "";
            foodname.Enabled = true;
            txtprice.Enabled = true;
            textBox1.Enabled = true;
            cboxclass.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button2.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection conn = BaseClass.DBConn.CyCon();
          
            //select foodname ,foodtype ,foodnum,foodprice from tb_food,tb_foodtype where tb_food.foodty=tb_foodtype.foodty  order by id asc
            SqlDataAdapter da = new SqlDataAdapter("select foodname ,foodty ,foodnum,foodprice from tb_food", conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
      
      
            dataGridView1.DataSource = ds.Tables[0];
  
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            foodname.Enabled = false;
            txtprice.Enabled = true;
            cboxclass.Enabled = true;
            textBox1.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;

           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button6.Enabled = false;
            foodname.Enabled = false;
            cboxclass.Enabled = false;
            txtprice.Enabled = false;
            textBox1.Enabled = false;


        }

        private void button3_Click(object sender, EventArgs e)
        {

            SqlConnection conn = BaseClass.DBConn.CyCon();
            conn.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from tb_food where foodname='" + foodname.Text + "'", conn);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            




          
            if (i > 0)
            {
                cmd = new SqlCommand("update tb_food  set foodty='" +cboxclass.SelectedItem.ToString().Trim()+" ',foodnum='" +  textBox1.Text + "',foodprice='" + txtprice.Text + "' where foodname='"+foodname.Text+"' ", conn);
                int x=cmd.ExecuteNonQuery();
                conn.Close();
                if (x > 0)
                {
                    MessageBox.Show("修改成功");
                    this.button5_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("修改失败");
                
                
                }
        
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = true;
                button6.Enabled = false;
                button7.Enabled = true;
                foodname.Enabled = false;
            }
            else
            {
                cmd = new SqlCommand("insert into tb_food (foodname,foodty,foodnum,foodprice) values('" + foodname.Text + "','" +cboxclass.SelectedItem.ToString().Trim()+ "','" + textBox1.Text + "','" + txtprice.Text + "')", conn);
                int x=cmd.ExecuteNonQuery();
                conn.Close();
                if (x > 0)
                {
                    MessageBox.Show("添加成功");
                    this.button5_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("添加失败");
                
                }
                
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = true;
                button6.Enabled = false;
                button7.Enabled = true;
                foodname.Enabled = false;
       
            } 
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection conn = BaseClass.DBConn.CyCon();
            conn.Open();
            SqlCommand cmd = new SqlCommand("delete from tb_food where foodName='" + dataGridView1.SelectedCells[0].Value.ToString() + "'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            this.button5_Click(sender, e);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foodname.Text = dataGridView1.SelectedCells[0].Value.ToString().Trim();
            cboxclass.SelectedItem = dataGridView1.SelectedCells[1].Value.ToString().Trim();
            textBox1.Text = dataGridView1.SelectedCells[2].Value.ToString().Trim();
            txtprice.Text = dataGridView1.SelectedCells[3].Value.ToString().Trim();

            button2.Enabled = true;
            button6.Enabled = true;

        }


    }
}