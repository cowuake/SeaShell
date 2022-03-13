using System.Diagnostics;

namespace SeaShell.Model
{
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
}