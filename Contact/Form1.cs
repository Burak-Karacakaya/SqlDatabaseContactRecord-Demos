using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace Contact
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjeDemos;Integrated Security=True");

        void listed()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from Contacts", connection);
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        void clear()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtSurname.Text = "";
            txtMailAddress.Text = "";
            mskPhonecellNumber.Text = "";
            txtName.Focus();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listed();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("insert into Contacts (Name, Surname, Phonecell, Mail) values (@p1, @p2, @p3, @p4)", connection);
            command.Parameters.AddWithValue("@p1", txtName.Text);
            command.Parameters.AddWithValue("@p2", txtSurname.Text);
            command.Parameters.AddWithValue("@p3", mskPhonecellNumber.Text);
            command.Parameters.AddWithValue("@p4", txtMailAddress.Text);
            command.ExecuteNonQuery(); //sorguyu alıştır anlamına gelir.

            connection.Close();
            MessageBox.Show("The person registered in the system", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listed();
            clear();
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int select = dataGridView1.SelectedCells[0].RowIndex;

            txtId.Text = dataGridView1.Rows[select].Cells[0].Value.ToString();
            txtName.Text = dataGridView1.Rows[select].Cells[1].Value.ToString();
            txtSurname.Text = dataGridView1.Rows[select].Cells[2].Value.ToString();
            mskPhonecellNumber.Text = dataGridView1.Rows[select].Cells[3].Value.ToString();
            txtMailAddress.Text = dataGridView1.Rows[select].Cells[4].Value.ToString();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("delete from contacts where Id=" + txtId.Text, connection);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("The person has been deleted.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            listed();
            clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("update contacts set Name=@p1, Surname=@p2, Phonecell=@p3, Mail=@p4 where Id=@p5", connection);
            command.Parameters.AddWithValue("@p1", txtName.Text);
            command.Parameters.AddWithValue("@p2", txtSurname.Text);
            command.Parameters.AddWithValue("@p3", mskPhonecellNumber.Text);
            command.Parameters.AddWithValue("@p4", txtMailAddress.Text);
            command.Parameters.AddWithValue("@p5", txtId.Text);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("The person Info is updated", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listed();
            clear();


        }
    }
}
