using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace EmployeeRegistry
{
    internal class EmployeeDatabase
    {
        private Dictionary<int, Employee> employeeDatabase;
        private int nextAvailableEmployeeNumber;
        private string databaseFilePath = @$"{System.IO.Directory.GetCurrentDirectory()}\data\";
        private string databaseFileName = "employees.json";
        string nextEmployeeNumberFileName = "nen.dat";

        public string[] DateFormat { get; set; } = { "MM/dd/yyyy", "dd/MM/yyyy", "M/d/yyyy", "d/M/yyyy", "M/dd/yyyy",
                                                     "yyyy-MM-dd", "yyyy-dd-MM", "M-d-yyyy", "d-M-yyyy", "M-dd-yyyy",
                                                     "yyyy MM dd", "yyyy dd MM", "M d yyyy", "d M yyyy", "M dd yyyy" };

        public EmployeeDatabase()
        {
            //load old data
            employeeDatabase = LoadOrCreateDatabase();
        }

        private Dictionary<int, Employee> LoadOrCreateDatabase()
        {
            Dictionary<int, Employee> database;

            if (System.IO.File.Exists(databaseFilePath + databaseFileName))
            {
                string jsonString = System.IO.File.ReadAllText(databaseFilePath + databaseFileName);
                database = JsonSerializer.Deserialize<Dictionary<int, Employee>>(jsonString) ?? new Dictionary<int, Employee>();
            }
            else
            {
                database = new Dictionary<int, Employee>();
            }

            //load next available employee number from file
            if (System.IO.File.Exists(databaseFilePath + nextEmployeeNumberFileName))
            {
                string nextEmployeeNumberString = System.IO.File.ReadAllText(databaseFilePath + nextEmployeeNumberFileName);
                if (int.TryParse(nextEmployeeNumberString, out int nextEmployeeNumber))
                {
                    nextAvailableEmployeeNumber = nextEmployeeNumber;
                }
            }
            else
            {
                nextAvailableEmployeeNumber = 1;
            }

            return database;
        }

        public void AddEmployee(Employee employee)
        {
            employee.SetEmployeeNumber(nextAvailableEmployeeNumber);
            employeeDatabase.Add(nextAvailableEmployeeNumber, employee);
            nextAvailableEmployeeNumber++;
        }

        public void AddEmployee(string firstName, string lastName, decimal salary, DateTime hireDate)
        {
            Employee employee = new Employee(firstName, lastName, salary, hireDate);
            AddEmployee(employee);
        }

        public void RemoveEmployee(int employeeNumber)
        {
            //making sure key and employee number match, if not, search for the employee number in the values and remove it.
            if (employeeDatabase[employeeNumber].EmployeeNumber == employeeNumber)
            {
                employeeDatabase.Remove(employeeNumber);
            }
            else
            {
                foreach (var employee in employeeDatabase)
                {
                    if (employee.Value.EmployeeNumber == employeeNumber)
                    {
                        employeeDatabase.Remove(employee.Key);
                        break;
                    }
                }
            }
        }

        public void RepairNextEmployeeNumber()
        {
            //Recover next available employee number by finding the highest employee number in the database and adding 1 to it.
            if (employeeDatabase.Count > 0)
            {
                nextAvailableEmployeeNumber = employeeDatabase.Keys.Max() + 1;
            }
            else
            {
                nextAvailableEmployeeNumber = 1;
            }
        }

        public bool UpdateEmployee(int employeeNumber, string firstName, string lastName)
        {
            Employee? employee = GetEmployee(employeeNumber);
            if (employee != null)
            {
                employee.changeName(firstName, lastName);
                return true;
            }
            return false;
        }

        public bool UpdateEmployee(int employeeNumber, decimal salary)
        {
            Employee? employee = GetEmployee(employeeNumber);
            if (employee != null)
            {
                employee.changeSalary(salary);
                return true;
            }
            return false;
        }

        public Employee? GetEmployee(int employeeNumber)
        {
            return employeeDatabase.GetValueOrDefault(employeeNumber);
        }

        public List<Employee> GetAllEmployees()
        {
            return employeeDatabase.Values.ToList<Employee>();
        }

        internal void SaveDatabase()
        {
            string jsonString = JsonSerializer.Serialize(employeeDatabase);

            if(!System.IO.Directory.Exists(databaseFilePath))
            {
                var directory = System.IO.Directory.CreateDirectory(databaseFilePath);
            }

            if(!System.IO.File.Exists(databaseFilePath + databaseFileName))
            {
                var file = System.IO.File.Create(databaseFilePath + databaseFileName);
                file.Close();
            }

            System.IO.File.WriteAllText(databaseFilePath + databaseFileName, jsonString);

            //save next available employee number to a separate file
            if (!System.IO.File.Exists(databaseFilePath + nextEmployeeNumberFileName))
            {
                var file = System.IO.File.Create(databaseFilePath + nextEmployeeNumberFileName);
                file.Close();
            }

            System.IO.File.WriteAllText(databaseFilePath + nextEmployeeNumberFileName, nextAvailableEmployeeNumber.ToString());
        }
    }
}
