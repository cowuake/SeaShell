using SeaShell.Model;
using SeaShell.Utilities;

namespace SeaShell
{
    class Program
    {
        static void Main(string[] args)
        {
            var shell = new SeaShell();

            try {
                shell.Run();
            }
            catch (Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }
    }

    public class SeaShell
    {
        private static void beep()
        { Console.Beep(); }

        private static void cow()
        { Console.WriteLine("🐮 There is no cow level 🐮"); }

        private static void exit()
        { Environment.Exit(0); }

        private static void hostname()
        { Console.WriteLine(Environment.MachineName); }

        private static void username()
        { Console.WriteLine(Environment.UserName); }

        public static Dictionary<string, Action<string>> BuiltinCommands =
            new Dictionary<string, Action<string>>()
        {
            { "beep"        , (args) => beep()        },
            { "cow"         , (args) => cow()         },
            { "exit"        , (args) => exit()        },
            { "hostname"    , (args) => hostname()    },
            { "quit"        , (args) => exit()        },
            { "username"    , (args) => username()    },
        };

        private void WriteColored(string text, ConsoleColor color, bool newLine = false)
        {
            Console.ForegroundColor = color;
            if (newLine) { Console.WriteLine(text); } else { Console.Write(text); }
            Console.ResetColor();
        }

        private void WriteLineColored(string text, ConsoleColor color)
        {
            WriteColored(text, color, true);
        }

        private (string?, string?) Prompt()
        {
        //Console.WriteLine("[{0}@{1}]", Environment.UserName, Environment.MachineName);
            WriteColored(">> ", ConsoleColor.DarkCyan);
            string? rawInput = Console.ReadLine();

            if (rawInput != null)
            {
                string[] input = rawInput.Trim().Split(' ');
                string command = input[0];

                for (int i = 1; i < input.Length-1; i++)
                {
                    // Clean command and arguments
                    input[i].Trim();
                }
                string args = string.Join(" ", input[1..]);

                return (command, args);
            }

            return (null, null);
        }

        public void Run()
        {
            string? command, args;

            while (true)
            {
                (command, args) = Prompt();

                // Don't waste time within the cycle if input was void
                if (string.IsNullOrEmpty(command) || string.IsNullOrWhiteSpace(command))
                {
                    continue;
                }

                if (command.IsAllUpper())
                {
                    WriteLineColored("Please, don't shout at me! D:", ConsoleColor.DarkMagenta);
                }

                if (BuiltinCommands.ContainsKey(command))
                {
                    BuiltinCommands[command](args);
                    continue;
                }

                try
                {
                    var instruction = new Instruction(command, args);
                    instruction.Execute();
                }
                catch (System.ComponentModel.Win32Exception e)
                {
                    //Console.WriteLine(e.ToString());

                    //var message = new StringBuilder();
                    var message = "Command not found: ";
                    var filler = "";
                    var body = String.Format($"{command} {args}");

                    filler += ((message + body).IsWithinBufferWidth()) ? "" : "\n";

                    WriteColored(message, ConsoleColor.Red);
                    Console.WriteLine($"{filler}{command} {args}");
                }
            }
        }
    }
}