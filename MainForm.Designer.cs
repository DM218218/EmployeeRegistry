namespace EmployeeRegistry
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            employeeList = new System.Windows.Forms.ListBox();
            firstNameTextBox = new System.Windows.Forms.TextBox();
            lastNameTextBox = new System.Windows.Forms.TextBox();
            salaryTextBox = new System.Windows.Forms.TextBox();
            hiringDateCalender = new System.Windows.Forms.MonthCalendar();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            selectedDateLabel = new System.Windows.Forms.Label();
            newButton = new System.Windows.Forms.Button();
            removeButton = new System.Windows.Forms.Button();
            saveButton = new System.Windows.Forms.Button();
            label5 = new System.Windows.Forms.Label();
            employeeNumberLabel = new System.Windows.Forms.Label();
            saveEmployeeButton = new System.Windows.Forms.Button();
            cancelButton = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // employeeList
            // 
            employeeList.FormattingEnabled = true;
            employeeList.Location = new System.Drawing.Point(51, 58);
            employeeList.Name = "employeeList";
            employeeList.Size = new System.Drawing.Size(364, 529);
            employeeList.TabIndex = 0;
            employeeList.SelectedIndexChanged += employeeList_SelectedIndexChanged;
            // 
            // firstNameTextBox
            // 
            firstNameTextBox.Location = new System.Drawing.Point(454, 111);
            firstNameTextBox.Name = "firstNameTextBox";
            firstNameTextBox.ReadOnly = true;
            firstNameTextBox.Size = new System.Drawing.Size(372, 23);
            firstNameTextBox.TabIndex = 1;
            // 
            // lastNameTextBox
            // 
            lastNameTextBox.Location = new System.Drawing.Point(454, 175);
            lastNameTextBox.Name = "lastNameTextBox";
            lastNameTextBox.ReadOnly = true;
            lastNameTextBox.Size = new System.Drawing.Size(372, 23);
            lastNameTextBox.TabIndex = 2;
            // 
            // salaryTextBox
            // 
            salaryTextBox.Location = new System.Drawing.Point(454, 239);
            salaryTextBox.Name = "salaryTextBox";
            salaryTextBox.ReadOnly = true;
            salaryTextBox.Size = new System.Drawing.Size(372, 23);
            salaryTextBox.TabIndex = 3;
            // 
            // hiringDateCalender
            // 
            hiringDateCalender.Enabled = false;
            hiringDateCalender.Location = new System.Drawing.Point(454, 309);
            hiringDateCalender.MaxSelectionCount = 1;
            hiringDateCalender.Name = "hiringDateCalender";
            hiringDateCalender.TabIndex = 4;
            hiringDateCalender.DateChanged += hiringDateCalender_DateChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(454, 93);
            label1.Margin = new System.Windows.Forms.Padding(3, 20, 3, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(62, 15);
            label1.TabIndex = 5;
            label1.Text = "First name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(454, 157);
            label2.Margin = new System.Windows.Forms.Padding(3, 20, 3, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(61, 15);
            label2.TabIndex = 6;
            label2.Text = "Last name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(454, 221);
            label3.Margin = new System.Windows.Forms.Padding(3, 20, 3, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(38, 15);
            label3.TabIndex = 7;
            label3.Text = "Salary";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(454, 285);
            label4.Margin = new System.Windows.Forms.Padding(3, 20, 3, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(66, 15);
            label4.TabIndex = 8;
            label4.Text = "Hiring date";
            // 
            // selectedDateLabel
            // 
            selectedDateLabel.AutoSize = true;
            selectedDateLabel.Location = new System.Drawing.Point(454, 480);
            selectedDateLabel.Name = "selectedDateLabel";
            selectedDateLabel.Size = new System.Drawing.Size(74, 15);
            selectedDateLabel.TabIndex = 9;
            selectedDateLabel.Text = "selectedDate";
            // 
            // newButton
            // 
            newButton.Location = new System.Drawing.Point(454, 564);
            newButton.Name = "newButton";
            newButton.Size = new System.Drawing.Size(120, 23);
            newButton.TabIndex = 10;
            newButton.Text = "New Employee";
            newButton.UseVisualStyleBackColor = true;
            newButton.Click += newButton_Click;
            // 
            // removeButton
            // 
            removeButton.Location = new System.Drawing.Point(580, 564);
            removeButton.Name = "removeButton";
            removeButton.Size = new System.Drawing.Size(120, 23);
            removeButton.TabIndex = 11;
            removeButton.Text = "Remove Employee";
            removeButton.UseVisualStyleBackColor = true;
            removeButton.Click += removeButton_Click;
            // 
            // saveButton
            // 
            saveButton.Location = new System.Drawing.Point(706, 564);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(120, 23);
            saveButton.TabIndex = 12;
            saveButton.Text = "Save Database";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(454, 58);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(107, 15);
            label5.TabIndex = 13;
            label5.Text = "Employee number:";
            // 
            // employeeNumberLabel
            // 
            employeeNumberLabel.AutoSize = true;
            employeeNumberLabel.Location = new System.Drawing.Point(567, 58);
            employeeNumberLabel.Name = "employeeNumberLabel";
            employeeNumberLabel.Size = new System.Drawing.Size(103, 15);
            employeeNumberLabel.TabIndex = 14;
            employeeNumberLabel.Text = "employeeNumber";
            // 
            // saveEmployeeButton
            // 
            saveEmployeeButton.Enabled = false;
            saveEmployeeButton.Location = new System.Drawing.Point(706, 309);
            saveEmployeeButton.Name = "saveEmployeeButton";
            saveEmployeeButton.Size = new System.Drawing.Size(120, 23);
            saveEmployeeButton.TabIndex = 15;
            saveEmployeeButton.Text = "Save Employee";
            saveEmployeeButton.UseVisualStyleBackColor = true;
            saveEmployeeButton.Click += saveEmployeeButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Enabled = false;
            cancelButton.Location = new System.Drawing.Point(706, 338);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(120, 23);
            cancelButton.TabIndex = 16;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(878, 669);
            Controls.Add(cancelButton);
            Controls.Add(saveEmployeeButton);
            Controls.Add(employeeNumberLabel);
            Controls.Add(label5);
            Controls.Add(saveButton);
            Controls.Add(removeButton);
            Controls.Add(newButton);
            Controls.Add(selectedDateLabel);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(hiringDateCalender);
            Controls.Add(salaryTextBox);
            Controls.Add(lastNameTextBox);
            Controls.Add(firstNameTextBox);
            Controls.Add(employeeList);
            Name = "MainForm";
            Text = "International Business Synergies - Employee Registry";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ListBox employeeList;
        private System.Windows.Forms.TextBox firstNameTextBox;
        private System.Windows.Forms.TextBox lastNameTextBox;
        private System.Windows.Forms.TextBox salaryTextBox;
        private System.Windows.Forms.MonthCalendar hiringDateCalender;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label selectedDateLabel;
        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label employeeNumberLabel;
        private System.Windows.Forms.Button saveEmployeeButton;
        private System.Windows.Forms.Button cancelButton;
    }
}