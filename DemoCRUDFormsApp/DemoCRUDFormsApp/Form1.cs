using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoCRUDFormsApp
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-TPOPQBT;Initial Catalog=DemoCRUD;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
            GetStudentRecord();
        }
      


      
        private void Form1_Load(object sender, EventArgs e)
        {
            loadBinding();
        }
        private void GetStudentRecord()
        {
            
            SqlCommand com = new SqlCommand("SELECT * FROM tbl_Students", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = com.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            StudentRecordData.DataSource = dt;
        }

        private bool IsValidData()
        {
            if(txtHName.Text == string.Empty 
                || txtTName.Text == string.Empty 
                || txtAddress.Text == string.Empty 
                || string.IsNullOrEmpty(txtSDT.Text) 
                || string.IsNullOrEmpty(txtSBD.Text)){
                MessageBox.Show("Có chỗ chưa nhập dữ liệu !!!", "Lỗi dữ diệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        
        private void StudentRecordData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        void loadBinding()
        {
            txtHName.DataBindings.Add(new Binding("Text", StudentRecordData.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txtTName.DataBindings.Add(new Binding("Text", StudentRecordData.DataSource, "FatherName", true, DataSourceUpdateMode.Never));
            txtSBD.DataBindings.Add(new Binding("Text", StudentRecordData.DataSource, "RollNumber", true, DataSourceUpdateMode.Never));
            txtAddress.DataBindings.Add(new Binding("Text", StudentRecordData.DataSource, "Address", true, DataSourceUpdateMode.Never));
            txtSDT.DataBindings.Add(new Binding("Text", StudentRecordData.DataSource, "Mobile", true, DataSourceUpdateMode.Never));
        }
        private void btnCapnhat_Click(object sender, EventArgs e)
        {
            if (StudentRecordData.SelectedCells.Count > 0)
            {
                string query = String.Format("update tbl_Students set Name = N'{0}', FatherName = N'{1}', RollNumber = {2}, Address = '{3}' where Mobile = '{4}'", txtHName.Text, txtTName.Text, txtSBD.Text, txtAddress.Text, txtSDT.Text);
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (StudentRecordData.SelectedCells.Count > 0)
            {
                string query = String.Format("delete from tbl_Students where RollNumber = '{0}'", txtSBD.Text);
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            
                if (DialogResult.Yes == MessageBox.Show("Bạn có chắc muốn thoát không?", "Thoát", MessageBoxButtons.YesNo))

            Application.Exit();
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            string query = "insert into tbl_Students values " +
                    "(@Name, @Fathername, @RollNumber, @Address, @Mobile)";
            SqlCommand com = new SqlCommand(query, con);
            com.CommandType = CommandType.Text;
            com.Parameters.AddWithValue("@Name", txtHName.Text);
            com.Parameters.AddWithValue("@FatherName", txtTName.Text);
            com.Parameters.AddWithValue("@RollNumber", txtSBD.Text);
            com.Parameters.AddWithValue("@Address", txtAddress.Text);
            com.Parameters.AddWithValue("@Mobile", txtSDT.Text);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            GetStudentRecord();
        }
    }
}
