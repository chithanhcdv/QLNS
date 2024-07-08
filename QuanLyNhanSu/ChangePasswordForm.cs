using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class ChangePasswordForm : Form
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=HRMS;Integrated Security=True";

        public ChangePasswordForm()
        {
            InitializeComponent();
        }

        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {
            ChangePasswordButton.Click += new EventHandler(ChangePasswordButton_Click);
        }

        private void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.ToLower(); // Chuyển username về chữ thường
            string currentPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmNewPassword = txtConfirmNewPassword.Text;

            if (newPassword != confirmNewPassword)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu mới không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ChangePassword(username, currentPassword, newPassword))
            {
                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Đổi mật khẩu thất bại. Tên tài khoản hoặc mật khẩu hiện tại không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ChangePassword(string username, string currentPassword, string newPassword)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Check if the username and current password match in Admin table
                    string adminQuery = "SELECT COUNT(1) FROM Admin WHERE LOWER(Username) = @Username AND Password = @Password";
                    SqlCommand adminCmd = new SqlCommand(adminQuery, connection);
                    adminCmd.Parameters.AddWithValue("@Username", username);
                    adminCmd.Parameters.AddWithValue("@Password", currentPassword);

                    int adminCount = Convert.ToInt32(adminCmd.ExecuteScalar());
                    if (adminCount == 1)
                    {
                        string updateAdminQuery = "UPDATE Admin SET Password = @NewPassword WHERE LOWER(Username) = @Username";
                        SqlCommand updateAdminCmd = new SqlCommand(updateAdminQuery, connection);
                        updateAdminCmd.Parameters.AddWithValue("@NewPassword", newPassword);
                        updateAdminCmd.Parameters.AddWithValue("@Username", username);
                        updateAdminCmd.ExecuteNonQuery();
                        return true;
                    }

                    // Check if the username and current password match in Employee table
                    string employeeQuery = "SELECT COUNT(1) FROM Employee WHERE LOWER(Username) = @Username AND Password = @Password";
                    SqlCommand employeeCmd = new SqlCommand(employeeQuery, connection);
                    employeeCmd.Parameters.AddWithValue("@Username", username);
                    employeeCmd.Parameters.AddWithValue("@Password", currentPassword);

                    int employeeCount = Convert.ToInt32(employeeCmd.ExecuteScalar());
                    if (employeeCount == 1)
                    {
                        string updateEmployeeQuery = "UPDATE Employee SET Password = @NewPassword WHERE LOWER(Username) = @Username";
                        SqlCommand updateEmployeeCmd = new SqlCommand(updateEmployeeQuery, connection);
                        updateEmployeeCmd.Parameters.AddWithValue("@NewPassword", newPassword);
                        updateEmployeeCmd.Parameters.AddWithValue("@Username", username);
                        updateEmployeeCmd.ExecuteNonQuery();
                        return true;
                    }

                    return false; // If neither Admin nor Employee credentials match
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
