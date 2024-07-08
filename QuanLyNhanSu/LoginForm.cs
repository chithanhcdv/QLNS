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

namespace QuanLyNhanSu
{
    public partial class LoginForm : Form
    {
        public bool IsAuthenticated { get; private set; }
        public string LoggedInUsername { get; private set; }
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=HRMS;Integrated Security=True";

        public LoginForm()
        {
            InitializeComponent();
            IsAuthenticated = false;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUsername.KeyDown += new KeyEventHandler(TextBox_KeyDown);
            txtPassword.KeyDown += new KeyEventHandler(TextBox_KeyDown);
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.ToLower(); // Chuyển username về chữ thường
            string password = txtPassword.Text;

            if (AuthenticateUser(username, password))
            {
                MessageBox.Show("Đăng nhập thành công");
                IsAuthenticated = true;
                LoggedInUsername = username;
                this.Close();
            }
            else
            {
                MessageBox.Show("Thông tin đăng nhập không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string adminQuery = "SELECT COUNT(1) FROM Admin WHERE LOWER(Username) = @Username AND Password = @Password";
                    string employeeQuery = "SELECT COUNT(1) FROM Employee WHERE LOWER(Username) = @Username AND Password = @Password";

                    SqlCommand admincmd = new SqlCommand(adminQuery, connection);
                    SqlCommand employeecmd = new SqlCommand(employeeQuery, connection);

                    admincmd.Parameters.AddWithValue("@Username", username);
                    admincmd.Parameters.AddWithValue("@Password", password);

                    employeecmd.Parameters.AddWithValue("@Username", username);
                    employeecmd.Parameters.AddWithValue("@Password", password);

                    int adminCount = Convert.ToInt32(admincmd.ExecuteScalar());
                    int employeeCount = Convert.ToInt32(employeecmd.ExecuteScalar());

                    return adminCount == 1 || employeeCount == 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                    return false;
                }
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Ngăn chặn âm thanh "ding"
                LoginButton_Click(sender, e); // Gọi sự kiện LoginButton_Click
            }
        }    
    }
}
