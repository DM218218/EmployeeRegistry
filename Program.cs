using System;

namespace EmployeeRegistry
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EmployeeDatabase employeeDatabase = new EmployeeDatabase();

            ConsoleDisplay consoleDisplay = new ConsoleDisplay(employeeDatabase);
            consoleDisplay.Start();
        }
    }
}
