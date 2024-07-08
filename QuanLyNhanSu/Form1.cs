using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class Form1 : Form
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=HRMS;Integrated Security=True";

        private bool isEditMode = false;

        private string loggedInUsername;

        public Form1(string username)
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.Form1_Load);
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.dataGridViewResults.CellClick += new DataGridViewCellEventHandler(this.dataGridViewResults_CellClick);
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);

            loggedInUsername = username;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            LoadDepartment();
            LoadData();
            this.dataGridViewResults.Columns[0].Width = 40;
            this.dataGridViewResults.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewResults.MultiSelect = false;
            SetFieldsEnabled(false);

            SelectComboBoxGender();
            SelectComboBoxStatus();
            SelectComboBoxDepartment();
            SelectComboBoxPosition();
            SelectComboBoxSpecialized();
            SelectComboBoxContract();
            SelectComboBoxEducationLevel();

            saveButton.Hide();
            ChooseFileButton.Hide();
            txtImage.Hide();
            addData.Hide();
            notAddData.Hide();
            notUpdateData.Hide();

            if (dataGridViewResults.Rows.Count > 1)
            {
                // Simulate a cell click event for the first row
                DataGridViewCellEventArgs args = new DataGridViewCellEventArgs(0, 0);
                dataGridViewResults_CellClick(this.dataGridViewResults, args);
            }

            searchText.KeyDown += new KeyEventHandler(TextBox_KeyDown);

            if (loggedInUsername != "admin")
            {
                SetVisibleButton(false);
            }

            SetVisibleDeleteDataGridButton(false);
        }

        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT EmployeeCode as Mã, FullName as [Họ và tên]FROM Employee ORDER BY EmployeeCode";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewResults.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void LoadDepartment()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT DepartmentCode, DepartmentName FROM Department";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    comboBox1.DisplayMember = "DepartmentName";
                    comboBox1.ValueMember = "DepartmentName";
                    comboBox1.DataSource = dataTable;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null)
            {
                string departmentName = comboBox1.SelectedValue.ToString();
                LoadEmployeeByDepartmentName(departmentName);
            }
        }

        private void LoadEmployeeByDepartmentName(string departmentName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT Employee.EmployeeCode as Mã , Employee.FullName as [Họ và tên]
                        FROM Employee
                        JOIN Department ON Employee.DepartmentCode = Department.DepartmentCode
                        WHERE Department.DepartmentName = @DepartmentName";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@DepartmentName", departmentName);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewResults.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void searchButton_Click(object sender, EventArgs e)
        {
            string keyword = searchText.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT EmployeeCode as Mã, FullName as [Họ và tên] FROM Employee WHERE EmployeeCode LIKE @keyword OR FullName LIKE @keyword";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewResults.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void refreshButton_Click(object sender, EventArgs e)
        {
            LoadDepartment();
            LoadData();
            if (dataGridViewResults.Rows.Count > 1)
            {
                // Simulate a cell click event for the first row
                DataGridViewCellEventArgs args = new DataGridViewCellEventArgs(0, 0);
                dataGridViewResults_CellClick(this.dataGridViewResults, args);
            }
        }


        private void dataGridViewResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewResults.Rows[e.RowIndex];
                if (row.Cells["Mã"].Value != null)
                {
                    string employeeCode = row.Cells["Mã"].Value.ToString();
                    LoadEmployeeInformation(employeeCode);
                    LoadEmployeeSalary(employeeCode);
                    LoadEmployeeSalaryDetail(employeeCode);
                    LoadEmployeeBonus(employeeCode);
                    LoadEmployeeDiscipline(employeeCode);
                    LoadEmployeeRotation(employeeCode);
                    LoadEmployeeOtherInformation(employeeCode);
                }
                else
                {
                    MessageBox.Show("Lỗi: Không tìm thấy mã nhân viên trong hàng đã chọn.");
                }
            }
            else
            {
                MessageBox.Show("Lỗi: Không thể truy cập hàng đã chọn.");
            }
        }

        private void LoadEmployeeInformation(string employeeCode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                SELECT Employee.EmployeeCode, Employee.Username, Employee.Password, Employee.FullName, Employee.Birthday, Employee.Hometown, Employee.Gender, Employee.Ethnic,
                Employee.PhoneNumber, Employee.Status,
                EmployeePosition.EmployeePositionCode, EmployeePosition.PositionName, Department.DepartmentCode, Department.DepartmentName, 
                Contract.ContractCode, Contract.ContractType, EducationLevel.EducationLevelCode, EducationLevel.EducationLevelName, 
                Specialized.SpecializedCode, Specialized.SpecializedName,
                Employee.IdentityCard, Employee.Image
                FROM Employee
                JOIN EmployeePosition ON Employee.EmployeePositionCode = EmployeePosition.EmployeePositionCode
                JOIN Department ON Employee.DepartmentCode = Department.DepartmentCode
                JOIN Contract ON Employee.ContractCode = Contract.ContractCode
                JOIN EducationLevel ON Employee.EducationLevelCode = EducationLevel.EducationLevelCode 
                JOIN Specialized ON Employee.SpecializedCode = Specialized.SpecializedCode
                WHERE EmployeeCode = @EmployeeCode";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtEmployeeCode.Text = reader["EmployeeCode"].ToString();
                        txtUsername.Text = reader["Username"].ToString();
                        txtPassword.Text = reader["Password"].ToString();
                        txtFullName.Text = reader["FullName"].ToString();
                        txtBirthday.Text = Convert.ToDateTime(reader["Birthday"]).ToString("dd/MM/yyyy");
                        txtEthnic.Text = reader["Ethnic"].ToString();
                        txtHometown.Text = reader["Hometown"].ToString();
                        txtPhoneNumber.Text = reader["PhoneNumber"].ToString();
                        txtIdentityCard.Text = reader["IdentityCard"].ToString();
                        txtImage.Text = reader["Image"].ToString();

                        int genderValue = Convert.ToInt32(reader["Gender"]);
                        comboBox_Gender.SelectedIndex = genderValue;

                        int statusValue = Convert.ToInt32(reader["Status"]);
                        comboBox_Status.SelectedIndex = statusValue;

                        // Setting the selected values for ComboBoxes
                        string contractCode = reader["ContractCode"].ToString();
                        string positionCode = reader["EmployeePositionCode"].ToString();
                        string departmentCode = reader["DepartmentCode"].ToString();
                        string educationLevelCode = reader["EducationLevelCode"].ToString();
                        string specializedCode = reader["SpecializedCode"].ToString();

                        // Populate and set ComboBox values
                        PopulateComboBox(comboBox_Contract, "SELECT ContractCode, ContractType FROM Contract", "ContractCode", "ContractType", contractCode);
                        PopulateComboBox(comboBox_Position, "SELECT EmployeePositionCode, PositionName FROM EmployeePosition", "EmployeePositionCode", "PositionName", positionCode);
                        PopulateComboBox(comboBox_Department, "SELECT DepartmentCode, DepartmentName FROM Department", "DepartmentCode", "DepartmentName", departmentCode);
                        PopulateComboBox(comboBox_EducationLevel, "SELECT EducationLevelCode, EducationLevelName FROM EducationLevel", "EducationLevelCode", "EducationLevelName", educationLevelCode);
                        PopulateComboBox(comboBox_Specialized, "SELECT SpecializedCode, SpecializedName FROM Specialized", "SpecializedCode", "SpecializedName", specializedCode);

                        string imagePath = reader["Image"].ToString();
                        //string baseDirectory = @"C:\Users\ADMIN\source\repos\QuanLyNhanSu\QuanLyNhanSu";            
                        if (!string.IsNullOrEmpty(imagePath))
                        {
                            /*if (!Path.IsPathRooted(imagePath))
                            {
                                // Combine the base directory with the relative path
                                imagePath = Path.Combine(baseDirectory, imagePath);
                            }*/

                            if (System.IO.File.Exists(imagePath))
                            {
                                Image.Image = System.Drawing.Image.FromFile(imagePath);
                            }
                            else
                            {
                                Image.Image = null;
                            }
                        }
                        else
                        {
                            Image.Image = null;
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void PopulateComboBox(ComboBox comboBox, string query, string valueMember, string displayMember, string selectedValue)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    comboBox.DisplayMember = displayMember;
                    comboBox.ValueMember = valueMember;
                    comboBox.DataSource = dataTable;
                    comboBox.SelectedValue = selectedValue;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        private void LoadEmployeeSalary(string employeeCode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT Employee.EmployeeCode, Salary.MinimumSalary, Salary.SalaryCoefficient, Salary.SocialInsurance, 
                        Salary.HealthInsurance, Salary.UnemploymentInsurance, Salary.Allowance, Salary.IncomeTax
                        FROM Employee
                        JOIN Salary ON Employee.EmployeeCode = Salary.EmployeeCode
                        WHERE Employee.EmployeeCode = @EmployeeCode";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        float minimumSalary = reader["MinimumSalary"] != DBNull.Value ? Convert.ToSingle(reader["MinimumSalary"]) : 0.0f;
                        txtMinimumSalary.Text = minimumSalary.ToString("N0") + "đ";

                        float salaryCoefficient = reader["SalaryCoefficient"] != DBNull.Value ? Convert.ToSingle(reader["SalaryCoefficient"]) : 0.0f;
                        txtSalaryCoefficient.Text = salaryCoefficient.ToString();

                        float socialInsurance = reader["SocialInsurance"] != DBNull.Value ? Convert.ToSingle(reader["SocialInsurance"]) : 0.0f;
                        txtSocialInsurance.Text = socialInsurance.ToString("N0") + "đ";

                        float healthInsurance = reader["HealthInsurance"] != DBNull.Value ? Convert.ToSingle(reader["HealthInsurance"]) : 0.0f;
                        txtHealthInsurance.Text = healthInsurance.ToString("N0") + "đ";

                        float unemploymentInsurance = reader["UnemploymentInsurance"] != DBNull.Value ? Convert.ToSingle(reader["UnemploymentInsurance"]) : 0.0f;
                        txtUnemploymentInsurance.Text = unemploymentInsurance.ToString("N0") + "đ";

                        float allowance = reader["Allowance"] != DBNull.Value ? Convert.ToSingle(reader["Allowance"]) : 0.0f;
                        txtAllowance.Text = allowance.ToString("N0") + "đ";

                        float incomeTax = reader["IncomeTax"] != DBNull.Value ? Convert.ToSingle(reader["IncomeTax"]) : 0.0f;
                        txtIncomeTax.Text = incomeTax.ToString("N0") + "đ";

                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void LoadEmployeeSalaryDetail(string employeeCode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT Employee.EmployeeCode, SalaryDetail.BasicSalary, SalaryDetail.SocialInsurance, 
                        SalaryDetail.HealthInsurance, SalaryDetail.UnemploymentInsurance, SalaryDetail.Allowance, SalaryDetail.IncomeTax,
                        SalaryDetail.BonusMoney, SalaryDetail.DisciplineMoney, SalaryDetail.PayDay, SalaryDetail.TotalSalary
                        FROM Employee
                        JOIN SalaryDetail ON Employee.EmployeeCode = SalaryDetail.EmployeeCode
                        WHERE Employee.EmployeeCode = @EmployeeCode";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtBasicSalary.Text = GetSafeFloat(reader, "BasicSalary").ToString("N0") + "đ";
                        txtSocialInsurance2.Text = GetSafeFloat(reader, "SocialInsurance").ToString("N0") + "đ";
                        txtHealthInsurance2.Text = GetSafeFloat(reader, "HealthInsurance").ToString("N0") + "đ";
                        txtUnemploymentInsurance2.Text = GetSafeFloat(reader, "UnemploymentInsurance").ToString("N0") + "đ";
                        txtAllowance2.Text = GetSafeFloat(reader, "Allowance").ToString("N0") + "đ";
                        txtIncomeTax2.Text = GetSafeFloat(reader, "IncomeTax").ToString("N0") + "đ";
                        txtBonusMoney.Text = GetSafeFloat(reader, "BonusMoney").ToString("N0") + "đ";
                        txtDisciplineMoney.Text = GetSafeFloat(reader, "DisciplineMoney").ToString("N0") + "đ";
                        txtTotalSalary.Text = GetSafeFloat(reader, "TotalSalary").ToString("N0") + "đ";
                        txtPayDay.Text = GetSafeDateTime(reader, "PayDay").ToString("dd/MM/yyyy");
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private float GetSafeFloat(SqlDataReader reader, string columnName)
        {
            return reader[columnName] != DBNull.Value ? Convert.ToSingle(reader[columnName]) : 0.0f;
        }

        private DateTime GetSafeDateTime(SqlDataReader reader, string columnName)
        {
            return reader[columnName] != DBNull.Value ? Convert.ToDateTime(reader[columnName]) : DateTime.MinValue;
        }
        private void LoadEmployeeRotation(string employeeCode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT EmployeeRotation.RotationDate as [Ngày luân chuyển], EmployeeRotation.RotationReason as [Lý do luân chuyển], 
                        Department1.DepartmentCode as [Phòng ban chuyển], Department2.DepartmentCode as [Phòng ban đến]
                        FROM EmployeeRotation
                        JOIN Department Department1 ON EmployeeRotation.DepartmentRotation = Department1.DepartmentCode
                        JOIN Department Department2 ON EmployeeRotation.IncomingDepartment = Department2.DepartmentCode
                        WHERE EmployeeRotation.EmployeeCode = @EmployeeCode";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewRotation.DataSource = dataTable;
                    dataGridViewRotation.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void LoadEmployeeBonus(string employeeCode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Truy vấn để lấy danh sách các lần khen thưởng
                    string query = @"
                    SELECT Reason as [Lý do thưởng], BonusDate as [Ngày khen thưởng], BonusMoney as [Tiền khen thưởng]
                    FROM Bonus
                    WHERE EmployeeCode = @EmployeeCode";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewBonus.DataSource = dataTable;


                    dataGridViewBonus.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


                    // Truy vấn để đếm số lần khen thưởng
                    string countQuery = @"
                    SELECT COUNT(*) AS BonusCount
                    FROM Bonus
                    WHERE EmployeeCode = @EmployeeCode";
                    SqlCommand countCommand = new SqlCommand(countQuery, connection);
                    countCommand.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                    int BonusCount = (int)countCommand.ExecuteScalar();

                    txtBonusCount.Text = BonusCount.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        private void LoadEmployeeDiscipline(string employeeCode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                            SELECT Reason as [Lý do kỷ luật], DisciplineDate as [Ngày kỷ luật], DisciplineMoney as [Tiền kỷ luật]
                            FROM Discipline
                            WHERE EmployeeCode = @EmployeeCode";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@EmployeeCode", employeeCode);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewDiscipline.DataSource = dataTable;

                    dataGridViewDiscipline.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


                    string countQuery = @"
                    SELECT COUNT(*) AS DisciplineCount
                    FROM Discipline
                    WHERE EmployeeCode = @EmployeeCode";
                    SqlCommand countCommand = new SqlCommand(countQuery, connection);
                    countCommand.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                    int DisciplineCount = (int)countCommand.ExecuteScalar();

                    // Hiển thị số lần khen thưởng trong txtSoLanKhenThuong
                    txtDisciplineCount.Text = DisciplineCount.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        private void LoadEmployeeOtherInformation(string employeeCode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string universityQuery = @"
                        SELECT EmployeeCode, UniversityName as [Tên trường đại học], TrainingCountry as [Quốc gia đào tạo], GraduateYear as [Năm tốt nghiệp]
                        FROM University
                        WHERE EmployeeCode = @EmployeeCode";
                    SqlDataAdapter universityAdapter = new SqlDataAdapter(universityQuery, connection);
                    universityAdapter.SelectCommand.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                    DataTable universityDataTable = new DataTable();
                    universityAdapter.Fill(universityDataTable);
                    dataGridViewUniversity.DataSource = universityDataTable;
                    dataGridViewUniversity.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridViewUniversity.Columns["EmployeeCode"].Visible = false;


                    string afterUniversityQuery = @"
                        SELECT EmployeeCode, SpecializedMaster as [Thạc sĩ chuyên ngành], TrainingPlaceMaster as [Nơi đào tạo thạc sĩ], DegreeYearMaster as [Năm cấp bằng thạc sĩ],
                        SpecializedDoctorate as [Tiến sĩ chuyên ngành], TrainingPlaceDoctorate as [Nơi đào tạo tiến sĩ], DegreeYearDoctorate as [Năm cấp bằng tiến sĩ]
                        FROM AfterUniversity
                        WHERE EmployeeCode = @EmployeeCode";
                    SqlDataAdapter afterUniversityAdapter = new SqlDataAdapter(afterUniversityQuery, connection);
                    afterUniversityAdapter.SelectCommand.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                    DataTable afterUniversityDataTable = new DataTable();
                    afterUniversityAdapter.Fill(afterUniversityDataTable);
                    dataGridViewAfterUniversity.DataSource = afterUniversityDataTable;
                    dataGridViewAfterUniversity.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridViewAfterUniversity.Columns["EmployeeCode"].Visible = false;


                    string foreignLanguageQuery = @"
                        SELECT EmployeeCode, ForeignLanguageName as [Tên ngoại ngữ], Level as [Mức độ sử dụng]
                        FROM ForeignLanguage
                        WHERE EmployeeCode = @EmployeeCode";
                    SqlDataAdapter foreignLanguageAdapter = new SqlDataAdapter(foreignLanguageQuery, connection);
                    foreignLanguageAdapter.SelectCommand.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                    DataTable foreignLanguageDataTable = new DataTable();
                    foreignLanguageAdapter.Fill(foreignLanguageDataTable);
                    dataGridViewForeignLanguage.DataSource = foreignLanguageDataTable;
                    dataGridViewForeignLanguage.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridViewForeignLanguage.Columns["EmployeeCode"].Visible = false;


                    string workingProcessQuery = @"
                        SELECT EmployeeCode, WorkPlace as [Nơi công tác], WorkUndertake as [Công việc đảm nhận], Time as [Thời gian]
                        FROM WorkingProcess
                        WHERE EmployeeCode = @EmployeeCode";
                    SqlDataAdapter workingProcessAdapter = new SqlDataAdapter(workingProcessQuery, connection);
                    workingProcessAdapter.SelectCommand.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                    DataTable workingProcessDataTable = new DataTable();
                    workingProcessAdapter.Fill(workingProcessDataTable);
                    dataGridViewWorkingProcess.DataSource = workingProcessDataTable;
                    dataGridViewWorkingProcess.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridViewWorkingProcess.Columns["EmployeeCode"].Visible = false;


                    string scientificResearchTopicsQuery = @"
                        SELECT EmployeeCode, ScientificResearchTopicName as [Tên đề tài nghiên cứu], YearOfBegin as [Năm bắt đầu], YearOfComplete as [Năm hoàn thành],
                        LevelTopic as [Đề tài cấp], ResponsibilityInTheTopic as [Trách nhiệm tham gia trong đề tài]
                        FROM ScientificResearchTopics
                        WHERE EmployeeCode = @EmployeeCode";
                    SqlDataAdapter scientificResearchTopicsAdapter = new SqlDataAdapter(scientificResearchTopicsQuery, connection);
                    scientificResearchTopicsAdapter.SelectCommand.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                    DataTable scientificResearchTopicsDataTable = new DataTable();
                    scientificResearchTopicsAdapter.Fill(scientificResearchTopicsDataTable);
                    dataGridViewScientificResearchTopics.DataSource = scientificResearchTopicsDataTable;
                    dataGridViewScientificResearchTopics.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridViewScientificResearchTopics.Columns["EmployeeCode"].Visible = false;

                    string scientificWorksQuery = @"
                        SELECT EmployeeCode, ScientificWorksName as [Tên công trình], Year as [Năm công bố], MagazineName [Tên tạp chí]
                        FROM ScientificWorks
                        WHERE EmployeeCode = @EmployeeCode";
                    SqlDataAdapter scientificWorksAdapter = new SqlDataAdapter(scientificWorksQuery, connection);
                    scientificWorksAdapter.SelectCommand.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                    DataTable scientificWorksDataTable = new DataTable();
                    scientificWorksAdapter.Fill(scientificWorksDataTable);
                    dataGridViewScientificWorks.DataSource = scientificWorksDataTable;
                    dataGridViewScientificWorks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridViewScientificWorks.Columns["EmployeeCode"].Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        /*private void addButton_Click(object sender, EventArgs e)
        {
            if (!isAddingNew)
            {
                isAddingNew = true;
                addButton.Text = "Hủy";
                ChooseFileButton.Show();
                txtImage.Show();
                SetFieldsEnabled(true); // Enable fields for user input
                ClearFields(); // Clear existing input fields
                MessageBox.Show("Thêm một nhân viên mới");
                addData.Show();
            }
            else
            {
          
                isAddingNew = false;
                addButton.Text = "Thêm mới";
                ChooseFileButton.Hide();
                txtImage.Hide();
                SetFieldsEnabled(false); // Disable fields as the action is cancelled
            }
        }*/

        private bool isAddingNew = false;
        //private DataRow newRow;

        private void addButton_Click(object sender, EventArgs e)
        {
            isAddingNew = true;
            ChooseFileButton.Show();
            txtImage.Show();
            SetFieldsEnabled(true); // Enable fields for user input
            ClearFields(); // Clear existing input fields
            MessageBox.Show("Thêm một nhân viên mới");
            addData.Show();
            notAddData.Show();
            dataGridViewResults.Enabled = false;
            SetFieldsEnabledButton(false);
        }
        private void notAddData_Click(object sender, EventArgs e)
        {
            isAddingNew = false;
            MessageBox.Show("Hủy thêm nhân viên mới");
            ClearFields();
            ChooseFileButton.Hide();
            txtImage.Hide();
            SetFieldsEnabled(false); // Disable fields as the action is cancelled
            addData.Hide();
            notAddData.Hide();
            dataGridViewResults.Enabled = true;
            SetFieldsEnabledButton(true);
        }

        private void addData_Click(object sender, EventArgs e)
        {
            if (isAddingNew)
            {
                isAddingNew = false;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        string query = @"
                    INSERT INTO Employee (EmployeeCode, Username, Password, FullName, Birthday, Hometown, Gender, Ethnic, PhoneNumber,
                    EmployeePositionCode, Status, DepartmentCode, ContractCode, EducationLevelCode, SpecializedCode, IdentityCard, Image)
                    VALUES (@EmployeeCode, @Username, @Password, @FullName, @Birthday, @Hometown, @Gender, @Ethnic, 
                    @PhoneNumber, @EmployeePositionCode, @Status, @DepartmentCode, @ContractCode, @EducationLevelCode, @SpecializedCode, @IdentityCard, @Image)";

                        SqlCommand command = new SqlCommand(query, connection);

                        command.Parameters.AddWithValue("@EmployeeCode", txtEmployeeCode.Text);
                        command.Parameters.AddWithValue("@Username", txtUsername.Text);
                        command.Parameters.AddWithValue("@Password", txtPassword.Text);
                        command.Parameters.AddWithValue("@FullName", txtFullName.Text);
                        command.Parameters.AddWithValue("@Birthday", DateTime.ParseExact(txtBirthday.Text, "dd/MM/yyyy", null));
                        command.Parameters.AddWithValue("@Hometown", txtHometown.Text);
                        command.Parameters.AddWithValue("@Gender", comboBox_Gender.SelectedIndex.ToString());
                        command.Parameters.AddWithValue("@Ethnic", txtEthnic.Text);
                        command.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text);

                        // Handling EmployeePositionCode
                        var selectedPosition = comboBox_Position.SelectedValue;
                        if (selectedPosition == null && comboBox_Position.SelectedItem is KeyValuePair<string, string> positionItem)
                        {
                            selectedPosition = positionItem.Key;
                        }
                        command.Parameters.AddWithValue("@EmployeePositionCode", selectedPosition ?? (object)DBNull.Value);

                        // Handling Status
                        command.Parameters.AddWithValue("@Status", comboBox_Status.SelectedIndex.ToString());

                        // Handling DepartmentCode
                        var selectedDepartment = comboBox_Department.SelectedValue;
                        if (selectedDepartment == null && comboBox_Department.SelectedItem is KeyValuePair<string, string> departmentItem)
                        {
                            selectedDepartment = departmentItem.Key;
                        }
                        command.Parameters.AddWithValue("@DepartmentCode", selectedDepartment ?? (object)DBNull.Value);

                        // Handling ContractCode
                        var selectedContract = comboBox_Contract.SelectedValue;
                        if (selectedContract == null && comboBox_Contract.SelectedItem is KeyValuePair<string, string> contractItem)
                        {
                            selectedContract = contractItem.Key;
                        }
                        command.Parameters.AddWithValue("@ContractCode", selectedContract ?? (object)DBNull.Value);

                        // Handling EducationLevelCode
                        var selectedEducationLevel = comboBox_EducationLevel.SelectedValue;
                        if (selectedEducationLevel == null && comboBox_EducationLevel.SelectedItem is KeyValuePair<string, string> educationItem)
                        {
                            selectedEducationLevel = educationItem.Key;
                        }
                        command.Parameters.AddWithValue("@EducationLevelCode", selectedEducationLevel ?? (object)DBNull.Value);

                        // Handling SpecializedCode
                        var selectedSpecialized = comboBox_Specialized.SelectedValue;
                        if (selectedSpecialized == null && comboBox_Specialized.SelectedItem is KeyValuePair<string, string> specializedItem)
                        {
                            selectedSpecialized = specializedItem.Key;
                        }
                        command.Parameters.AddWithValue("@SpecializedCode", selectedSpecialized ?? (object)DBNull.Value);

                        command.Parameters.AddWithValue("@IdentityCard", txtIdentityCard.Text);
                        command.Parameters.AddWithValue("@Image", txtImage.Text);

                        command.ExecuteNonQuery();



                        string salaryQuery = @"
                        INSERT INTO Salary (EmployeeCode, MinimumSalary, SalaryCoefficient, SocialInsurance,
                        HealthInsurance, UnemploymentInsurance, Allowance, IncomeTax)
                        VALUES (@EmployeeCode, @MinimumSalary, @SalaryCoefficient, @SocialInsurance,
                        @HealthInsurance, @UnemploymentInsurance, @Allowance, @IncomeTax)";

                        SqlCommand salaryCommand = new SqlCommand(salaryQuery, connection);
                        salaryCommand.Parameters.AddWithValue("@EmployeeCode", txtEmployeeCode.Text);
                        salaryCommand.Parameters.AddWithValue("@MinimumSalary", txtMinimumSalary.Text);
                        salaryCommand.Parameters.AddWithValue("@SalaryCoefficient", txtSalaryCoefficient.Text);
                        salaryCommand.Parameters.AddWithValue("@SocialInsurance", txtSocialInsurance.Text);
                        salaryCommand.Parameters.AddWithValue("@HealthInsurance", txtHealthInsurance.Text);
                        salaryCommand.Parameters.AddWithValue("@UnemploymentInsurance", txtUnemploymentInsurance.Text);
                        salaryCommand.Parameters.AddWithValue("@Allowance", txtAllowance.Text);
                        salaryCommand.Parameters.AddWithValue("@IncomeTax", txtIncomeTax.Text);
                        salaryCommand.ExecuteNonQuery();

                        string insertSalaryDetailQuery = @"
                            INSERT INTO SalaryDetail (EmployeeCode, BasicSalary, SocialInsurance, 
                            HealthInsurance, UnemploymentInsurance, Allowance, IncomeTax, PayDay)                       
                            VALUES (@EmployeeCode, @BasicSalary, @SocialInsurance, 
                            @HealthInsurance, @UnemploymentInsurance, @Allowance, @IncomeTax, @PayDay)";

                        SqlCommand insertSalaryDetailCommand = new SqlCommand(insertSalaryDetailQuery, connection);
                        insertSalaryDetailCommand.Parameters.AddWithValue("@EmployeeCode", txtEmployeeCode.Text);
                        insertSalaryDetailCommand.Parameters.AddWithValue("@BasicSalary", txtMinimumSalary.Text);
                        insertSalaryDetailCommand.Parameters.AddWithValue("@SocialInsurance", txtSocialInsurance.Text);
                        insertSalaryDetailCommand.Parameters.AddWithValue("@HealthInsurance", txtHealthInsurance.Text);
                        insertSalaryDetailCommand.Parameters.AddWithValue("@UnemploymentInsurance", txtUnemploymentInsurance.Text);
                        insertSalaryDetailCommand.Parameters.AddWithValue("@Allowance", txtAllowance.Text);
                        insertSalaryDetailCommand.Parameters.AddWithValue("@IncomeTax", txtIncomeTax.Text);
                        insertSalaryDetailCommand.Parameters.AddWithValue("@PayDay", DateTime.Now);
                        insertSalaryDetailCommand.ExecuteNonQuery();

                        string insertSalaryUpdateQuery = @"
                            INSERT INTO SalaryUpdate (EmployeeCode, CurrentSalary, SalaryCoefficient, SocialInsurance, 
                            HealthInsurance, UnemploymentInsurance, Allowance, IncomeTax)                       
                            VALUES (@EmployeeCode, @CurrentSalary, @SalaryCoefficient, @SocialInsurance, 
                            @HealthInsurance, @UnemploymentInsurance, @Allowance, @IncomeTax)";

                        SqlCommand insertSalaryUpdateCommand = new SqlCommand(insertSalaryUpdateQuery, connection);
                        insertSalaryUpdateCommand.Parameters.AddWithValue("@EmployeeCode", txtEmployeeCode.Text);
                        insertSalaryUpdateCommand.Parameters.AddWithValue("@CurrentSalary", txtMinimumSalary.Text);
                        insertSalaryUpdateCommand.Parameters.AddWithValue("@SalaryCoefficient", txtSalaryCoefficient.Text);
                        insertSalaryUpdateCommand.Parameters.AddWithValue("@SocialInsurance", txtSocialInsurance.Text);
                        insertSalaryUpdateCommand.Parameters.AddWithValue("@HealthInsurance", txtHealthInsurance.Text);
                        insertSalaryUpdateCommand.Parameters.AddWithValue("@UnemploymentInsurance", txtUnemploymentInsurance.Text);
                        insertSalaryUpdateCommand.Parameters.AddWithValue("@Allowance", txtAllowance.Text);
                        insertSalaryUpdateCommand.Parameters.AddWithValue("@IncomeTax", txtIncomeTax.Text);
                        insertSalaryUpdateCommand.ExecuteNonQuery();

                        MessageBox.Show("Thêm nhân viên mới thành công.");
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }

                SetFieldsEnabled(false); // Disable fields after data is saved
                dataGridViewResults.Enabled = true;
                addData.Hide();
                notAddData.Hide();
                ChooseFileButton.Hide();
                txtImage.Hide();
                SetFieldsEnabledButton(true);
            }
        }

        private void ClearFields()
        {
            //EmployeeInformation
            txtEmployeeCode.Text = string.Empty;
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtFullName.Text = string.Empty;
            txtBirthday.Text = string.Empty;
            txtHometown.Text = string.Empty;
            comboBox_Gender.SelectedIndex = -1;
            txtEthnic.Text = string.Empty;
            txtPhoneNumber.Text = string.Empty;
            comboBox_Position.SelectedIndex = -1;
            comboBox_Contract.SelectedIndex = -1;
            comboBox_Status.SelectedIndex = -1;
            comboBox_Department.SelectedIndex = -1;
            comboBox_EducationLevel.SelectedIndex = -1;
            comboBox_Specialized.SelectedIndex = -1;
            txtIdentityCard.Text = string.Empty;
            txtImage.Text = string.Empty;
            Image.Image = null;


            //EmployeeSalary
            txtMinimumSalary.Text = string.Empty;
            txtSalaryCoefficient.Text = string.Empty;
            txtSocialInsurance.Text = string.Empty;
            txtHealthInsurance.Text = string.Empty;
            txtUnemploymentInsurance.Text = string.Empty;
            txtAllowance.Text = string.Empty;
            txtIncomeTax.Text = string.Empty;

            //SalaryDetail
            txtBasicSalary.Text = string.Empty;
            txtSocialInsurance2.Text = string.Empty;
            txtHealthInsurance2.Text = string.Empty;
            txtUnemploymentInsurance2.Text = string.Empty;
            txtAllowance2.Text = string.Empty;
            txtIncomeTax2.Text = string.Empty;
            txtBonusMoney.Text = string.Empty;
            txtDisciplineMoney.Text = string.Empty;
            txtPayDay.Text = string.Empty;
            txtTotalSalary.Text = string.Empty;           
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (isEditMode)
            {
                // Save changes to the database
                isEditMode = true;
                SetFieldsEnabled(true);
                txtImage.Show();
                ChooseFileButton.Show();
                dataGridViewResults.Enabled = false;
            }
            else
            {
                // Enable fields for editing
                if (!string.IsNullOrEmpty(GetSelectedEmployeeId()))
                {
                    isEditMode = true;
                    saveButton.Show();
                    notUpdateData.Show();
                    SetFieldsEnabled(true);
                    SetFieldsEnabledButton(false);
                    SetVisibleDeleteDataGridButton(true);
                }
                else
                {
                    MessageBox.Show("Hãy chọn một nhân viên để cập nhật thông tin.");
                }
            }
        }

        private void SaveEmployeeInformation()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Update Employee information
                            string employeeQuery = @"
                            UPDATE Employee
                            SET Username = @Username, Password = @Password, FullName = @FullName, Birthday = @Birthday, Hometown = @Hometown, Gender = @Gender,
                            Ethnic = @Ethnic, PhoneNumber = @PhoneNumber, EmployeePositionCode = @EmployeePositionCode, Status = @Status,
                            DepartmentCode = @DepartmentCode, ContractCode = @ContractCode, EducationLevelCode = @EducationLevelCode,
                            SpecializedCode = @SpecializedCode, IdentityCard = @IdentityCard, Image = @Image
                            WHERE EmployeeCode = @EmployeeCode";

                            SqlCommand employeeCommand = new SqlCommand(employeeQuery, connection, transaction);

                            employeeCommand.Parameters.AddWithValue("@EmployeeCode", txtEmployeeCode.Text);
                            employeeCommand.Parameters.AddWithValue("@Username", txtUsername.Text);
                            employeeCommand.Parameters.AddWithValue("@Password", txtPassword.Text);
                            employeeCommand.Parameters.AddWithValue("@FullName", txtFullName.Text);
                            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy" };
                            DateTime birthday;
                            if (DateTime.TryParseExact(txtBirthday.Text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out birthday))
                            {
                                employeeCommand.Parameters.AddWithValue("@Birthday", birthday);
                            }
                            else
                            {
                                // Xử lý lỗi khi chuyển đổi không thành công
                                MessageBox.Show("Ngày sinh không hợp lệ. Vui lòng nhập lại với định dạng là ngày/tháng/năm hoặc ngày-tháng-năm");
                                return;
                            }
                            employeeCommand.Parameters.AddWithValue("@Hometown", txtHometown.Text);
                            employeeCommand.Parameters.AddWithValue("@Gender", comboBox_Gender.SelectedIndex);
                            employeeCommand.Parameters.AddWithValue("@Ethnic", txtEthnic.Text);
                            employeeCommand.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text);
                            employeeCommand.Parameters.AddWithValue("@EmployeePositionCode", comboBox_Position.SelectedValue);
                            employeeCommand.Parameters.AddWithValue("@Status", comboBox_Status.SelectedIndex);
                            employeeCommand.Parameters.AddWithValue("@DepartmentCode", comboBox_Department.SelectedValue);
                            employeeCommand.Parameters.AddWithValue("@ContractCode", comboBox_Contract.SelectedValue);
                            employeeCommand.Parameters.AddWithValue("@EducationLevelCode", comboBox_EducationLevel.SelectedValue);
                            employeeCommand.Parameters.AddWithValue("@SpecializedCode", comboBox_Specialized.SelectedValue);
                            employeeCommand.Parameters.AddWithValue("@IdentityCard", txtIdentityCard.Text);
                            employeeCommand.Parameters.AddWithValue("@Image", txtImage.Text);
                            employeeCommand.ExecuteNonQuery();


                            string selectQuery = @"
                            SELECT MinimumSalary
                            FROM Salary
                            WHERE EmployeeCode = @EmployeeCode";

                            SqlCommand selectCommand = new SqlCommand(selectQuery, connection, transaction);
                            selectCommand.Parameters.AddWithValue("@EmployeeCode", txtEmployeeCode.Text);

                            string currentMinimumSalary = string.Empty;
                            using (SqlDataReader reader = selectCommand.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    currentMinimumSalary = reader["MinimumSalary"].ToString();

                                }
                            }

                            // Update Employee Salary information
                            string salaryQuery = @"
                            UPDATE Salary
                            SET MinimumSalary = @MinimumSalary, SalaryCoefficient = @SalaryCoefficient, 
                                SocialInsurance = @SocialInsurance, HealthInsurance = @HealthInsurance, 
                                UnemploymentInsurance = @UnemploymentInsurance, Allowance = @Allowance, IncomeTax = @IncomeTax
                            WHERE EmployeeCode = @EmployeeCode";

                            SqlCommand salaryCommand = new SqlCommand(salaryQuery, connection, transaction);
                            salaryCommand.Parameters.AddWithValue("@EmployeeCode", txtEmployeeCode.Text);
                            salaryCommand.Parameters.AddWithValue("@MinimumSalary", Convert.ToSingle(txtMinimumSalary.Text.Replace("đ", "").Replace(",", "")));
                            salaryCommand.Parameters.AddWithValue("@SalaryCoefficient", Convert.ToSingle(txtSalaryCoefficient.Text.Replace("đ", "").Replace(",", "")));
                            salaryCommand.Parameters.AddWithValue("@SocialInsurance", Convert.ToSingle(txtSocialInsurance.Text.Replace("đ", "").Replace(",", "")));
                            salaryCommand.Parameters.AddWithValue("@HealthInsurance", Convert.ToSingle(txtHealthInsurance.Text.Replace("đ", "").Replace(",", "")));
                            salaryCommand.Parameters.AddWithValue("@UnemploymentInsurance", Convert.ToSingle(txtUnemploymentInsurance.Text.Replace("đ", "").Replace(",", "")));
                            salaryCommand.Parameters.AddWithValue("@Allowance", Convert.ToSingle(txtAllowance.Text.Replace("đ", "").Replace(",", "")));
                            salaryCommand.Parameters.AddWithValue("@IncomeTax", Convert.ToSingle(txtIncomeTax.Text.Replace("đ", "").Replace(",", "")));
                            salaryCommand.ExecuteNonQuery();


                            string updateSalaryUpdateQuery = @"
                            UPDATE SalaryUpdate 
                            SET CurrentSalary = @CurrentSalary, SalaryAfterUpdate = @SalaryAfterUpdate, 
                            SalaryCoefficient = @SalaryCoefficient, SocialInsurance = @SocialInsurance, 
                            HealthInsurance = @HealthInsurance, UnemploymentInsurance = @UnemploymentInsurance, 
                            Allowance = @Allowance, IncomeTax = @IncomeTax, UpdateDay = @UpdateDay
                            WHERE EmployeeCode = @EmployeeCode";

                            SqlCommand updateSalaryUpdateCommand = new SqlCommand(updateSalaryUpdateQuery, connection, transaction);
                            updateSalaryUpdateCommand.Parameters.AddWithValue("@EmployeeCode", txtEmployeeCode.Text);
                            updateSalaryUpdateCommand.Parameters.AddWithValue("@CurrentSalary", currentMinimumSalary);
                            updateSalaryUpdateCommand.Parameters.AddWithValue("@SalaryAfterUpdate", Convert.ToSingle(txtMinimumSalary.Text.Replace("đ", "").Replace(",", "")));
                            updateSalaryUpdateCommand.Parameters.AddWithValue("@SalaryCoefficient", Convert.ToSingle(txtSalaryCoefficient.Text.Replace("đ", "").Replace(",", "")));
                            updateSalaryUpdateCommand.Parameters.AddWithValue("@SocialInsurance", Convert.ToSingle(txtSocialInsurance.Text.Replace("đ", "").Replace(",", "")));
                            updateSalaryUpdateCommand.Parameters.AddWithValue("@HealthInsurance", Convert.ToSingle(txtHealthInsurance.Text.Replace("đ", "").Replace(",", "")));
                            updateSalaryUpdateCommand.Parameters.AddWithValue("@UnemploymentInsurance", Convert.ToSingle(txtUnemploymentInsurance.Text.Replace("đ", "").Replace(",", "")));
                            updateSalaryUpdateCommand.Parameters.AddWithValue("@Allowance", Convert.ToSingle(txtAllowance.Text.Replace("đ", "").Replace(",", "")));
                            updateSalaryUpdateCommand.Parameters.AddWithValue("@IncomeTax", Convert.ToSingle(txtIncomeTax.Text.Replace("đ", "").Replace(",", "")));
                            updateSalaryUpdateCommand.Parameters.AddWithValue("@UpdateDay", DateTime.Now);
                            updateSalaryUpdateCommand.ExecuteNonQuery();


                            /* Cập nhật chi tiết lương */
                            string updateSalaryDetailQuery = @"
                            UPDATE SalaryDetail
                            SET BasicSalary = @BasicSalary, SocialInsurance = @SocialInsurance, HealthInsurance = @HealthInsurance,
                            UnemploymentInsurance = @UnemploymentInsurance, Allowance = @Allowance, IncomeTax = @IncomeTax,
                            BonusMoney = @BonusMoney, DisciplineMoney = @DisciplineMoney, PayDay = @PayDay, TotalSalary = @TotalSalary
                            WHERE EmployeeCode = @EmployeeCode";

                            SqlCommand updateSalaryDetailCommand = new SqlCommand(updateSalaryDetailQuery, connection, transaction);
                            updateSalaryDetailCommand.Parameters.AddWithValue("@EmployeeCode", txtEmployeeCode.Text);
                            updateSalaryDetailCommand.Parameters.AddWithValue("@BasicSalary", Convert.ToSingle(txtMinimumSalary.Text.Replace("đ", "").Replace(",", "")));
                            updateSalaryDetailCommand.Parameters.AddWithValue("@SocialInsurance", Convert.ToSingle(txtSocialInsurance.Text.Replace("đ", "").Replace(",", "")));
                            updateSalaryDetailCommand.Parameters.AddWithValue("@HealthInsurance", Convert.ToSingle(txtHealthInsurance.Text.Replace("đ", "").Replace(",", "")));
                            updateSalaryDetailCommand.Parameters.AddWithValue("@UnemploymentInsurance", Convert.ToSingle(txtUnemploymentInsurance.Text.Replace("đ", "").Replace(",", "")));
                            updateSalaryDetailCommand.Parameters.AddWithValue("@Allowance", Convert.ToSingle(txtAllowance.Text.Replace("đ", "").Replace(",", "")));
                            updateSalaryDetailCommand.Parameters.AddWithValue("@IncomeTax", Convert.ToSingle(txtIncomeTax.Text.Replace("đ", "").Replace(",", "")));
                            updateSalaryDetailCommand.Parameters.AddWithValue("@BonusMoney", Convert.ToSingle(txtBonusMoney.Text.Replace("đ", "").Replace(",", "")));
                            updateSalaryDetailCommand.Parameters.AddWithValue("@DisciplineMoney", Convert.ToSingle(txtDisciplineMoney.Text.Replace("đ", "").Replace(",", "")));
                            updateSalaryDetailCommand.Parameters.AddWithValue("@TotalSalary", Convert.ToSingle(txtTotalSalary.Text.Replace("đ", "").Replace(",", "")));

                            DateTime payday;
                            if (DateTime.TryParseExact(txtPayDay.Text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out payday))
                            {
                                updateSalaryDetailCommand.Parameters.AddWithValue("@PayDay", payday);
                            }
                            else
                            {
                                // Xử lý lỗi khi chuyển đổi không thành công
                                MessageBox.Show("Ngày trả lương hợp lệ. Vui lòng nhập lại với định dạng là ngày/tháng/năm hoặc ngày-tháng-năm");
                                return;
                            }
                            updateSalaryDetailCommand.ExecuteNonQuery();


                            // Update Employee Rotation information
                            foreach (DataGridViewRow row in dataGridViewRotation.Rows)
                            {
                                if (row.IsNewRow) continue; // Bỏ qua dòng mới không có dữ liệu

                                string rotationQuery = @"
                                IF EXISTS (SELECT 1 FROM EmployeeRotation WHERE EmployeeCode = @EmployeeCode AND RotationDate = @RotationDate)
                                BEGIN
                                    UPDATE EmployeeRotation
                                    SET RotationReason = @RotationReason, DepartmentRotation = @DepartmentRotation, IncomingDepartment = @IncomingDepartment
                                    WHERE EmployeeCode = @EmployeeCode AND RotationDate = @RotationDate
                                END
                                ELSE
                                BEGIN
                                    INSERT INTO EmployeeRotation (EmployeeCode, RotationDate, RotationReason, DepartmentRotation, IncomingDepartment)
                                    VALUES (@EmployeeCode, @RotationDate, @RotationReason, @DepartmentRotation, @IncomingDepartment)
                                END";

                                SqlCommand rotationCommand = new SqlCommand(rotationQuery, connection, transaction);
                                rotationCommand.Parameters.AddWithValue("@EmployeeCode", txtEmployeeCode.Text);
                                rotationCommand.Parameters.AddWithValue("@RotationDate", Convert.ToDateTime(row.Cells["Ngày luân chuyển"].Value));
                                rotationCommand.Parameters.AddWithValue("@RotationReason", row.Cells["Lý do luân chuyển"].Value.ToString());
                                rotationCommand.Parameters.AddWithValue("@DepartmentRotation", row.Cells["Phòng ban chuyển"].Value.ToString());
                                rotationCommand.Parameters.AddWithValue("@IncomingDepartment", row.Cells["Phòng ban đến"].Value.ToString());
                                rotationCommand.ExecuteNonQuery();
                            }

                            // Update Employee Bonus information
                            foreach (DataGridViewRow row in dataGridViewBonus.Rows)
                            {
                                if (row.IsNewRow) continue;

                                string bonusQuery = @"
                            IF EXISTS (SELECT 1 FROM Bonus WHERE EmployeeCode = @EmployeeCode AND BonusDate = @BonusDate)
                            BEGIN
                                UPDATE Bonus
                                SET Reason = @Reason, BonusMoney = @BonusMoney
                                WHERE EmployeeCode = @EmployeeCode AND BonusDate = @BonusDate
                            END
                            ELSE
                            BEGIN
                                INSERT INTO Bonus (EmployeeCode, BonusDate, Reason, BonusMoney)
                                VALUES (@EmployeeCode, @BonusDate, @Reason, @BonusMoney)
                            END";

                                SqlCommand bonusCommand = new SqlCommand(bonusQuery, connection, transaction);
                                bonusCommand.Parameters.AddWithValue("@EmployeeCode", txtEmployeeCode.Text);
                                bonusCommand.Parameters.AddWithValue("@Reason", row.Cells["Lý do thưởng"].Value.ToString());
                                bonusCommand.Parameters.AddWithValue("@BonusDate", Convert.ToDateTime(row.Cells["Ngày khen thưởng"].Value));
                                bonusCommand.Parameters.AddWithValue("@BonusMoney", Convert.ToSingle(row.Cells["Tiền khen thưởng"].Value));
                                bonusCommand.ExecuteNonQuery();
                            }

                            // Update Employee Discipline information
                            foreach (DataGridViewRow row in dataGridViewDiscipline.Rows)
                            {
                                if (row.IsNewRow) continue;

                                string disciplineQuery = @"
                            IF EXISTS (SELECT 1 FROM Discipline WHERE EmployeeCode = @EmployeeCode AND DisciplineDate = @DisciplineDate)
                            BEGIN
                                UPDATE Discipline
                                SET Reason = @Reason, DisciplineMoney = @DisciplineMoney
                                WHERE EmployeeCode = @EmployeeCode AND DisciplineDate = @DisciplineDate
                            END
                            ELSE
                            BEGIN
                                INSERT INTO Discipline (EmployeeCode, DisciplineDate, Reason, DisciplineMoney)
                                VALUES (@EmployeeCode, @DisciplineDate, @Reason, @DisciplineMoney)
                            END";

                                SqlCommand disciplineCommand = new SqlCommand(disciplineQuery, connection, transaction);
                                disciplineCommand.Parameters.AddWithValue("@EmployeeCode", txtEmployeeCode.Text);
                                disciplineCommand.Parameters.AddWithValue("@Reason", row.Cells["Lý do kỷ luật"].Value.ToString());
                                disciplineCommand.Parameters.AddWithValue("@DisciplineDate", Convert.ToDateTime(row.Cells["Ngày kỷ luật"].Value));
                                disciplineCommand.Parameters.AddWithValue("@DisciplineMoney", Convert.ToSingle(row.Cells["Tiền kỷ luật"].Value));
                                disciplineCommand.ExecuteNonQuery();
                            }

                            foreach (DataGridViewRow row in dataGridViewUniversity.Rows)
                            {
                                if (row.IsNewRow) continue;

                                string universityQuery = @"
                            IF EXISTS (SELECT 1 FROM University WHERE EmployeeCode = @EmployeeCode AND GraduateYear = @GraduateYear)
                            BEGIN
                                UPDATE University
                                SET UniversityName = @UniversityName, TrainingCountry = @TrainingCountry, GraduateYear = @GraduateYear
                                WHERE EmployeeCode = @EmployeeCode AND GraduateYear = @GraduateYear
                            END
                            ELSE
                            BEGIN
                                INSERT INTO University (EmployeeCode, UniversityName, TrainingCountry, GraduateYear)
                                VALUES (@EmployeeCode, @UniversityName, @TrainingCountry, @GraduateYear)
                            END";

                                SqlCommand universityCommand = new SqlCommand(universityQuery, connection, transaction);
                                universityCommand.Parameters.AddWithValue("@EmployeeCode", txtEmployeeCode.Text);
                                universityCommand.Parameters.AddWithValue("@UniversityName", row.Cells["Tên trường đại học"].Value.ToString());
                                universityCommand.Parameters.AddWithValue("@TrainingCountry", row.Cells["Quốc gia đào tạo"].Value.ToString());
                                universityCommand.Parameters.AddWithValue("@GraduateYear", row.Cells["Năm tốt nghiệp"].Value.ToString());
                                universityCommand.ExecuteNonQuery();
                            }

                            foreach (DataGridViewRow row in dataGridViewAfterUniversity.Rows)
                            {
                                if (row.IsNewRow) continue;

                                string afterUniversityQuery = @"
                            IF EXISTS (SELECT 1 FROM AfterUniversity WHERE EmployeeCode = @EmployeeCode AND DegreeYearMaster = @DegreeYearMaster)
                            BEGIN
                                UPDATE AfterUniversity
                                SET SpecializedMaster = @SpecializedMaster, TrainingPlaceMaster = @TrainingPlaceMaster, DegreeYearMaster = @DegreeYearMaster,
                                SpecializedDoctorate = @SpecializedDoctorate, TrainingPlaceDoctorate = @TrainingPlaceDoctorate, DegreeYearDoctorate = @DegreeYearDoctorate
                                WHERE EmployeeCode = @EmployeeCode AND DegreeYearMaster = @DegreeYearMaster
                            END
                            ELSE
                            BEGIN
                                INSERT INTO AfterUniversity (EmployeeCode, SpecializedMaster, TrainingPlaceMaster, DegreeYearMaster,
                                SpecializedDoctorate, TrainingPlaceDoctorate, DegreeYearDoctorate)
                                VALUES (@EmployeeCode, @SpecializedMaster, @TrainingPlaceMaster, @DegreeYearMaster, 
                                @SpecializedDoctorate, @TrainingPlaceDoctorate, @DegreeYearDoctorate)
                            END";

                                SqlCommand afterUniversityCommand = new SqlCommand(afterUniversityQuery, connection, transaction);
                                afterUniversityCommand.Parameters.AddWithValue("@EmployeeCode", txtEmployeeCode.Text);
                                afterUniversityCommand.Parameters.AddWithValue("@SpecializedMaster", row.Cells["Thạc sĩ chuyên ngành"].Value.ToString());
                                afterUniversityCommand.Parameters.AddWithValue("@TrainingPlaceMaster", row.Cells["Nơi đào tạo thạc sĩ"].Value.ToString());
                                afterUniversityCommand.Parameters.AddWithValue("@DegreeYearMaster", row.Cells["Năm cấp bằng thạc sĩ"].Value.ToString());
                                afterUniversityCommand.Parameters.AddWithValue("@SpecializedDoctorate", row.Cells["Tiến sĩ chuyên ngành"].Value.ToString());
                                afterUniversityCommand.Parameters.AddWithValue("@TrainingPlaceDoctorate", row.Cells["Nơi đào tạo tiến sĩ"].Value.ToString());
                                afterUniversityCommand.Parameters.AddWithValue("@DegreeYearDoctorate", row.Cells["Năm cấp bằng tiến sĩ"].Value.ToString());
                                afterUniversityCommand.ExecuteNonQuery();
                            }

                            foreach (DataGridViewRow row in dataGridViewForeignLanguage.Rows)
                            {
                                if (row.IsNewRow) continue;

                                string foreignLanguageQuery = @"
                                IF EXISTS (SELECT 1 FROM ForeignLanguage WHERE EmployeeCode = @EmployeeCode AND ForeignLanguageName = @ForeignLanguageName)
                                BEGIN
                                    UPDATE ForeignLanguage
                                    SET Level = @Level, ForeignLanguageName = @ForeignLanguageName
                                    WHERE EmployeeCode = @EmployeeCode AND ForeignLanguageName = @ForeignLanguageName
                                END
                                ELSE
                                BEGIN
                                    INSERT INTO ForeignLanguage (EmployeeCode, ForeignLanguageName, Level)
                                    VALUES (@EmployeeCode, @ForeignLanguageName, @Level)
                                END";

                                SqlCommand foreignLanguageCommand = new SqlCommand(foreignLanguageQuery, connection, transaction);
                                foreignLanguageCommand.Parameters.AddWithValue("@EmployeeCode", txtEmployeeCode.Text);
                                foreignLanguageCommand.Parameters.AddWithValue("@ForeignLanguageName", row.Cells["Tên ngoại ngữ"].Value.ToString());
                                foreignLanguageCommand.Parameters.AddWithValue("@Level", row.Cells["Mức độ sử dụng"].Value.ToString());
                                foreignLanguageCommand.ExecuteNonQuery();
                            }

                            foreach (DataGridViewRow row in dataGridViewWorkingProcess.Rows)
                            {
                                if (row.IsNewRow) continue;

                                string workingProcessQuery = @"
                                IF EXISTS (SELECT 1 FROM WorkingProcess WHERE EmployeeCode = @EmployeeCode AND Time = @Time)
                                BEGIN
                                    UPDATE WorkingProcess
                                    SET WorkPlace = @WorkPlace, WorkUndertake = @WorkUndertake, Time = @Time
                                    WHERE EmployeeCode = @EmployeeCode AND Time = @Time
                                END
                                ELSE
                                BEGIN
                                    INSERT INTO WorkingProcess (EmployeeCode, WorkPlace, WorkUndertake, Time)
                                    VALUES (@EmployeeCode, @WorkPlace, @WorkUndertake, @Time)
                                END";

                                SqlCommand workingProcessCommand = new SqlCommand(workingProcessQuery, connection, transaction);
                                workingProcessCommand.Parameters.AddWithValue("@EmployeeCode", txtEmployeeCode.Text);
                                workingProcessCommand.Parameters.AddWithValue("@WorkPlace", row.Cells["Nơi công tác"].Value.ToString());
                                workingProcessCommand.Parameters.AddWithValue("@WorkUndertake", row.Cells["Công việc đảm nhận"].Value.ToString());
                                workingProcessCommand.Parameters.AddWithValue("@Time", row.Cells["Thời gian"].Value.ToString());
                                workingProcessCommand.ExecuteNonQuery();
                            }

                            foreach (DataGridViewRow row in dataGridViewScientificResearchTopics.Rows)
                            {
                                if (row.IsNewRow) continue;

                                string scientificResearchTopicsQuery = @"
                                IF EXISTS (SELECT 1 FROM ScientificResearchTopics WHERE EmployeeCode = @EmployeeCode AND YearOfBegin = @YearOfBegin)
                                BEGIN
                                    UPDATE ScientificResearchTopics
                                    SET ScientificResearchTopicName = @ScientificResearchTopicName, YearOfBegin = @YearOfBegin, 
                                    YearOfComplete = @YearOfComplete, LevelTopic = @LevelTopic, ResponsibilityInTheTopic = @ResponsibilityInTheTopic
                                    WHERE EmployeeCode = @EmployeeCode AND YearOfBegin = @YearOfBegin
                                END
                                ELSE
                                BEGIN
                                    INSERT INTO ScientificResearchTopics (EmployeeCode, ScientificResearchTopicName, YearOfBegin, YearOfComplete,
                                    LevelTopic, ResponsibilityInTheTopic)
                                    VALUES (@EmployeeCode, @ScientificResearchTopicName, @YearOfBegin, @YearOfComplete, @LevelTopic, @ResponsibilityInTheTopic)
                                END";

                                SqlCommand scientificResearchTopicsCommand = new SqlCommand(scientificResearchTopicsQuery, connection, transaction);
                                scientificResearchTopicsCommand.Parameters.AddWithValue("@EmployeeCode", txtEmployeeCode.Text);
                                scientificResearchTopicsCommand.Parameters.AddWithValue("@ScientificResearchTopicName", row.Cells["Tên đề tài nghiên cứu"].Value.ToString());
                                scientificResearchTopicsCommand.Parameters.AddWithValue("@YearOfBegin", row.Cells["Năm bắt đầu"].Value.ToString());
                                scientificResearchTopicsCommand.Parameters.AddWithValue("@YearOfComplete", row.Cells["Năm hoàn thành"].Value.ToString());
                                scientificResearchTopicsCommand.Parameters.AddWithValue("@LevelTopic", row.Cells["Đề tài cấp"].Value.ToString());
                                scientificResearchTopicsCommand.Parameters.AddWithValue("@ResponsibilityInTheTopic", row.Cells["Trách nhiệm tham gia trong đề tài"].Value.ToString());
                                scientificResearchTopicsCommand.ExecuteNonQuery();
                            }

                            foreach (DataGridViewRow row in dataGridViewScientificWorks.Rows)
                            {
                                if (row.IsNewRow) continue;

                                string scientificWorksQuery = @"
                                IF EXISTS (SELECT 1 FROM ScientificWorks WHERE EmployeeCode = @EmployeeCode AND Year = @Year)
                                BEGIN
                                    UPDATE ScientificWorks
                                    SET ScientificWorksName = @ScientificWorksName, Year = @Year, MagazineName = @MagazineName
                                    WHERE EmployeeCode = @EmployeeCode AND Year = @Year
                                END
                                ELSE
                                BEGIN
                                    INSERT INTO ScientificWorks (EmployeeCode, ScientificWorksName, Year, MagazineName)
                                    VALUES (@EmployeeCode, @ScientificWorksName, @Year, @MagazineName)
                                END";

                                SqlCommand scientificWorksCommand = new SqlCommand(scientificWorksQuery, connection, transaction);
                                scientificWorksCommand.Parameters.AddWithValue("@EmployeeCode", txtEmployeeCode.Text);
                                scientificWorksCommand.Parameters.AddWithValue("@ScientificWorksName", row.Cells["Tên công trình"].Value.ToString());
                                scientificWorksCommand.Parameters.AddWithValue("@Year", row.Cells["Năm công bố"].Value.ToString());
                                scientificWorksCommand.Parameters.AddWithValue("@MagazineName", row.Cells["Tên tạp chí"].Value.ToString());
                                scientificWorksCommand.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Cập nhật thông tin thành công.");

                            ChooseFileButton.Hide();
                            txtImage.Hide();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Lỗi: " + ex.Message);

                            ChooseFileButton.Hide();
                            txtImage.Hide();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void SetFieldsEnabled(bool enabled)
        {
            /* Thông tin nhân viên */
            txtEmployeeCode.Enabled = enabled;
            txtUsername.Enabled = enabled;
            txtPassword.Enabled = enabled;
            txtFullName.Enabled = enabled;
            txtBirthday.Enabled = enabled;
            txtHometown.Enabled = enabled;
            comboBox_Gender.Enabled = enabled;
            txtEthnic.Enabled = enabled;
            txtPhoneNumber.Enabled = enabled;
            comboBox_Status.Enabled = enabled;
            comboBox_Position.Enabled = enabled;
            comboBox_Department.Enabled = enabled;
            comboBox_Contract.Enabled = enabled;
            comboBox_EducationLevel.Enabled = enabled;
            comboBox_Specialized.Enabled = enabled;
            txtIdentityCard.Enabled = enabled;

            /* Thông tin lương */
            txtMinimumSalary.Enabled = enabled;
            txtSalaryCoefficient.Enabled = enabled;
            txtSocialInsurance.Enabled = enabled;
            txtHealthInsurance.Enabled = enabled;
            txtUnemploymentInsurance.Enabled = enabled;
            txtIncomeTax.Enabled = enabled;
            txtAllowance.Enabled = enabled;

            /* Chi tiết lương */
            txtBonusMoney.Enabled = enabled;
            txtDisciplineMoney.Enabled = enabled;
            txtPayDay.Enabled = enabled;
            txtTotalSalary.Enabled = enabled;

            /* DataGridView */
            dataGridViewRotation.Enabled = enabled;
            dataGridViewBonus.Enabled = enabled;
            dataGridViewDiscipline.Enabled = enabled;
            dataGridViewUniversity.Enabled = enabled;
            dataGridViewAfterUniversity.Enabled = enabled;
            dataGridViewForeignLanguage.Enabled = enabled;
            dataGridViewWorkingProcess.Enabled = enabled;
            dataGridViewScientificResearchTopics.Enabled = enabled;
            dataGridViewScientificWorks.Enabled = enabled;
        }
        private void SetFieldsEnabledButton(bool enabled)
        {
            addButton.Enabled = enabled;
            updateButton.Enabled = enabled;
            deleteButton.Enabled = enabled;
            reportButton.Enabled = enabled;
        }

        private void SetVisibleButton(bool visible)
        {
            addButton.Visible = visible;
            updateButton.Visible = visible;
            deleteButton.Visible = visible;
            reportButton.Visible = visible;
        }
        private string GetSelectedEmployeeId()
        {
            if (dataGridViewResults.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewResults.SelectedRows[0];
                var cellValue = selectedRow.Cells["Mã"].Value;

                if (cellValue != null)
                {
                    MessageBox.Show($"Cập nhật nhân viên mã số: {cellValue.ToString()}");
                    return cellValue.ToString();
                }
            }
            else
            {
                MessageBox.Show("Không có nhân viên được chọn.");
            }
            return null;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveEmployeeInformation();
            isEditMode = false;
            SetFieldsEnabled(false);
            saveButton.Hide();
            notUpdateData.Hide();
            updateButton.Show();
            dataGridViewResults.Enabled = true;
            SetFieldsEnabledButton(true);
            SetVisibleDeleteDataGridButton(false);
        }

        private void notUpdateData_Click(object sender, EventArgs e)
        {
            isEditMode = false;
            SetFieldsEnabled(false);
            dataGridViewResults.Enabled = true;
            notUpdateData.Hide();
            saveButton.Hide();
            updateButton.Show();
            MessageBox.Show("Hủy cập nhật thông tin");
            SetFieldsEnabledButton(true);

            ChooseFileButton.Hide();
            txtImage.Hide();
            SetVisibleDeleteDataGridButton(false);
        }

        private void SelectComboBoxGender()
        {
            comboBox_Gender.Items.Clear();
            comboBox_Gender.Items.AddRange(new object[] { "Nữ", "Nam" });
            comboBox_Gender.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void SelectComboBoxStatus()
        {
            comboBox_Status.Items.Clear();
            comboBox_Status.Items.AddRange(new object[] { "Thôi việc", "Đang làm việc" });
            comboBox_Status.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void SelectComboBoxPosition()
        {
            comboBox_Position.DataSource = null;
            comboBox_Position.Items.Clear();
            comboBox_Position.Items.AddRange(new object[]
            {
                new KeyValuePair<string, string>("nv", "Nhân viên"),
                new KeyValuePair<string, string>("pp", "Phó phòng, Phó khoa"),
                new KeyValuePair<string, string>("tbm", "Trưởng bộ môn"),
                new KeyValuePair<string, string>("tp", "Trưởng phòng, Trưởng khoa")
            });

            comboBox_Position.DisplayMember = "Value";
            comboBox_Position.ValueMember = "Key";
            comboBox_Position.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void SelectComboBoxDepartment()
        {
            comboBox_Department.DataSource = null;
            comboBox_Department.Items.Clear();
            comboBox_Department.Items.AddRange(new object[]
            {
                new KeyValuePair<string, string>("cntt", "Công nghệ thông tin"),
                new KeyValuePair<string, string>("daotao", "Đào tạo"),
                new KeyValuePair<string, string>("ketoan", "Kế toán"),
                new KeyValuePair<string, string>("xaydung", "Xây dựng")
            });

            comboBox_Department.DisplayMember = "Value";
            comboBox_Department.ValueMember = "Key";
            comboBox_Department.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void SelectComboBoxContract()
        {
            comboBox_Contract.DataSource = null;
            comboBox_Contract.Items.Clear();
            comboBox_Contract.Items.AddRange(new object[]
            {
                new KeyValuePair<string, string>("0001", "Nhân viên chính thức"),
                new KeyValuePair<string, string>("0002", "Thử việc")
            });

            comboBox_Contract.DisplayMember = "Value";
            comboBox_Contract.ValueMember = "Key";
            comboBox_Contract.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void SelectComboBoxSpecialized()
        {
            comboBox_Specialized.DataSource = null;
            comboBox_Specialized.Items.Clear();
            comboBox_Specialized.Items.AddRange(new object[]
            {
                new KeyValuePair<string, string>("ck", "Cơ khí"),
                new KeyValuePair<string, string>("cntt", "Công nghệ thông tin"),
                new KeyValuePair<string, string>("cth", "Chính trị học"),
                new KeyValuePair<string, string>("dientu", "Điện tử"),
                new KeyValuePair<string, string>("hoahoc", "Hóa học"),
                new KeyValuePair<string, string>("kt", "Kế toán"),
                new KeyValuePair<string, string>("nl", "Nhiệt lạnh"),
                new KeyValuePair<string, string>("sinhhoc", "Sinh học"),
                new KeyValuePair<string, string>("toan", "Toán")
            });

            comboBox_Specialized.DisplayMember = "Value";
            comboBox_Specialized.ValueMember = "Key";
            comboBox_Specialized.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void SelectComboBoxEducationLevel()
        {
            comboBox_EducationLevel.DataSource = null;
            comboBox_EducationLevel.Items.Clear();
            comboBox_EducationLevel.Items.AddRange(new object[]
            {
                new KeyValuePair<string, string>("gs", "Giáo sư"),
                new KeyValuePair<string, string>("ks", "Kỹ sư"),
                new KeyValuePair<string, string>("pgs", "Phó giáo sư"),
                new KeyValuePair<string, string>("ths", "Thạc sỹ"),
                new KeyValuePair<string, string>("ts", "Tiến sỹ"),
            });

            comboBox_EducationLevel.DisplayMember = "Value";
            comboBox_EducationLevel.ValueMember = "Key";
            comboBox_EducationLevel.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn hay không
            if (dataGridViewResults.CurrentRow != null)
            {
                // Lấy hàng hiện tại được chọn
                DataGridViewRow selectedRow = dataGridViewResults.CurrentRow;

                // Lấy mã nhân viên từ cột "MaNhanVien"
                string employeeCode = selectedRow.Cells["Mã"].Value.ToString();

                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Thực hiện lệnh xóa trong cơ sở dữ liệu
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = "DELETE FROM Employee WHERE EmployeeCode = @EmployeeCode";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Nhân viên đã được xóa thành công.");
                                LoadData(); // Refresh the data grid view
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy nhân viên để xóa.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                    ClearFields();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa.");
            }
        }

        /*private void ChooseFileButton_Click_1(object sender, EventArgs e)
        {
            // Define the target directory (Images folder)
            string targetDirectory = @"C:\Users\ADMIN\source\repos\QuanLyNhanSu\QuanLyNhanSu\Images";

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Set the initial directory to the Images folder
                openFileDialog.InitialDirectory = targetDirectory;
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of the selected file
                    string sourceFilePath = openFileDialog.FileName;

                    // Set the file name and target file path
                    string fileName = Path.GetFileName(sourceFilePath);
                    string targetFilePath = Path.Combine(targetDirectory, fileName);

                    // Copy the file to the target directory if it's not already there
                    try
                    {
                        if (!File.Exists(targetFilePath))
                        {
                            File.Copy(sourceFilePath, targetFilePath, true);
                        }

                        // Display the relative file path in txtImage
                        string relativeFilePath = @"Images\" + fileName;
                        txtImage.Text = relativeFilePath;
                        Image.Image = new Bitmap(targetFilePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }*/

        private void ChooseFileButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of specified file
                    string filePath = openFileDialog.FileName;
                    // Show the path in txtImage
                    txtImage.Text = filePath;
                    Image.Image = new Bitmap(filePath);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
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

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Ngăn chặn âm thanh "ding"
                searchButton_Click(sender, e); // Gọi sự kiện LoginButton_Click
            }
        }
        private DataTable LoadEmployeeInformationReport(string employeeCode)
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
                Employee.Ethnic, Employee.PhoneNumber, EmployeePosition.PositionName, Department.DepartmentName, Contract.ContractType, 
                EducationLevel.EducationLevelName, Specialized.SpecializedName, Employee.IdentityCard,
                University.UniversityName, University.TrainingCountry, University.GraduateYear,
                AfterUniversity.SpecializedMaster, AfterUniversity.TrainingPlaceMaster, AfterUniversity.DegreeYearMaster,
                AfterUniversity.SpecializedDoctorate, AfterUniversity.TrainingPlaceDoctorate, AfterUniversity.DegreeYearDoctorate,
                ForeignLanguage.ForeignLanguageName, ForeignLanguage.Level,
                WorkingProcess.WorkPlace, WorkingProcess.WorkUndertake, WorkingProcess.Time,
                ScientificWorks.ScientificWorksName, ScientificWorks.Year, ScientificWorks.MagazineName,
                ScientificResearchTopics.ScientificResearchTopicName, ScientificResearchTopics.YearOfBegin, 
                ScientificResearchTopics.YearOfComplete, ScientificResearchTopics.LevelTopic, ScientificResearchTopics.ResponsibilityInTheTopic
                FROM Employee
                JOIN EmployeePosition ON Employee.EmployeePositionCode = EmployeePosition.EmployeePositionCode
                JOIN Department ON Employee.DepartmentCode = Department.DepartmentCode
                JOIN Contract ON Employee.ContractCode = Contract.ContractCode
                JOIN EducationLevel ON Employee.EducationLevelCode = EducationLevel.EducationLevelCode 
                JOIN Specialized ON Employee.SpecializedCode = Specialized.SpecializedCode
                LEFT JOIN University ON Employee.EmployeeCode = University.EmployeeCode
                LEFT JOIN AfterUniversity ON Employee.EmployeeCode = AfterUniversity.EmployeeCode
                LEFT JOIN ForeignLanguage ON Employee.EmployeeCode = ForeignLanguage.EmployeeCode
                LEFT JOIN WorkingProcess ON Employee.EmployeeCode = WorkingProcess.EmployeeCode
                LEFT JOIN ScientificWorks ON Employee.EmployeeCode = ScientificWorks.EmployeeCode
                LEFT JOIN ScientificResearchTopics ON Employee.EmployeeCode = ScientificResearchTopics.EmployeeCode
                WHERE Employee.EmployeeCode = @EmployeeCode";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
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

        private DataTable LoadEmployeeSalaryReport(string employeeCode)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"          
                    SELECT Employee.EmployeeCode, Employee.FullName, Salary.MinimumSalary, 
                    Salary.SalaryCoefficient, Salary.SocialInsurance, Salary.HealthInsurance,
                    Salary.UnemploymentInsurance, Salary.Allowance, Salary.IncomeTax
                    FROM Employee
                    JOIN Salary ON Employee.EmployeeCode = Salary.EmployeeCode
                    WHERE Employee.EmployeeCode = @EmployeeCode";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
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

        private DataTable LoadEmployeeSalaryDetailReport(string employeeCode)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"          
                    SELECT Employee.EmployeeCode, Employee.FullName, SalaryDetail.BasicSalary, 
                    SalaryDetail.SocialInsurance, SalaryDetail.HealthInsurance, SalaryDetail.UnemploymentInsurance,
                    SalaryDetail.Allowance, SalaryDetail.IncomeTax, SalaryDetail.BonusMoney,
                    SalaryDetail.DisciplineMoney, SalaryDetail.PayDay, SalaryDetail.TotalSalary
                    FROM Employee
                    JOIN SalaryDetail ON Employee.EmployeeCode = SalaryDetail.EmployeeCode
                    WHERE Employee.EmployeeCode = @EmployeeCode";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
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
        private void reportButton_Click(object sender, EventArgs e)
        {
            bool areButtonsVisible = EmployeeInformationReport.Visible;

            EmployeeInformationReport.Visible = !areButtonsVisible;
            EmployeeSalaryReport.Visible = !areButtonsVisible;
            EmployeeSalaryDetailReport.Visible = !areButtonsVisible;
            EmployeeInDepartmentReport.Visible = !areButtonsVisible;

            SetFieldsEnabledButton(false);
            reportButton.Enabled = true;

            if (areButtonsVisible)
            {
                SetFieldsEnabledButton(true);
            }
        }

        private void EmployeeInformationReport_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmployeeCode.Text))
            {
                string employeeCode = txtEmployeeCode.Text;

                // Chuẩn bị dữ liệu cho report
                DataTable dataTable = LoadEmployeeInformationReport(employeeCode);
                DataTable dataTable1 = LoadUnitUsedtReport();

                // Tạo form report và truyền dữ liệu vào
                Report reportForm = new Report();
                reportForm.EmployeeInformationData = dataTable;
                reportForm.UnitUsedData = dataTable1;
                reportForm.Show();
            }
            else
            {
                MessageBox.Show("Hãy chọn một nhân viên.");
            }
        }

        private void EmployeeSalaryReport_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmployeeCode.Text))
            {
                string employeeCode = txtEmployeeCode.Text;

                // Chuẩn bị dữ liệu cho report
                DataTable dataTable = LoadEmployeeSalaryReport(employeeCode);
                DataTable dataTable1 = LoadUnitUsedtReport();

                // Tạo form report và truyền dữ liệu vào
                EmployeeSalaryReport employeeSalaryReport = new EmployeeSalaryReport();
                employeeSalaryReport.EmployeeSalaryData = dataTable;
                employeeSalaryReport.UnitUsedData = dataTable1;
                employeeSalaryReport.Show();
            }
            else
            {
                MessageBox.Show("Hãy chọn một nhân viên.");
            }
        }

        private void EmployeeSalaryDetailReport_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmployeeCode.Text))
            {
                string employeeCode = txtEmployeeCode.Text;

                // Chuẩn bị dữ liệu cho report
                DataTable dataTable = LoadEmployeeSalaryDetailReport(employeeCode);
                DataTable dataTable1 = LoadUnitUsedtReport();

                // Tạo form report và truyền dữ liệu vào
                EmployeeSalaryDetailReport employeeSalaryDetailReport = new EmployeeSalaryDetailReport();
                employeeSalaryDetailReport.EmployeeSalaryDetailData = dataTable;
                employeeSalaryDetailReport.UnitUsedData = dataTable1;
                employeeSalaryDetailReport.Show();
            }
            else
            {
                MessageBox.Show("Hãy chọn một nhân viên.");
            }
        }

        /* Xóa thông tin trong các dataGridView */
        private void DeleteBonusButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewResults.CurrentRow != null)
            {
                // Lấy hàng hiện tại được chọn
                DataGridViewRow selectedRow = dataGridViewResults.CurrentRow;

                // Lấy mã nhân viên từ cột "Mã"
                string employeeCode = selectedRow.Cells["Mã"].Value.ToString();

                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa thông tin khen thưởng này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Thực hiện lệnh xóa trong cơ sở dữ liệu
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = "DELETE FROM Bonus WHERE EmployeeCode = @EmployeeCode";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Thông tin khen thưởng đã được xóa thành công.");
                                LoadData(); // Refresh the data grid view
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy thông tin khen thưởng để xóa.");
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
                MessageBox.Show("Vui lòng chọn một nhân viên.");
            }
        }
        private void DeleteDisciplineButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewResults.CurrentRow != null)
            {
                // Lấy hàng hiện tại được chọn
                DataGridViewRow selectedRow = dataGridViewResults.CurrentRow;

                // Lấy mã nhân viên từ cột "Mã"
                string employeeCode = selectedRow.Cells["Mã"].Value.ToString();

                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa thông tin kỷ luật này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Thực hiện lệnh xóa trong cơ sở dữ liệu
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = "DELETE FROM Discipline WHERE EmployeeCode = @EmployeeCode";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Thông tin kỷ luật đã được xóa thành công.");
                                LoadData(); // Refresh the data grid view
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy thông tin kỷ luật để xóa.");
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
                MessageBox.Show("Vui lòng chọn một nhân viên.");
            }
        }
        private void DeleteEmployeeRotationButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewResults.CurrentRow != null)
            {
                // Lấy hàng hiện tại được chọn
                DataGridViewRow selectedRow = dataGridViewResults.CurrentRow;

                // Lấy mã nhân viên từ cột "Mã"
                string employeeCode = selectedRow.Cells["Mã"].Value.ToString();

                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa thông công tác này này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Thực hiện lệnh xóa trong cơ sở dữ liệu
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = "DELETE FROM EmployeeRotation WHERE EmployeeCode = @EmployeeCode";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Nhân viên đã được xóa thành công.");
                                LoadData(); // Refresh the data grid view
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy nhân viên để xóa.");
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
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa.");
            }
        }

        private void DeleteUniversityButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewResults.CurrentRow != null)
            {
                // Lấy hàng hiện tại được chọn
                DataGridViewRow selectedRow = dataGridViewResults.CurrentRow;

                // Lấy mã nhân viên từ cột "Mã"
                string employeeCode = selectedRow.Cells["Mã"].Value.ToString();

                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa thông tin đại học này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Thực hiện lệnh xóa trong cơ sở dữ liệu
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = "DELETE FROM University WHERE EmployeeCode = @EmployeeCode";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Thông tin đại học đã được xóa thành công.");
                                LoadData(); // Refresh the data grid view
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy thông tin đại học để xóa.");
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
                MessageBox.Show("Vui lòng chọn một nhân viên.");
            }
        }

        private void DeleteAfterUniversityButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewResults.CurrentRow != null)
            {
                // Lấy hàng hiện tại được chọn
                DataGridViewRow selectedRow = dataGridViewResults.CurrentRow;

                // Lấy mã nhân viên từ cột "Mã"
                string employeeCode = selectedRow.Cells["Mã"].Value.ToString();

                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa thông tin sau đại học này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Thực hiện lệnh xóa trong cơ sở dữ liệu
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = "DELETE FROM AfterUniversity WHERE EmployeeCode = @EmployeeCode";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Thông tin sau đại học đã được xóa thành công.");
                                LoadData(); // Refresh the data grid view
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy thông tin sau đại học để xóa.");
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
                MessageBox.Show("Vui lòng chọn một nhân viên.");
            }
        }

        private void DeleteForeignLanguageButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewResults.CurrentRow != null)
            {
                // Lấy hàng hiện tại được chọn
                DataGridViewRow selectedRow = dataGridViewResults.CurrentRow;

                // Lấy mã nhân viên từ cột "Mã"
                string employeeCode = selectedRow.Cells["Mã"].Value.ToString();

                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa thông tin ngoại ngữ này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Thực hiện lệnh xóa trong cơ sở dữ liệu
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = "DELETE FROM ForeignLanguage WHERE EmployeeCode = @EmployeeCode";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Thông tin ngoại ngữ đã được xóa thành công.");
                                LoadData(); // Refresh the data grid view
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy thông tin ngoại ngữ để xóa.");
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
                MessageBox.Show("Vui lòng chọn một nhân viên.");
            }
        }

        private void DeleteWorkingProcessButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewResults.CurrentRow != null)
            {
                // Lấy hàng hiện tại được chọn
                DataGridViewRow selectedRow = dataGridViewResults.CurrentRow;

                // Lấy mã nhân viên từ cột "Mã"
                string employeeCode = selectedRow.Cells["Mã"].Value.ToString();

                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa thông công tác chuyên môn này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Thực hiện lệnh xóa trong cơ sở dữ liệu
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = "DELETE FROM WorkingProcess WHERE EmployeeCode = @EmployeeCode";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Thông tin công tác chuyên môn đã được xóa thành công.");
                                LoadData(); // Refresh the data grid view
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy thông tin công tác chuyên môn để xóa.");
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
                MessageBox.Show("Vui lòng chọn một nhân viên.");
            }
        }

        private void DeleteScientificResearchTopicsButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewResults.CurrentRow != null)
            {
                // Lấy hàng hiện tại được chọn
                DataGridViewRow selectedRow = dataGridViewResults.CurrentRow;

                // Lấy mã nhân viên từ cột "Mã"
                string employeeCode = selectedRow.Cells["Mã"].Value.ToString();

                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa thông tin các đề tài nghiên cứu khoa học này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Thực hiện lệnh xóa trong cơ sở dữ liệu
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = "DELETE FROM ScientificResearchTopics WHERE EmployeeCode = @EmployeeCode";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Thông tin các đề tài nghiên cứu khoa học đã được xóa thành công.");
                                LoadData(); // Refresh the data grid view
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy thông tin các đề tài nghiên cứu khoa học để xóa.");
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
                MessageBox.Show("Vui lòng chọn một nhân viên.");
            }
        }

        private void DeleteScientificWorksButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewResults.CurrentRow != null)
            {
                // Lấy hàng hiện tại được chọn
                DataGridViewRow selectedRow = dataGridViewResults.CurrentRow;

                // Lấy mã nhân viên từ cột "Mã"
                string employeeCode = selectedRow.Cells["Mã"].Value.ToString();

                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa thông tin các công trình khoa học này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Thực hiện lệnh xóa trong cơ sở dữ liệu
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = "DELETE FROM ScientificWorks WHERE EmployeeCode = @EmployeeCode";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Thông tin các công trình khoa học đã được xóa thành công.");
                                LoadData(); // Refresh the data grid view
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy thông tin các công trình khoa học để xóa.");
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
                MessageBox.Show("Vui lòng chọn một nhân viên.");
            }
        }

        /*****************************************************************************/

        private void SetVisibleDeleteDataGridButton(bool visible)
        {
            DeleteBonusButton.Visible = visible;
            DeleteDisciplineButton.Visible = visible;
            DeleteEmployeeRotationButton.Visible = visible;
            DeleteUniversityButton.Visible = visible;
            DeleteAfterUniversityButton.Visible = visible;
            DeleteForeignLanguageButton.Visible = visible;
            DeleteWorkingProcessButton.Visible = visible;
            DeleteScientificResearchTopicsButton.Visible = visible;
            DeleteScientificWorksButton.Visible = visible;
        }
    }
}