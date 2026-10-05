using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EmployeeRegistry
{
    internal class Employee
    {
        private int employeeNumber = -1;
        private string firstName;
        private string lastName;
        private decimal salary;
        private DateTime hireDate;
        private Department department;
        private Clearance clearance;

        [JsonInclude]
        public int EmployeeNumber { get => employeeNumber; private set => employeeNumber = value; }
        public string FirstName { get => firstName; }
        public string LastName { get => lastName; }
        public decimal Salary { get => salary; }
        public DateTime HireDate { get => hireDate; }
        public Department Department { get => department; }
        public Clearance Clearance { get => clearance; }

        public Employee(string firstName, string lastName, decimal salary, DateTime hireDate, Department department, Clearance clearance)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.salary = salary;
            this.hireDate = hireDate;
            this.department = department;
            this.clearance = clearance;
        }

        public void SetEmployeeNumber(int employeeNumber)
        {
            //Only set if it is not already set. Can't change employee number once it is set.
            if (this.employeeNumber == -1)
            {
                this.employeeNumber = employeeNumber;
            }
        }

        public void changeSalary(decimal newSalary)
        {
            salary = newSalary;
        }

        public void changeName(string newFirstName, string newLastName)
        {
            firstName = newFirstName;
            lastName = newLastName;
        }

        public void changeDepartment(Department newDepartment)
        {
            department = newDepartment;
        }

        public void changeClearance(Clearance newClearance)
        {
            clearance = newClearance;
        }

        public override string ToString()
        {
            return $"Employee {employeeNumber}: {firstName} {lastName} - {department}";
        }
    }
}
