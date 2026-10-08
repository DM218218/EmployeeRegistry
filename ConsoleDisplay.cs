using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EmployeeRegistry
{
    internal class ConsoleDisplay
    {
        private EmployeeDatabase employeeDatabase;

        private ConsoleDisplay()
        {
        }

        public ConsoleDisplay(EmployeeDatabase employeeDatabase)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            this.employeeDatabase = employeeDatabase;
        }

        public void Start()
        {
            int choice = -1;

            do
            {
                choice = ShowStartMenu();
                switch (choice)
                {
                    case 1:
                        // Add Employee
                        ShowAddEmployeeMenu();
                        break;
                    case 2:
                        // Remove Employee
                        ShowRemoveEmployeeMeny();
                        break;
                    case 3:
                        // Display Employees
                        ListEmployees();
                        break;
                    case 4:
                        //Save Database
                        ShowSaveDatabase();
                        break;
                    case 0:
                        // Exit
                        break;
                    default:
                        break;
                }
            }
            while (choice != 0);
        }

        private void ShowSaveDatabase()
        {
            try
            { 
                PrintDisplayHeader("Saving Database...");
                employeeDatabase.SaveDatabase();
                Console.WriteLine("Database saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving database: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void ShowRemoveEmployeeMeny()
        {
            PrintDisplayHeader("Remove Employee");

            Console.WriteLine("Please enter the employee number of the employee you wish to remove:");
            Console.WriteLine("Press 0 to cancel and return to the main menu.");
            int employeeNumber;

            while (!int.TryParse(Console.ReadLine(), out employeeNumber))
            {
                Console.WriteLine("Invalid input. Please enter a valid employee number.");
                Console.WriteLine("Please enter the employee number of the employee you wish to remove:");
            }

            if (employeeNumber == 0)
            {
                Console.WriteLine("Operation cancelled.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Are you sure you want to remove this employee? (Y/N)");
            Console.WriteLine();
            Console.WriteLine($"Name: {employeeDatabase.GetEmployee(employeeNumber)?.FirstName} {employeeDatabase.GetEmployee(employeeNumber)?.LastName}");

            string confirmation = Console.ReadKey().KeyChar.ToString().ToLower();

            if (confirmation == "y")
            {
                employeeDatabase.RemoveEmployee(employeeNumber);
                Console.WriteLine();
                Console.WriteLine($"Employee {employeeNumber} removed successfully!");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Operation cancelled.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void ShowAddEmployeeMenu()
        {
            PrintDisplayHeader("Add Employee");
            
            Console.WriteLine("Please enter the following information:");
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Salary: ");
            decimal salary;

            while (!decimal.TryParse(Console.ReadLine(), out salary))
            {
                Console.WriteLine("Invalid input. Please enter a valid salary.");
            }

            Console.WriteLine("Department:");

            foreach (var department in employeeDatabase.GetAllDepartments())
            {
                Console.WriteLine($"{department.DepartmentNumber}. {department.DepartmentName}");
            }

            int departmentChoiceNumeric;

            while (!(int.TryParse(Console.ReadLine(), out departmentChoiceNumeric) && employeeDatabase.GetAllIds(2).Contains(departmentChoiceNumeric)))
            {
                Console.WriteLine("Invalid choice. Please select a valid department.");
            }

            Console.WriteLine("Security clearance level:");

            foreach (var clearance in employeeDatabase.GetAllClearances())
            {
                Console.WriteLine($"{clearance.ClearanceLevel}. {clearance.ClearanceLevelName}");
            }

            int clearanceChoiceNumeric;

            while (!(int.TryParse(Console.ReadLine(), out clearanceChoiceNumeric) && employeeDatabase.GetAllIds(1).Contains(clearanceChoiceNumeric)))
            {
                Console.WriteLine("Invalid choice. Please select a valid security clearance level.");
            }

            Console.Write("Hire Date (DD/MM/YYYY): ");
            DateTime hireDate;

            while (!DateTime.TryParseExact(Console.ReadLine(), employeeDatabase.DateFormat, 
                                           CultureInfo.CurrentCulture, DateTimeStyles.None, out hireDate))
            {
                Console.WriteLine("Invalid input. Please enter a valid date.");
                Console.Write("Hire Date (DD/MM/YYYY): ");
            }

            Console.WriteLine("Are you sure you want to add this employee? (Y/N)");
            Console.WriteLine();
            Console.WriteLine($"First Name: {firstName}");
            Console.WriteLine($"Last Name: {lastName}");
            Console.WriteLine($"Salary: {salary:C}");
            Console.WriteLine($"Department: {employeeDatabase.GetDepartmentByNumber(departmentChoiceNumeric).DepartmentName}");
            Console.WriteLine($"Security Clearance Level: {employeeDatabase.GetClearanceByLevel(clearanceChoiceNumeric).ClearanceLevelName}");
            Console.WriteLine($"Hire Date: {hireDate.ToShortDateString()}");

            string confirmation = Console.ReadKey().KeyChar.ToString().ToLower();

            if (confirmation == "y")
            {
                try
                {
                    employeeDatabase.AddEmployee(firstName, lastName, salary, hireDate, 
                                                 employeeDatabase.GetDepartmentByNumber(departmentChoiceNumeric), 
                                                 employeeDatabase.GetClearanceByLevel(clearanceChoiceNumeric));
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Error adding employee. Try to repair the employee number? [Y/N]");
                    string confirm = Console.ReadKey().KeyChar.ToString().ToLower();

                    if (confirm == "y")
                    {
                        employeeDatabase.RepairNextEmployeeNumber();
                        Console.WriteLine("Employee number repaired. Please try adding the employee again.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }

                    return;
                }

                Console.WriteLine();
                Console.WriteLine($"Employee {firstName} {lastName} added successfully!");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Operation cancelled.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void ListEmployees()
        {
            PrintDisplayHeader("Employee List");

            foreach (var employee in employeeDatabase.GetAllEmployees())
            {
                Console.WriteLine($"Employee Number: {employee.EmployeeNumber}");
                Console.WriteLine($"Name: {employee.FirstName} {employee.LastName}");
                Console.WriteLine($"Salary: {employee.Salary:C}");
                Console.WriteLine($"Hire Date: {employee.HireDate.ToShortDateString()}");
                Console.WriteLine($"Department: {employee.Department.DepartmentName}");
                Console.WriteLine($"Security Clearance Level: {employee.Clearance.ClearanceLevelName}");
                Console.WriteLine();
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void PrintDisplayHeader(string header)
        {
            Console.Clear();
            Console.WriteLine("************************************************************");
            Console.WriteLine("*   International Business Synergies - Employee Registry   *");
            Console.WriteLine("************************************************************");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(header);
            Console.WriteLine();
        }

        public int ShowStartMenu()
        {
            PrintDisplayHeader("Welcome to the Employee Registry!");

            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Add Employee"); 
            Console.WriteLine("2. Remove Employee");
            Console.WriteLine("3. Display Employees");
            Console.WriteLine("4. Save Database");
            Console.WriteLine("0. Exit");

            string choice = Console.ReadLine();

            if(int.TryParse(choice, out int result))
            {
                if(result < 0 || result > 4)
                {
                    Console.WriteLine("Invalid input. Please enter a valid choice.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return -1;
                }

                return result;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid choice.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return -1;
            }
        }
    }
}
