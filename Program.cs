using System.Diagnostics;
using System.Text;

namespace SeaShell
{
    using SeaShellUtilities;

    class Program
    {
        static void Main(string[] args)
        {
            var shell = new SeaShell();
            try { shell.Run(); } catch {}
        }
    }

    public class Instruction
    {
        public string Command;
        public string Args;
        public Process Proc;

        public Int32 ?ExitCode;
        public string ?Stdout;
        public string ?Stderr;

        // Constructor
        public Instruction(string command, string args)
        {
            Command = command;
            Args = args;
            Proc = new Process();
        }

        public void Execute()
        {
            this.Proc.StartInfo.FileName = this.Command;
            this.Proc.StartInfo.Arguments = this.Args;

            // Spawn the process
            this.Proc.Start();

            if (this.Proc.StartInfo.RedirectStandardError == true)
            { this.Stderr = this.Proc.StandardError.ReadToEnd(); }

            if (this.Proc.StartInfo.RedirectStandardOutput == true)
            { this.Stdout = this.Proc.StandardOutput.ReadToEnd(); }

            this.Proc.WaitForExit();

            //Console.WriteLine(this.Stdout);
            this.ExitCode = this.Proc.ExitCode;
        }
    }

    public class Pipeline
    {
        public Instruction[] Instructions;
        public Int32 Length;

        // Constructor
        public Pipeline(Instruction[] instructions)
        {
            this.Instructions = instructions;
            this.Length = instructions.Length;
        }

        public void Execute()
        {
            if (this.Length > 1) { this.Instructions[0].Proc.StartInfo.RedirectStandardOutput = true; }
            this.Instructions[0].Execute();

            for (int i = 1; i < this.Length; i++)
            {
                if (this.Instructions[i-1].ExitCode == 0)
                {
                    if (i < this.Length - 1)
                    {
                        this.Instructions[i].Proc.StartInfo.RedirectStandardOutput = true;
                    }
                    //this.Instructions[i].Args += (" " + this.Instructions[i-1].Stdout);

                    this.Instructions[i].Execute();
                }
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

                if (BuiltinCommands.ContainsKey(command))
                {
                    BuiltinCommands[command](args);
                    continue;
                }

                try
                {
                    var instruction = new Instruction(command, args);
                    instruction.Execute();
                } catch (System.ComponentModel.Win32Exception e) {
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