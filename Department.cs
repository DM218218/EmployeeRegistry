using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeRegistry
{
    internal class Department
    {
        public string DepartmentName { get; private set; }
        public int DepartmentNumber { get; private set; }

        public Department(int departmentNumber, string departmentName)
        {
            this.DepartmentNumber = departmentNumber;
            this.DepartmentName = departmentName;
        }

        public override string ToString()
        {
            return DepartmentName;
        }
    }
}
