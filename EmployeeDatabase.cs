using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeRegistry
{
    internal class EmployeeDatabase
    {
        private Dictionary<int, Employee> employeeDatabase;
        private int nextAvailableEmployeeNumber;

        public EmployeeDatabase()
        {
            //load old data
            employeeDatabase = new Dictionary<int, Employee>();
            //set next employee number
            nextAvailableEmployeeNumber = 1;
        }

        public void AddEmployee(Employee employee)
        {
        }

        public void RemoveEmployee(int employeeNumber)
        {
        }


    }
}
