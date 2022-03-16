using System.Linq;
using SeaShell.Commands;
using SeaShell.Utilities;

namespace SeaShell.Model
{
    public class Shell
    {
        public static Dictionary<string, Action<string[]>> BuiltinCommands =
            new Dictionary<string, Action<string[]>>()
        {
            { "beep"        , (args) => BaseCommands.Beep()        },
            { "cow"         , (args) => BaseCommands.Cow()         },
            { "exit"        , (args) => BaseCommands.Exit()        },
            { "hostname"    , (args) => BaseCommands.Hostname()    },
            { "quit"        , (args) => BaseCommands.Exit()        },
            { "username"    , (args) => BaseCommands.Username()    },
        };

        private void WriteColored(string text, ConsoleColor color, bool newLine = false)
        {
            Console.ForegroundColor = color;

            Action<string> Write = newLine ? Console.WriteLine : Console.Write;
            Write(text);

            Console.ResetColor();
        }

        private void WriteLineColored(string text, ConsoleColor color)
        {
            WriteColored(text, color, true);
        }

        private bool ReadFromPrompt(out string command, out string[] args)
        {
            //Console.WriteLine("[{0}@{1}]", Environment.UserName, Environment.MachineName);
            WriteColored(">> ", ConsoleColor.DarkCyan);
            string? rawInput = Console.ReadLine();

            command = String.Empty;
            args = new string[0];

            if (rawInput is null)
                return false;

            string[] input = rawInput.Trim().Split(' ');
            input.Select(s => s.Trim()).ToArray();

            command = input[0];

            if (input.Length > 1)
                args = input[1..];

            return true;
        }

        public void Run()
        {
            string? command;
            string[]? args;

            while (true)
            {
                if (!ReadFromPrompt(out command, out args))
                    continue;

                if (command.IsAllUpper())
                    WriteLineColored("Please, don't shout at me! D:", ConsoleColor.DarkMagenta);

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
                catch
                {
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