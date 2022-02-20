using System.Diagnostics;

namespace SeaShell {
    class Program
    {
        static void Main(string[] args)
        {
            var shell = new SeaShell();

            try
            {
                shell.Run();
            }
            catch {}
        }
    }

    public class SeaShell
    {
        private static void cow()
        { Console.WriteLine("🐮 There is no cow level 🐮"); }

        private Dictionary<string, Action> Builtin = new Dictionary<string, Action>()
        {
            { "cow", () => cow() },
        };

        private (string, string) Prompt()
        {
        //Console.WriteLine("[{0}@{1}]", Environment.UserName, Environment.MachineName);
            Console.Write(">> ");
            var input = Console.ReadLine().Trim().Split(' ');
            var command = input[0];

            for (int i = 1; i < input.Length-1; i++)
            {
                // Clean command and arguments
                input[i].Trim();
            }
            var args = string.Join(" ", input[1..]);

            return (command, args);
        }
        public void Run()
        {
            string command = "", args = "";

            do
            {
                (command, args) = Prompt();

                if (Builtin.ContainsKey(command))
		{
                    Builtin[command]();
                } else {
                    try
                    {
                        var exitStatus = Exec(command, args);
                    } catch {
                        string message = "Command not found: ";
                        string filler = "";

                        if ((message+command+" "+args).Length > Console.BufferWidth )
                        {
                            filler = "\n";
                        } else {
                            filler = " ";
                        }

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(message);
                        Console.ResetColor();
                        Console.WriteLine("{0}{1} {2}", filler, command, args);
                    }
                }

            } while (command != "exit" && command != "quit");
        }

        public int Exec(string command, string args)
        {
            var proc = new Process();

            proc.StartInfo.FileName = command;
            proc.StartInfo.Arguments = args;

            // Spawn the process
            proc.Start();
            proc.WaitForExit();
            return 0;
        }
    }
}
