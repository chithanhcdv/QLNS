using iTextSharp.text.error_messages;
using Microsoft.ReportingServices.Diagnostics.Internal;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class MenuForm : Form
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=HRMS;Integrated Security=True";

        private string loggedInUsername;
        public MenuForm(string username)
        {
            InitializeComponent();
            loggedInUsername = username;
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;                  

            SetVisibleDataGridView(false);
            SetVisiblePanel(false);
            SetFieldsEnabled(false);
            SetVisibleButton(false);
            SetVisibleCancelAndSaveButton(false);

            if(loggedInUsername != "admin")
            {
                unitUsedToolStripMenuItem.Visible = false;
            }
        }
 

        /* Tải danh sách phòng ban vào dataGridViewDepartment, khi nhấn vào các phòng ban sẽ hiện chi tiết phòng ban*/
        private void LoadDataGridViewDepartment()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"SELECT DepartmentCode as [Mã phòng ban], DepartmentName as [Tên phòng ban],
                                    Address as [Địa chỉ], DepartmentPhoneNumber as [Số điện thoại phòng ban]
                                    FROM Department ORDER BY DepartmentCode";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewDepartment.DataSource = dataTable;
                    dataGridViewDepartment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void dataGridViewDepartment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewDepartment.Rows[e.RowIndex];
                if (row.Cells["Mã phòng ban"].Value != null)
                {
                    string departmentCode = row.Cells["Mã phòng ban"].Value.ToString();
                    LoadDepartment(departmentCode);
                }
                else
                {
                    MessageBox.Show("Lỗi: Không tìm thấy thông tin trong hàng đã chọn.");
                }
            }
            else
            {
                MessageBox.Show("Lỗi: Không thể truy cập hàng đã chọn.");
            }
        }

        private void LoadDepartment(string departmentCode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT DepartmentCode, DepartmentName, Address, DepartmentPhoneNumber
                        FROM Department
                        WHERE DepartmentCode = @DepartmentCode";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@DepartmentCode", departmentCode);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtDepartmentCode.Text = reader["DepartmentCode"].ToString();
                        txtDepartmentName.Text = reader["DepartmentName"].ToString();
                        txtAddress.Text = reader["Address"].ToString();
                        txtDepartmentPhoneNumber.Text = reader["DepartmentPhoneNumber"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }  
        private void departmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetVisibleDataGridView(false);
            SetVisiblePanel(false);
            SetFieldsEnabled(false);
            LoadDataGridViewDepartment();
            dataGridViewDepartment.Visible = true;
            dataGridViewDepartment.Enabled = true;
            panelDepartment.Visible = true;
            SetVisibleButton(true);
            SetEnabledButton(true);
            SetVisibleCancelAndSaveButton(false);
            SetEnabledCancelAndSaveButton(true);

            if (loggedInUsername != "admin")
            {
                SetVisibleButton(false);
            }
        }

        /**************************************/

        private void LoadDataGridViewPosition()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"SELECT EmployeePositionCode as [Mã chức vụ], PositionName as [Tên chức vụ], HSPC
                                    FROM EmployeePosition ORDER BY EmployeePositionCode";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewPosition.DataSource = dataTable;
                    dataGridViewPosition.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void dataGridViewPosition_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewPosition.Rows[e.RowIndex];
                if (row.Cells["Mã chức vụ"].Value != null)
                {
                    string positionCode = row.Cells["Mã chức vụ"].Value.ToString();
                    LoadPosition(positionCode);
                }
                else
                {
                    MessageBox.Show("Lỗi: Không tìm thấy thông tin trong hàng đã chọn.");
                }
            }
            else
            {
                MessageBox.Show("Lỗi: Không thể truy cập hàng đã chọn.");
            }
        }

        private void LoadPosition(string positionCode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT EmployeePositionCode, PositionName, HSPC
                        FROM EmployeePosition
                        WHERE EmployeePositionCode = @EmployeePositionCode";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@EmployeePositionCode", positionCode);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtPositionCode.Text = reader["EmployeePositionCode"].ToString();
                        txtPositionName.Text = reader["PositionName"].ToString();
                        txtHSPC.Text = reader["HSPC"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }     
        private void positionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetVisibleDataGridView(false);
            SetVisiblePanel(false);
            SetFieldsEnabled(false);
            LoadDataGridViewPosition();
            dataGridViewPosition.Visible = true;
            dataGridViewPosition.Enabled = true;
            panelPosition.Visible = true;
            SetVisibleButton(true);
            SetEnabledButton(true);
            SetVisibleCancelAndSaveButton(false);

            if (loggedInUsername != "admin")
            {
                SetVisibleButton(false);
            }
        }

        private void LoadDataGridViewSpecialized()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"SELECT SpecializedCode as [Mã chuyên ngành], SpecializedName as [Tên chuyên ngành]
                                    FROM Specialized ORDER BY SpecializedCode";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewSpecialized.DataSource = dataTable;
                    dataGridViewSpecialized.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void dataGridViewSpecialized_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewSpecialized.Rows[e.RowIndex];
                if (row.Cells["Mã chuyên ngành"].Value != null)
                {
                    string specializedCode = row.Cells["Mã chuyên ngành"].Value.ToString();
                    LoadSpecialized(specializedCode);
                }
                else
                {
                    MessageBox.Show("Lỗi: Không tìm thấy thông tin trong hàng đã chọn.");
                }
            }
            else
            {
                MessageBox.Show("Lỗi: Không thể truy cập hàng đã chọn.");
            }
        }

        private void LoadSpecialized(string specializedCode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT SpecializedCode, SpecializedName
                        FROM Specialized
                        WHERE SpecializedCode = @SpecializedCode";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SpecializedCode", specializedCode);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtSpecializedCode.Text = reader["SpecializedCode"].ToString();
                        txtSpecializedName.Text = reader["SpecializedName"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }   
        private void specializedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetVisibleDataGridView(false);
            SetVisiblePanel(false);
            SetFieldsEnabled(false);
            LoadDataGridViewSpecialized();
            dataGridViewSpecialized.Visible = true;
            dataGridViewSpecialized.Enabled = true;
            panelSpecialized.Visible = true;
            SetVisibleButton(true);
            SetEnabledButton(true);
            SetVisibleCancelAndSaveButton(false);

            if (loggedInUsername != "admin")
            {
                SetVisibleButton(false);
            }
        }

        private void LoadDataGridViewEducationLevel()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"SELECT EducationLevelCode as [Mã trình độ học vấn], EducationLevelName as [Tên trình độ học vấn],
                                    TierCoefficient as [Hệ số bậc] FROM EducationLevel ORDER BY EducationLevelCode";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewEducationLevel.DataSource = dataTable;
                    dataGridViewEducationLevel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void dataGridViewEducationLevel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewEducationLevel.Rows[e.RowIndex];
                if (row.Cells["Mã trình độ học vấn"].Value != null)
                {
                    string educationLevelCode = row.Cells["Mã trình độ học vấn"].Value.ToString();
                    LoadEducationLevel(educationLevelCode);
                }
                else
                {
                    MessageBox.Show("Lỗi: Không tìm thấy thông tin trong hàng đã chọn.");
                }
            }
            else
            {
                MessageBox.Show("Lỗi: Không thể truy cập hàng đã chọn.");
            }
        }

        private void LoadEducationLevel(string educationLevelCode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT EducationLevelCode, EducationLevelName, TierCoefficient
                        FROM EducationLevel
                        WHERE EducationLevelCode = @EducationLevelCode";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@EducationLevelCode", educationLevelCode);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtEducationLevelCode.Text = reader["EducationLevelCode"].ToString();
                        txtEducationLevelName.Text = reader["EducationLevelName"].ToString();
                        txtTierCoefficient.Text = reader["TierCoefficient"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void educationLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetVisibleDataGridView(false);
            SetVisiblePanel(false);
            SetFieldsEnabled(false);
            LoadDataGridViewEducationLevel();
            dataGridViewEducationLevel.Visible = true;
            dataGridViewEducationLevel.Enabled = true;
            panelEducationLevel.Visible = true;
            SetVisibleButton(true);
            SetEnabledButton(true);
            SetVisibleCancelAndSaveButton(false);

            if (loggedInUsername != "admin")
            {
                SetVisibleButton(false);
            }
        }
        private void LoadDataGridViewContract()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"SELECT ContractCode as [Mã hợp đồng], ContractType as [Loại hợp đồng], 
                                    StartDate as [Ngày bắt đầu], EndDate as [Ngày kết thúc], Note as [Ghi chú] 
                                    FROM Contract ORDER BY ContractCode";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewContract.DataSource = dataTable;
                    dataGridViewContract.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void dataGridViewContract_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewContract.Rows[e.RowIndex];
                if (row.Cells["Mã hợp đồng"].Value != null)
                {
                    string contractCode = row.Cells["Mã hợp đồng"].Value.ToString();
                    LoadContract(contractCode);
                }
                else
                {
                    MessageBox.Show("Lỗi: Không tìm thấy thông tin trong hàng đã chọn.");
                }
            }
            else
            {
                MessageBox.Show("Lỗi: Không thể truy cập hàng đã chọn.");
            }
        }

        private void LoadContract(string contractCode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT ContractCode, ContractType, StartDate, EndDate, Note
                        FROM Contract
                        WHERE ContractCode = @ContractCode";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ContractCode", contractCode);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtContractCode.Text = reader["ContractCode"].ToString();
                        txtContractType.Text = reader["ContractType"].ToString();
                        txtStartDate.Text = Convert.ToDateTime(reader["StartDate"]).ToString("dd/MM/yyyy");
                        txtEndDate.Text = Convert.ToDateTime(reader["EndDate"]).ToString("dd/MM/yyyy");
                        txtNote.Text = reader["Note"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }        
        private void contractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetVisibleDataGridView(false);
            SetVisiblePanel(false);
            SetFieldsEnabled(false);
            LoadDataGridViewContract();
            dataGridViewContract.Visible = true;
            dataGridViewContract.Enabled = true;
            panelContract.Visible = true;
            SetVisibleButton(true);
            SetEnabledButton(true);
            SetVisibleCancelAndSaveButton(false);

            if (loggedInUsername != "admin")
            {
                SetVisibleButton(false);
            }
        }

        private void SetVisibleDataGridView(bool visible)
        {
            dataGridViewDepartment.Visible = visible;
            dataGridViewPosition.Visible = visible;
            dataGridViewSpecialized.Visible = visible;
            dataGridViewEducationLevel.Visible = visible;
            dataGridViewContract.Visible = visible;
        }

        private void SetVisiblePanel(bool visible)
        {
            panelDepartment.Visible = visible;
            panelPosition.Visible = visible;
            panelSpecialized.Visible = visible;
            panelEducationLevel.Visible = visible;
            panelContract.Visible = visible;

            panelUnitUsed.Visible = visible;
        }

        private void SetFieldsEnabled(bool enabled)
        {
            txtDepartmentCode.Enabled = enabled;
            txtDepartmentName.Enabled = enabled;
            txtAddress.Enabled = enabled;
            txtDepartmentPhoneNumber.Enabled = enabled;

            txtSpecializedCode.Enabled = enabled;
            txtSpecializedName.Enabled = enabled;

            txtPositionCode.Enabled = enabled;
            txtPositionName.Enabled = enabled;         
            txtHSPC.Enabled = enabled;

            txtEducationLevelCode.Enabled = enabled;
            txtEducationLevelName.Enabled = enabled;
            txtTierCoefficient.Enabled = enabled;

            txtContractCode.Enabled = enabled;
            txtContractType.Enabled = enabled;
            txtStartDate.Enabled = enabled;
            txtEndDate.Enabled = enabled;
            txtNote.Enabled = enabled;

            txtUnitUsedName.Enabled = enabled;
            txtSchoolName.Enabled = enabled;
            txtUnitUsedAddress.Enabled = enabled;
            txtPhoneNumber.Enabled = enabled;
            txtEmail.Enabled = enabled;
            txtSalaryIncreasePeriod.Enabled = enabled;
        }

        private void SetVisibleButton(bool enabled)
        {
            AddButton.Visible = enabled;           
            EditButton.Visible = enabled;            
            DeleteButton.Visible = enabled;
        }

        private void SetVisibleCancelAndSaveButton(bool visible)
        {
            CancelAddButton.Visible = visible;
            SaveAddButton.Visible = visible;
            CancelEditButton.Visible = visible;
            SaveEditButton.Visible = visible;
        }

        private void SetEnabledCancelAndSaveButton(bool enabled)
        {
            CancelAddButton.Enabled = enabled;
            SaveAddButton.Enabled = enabled;
            CancelEditButton.Enabled = enabled;
            SaveEditButton.Enabled = enabled;
        }

        private void SetEnabledButton(bool enabled)
        {
            AddButton.Enabled = enabled;
            EditButton.Enabled = enabled;
            DeleteButton.Enabled = enabled;
        }

        private void SetEnabledDataGridView(bool enabled)
        {
            dataGridViewDepartment.Enabled = enabled;
            dataGridViewPosition.Enabled = enabled;
            dataGridViewEducationLevel.Enabled = enabled;
            dataGridViewSpecialized.Enabled = enabled;
            dataGridViewContract.Enabled = enabled;
        }

        private void ClearFields()
        {
            if (dataGridViewDepartment.Visible == true)
            {
                MessageBox.Show("Thêm phòng ban mới");
                txtDepartmentCode.Text = string.Empty;
                txtDepartmentName.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtDepartmentPhoneNumber.Text = string.Empty;
            }

            if (dataGridViewPosition.Visible == true)
            {
                MessageBox.Show("Thêm chức vụ mới");
                txtPositionCode.Text = string.Empty;
                txtPositionName.Text = string.Empty;
                txtHSPC.Text = string.Empty;
            }

            if (dataGridViewSpecialized.Visible == true)
            {
                MessageBox.Show("Thêm chuyên ngành mới");
                txtSpecializedCode.Text = string.Empty;
                txtSpecializedName.Text = string.Empty;
            }

            if (dataGridViewEducationLevel.Visible == true)
            {
                MessageBox.Show("Thêm trình độ học vấn mới");
                txtEducationLevelCode.Text = string.Empty;
                txtEducationLevelName.Text = string.Empty;
                txtTierCoefficient.Text = string.Empty;
            }

            if (dataGridViewContract.Visible == true)
            {
                MessageBox.Show("Thêm hợp đồng mới");
                txtContractCode.Text = string.Empty;
                txtContractType.Text = string.Empty;
                txtStartDate.Text = string.Empty;
                txtEndDate.Text = string.Empty;
                txtNote.Text = string.Empty;
            }
        }
        private void EditButton_Click(object sender, EventArgs e)
        {                
            SetFieldsEnabled(true);
            CancelEditButton.Visible = true;
            SaveEditButton.Visible = true;
            if(dataGridViewContract.Visible == true)
            {
                dataGridViewContract.Enabled = false;
                MessageBox.Show("Cập nhật thông tin hợp đồng");
            }      

            if(dataGridViewDepartment.Visible == true)
            {              
                dataGridViewDepartment.Enabled = false;
                MessageBox.Show("Cập nhật thông tin phòng ban");
            }           

            if (dataGridViewPosition.Visible == true)
            {
                dataGridViewPosition.Enabled = false;
                MessageBox.Show("Cập nhật thông tin chức vụ");
            }

            if (dataGridViewEducationLevel.Visible == true)
            {
                dataGridViewEducationLevel.Enabled = false;
                MessageBox.Show("Cập nhật thông tin trình độ học vấn");
            }

            if (dataGridViewSpecialized.Visible == true)
            {
                dataGridViewSpecialized.Enabled = false;
                MessageBox.Show("Cập nhật thông tin chuyên ngành");
            }

            if (panelUnitUsed.Visible == true)
            {             
                MessageBox.Show("Cập nhật thông tin đơn vị sử dụng");
            }
        }
     
        private void SaveEdit()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();                   
                    if(dataGridViewDepartment.Visible == true)
                    {
                        string departmentQuery = @"
                        UPDATE Department
                        SET DepartmentCode = @DepartmentCode, DepartmentName = @DepartmentName,
                        Address = @Address, DepartmentPhoneNumber = @DepartmentPhoneNumber
                        WHERE DepartmentCode = @DepartmentCode";

                        SqlCommand departmentCommand = new SqlCommand(departmentQuery, connection);

                        departmentCommand.Parameters.AddWithValue("@DepartmentCode", txtDepartmentCode.Text);
                        departmentCommand.Parameters.AddWithValue("@DepartmentName", txtDepartmentName.Text);
                        departmentCommand.Parameters.AddWithValue("@Address", txtAddress.Text);
                        departmentCommand.Parameters.AddWithValue("@DepartmentPhoneNumber", txtDepartmentPhoneNumber.Text);

                        departmentCommand.ExecuteNonQuery();
                        dataGridViewDepartment.Enabled = true;
                        LoadDataGridViewDepartment();
                    }

                    if(dataGridViewPosition.Visible == true)
                    {
                        string positionQuery = @"
                        UPDATE EmployeePosition
                        SET EmployeePositionCode = @EmployeePositionCode, PositionName = @PositionName, HSPC = @HSPC
                        WHERE EmployeePositionCode = @EmployeePositionCode";

                        SqlCommand positionCommand = new SqlCommand(positionQuery, connection);

                        positionCommand.Parameters.AddWithValue("@EmployeePositionCode", txtPositionCode.Text);
                        positionCommand.Parameters.AddWithValue("@PositionName", txtPositionName.Text);
                        positionCommand.Parameters.AddWithValue("@HSPC", txtHSPC.Text);

                        positionCommand.ExecuteNonQuery();
                        dataGridViewPosition.Enabled = true;
                        LoadDataGridViewPosition();
                    }

                    if (dataGridViewSpecialized.Visible == true)
                    {
                        string specializedQuery = @"
                        UPDATE Specialized
                        SET SpecializedCode = @SpecializedCode, SpecializedName = @SpecializedName
                        WHERE SpecializedCode = @SpecializedCode";

                        SqlCommand specializedCommand = new SqlCommand(specializedQuery, connection);

                        specializedCommand.Parameters.AddWithValue("@SpecializedCode", txtSpecializedCode.Text);
                        specializedCommand.Parameters.AddWithValue("@SpecializedName", txtSpecializedName.Text);

                        specializedCommand.ExecuteNonQuery();
                        dataGridViewSpecialized.Enabled = true;
                        LoadDataGridViewSpecialized();
                    }

                    if (dataGridViewEducationLevel.Visible == true)
                    {
                        string educationLevelQuery = @"
                        UPDATE EducationLevel
                        SET EducationLevelCode = @EducationLevelCode, EducationLevelName = @EducationLevelName,
                        TierCoefficient = @TierCoefficient
                        WHERE EducationLevelCode = @EducationLevelCode";

                        SqlCommand educationLevelCommand = new SqlCommand(educationLevelQuery, connection);

                        educationLevelCommand.Parameters.AddWithValue("@EducationLevelCode", txtEducationLevelCode.Text);
                        educationLevelCommand.Parameters.AddWithValue("@EducationLevelName", txtEducationLevelName.Text);
                        educationLevelCommand.Parameters.AddWithValue("@TierCoefficient", txtTierCoefficient.Text);

                        educationLevelCommand.ExecuteNonQuery();
                        dataGridViewEducationLevel.Enabled = true;
                        LoadDataGridViewEducationLevel();
                    }

                    if (dataGridViewContract.Visible == true)
                    {
                        string contractQuery = @"
                        UPDATE Contract
                        SET ContractCode = @ContractCode, ContractType = @ContractType,
                        StartDate = @StartDate, EndDate = @EndDate, Note = @Note
                        WHERE ContractCode = @ContractCode";

                        SqlCommand contractCommand = new SqlCommand(contractQuery, connection);

                        contractCommand.Parameters.AddWithValue("@ContractCode", txtContractCode.Text);
                        contractCommand.Parameters.AddWithValue("@ContractType", txtContractType.Text);

                        string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy" };
                        DateTime startDate;
                        if (DateTime.TryParseExact(txtStartDate.Text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
                        {
                            contractCommand.Parameters.AddWithValue("@StartDate", startDate);
                        }
                        else
                        {
                            // Xử lý lỗi khi chuyển đổi không thành công
                            MessageBox.Show("Ngày bắt đầu không hợp lệ. Vui lòng nhập lại với định dạng là ngày/tháng/năm hoặc ngày-tháng-năm");
                            return;
                        }

                        DateTime endDate;
                        if (DateTime.TryParseExact(txtEndDate.Text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
                        {
                            contractCommand.Parameters.AddWithValue("@EndDate", endDate);
                        }
                        else
                        {
                            // Xử lý lỗi khi chuyển đổi không thành công
                            MessageBox.Show("Ngày kết thúc không hợp lệ. Vui lòng nhập lại với định dạng là ngày/tháng/năm hoặc ngày-tháng-năm");
                            return;
                        }

                        contractCommand.Parameters.AddWithValue("@Note", txtNote.Text);

                        contractCommand.ExecuteNonQuery();
                        dataGridViewContract.Enabled = true;
                        LoadDataGridViewContract();
                    }

                    if (panelUnitUsed.Visible == true)
                    {
                        string deleteQuery = "DELETE FROM UnitUsed";
                        SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                        deleteCommand.ExecuteNonQuery();

                        string insertQuery = @"
                        INSERT INTO UnitUsed (UnitUsedName, SchoolName, Address, PhoneNumber, Email, SalaryIncreasePeriod)
                        VALUES (@UnitUsedName, @SchoolName, @Address, @PhoneNumber, @Email, @SalaryIncreasePeriod)";

                        SqlCommand insertCommand = new SqlCommand(insertQuery, connection);

                        insertCommand.Parameters.AddWithValue("@UnitUsedName", txtUnitUsedName.Text);
                        insertCommand.Parameters.AddWithValue("@SchoolName", txtSchoolName.Text);
                        insertCommand.Parameters.AddWithValue("@Address", txtUnitUsedAddress.Text);
                        insertCommand.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text);
                        insertCommand.Parameters.AddWithValue("@Email", txtEmail.Text);
                        insertCommand.Parameters.AddWithValue("@SalaryIncreasePeriod", txtSalaryIncreasePeriod.Text);

                        insertCommand.ExecuteNonQuery();
                    }


                    MessageBox.Show("Cập nhật thông tin thành công.");                                      
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void CancelEditButton_Click(object sender, EventArgs e)
        {
            SetFieldsEnabled(false);
            SetVisibleCancelAndSaveButton(false);
            if (dataGridViewContract.Visible == true)
            {
                dataGridViewContract.Enabled = true;
                MessageBox.Show("Hủy cập nhật thông tin hợp đồng");
            }

            if (dataGridViewDepartment.Visible == true)
            {
                dataGridViewDepartment.Enabled = true;
                MessageBox.Show("Hủy cập nhật thông tin phòng ban");
            }

            if (dataGridViewPosition.Visible == true)
            {
                dataGridViewPosition.Enabled = true;
                MessageBox.Show("Hủy cập nhật thông tin chức vụ");
            }

            if (dataGridViewEducationLevel.Visible == true)
            {
                dataGridViewEducationLevel.Enabled = true;
                MessageBox.Show("Hủy cập nhật thông tin trình độ học vấn");
            }

            if (dataGridViewSpecialized.Visible == true)
            {
                dataGridViewSpecialized.Enabled = true;
                MessageBox.Show("Hủy cập nhật thông tin chuyên ngành");
            }

            if (panelUnitUsed.Visible == true)
            {                
                MessageBox.Show("Hủy cập nhật thông tin đơn vị sử dụng");
            }
        }

        private void SaveEditButton_Click(object sender, EventArgs e)
        {
            SaveEdit();            
            SetFieldsEnabled(false);

            CancelEditButton.Visible = false;
            SaveEditButton.Visible = false;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            ClearFields();
            SetFieldsEnabled(true);
            SetEnabledButton(false);

            SetEnabledDataGridView(false);

            CancelAddButton.Visible = true;
            SaveAddButton.Visible = true;
        }

        private void CancelAddButton_Click(object sender, EventArgs e)
        {
            SetFieldsEnabled(false);
            SetEnabledButton(true);

            SetEnabledDataGridView(true);

            CancelAddButton.Visible = false;
            SaveAddButton.Visible = false;

            MessageBox.Show("Hủy thêm mới");
        }

        private void SaveAddButton_Click(object sender, EventArgs e)
        {
            SaveAdd();
            SetFieldsEnabled(false);
            SetEnabledButton(true);

            CancelAddButton.Visible = false;
            SaveAddButton.Visible = false;
        }

        private void SaveAdd()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                    {
                    connection.Open();
                    if (dataGridViewDepartment.Visible == true)
                    {
                        string departmentQuery = @"
                        INSERT INTO Department (DepartmentCode, DepartmentName, Address, DepartmentPhoneNumber)
                        VALUES (@DepartmentCode, @DepartmentName, @Address, @DepartmentPhoneNumber)";

                        SqlCommand departmentCommand = new SqlCommand(departmentQuery, connection);

                        departmentCommand.Parameters.AddWithValue("@DepartmentCode", txtDepartmentCode.Text);
                        departmentCommand.Parameters.AddWithValue("@DepartmentName", txtDepartmentName.Text);
                        departmentCommand.Parameters.AddWithValue("@Address", txtAddress.Text);
                        departmentCommand.Parameters.AddWithValue("@DepartmentPhoneNumber", txtDepartmentPhoneNumber.Text);

                        departmentCommand.ExecuteNonQuery();
                        MessageBox.Show("Thêm phòng ban mới thành công");
                        dataGridViewDepartment.Enabled = true;
                        LoadDataGridViewDepartment();
                    }

                    if (dataGridViewPosition.Visible == true)
                    {
                        string positionQuery = @"
                        INSERT INTO EmployeePosition (EmployeePositionCode, PositionName, HSPC)
                        VALUES (@EmployeePositionCode, @PositionName, @HSPC)";

                        SqlCommand positionCommand = new SqlCommand(positionQuery, connection);

                        positionCommand.Parameters.AddWithValue("@EmployeePositionCode", txtPositionCode.Text);
                        positionCommand.Parameters.AddWithValue("@PositionName", txtPositionName.Text);
                        positionCommand.Parameters.AddWithValue("@HSPC", txtHSPC.Text);

                        positionCommand.ExecuteNonQuery();
                        MessageBox.Show("Thêm chức vụ mới thành công");
                        dataGridViewPosition.Enabled = true;
                        LoadDataGridViewPosition();
                    }

                    if (dataGridViewSpecialized.Visible == true)
                    {
                        string specializedQuery = @"
                        INSERT INTO Specialized (SpecializedCode, SpecializedName)
                        VALUES (@SpecializedCode, @SpecializedName)";

                        SqlCommand specializedCommand = new SqlCommand(specializedQuery, connection);

                        specializedCommand.Parameters.AddWithValue("@SpecializedCode", txtSpecializedCode.Text);
                        specializedCommand.Parameters.AddWithValue("@SpecializedName", txtSpecializedName.Text);

                        specializedCommand.ExecuteNonQuery();
                        MessageBox.Show("Thêm chuyên ngành mới thành công");
                        dataGridViewSpecialized.Enabled = true;
                        LoadDataGridViewSpecialized();
                    }

                    if (dataGridViewEducationLevel.Visible == true)
                    {
                        string educationLevelQuery = @"
                        INSERT INTO EducationLevel (EducationLevelCode, EducationLevelName, TierCoefficient)
                        VALUES (@EducationLevelCode, @EducationLevelName, @TierCoefficient)";

                        SqlCommand educationLevelCommand = new SqlCommand(educationLevelQuery, connection);

                        educationLevelCommand.Parameters.AddWithValue("@EducationLevelCode", txtEducationLevelCode.Text);
                        educationLevelCommand.Parameters.AddWithValue("@EducationLevelName", txtEducationLevelName.Text);
                        educationLevelCommand.Parameters.AddWithValue("@TierCoefficient", txtTierCoefficient.Text);

                        educationLevelCommand.ExecuteNonQuery();
                        MessageBox.Show("Thêm trình độ học vấn mới thành công");
                        dataGridViewEducationLevel.Enabled = true;
                        LoadDataGridViewEducationLevel();
                    }

                    if (dataGridViewContract.Visible == true)
                    {
                        string contractQuery = @"
                        INSERT INTO Contract (ContractCode, ContractType, StartDate, EndDate, Note)
                        VALUES (@ContractCode, @ContractType, @StartDate, @EndDate, @Note)";

                        SqlCommand contractCommand = new SqlCommand(contractQuery, connection);

                        contractCommand.Parameters.AddWithValue("@ContractCode", txtContractCode.Text);
                        contractCommand.Parameters.AddWithValue("@ContractType", txtContractType.Text);
                        string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy" };
                        DateTime startDate;
                        if (DateTime.TryParseExact(txtStartDate.Text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
                        {
                            contractCommand.Parameters.AddWithValue("@StartDate", startDate);
                        }
                        else
                        {
                            // Xử lý lỗi khi chuyển đổi không thành công
                            MessageBox.Show("Ngày bắt đầu không hợp lệ. Vui lòng nhập lại với định dạng là ngày/tháng/năm hoặc ngày-tháng-năm");
                            return;
                        }

                        DateTime endDate;
                        if (DateTime.TryParseExact(txtEndDate.Text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
                        {
                            contractCommand.Parameters.AddWithValue("@EndDate", endDate);
                        }
                        else
                        {
                            // Xử lý lỗi khi chuyển đổi không thành công
                            MessageBox.Show("Ngày kết thúc không hợp lệ. Vui lòng nhập lại với định dạng là ngày/tháng/năm hoặc ngày-tháng-năm");
                            return;
                        }
                        contractCommand.Parameters.AddWithValue("@Note", txtNote.Text);

                        contractCommand.ExecuteNonQuery();
                        MessageBox.Show("Thêm hợp đồng mới thành công");
                        dataGridViewContract.Enabled = true;
                        LoadDataGridViewContract();
                    }                 
                    }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void DeleteDepartment()
        {
            // Kiểm tra xem có hàng nào được chọn hay không
            if (dataGridViewDepartment.CurrentRow != null)
            {
                // Lấy hàng hiện tại được chọn
                DataGridViewRow selectedRow = dataGridViewDepartment.CurrentRow;

                // Lấy mã nhân viên từ cột "Mã phòng ban"
                string departmentCode = selectedRow.Cells["Mã phòng ban"].Value.ToString();

                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng ban này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Thực hiện lệnh xóa trong cơ sở dữ liệu
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = "DELETE FROM Department WHERE DepartmentCode = @DepartmentCode";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@DepartmentCode", departmentCode);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Phòng ban đã được xóa thành công.");
                                LoadDataGridViewDepartment();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy phòng ban để xóa.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phòng ban để xóa.");
            }
        }

        private void DeletePosition()
        {
            if (dataGridViewPosition.CurrentRow != null)
            {
                DataGridViewRow selectedRow = dataGridViewPosition.CurrentRow;

                string positionCode = selectedRow.Cells["Mã chức vụ"].Value.ToString();

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa chức vụ này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = "DELETE FROM EmployeePosition WHERE EmployeePositionCode = @EmployeePositionCode";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@EmployeePositionCode", positionCode);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Chức vụ đã được xóa thành công.");
                                LoadDataGridViewPosition();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy chức vụ để xóa.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một chức vụ để xóa.");
            }
        }

        private void DeleteSpecialized()
        {
            if (dataGridViewSpecialized.CurrentRow != null)
            {
                DataGridViewRow selectedRow = dataGridViewSpecialized.CurrentRow;

                string specializedCode = selectedRow.Cells["Mã chuyên ngành"].Value.ToString();

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa chuyên ngành này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = "DELETE FROM Specialized WHERE SpecializedCode = @SpecializedCode";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@SpecializedCode", specializedCode);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Chuyên ngành đã được xóa thành công.");
                                LoadDataGridViewSpecialized();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy chuyên ngành để xóa.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một chuyên ngành để xóa.");
            }
        }

        private void DeleteEducationLevel()
        {
            if (dataGridViewEducationLevel.CurrentRow != null)
            {
                DataGridViewRow selectedRow = dataGridViewEducationLevel.CurrentRow;

                string educationLevelCode = selectedRow.Cells["Mã trình độ học vấn"].Value.ToString();

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa trình độ học vấn này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = "DELETE FROM EducationLevel WHERE EducationLevelCode = @EducationLevelCode";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@EducationLevelCode", educationLevelCode);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Trình độ học vấn đã được xóa thành công.");
                                LoadDataGridViewEducationLevel();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy trình độ học vấn để xóa.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một trình độ học vấn để xóa.");
            }
        }

        private void DeleteContract()
        {
            if (dataGridViewContract.CurrentRow != null)
            {
                DataGridViewRow selectedRow = dataGridViewContract.CurrentRow;

                string contractCode = selectedRow.Cells["Mã hợp đồng"].Value.ToString();

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hợp đồng này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = "DELETE FROM Contract WHERE ContractCode = @ContractCode";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@ContractCode", contractCode);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Hợp đồng đã được xóa thành công.");
                                LoadDataGridViewContract();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy hợp đồng để xóa.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hợp đồng để xóa.");
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if(dataGridViewDepartment.Visible == true)
            {
                DeleteDepartment();
            }

            if (dataGridViewPosition.Visible == true)
            {
                DeletePosition();
            }

            if (dataGridViewSpecialized.Visible == true)
            {
                DeleteSpecialized();
            }

            if (dataGridViewEducationLevel.Visible == true)
            {
                DeleteEducationLevel();
            }

            if (dataGridViewContract.Visible == true)
            {
                DeleteContract();
            }
        }      
        private DataTable LoadDepartmentReport()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"  
                    SELECT DepartmentCode, DepartmentName, Address, DepartmentPhoneNumber
                    FROM Department";

                    SqlCommand command = new SqlCommand(query, connection);       
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            return dataTable;
        }

        private DataTable LoadPositionReport()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"  
                    SELECT EmployeePositionCode, PositionName, HSPC
                    FROM EmployeePosition";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            return dataTable;
        }

        private DataTable LoadSpecializedReport()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"  
                    SELECT SpecializedCode, SpecializedName
                    FROM Specialized";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            return dataTable;
        }

        private DataTable LoadEducationLevelReport()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"  
                    SELECT EducationLevelCode, EducationLevelName, TierCoefficient
                    FROM EducationLevel";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            return dataTable;
        }

        private DataTable LoadContractReport()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"  
                    SELECT ContractCode, ContractType, StartDate, EndDate, Note
                    FROM Contract";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);                  
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            return dataTable;
        }

        private DataTable LoadSalaryReport()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"  
                    SELECT Employee.EmployeeCode, Employee.FullName, Salary.MinimumSalary, Salary.SalaryCoefficient, Salary.SocialInsurance,
                    Salary.HealthInsurance, Salary.UnemploymentInsurance, Salary.Allowance, Salary.IncomeTax
                    FROM Employee  
                    JOIN Salary ON Employee.EmployeeCode = Salary.EmployeeCode";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            return dataTable;
        }
        private DataTable LoadSalaryDetailReport()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"  
                    SELECT Employee.EmployeeCode, Employee.FullName, SalaryDetail.BasicSalary, 
                    SalaryDetail.SocialInsurance,SalaryDetail.HealthInsurance, SalaryDetail.UnemploymentInsurance,
                    SalaryDetail.Allowance, SalaryDetail.IncomeTax, SalaryDetail.BonusMoney,
                    SalaryDetail.DisciplineMoney, SalaryDetail.PayDay, SalaryDetail.TotalSalary
                    FROM Employee  
                    JOIN SalaryDetail ON Employee.EmployeeCode = SalaryDetail.EmployeeCode";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            return dataTable;
        }

        private DataTable LoadSalaryUpdateReport()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"  
                    SELECT Employee.EmployeeCode, Employee.FullName, SalaryUpdate.CurrentSalary, SalaryUpdate.SalaryAfterUpdate, 
                    SalaryUpdate.SalaryCoefficient,SalaryUpdate.SocialInsurance, SalaryUpdate.HealthInsurance, 
                    SalaryUpdate.UnemploymentInsurance, SalaryUpdate.Allowance, SalaryUpdate.IncomeTax, SalaryUpdate.UpdateDay
                    FROM Employee  
                    JOIN SalaryUpdate ON Employee.EmployeeCode = SalaryUpdate.EmployeeCode";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            return dataTable;
        }

        private DataTable LoadEmployeeListReport()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
            SELECT Employee.EmployeeCode, Employee.FullName, Employee.Birthday, Employee.Hometown, 
            CASE 
                WHEN Employee.Gender = 1 THEN 'Nam' 
                ELSE 'Nu' 
            END AS Gender,
            Employee.Ethnic, Employee.PhoneNumber,
            EmployeePosition.PositionName, Department.DepartmentName, 
            Contract.ContractType, EducationLevel.EducationLevelName, 
            Specialized.SpecializedCode, Specialized.SpecializedName,
            Employee.IdentityCard
            FROM Employee
            JOIN EmployeePosition ON Employee.EmployeePositionCode = EmployeePosition.EmployeePositionCode
            JOIN Department ON Employee.DepartmentCode = Department.DepartmentCode
            JOIN Contract ON Employee.ContractCode = Contract.ContractCode
            JOIN EducationLevel ON Employee.EducationLevelCode = EducationLevel.EducationLevelCode 
            JOIN Specialized ON Employee.SpecializedCode = Specialized.SpecializedCode
            ";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            return dataTable;
        }


        private DataTable LoadEmployeeListDepartment1Report()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"  
                    SELECT Employee.EmployeeCode, Employee.FullName, Employee.Birthday, Employee.Hometown,
                    CASE 
                        WHEN Employee.Gender = 1 THEN 'Nam' 
                        ELSE 'Nu' 
                    END AS Gender,
                    Employee.Ethnic,Employee.PhoneNumber,
                    EmployeePosition.PositionName, Department.DepartmentName, 
                    Contract.ContractType, EducationLevel.EducationLevelName, 
                    Specialized.SpecializedCode, Specialized.SpecializedName,
                    Employee.IdentityCard
                    FROM Employee
                    JOIN EmployeePosition ON Employee.EmployeePositionCode = EmployeePosition.EmployeePositionCode
                    JOIN Department ON Employee.DepartmentCode = Department.DepartmentCode
                    JOIN Contract ON Employee.ContractCode = Contract.ContractCode
                    JOIN EducationLevel ON Employee.EducationLevelCode = EducationLevel.EducationLevelCode 
                    JOIN Specialized ON Employee.SpecializedCode = Specialized.SpecializedCode
                    WHERE Department.DepartmentCode = 'cntt'
                    ";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            return dataTable;
        }

        private DataTable LoadEmployeeListDepartment2Report()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"  
                    SELECT Employee.EmployeeCode, Employee.FullName, Employee.Birthday, Employee.Hometown,
                    CASE 
                        WHEN Employee.Gender = 1 THEN 'Nam' 
                        ELSE 'Nu' 
                    END AS Gender,
                    Employee.Ethnic,Employee.PhoneNumber,
                    EmployeePosition.PositionName, Department.DepartmentName, 
                    Contract.ContractType, EducationLevel.EducationLevelName, 
                    Specialized.SpecializedCode, Specialized.SpecializedName,
                    Employee.IdentityCard
                    FROM Employee
                    JOIN EmployeePosition ON Employee.EmployeePositionCode = EmployeePosition.EmployeePositionCode
                    JOIN Department ON Employee.DepartmentCode = Department.DepartmentCode
                    JOIN Contract ON Employee.ContractCode = Contract.ContractCode
                    JOIN EducationLevel ON Employee.EducationLevelCode = EducationLevel.EducationLevelCode 
                    JOIN Specialized ON Employee.SpecializedCode = Specialized.SpecializedCode
                    WHERE Department.DepartmentCode = 'daotao'
                    ";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            return dataTable;
        }
        private DataTable LoadEmployeeListDepartment3Report()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"  
                    SELECT Employee.EmployeeCode, Employee.FullName, Employee.Birthday, Employee.Hometown,
                    CASE 
                        WHEN Employee.Gender = 1 THEN 'Nam' 
                        ELSE 'Nu' 
                    END AS Gender,
                    Employee.Ethnic,Employee.PhoneNumber,
                    EmployeePosition.PositionName, Department.DepartmentName, 
                    Contract.ContractType, EducationLevel.EducationLevelName, 
                    Specialized.SpecializedCode, Specialized.SpecializedName,
                    Employee.IdentityCard
                    FROM Employee
                    JOIN EmployeePosition ON Employee.EmployeePositionCode = EmployeePosition.EmployeePositionCode
                    JOIN Department ON Employee.DepartmentCode = Department.DepartmentCode
                    JOIN Contract ON Employee.ContractCode = Contract.ContractCode
                    JOIN EducationLevel ON Employee.EducationLevelCode = EducationLevel.EducationLevelCode 
                    JOIN Specialized ON Employee.SpecializedCode = Specialized.SpecializedCode
                    WHERE Department.DepartmentCode = 'ketoan'
                    ";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            return dataTable;
        }

        private DataTable LoadEmployeeListDepartment4Report()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"  
                    SELECT Employee.EmployeeCode, Employee.FullName, Employee.Birthday, Employee.Hometown,
                    CASE 
                        WHEN Employee.Gender = 1 THEN 'Nam' 
                        ELSE 'Nu' 
                    END AS Gender,
                    Employee.Ethnic,Employee.PhoneNumber,
                    EmployeePosition.PositionName, Department.DepartmentName, 
                    Contract.ContractType, EducationLevel.EducationLevelName, 
                    Specialized.SpecializedCode, Specialized.SpecializedName,
                    Employee.IdentityCard
                    FROM Employee
                    JOIN EmployeePosition ON Employee.EmployeePositionCode = EmployeePosition.EmployeePositionCode
                    JOIN Department ON Employee.DepartmentCode = Department.DepartmentCode
                    JOIN Contract ON Employee.ContractCode = Contract.ContractCode
                    JOIN EducationLevel ON Employee.EducationLevelCode = EducationLevel.EducationLevelCode 
                    JOIN Specialized ON Employee.SpecializedCode = Specialized.SpecializedCode
                    WHERE Department.DepartmentCode = 'xaydung'
                    ";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            return dataTable;
        }

        private void departmentReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dataTable = LoadDepartmentReport();
            DataTable dataTable1 = LoadUnitUsedtReport();
            DepartmentReport departmentReport = new DepartmentReport();
            departmentReport.DepartmentData = dataTable;
            departmentReport.UnitUsedData = dataTable1;
            departmentReport.Show();
        }

        private void positionReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataTable dataTable = LoadPositionReport();
            DataTable dataTable1 = LoadUnitUsedtReport();
            PositionReport positionReport = new PositionReport();
            positionReport.PositionData = dataTable;
            positionReport.UnitUsedData = dataTable1;
            positionReport.Show();
        }            
        private void specializedReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dataTable = LoadSpecializedReport();
            DataTable dataTable1 = LoadUnitUsedtReport();
            SpecializedReport specializedReport = new SpecializedReport();
            specializedReport.SpecializedData = dataTable;
            specializedReport.UnitUsedData = dataTable1;
            specializedReport.Show();
        }
      
        private void educationLevelReportToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataTable dataTable = LoadEducationLevelReport();
            DataTable dataTable1 = LoadUnitUsedtReport();
            EducationLevelReport educationLevelReport = new EducationLevelReport();
            educationLevelReport.EducationLevelData = dataTable;
            educationLevelReport.UnitUsedData = dataTable1;
            educationLevelReport.Show();
        }
     
        private void contractReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataTable dataTable = LoadContractReport();
            DataTable dataTable1 = LoadUnitUsedtReport();
            ContractReport contractReport = new ContractReport();
            contractReport.ContractData = dataTable;
            contractReport.UnitUsedData = dataTable1;
            contractReport.Show();
        }      
        private void allEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dataTable = LoadEmployeeListReport();
            DataTable dataTable1 = LoadUnitUsedtReport();
            EmployeeListReport employeeListReport = new EmployeeListReport();
            employeeListReport.EmployeeListData = dataTable;
            employeeListReport.UnitUsedData = dataTable1;
            employeeListReport.Show();
        }
        private void salaryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dataTable = LoadSalaryReport();
            DataTable dataTable1 = LoadUnitUsedtReport();
            SalaryReport salaryReport = new SalaryReport();
            salaryReport.SalaryData = dataTable;
            salaryReport.UnitUsedData = dataTable1;
            salaryReport.Show();
        }     
        private void salaryUpdateReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dataTable = LoadSalaryUpdateReport();
            DataTable dataTable1 = LoadUnitUsedtReport();
            SalaryUpdateReport salaryUpdateReport = new SalaryUpdateReport();
            salaryUpdateReport.SalaryUpdateData = dataTable;
            salaryUpdateReport.UnitUsedData = dataTable1;
            salaryUpdateReport.Show();
        }   
        private void salaryDetailReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dataTable = LoadSalaryDetailReport();
            DataTable dataTable1 = LoadUnitUsedtReport();
            SalaryDetailReport salaryDetailReport = new SalaryDetailReport();
            salaryDetailReport.SalaryDetailData = dataTable;
            salaryDetailReport.UnitUsedData = dataTable1;
            salaryDetailReport.Show();
        }      
        private void department1TinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dataTable = LoadEmployeeListDepartment1Report();
            DataTable dataTable1 = LoadUnitUsedtReport();
            EmployeeListReport employeeListReport = new EmployeeListReport();
            employeeListReport.EmployeeListData = dataTable;
            employeeListReport.UnitUsedData = dataTable1;
            employeeListReport.Show();
        }
        private void department2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dataTable = LoadEmployeeListDepartment2Report();
            DataTable dataTable1 = LoadUnitUsedtReport();
            EmployeeListReport employeeListReport = new EmployeeListReport();
            employeeListReport.EmployeeListData = dataTable;
            employeeListReport.UnitUsedData = dataTable1;
            employeeListReport.Show();
        }

        private void department3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dataTable = LoadEmployeeListDepartment3Report();
            DataTable dataTable1 = LoadUnitUsedtReport();
            EmployeeListReport employeeListReport = new EmployeeListReport();
            employeeListReport.EmployeeListData = dataTable;
            employeeListReport.UnitUsedData = dataTable1;
            employeeListReport.Show();
        }

        private void department4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dataTable = LoadEmployeeListDepartment4Report();
            DataTable dataTable1 = LoadUnitUsedtReport();
            EmployeeListReport employeeListReport = new EmployeeListReport();
            employeeListReport.EmployeeListData = dataTable;
            employeeListReport.UnitUsedData = dataTable1;
            employeeListReport.Show();
        }                      
        private DataTable LoadUnitUsedtReport()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"  
                    SELECT * FROM UnitUsed ";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            return dataTable;
        }

        /****************** Menu hệ thống *********************/
        private void userInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loggedInUsername != "admin")
            {
                MessageBox.Show("Thông tin đăng nhập: Nhân viên");
            }
            else
            {
                MessageBox.Show("Thông tin đăng nhập: Quản trị viên");
            }
        }
        private void LoadUnitUsed()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT UnitUsedName, SchoolName, Address,
                        PhoneNumber, Email, SalaryIncreasePeriod
                        FROM UnitUsed";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtUnitUsedName.Text = reader["UnitUsedName"].ToString();
                        txtSchoolName.Text = reader["SchoolName"].ToString();
                        txtUnitUsedAddress.Text = reader["Address"].ToString();
                        txtPhoneNumber.Text = reader["PhoneNumber"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtSalaryIncreasePeriod.Text = reader["SalaryIncreasePeriod"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void unitUsedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUnitUsed();
            SetVisibleDataGridView(false);
            SetVisiblePanel(false);
            SetFieldsEnabled(false);
            SetVisibleButton(true);
            SetEnabledButton(false);
            EditButton.Enabled = true;
            SetVisibleCancelAndSaveButton(false);
            panelUnitUsed.Visible = true;

            if (loggedInUsername != "admin")
            {
                SetVisibleButton(false);
            }
        }
        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChangePasswordForm changePasswordForm = new ChangePasswordForm();
            changePasswordForm.ShowDialog();
            changePasswordForm = null;
            this.Show();
        }
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /******************************************************/

        /****************** Mở Form quản lý nhân sự *****************/
        private void employeeManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form1 form1 = new Form1(loggedInUsername);
            form1.ShowDialog();
            //form1 = null;
            //this.Show();
        }
        /***********************************************************/

        /************** Hiển thị icon nhỏ dưới thanh TaskBar **************/
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void notifyIcon1_DoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void maximizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void minimizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        /*****************************************************************/


    }
}
