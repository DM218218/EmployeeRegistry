using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeRegistry
{
    internal class Employee
    {
        private int employeeNumber = -1;
        private string firstName;
        private string lastName;
        private decimal salary;
        private DateTime hireDate;

        public int EmployeeNumber { get => employeeNumber; }
        public string FirstName { get => firstName; }
        public string LastName { get => lastName; }
        public decimal Salary { get => salary; }
        public DateTime HireDate { get => hireDate; }

        public Employee(string firstName, string lastName, decimal salary, DateTime hireDate)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.salary = salary;
            this.hireDate = hireDate;
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
    }
}
