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
        private List<Clearance> clearances;
        private List<Department> departments;
        private int nextAvailableEmployeeNumber;
        private string dataFilePath = @$"{System.IO.Directory.GetCurrentDirectory()}\data\";
        private string databaseFileName = "employees.json";
        private string clearanceLevelsFileName = "clearancelevels.json";
        private string departmentFileName = "departments.json";
        private string nextEmployeeNumberFileName = "nen.dat";

        private const string dbVersion = "1.1.1";

        public string[] DateFormat { get; set; } = { "MM/dd/yyyy", "dd/MM/yyyy", "M/d/yyyy", "d/M/yyyy", "M/dd/yyyy",
                                                     "yyyy-MM-dd", "yyyy-dd-MM", "M-d-yyyy", "d-M-yyyy", "M-dd-yyyy",
                                                     "yyyy MM dd", "yyyy dd MM", "M d yyyy", "d M yyyy", "M dd yyyy" };

        public EmployeeDatabase()
        {
            //load employee number and check db validity first
            if (!LoadEmployeeNumberAndValidateDbVersion())
            {
                //initialize data
                clearances = LoadClearanceLevels();
                departments = LoadDepartments();
                //load old data
                employeeDatabase = LoadOrCreateDatabase();
            }
            else
            {
                clearances = new List<Clearance>();
                clearances.Add(new Clearance(1, "White", 255, 255, 255));
                clearances.Add(new Clearance(2, "Green", 0, 255, 0));
                clearances.Add(new Clearance(3, "Yellow", 255, 255, 0));
                clearances.Add(new Clearance(4, "Red", 255, 0, 0));
                clearances.Add(new Clearance(5, "Black", 128, 128, 128));

                departments = new List<Department>();
                departments.Add(new Department(1, "Security"));
                departments.Add(new Department(2, "Production"));
                departments.Add(new Department(3, "Marketing"));
                departments.Add(new Department(4, "Sales"));
                departments.Add(new Department(5, "Research"));
                departments.Add(new Department(6, "Management"));

                SaveDepartments();
                SaveClearanceLevels();

                employeeDatabase = new Dictionary<int, Employee>();
            }
        }

        private bool LoadEmployeeNumberAndValidateDbVersion()
        {
            bool invalidDatabase = true;

            //load next available employee number and database version from file
            if (System.IO.File.Exists(dataFilePath + nextEmployeeNumberFileName))
            {
                string dataFileContents = System.IO.File.ReadAllText(dataFilePath + nextEmployeeNumberFileName);
                string[] dataValues = dataFileContents.Split('X');

                //try to parse the next available employee number first
                if (int.TryParse(dataValues[0], out int nextEmployeeNumber))
                {
                    nextAvailableEmployeeNumber = nextEmployeeNumber;
                }
                else
                {
                    nextAvailableEmployeeNumber = 1;
                }

                //bool a = dataValues.Length > 1;
                //bool b = !(string.Compare(dataValues[1], dbVersion) == 0);
                //bool c = (a && b);

                //check for a database version field
                if (dataValues.Length > 1 && !(string.Compare(dataValues[1], dbVersion) == 0))
                {
                    //database version mismatch, No more loading, database not usable
                    nextAvailableEmployeeNumber = 1;
                }
                else
                {
                    //database version matches, continue loading. If no version was found (lenght < 2), keep db invalid
                    invalidDatabase = false;
                }
            }
            else
            {
                nextAvailableEmployeeNumber = 1;
            }

            return invalidDatabase;
        }

        private List<Clearance> LoadClearanceLevels()
        {
            List<Clearance> clearanceList;

            if(System.IO.File.Exists(dataFilePath + clearanceLevelsFileName))
            {
                string jsonString = System.IO.File.ReadAllText(dataFilePath + clearanceLevelsFileName);
                clearanceList = JsonSerializer.Deserialize<List<Clearance>>(jsonString) ?? new List<Clearance>();
            }
            else
            {
                clearanceList = new List<Clearance>();
                clearanceList.Add(new Clearance(1, "White", 255, 255, 255));
                clearanceList.Add(new Clearance(2, "Green", 0, 255, 0));
                clearanceList.Add(new Clearance(3, "Yellow", 255, 255, 0));
                clearanceList.Add(new Clearance(4, "Red", 255, 0, 0));
                clearanceList.Add(new Clearance(5, "Black", 128, 128, 128));

                SaveClearanceLevels(clearanceList);
            }

            return clearanceList;
        }

        private List<Department> LoadDepartments()
        {
            List<Department> departmentList;

            if (System.IO.File.Exists(dataFilePath + departmentFileName))
            {
                string jsonString = System.IO.File.ReadAllText(dataFilePath + departmentFileName);
                departmentList = JsonSerializer.Deserialize<List<Department>>(jsonString) ?? new List<Department>();
            }
            else
            {
                departmentList = new List<Department>();
                departmentList.Add(new Department(1, "Security"));
                departmentList.Add(new Department(2, "Production"));
                departmentList.Add(new Department(3, "Marketing"));
                departmentList.Add(new Department(4, "Sales"));
                departmentList.Add(new Department(5, "Research"));
                departmentList.Add(new Department(6, "Management"));

                SaveDepartments(departmentList);
            }

            return departmentList;
        }

        private void SaveClearanceLevels(List<Clearance> clearanceList = null)
        {
            //save provided clearances if provided, otherwise save the current clearances
            //used for saving default clearances when the file does not exist
            string jsonString = JsonSerializer.Serialize(clearanceList ?? clearances);

            if(!System.IO.Directory.Exists(dataFilePath))
            {
                System.IO.Directory.CreateDirectory(dataFilePath);
            }

            if(!System.IO.File.Exists(dataFilePath + clearanceLevelsFileName))
            {
                var file = System.IO.File.Create(dataFilePath + clearanceLevelsFileName);
                file.Close();
            }

            System.IO.File.WriteAllText(dataFilePath + clearanceLevelsFileName, jsonString);
        }

        private void SaveDepartments(List<Department> departmentList = null)
        {
            //save provided departments if provided, otherwise save the current departments
            //used for saving default departments when the file doesn't exist yet
            string jsonString = JsonSerializer.Serialize(departmentList ?? departments);

            if(!System.IO.Directory.Exists(dataFilePath))
            {
                System.IO.Directory.CreateDirectory(dataFilePath);
            }

            if(!System.IO.File.Exists(dataFilePath + departmentFileName))
            {
                var file = System.IO.File.Create(dataFilePath + departmentFileName);
                file.Close();
            }

            System.IO.File.WriteAllText(dataFilePath + departmentFileName, jsonString);
        }

        private Dictionary<int, Employee> LoadOrCreateDatabase()
        {
            Dictionary<int, Employee> database;

            if (System.IO.File.Exists(dataFilePath + databaseFileName))
            {
                string jsonString = System.IO.File.ReadAllText(dataFilePath + databaseFileName);
                database = JsonSerializer.Deserialize<Dictionary<int, Employee>>(jsonString) ?? new Dictionary<int, Employee>();
            }
            else
            {
                database = new Dictionary<int, Employee>();
            }

            return database;
        }

        public void AddEmployee(Employee employee)
        {
            employee.SetEmployeeNumber(nextAvailableEmployeeNumber);
            employeeDatabase.Add(nextAvailableEmployeeNumber, employee);
            nextAvailableEmployeeNumber++;
        }

        public void AddEmployee(string firstName, string lastName, decimal salary, DateTime hireDate, Department department, Clearance clearance)
        {
            Employee employee = new Employee(firstName, lastName, salary, hireDate, department, clearance);
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

            if(!System.IO.Directory.Exists(dataFilePath))
            {
                System.IO.Directory.CreateDirectory(dataFilePath);
            }

            if(!System.IO.File.Exists(dataFilePath + databaseFileName))
            {
                var file = System.IO.File.Create(dataFilePath + databaseFileName);
                file.Close();
            }

            System.IO.File.WriteAllText(dataFilePath + databaseFileName, jsonString);

            //save next available employee number and database version to a separate file
            if (!System.IO.File.Exists(dataFilePath + nextEmployeeNumberFileName))
            {
                var file = System.IO.File.Create(dataFilePath + nextEmployeeNumberFileName);
                file.Close();
            }

            System.IO.File.WriteAllText(dataFilePath + nextEmployeeNumberFileName, $"{nextAvailableEmployeeNumber}X{dbVersion}");
        }

        internal List<Clearance> GetAllClearances()
        {
            return clearances;
        }

        internal List<Department> GetAllDepartments()
        {
            return departments;
        }

        internal int[] GetAllIds(int type)
        {
            if(type == 1)
            {
                return clearances.Select(clearances => clearances.ClearanceLevel).ToArray();
            }
            else if (type == 2)
            {
                return departments.Select(departments => departments.DepartmentNumber).ToArray();
            }

            return new int[0];
        }

        public Department GetDepartmentByNumber(int departmentNumber)
        {
            return departments.FirstOrDefault(d => d.DepartmentNumber == departmentNumber);
        }

        public Clearance GetClearanceByLevel(int clearanceLevel)
        {
            return clearances.FirstOrDefault(c => c.ClearanceLevel == clearanceLevel);
        }
    }
}
