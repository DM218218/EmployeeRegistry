using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EmployeeRegistry
{
    internal class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AttachConsole(int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        const int ATTACH_PARENT_PROCESS = -1;

        static void Main(string[] args)
        {
            EmployeeDatabase employeeDatabase = new EmployeeDatabase();

            // handle arguments and do something funny.
            if(args.Length > 0 && args[0].ToLower() == "-gui")
            {
                // Start the GUI application                
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(employeeDatabase));
            }
            else if(args.Length > 0 && args[0].ToLower() == "-console")
            {
                if (!AttachConsole(ATTACH_PARENT_PROCESS))
                {
                    // No parent console (e.g. started from VS or Explorer).
                    // Create a new one so output has somewhere to go.
                    AllocConsole();
                }

                // Rebind the streams in case the handles were cached before attaching
                Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
                Console.SetError(new StreamWriter(Console.OpenStandardError()) { AutoFlush = true });
                Console.SetIn(new StreamReader(Console.OpenStandardInput()));

                // Start the console application
                ConsoleDisplay consoleDisplay = new ConsoleDisplay(employeeDatabase);
                consoleDisplay.Start();
            }
            else
            {
                if (!AttachConsole(ATTACH_PARENT_PROCESS))
                {
                    // No parent console (e.g. started from VS or Explorer).
                    // Create a new one so output has somewhere to go.
                    AllocConsole();
                }

                // Rebind the streams in case the handles were cached before attaching
                Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
                Console.SetError(new StreamWriter(Console.OpenStandardError()) { AutoFlush = true });
                Console.SetIn(new StreamReader(Console.OpenStandardInput()));

                // Default to console if no arguments are provided
                ConsoleDisplay consoleDisplay = new ConsoleDisplay(employeeDatabase);
                consoleDisplay.Start();
            }
        }
    }
}
