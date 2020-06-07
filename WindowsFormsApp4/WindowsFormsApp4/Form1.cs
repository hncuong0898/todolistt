using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost; User Id=root; Password ='';Database=db_cs1");
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        MySqlCommand command = new MySqlCommand();
        public DataSet ds = new DataSet();
        public String currentid = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetRecords();
        }
        public void GetRecords()
        {
            ds = new DataSet();
            adapter = new MySqlDataAdapter("select * from tbl_task",conn);
            adapter.Fill(ds, "tbl_task");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "tbl_task";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ds = new DataSet();
            var done = 0;
            var checkboxvalues = StatusBox.Checked;
            if (checkboxvalues == true)
            {
                done = 1;
            }
            var query = "insert into tbl_task(`task_name`, `status`,`creat_date`,`due_date`) VALUES('" + textBox1.Text + "','" + done + "','" + CreateDate.Value + "','" + DueDate.Value + "')";
            adapter = new MySqlDataAdapter(query,conn);
            adapter.Fill(ds, "tbl_task");
            textBox1.Clear();
            GetRecords();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ds = new DataSet();
            var done = 0;
            var checkboxvalues = StatusBox.Checked;
            if (checkboxvalues == true)
            {
                done = 1;
            }
            var query = "update tbl_task set `task_name`= '" + textBox1.Text + "',`status` ='" + done + "',`creat_date`='" + CreateDate.Value + "',`due_date`='" + DueDate.Value + "' where id = " + currentid;
            adapter = new MySqlDataAdapter(query, conn);
            adapter.Fill(ds, "tbl_task");
            textBox1.Clear();
            GetRecords();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            currentid = dataGridView1[0, i].Value.ToString();
            textBox1.Text = dataGridView1[1, i].Value.ToString();
            var status = Convert.ToBoolean(dataGridView1[2, i].Value);
            StatusBox.Checked = status;
            var creat_date = Convert.ToDateTime(dataGridView1[3, i].Value);
            CreateDate.Value= creat_date;
            var due_date = Convert.ToDateTime(dataGridView1[4, i].Value);
            DueDate.Value = due_date;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            currentid = dataGridView1[0, i].Value.ToString();
            ds = new DataSet();
            adapter = new MySqlDataAdapter("delete from tbl_task where id = "+currentid,conn);
            adapter.Fill(ds, "tbl_task");
            textBox1.Clear();
            GetRecords();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void loadGrip()
        {
            ds = new DataSet();
            adapter = new MySqlDataAdapter("select * from tbl_task where task_name like '" + textBox1.Text + "'", conn);
            adapter.Fill(ds, "tbl_task");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "tbl_task";
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            loadGrip();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            GetRecords();
        }
    }
}
