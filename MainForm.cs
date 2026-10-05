using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace EmployeeRegistry
{
    internal partial class MainForm : Form
    {
        private EmployeeDatabase employeeDatabase;
        private bool addingMode = false;

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(EmployeeDatabase employeeDatabase)
        {
            InitializeComponent();
            this.employeeDatabase = employeeDatabase;

            Initialize();
        }

        private void Initialize()
        {
            this.employeeList.DataSource = employeeDatabase.GetAllEmployees();
            this.securityClearanceComboBox.DataSource = employeeDatabase.GetAllClearances();
            this.departmentComboBox.DataSource = employeeDatabase.GetAllDepartments();
            employeeList_SelectedIndexChanged(this, EventArgs.Empty);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                employeeDatabase.SaveDatabase();
                MessageBox.Show("Database saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void employeeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (addingMode)
            {
                return; // Ignore selection changes while adding a new employee
            }

            if (this.employeeList.SelectedItem is Employee selectedEmployee)
            {
                this.employeeNumberLabel.Text = $"{selectedEmployee.EmployeeNumber}";
                this.firstNameTextBox.Text = selectedEmployee.FirstName;
                this.lastNameTextBox.Text = selectedEmployee.LastName;
                this.salaryTextBox.Text = selectedEmployee.Salary.ToString("F2");
                this.hiringDateCalender.SetDate(selectedEmployee.HireDate);

                foreach (var item in this.departmentComboBox.Items)
                {
                    if (item is Department department && department.DepartmentNumber == selectedEmployee.Department.DepartmentNumber)
                    {
                        this.departmentComboBox.SelectedItem = item;
                        break;
                    }
                }

                foreach (var item in this.securityClearanceComboBox.Items)
                {
                    if (item is Clearance clearance && clearance.ClearanceLevel == selectedEmployee.Clearance.ClearanceLevel)
                    {
                        this.securityClearanceComboBox.SelectedItem = item;
                        this.securityClearanceComboBox.BackColor = clearance.GetClearanceColor();
                        break;
                    }
                }
            }
            else
            {
                this.employeeNumberLabel.Text = string.Empty;
                this.firstNameTextBox.Text = string.Empty;
                this.lastNameTextBox.Text = string.Empty;
                this.salaryTextBox.Text = string.Empty;
                this.hiringDateCalender.SetDate(DateTime.Today);
                this.departmentComboBox.SelectedItem = null;
                this.securityClearanceComboBox.SelectedItem = null;
                this.securityClearanceComboBox.BackColor = SystemColors.Window;
            }
        }

        private void hiringDateCalender_DateChanged(object sender, DateRangeEventArgs e)
        {
            this.selectedDateLabel.Text = $"Selected Date: {e.Start.ToShortDateString()}";
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (this.employeeList.SelectedItem is Employee selectedEmployee)
            {
                DialogResult result = MessageBox.Show($"Are you sure you want to remove employee {selectedEmployee.FirstName} {selectedEmployee.LastName}?", "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    employeeDatabase.RemoveEmployee(selectedEmployee.EmployeeNumber);
                    this.employeeList.DataSource = null;
                    this.employeeList.DataSource = employeeDatabase.GetAllEmployees();
                }
            }
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            addingMode = true;
            this.firstNameTextBox.Text = string.Empty;
            this.lastNameTextBox.Text = string.Empty;
            this.salaryTextBox.Text = string.Empty;
            this.hiringDateCalender.SetDate(DateTime.Today);
            this.employeeNumberLabel.Text = "NEW";

            this.firstNameTextBox.ReadOnly = false;
            this.lastNameTextBox.ReadOnly = false;
            this.salaryTextBox.ReadOnly = false;
            this.hiringDateCalender.Enabled = true;
            this.departmentComboBox.Enabled = true;
            this.securityClearanceComboBox.Enabled = true;

            this.saveEmployeeButton.Enabled = true;
            this.cancelButton.Enabled = true;
            this.newButton.Enabled = false;
            this.removeButton.Enabled = false;
            this.saveButton.Enabled = false;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            addingMode = false;
            this.firstNameTextBox.Text = string.Empty;
            this.lastNameTextBox.Text = string.Empty;
            this.salaryTextBox.Text = string.Empty;
            this.hiringDateCalender.SetDate(DateTime.Today);
            this.employeeNumberLabel.Text = String.Empty;

            this.firstNameTextBox.ReadOnly = true;
            this.lastNameTextBox.ReadOnly = true;
            this.salaryTextBox.ReadOnly = true;
            this.hiringDateCalender.Enabled = false;
            this.departmentComboBox.Enabled = false;
            this.securityClearanceComboBox.Enabled = false;

            this.saveEmployeeButton.Enabled = false;
            this.cancelButton.Enabled = false;
            this.newButton.Enabled = true;
            this.removeButton.Enabled = true;
            this.saveButton.Enabled = true;
        }

        private void saveEmployeeButton_Click(object sender, EventArgs e)
        {
            if (this.firstNameTextBox.Text == string.Empty || this.lastNameTextBox.Text == string.Empty || this.salaryTextBox.Text == string.Empty)
            {
                MessageBox.Show("Please fill in all fields before saving.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                try
                {
                    decimal salary = decimal.Parse(this.salaryTextBox.Text);
                    DateTime hireDate = this.hiringDateCalender.SelectionStart;

                    employeeDatabase.AddEmployee(this.firstNameTextBox.Text, this.lastNameTextBox.Text, salary, hireDate, 
                                                 this.departmentComboBox.SelectedItem as Department, this.securityClearanceComboBox.SelectedItem as Clearance);

                    this.employeeList.DataSource = null;
                    this.employeeList.DataSource = employeeDatabase.GetAllEmployees();
                    MessageBox.Show("Employee added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cancelButton_Click(sender, e); // Reset the form after adding
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid salary format. Please enter a valid decimal number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (ArgumentException ex)
                {
                    DialogResult result = MessageBox.Show($"Error adding employee. Try to repair the employee number?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                    if (result == DialogResult.Yes)
                    {
                        employeeDatabase.RepairNextEmployeeNumber();
                        MessageBox.Show("Employee number repaired. Please try adding the employee again.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void securityClearanceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.securityClearanceComboBox.SelectedItem != null)
            {
                this.securityClearanceComboBox.BackColor = (this.securityClearanceComboBox.SelectedItem as Clearance).GetClearanceColor();
            }
        }
    }
}
