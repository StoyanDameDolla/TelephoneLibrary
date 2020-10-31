using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace StoyanStoyanov.TelephoneLibrary
{
    public partial class Phonebook : Form
    {
        //Establish connection to DT Phonebook where table is created dbo.Table;
        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ststoyan\\Documents\\Phonebook.mdf;Integrated Security=True");
        public Phonebook()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Phonebook_Load(object sender, EventArgs e)
        {
            //Change the default starting tab for the form;
            //this.ActiveControl = textBox1;
            //textBox1.Focus();

            DisplayData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Clear();
            textBox3.Text = "";
            textBox4.Clear();
            //Use -1 index to clear the combo box, will show empty records;
            comboBox1.SelectedIndex = -1;

            //After end of data input and data save --> focus box will be First Name text box.
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Closing SQL Server connection;
            connection.Open();
            SqlCommand cmd = new SqlCommand(@"INSERT INTO Phonebook.mdf
            (First, Last, Mobile, Email, Category)
                                    VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + comboBox1.Text + "')", connection);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Transaction successfully completed!");
            //Closing SQL Server connection;
            connection.Close();

            DisplayData();

        }

        public void DisplayData() 
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * From Table", connection);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int num = dataGridView1.Rows.Add();
                dataGridView1.Rows[num].Cells[0].Value = item[0].ToString();
                dataGridView1.Rows[num].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[num].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[num].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[num].Cells[4].Value = item[4].ToString();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            //Rows from Grid once clicked will be populated in the text box fields in the Phonebook form;
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Closing SQL Server connection;
            connection.Open();
            SqlCommand cmd = new SqlCommand(@"INSERT INTO Phonebook.mdf
            (First, Last, Mobile, Email, Category)
                                    VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + comboBox1.Text + "')", connection);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Transaction records successfully deleted!");
            //Closing SQL Server connection;
            connection.Close();

            DisplayData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Closing SQL Server connection;
            connection.Open();
            SqlCommand cmd = new SqlCommand(@"UPDATE Mobiles SET First='" + textBox1.Text + "', Last='" + textBox2.Text + "', Mobile='" + textBox3.Text + "', " +
                "                           Email='" + textBox4.Text + "', Category= '" + comboBox1.Text + "' WHERE (Table = '" + textBox3.Text + "')", connection);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Transaction records successfully updated!");
            //Closing SQL Server connection;
            connection.Close();

            DisplayData();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //Indicate the row(s) from the Grid view which the user is inserting into the text box.
            SqlDataAdapter sda = new SqlDataAdapter("Select * From Table WHERE (Mobile Like '%" + textBox5.Text + "%') Or (First like '%" + textBox5.Text + "%') Or (First like '%" + textBox5.Text + "%')", connection);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int num = dataGridView1.Rows.Add();
                dataGridView1.Rows[num].Cells[0].Value = item[0].ToString();
                dataGridView1.Rows[num].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[num].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[num].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[num].Cells[4].Value = item[4].ToString();
            }
        }
    }
}
