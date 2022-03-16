using System.Diagnostics;

namespace SeaShell.Model
{
    public class Instruction
    {
        public string Command { get; }
        public string[]? Args { get; }
        public Process Proc { get; }

        public int? ExitCode { get; set; }
        public string? Stdout { get; set; }
        public string? Stderr { get; set; }

        public Instruction(string command, string[] args)
        {
            Command = command;
            Args = args;
            Proc = new Process();
        }

        public void Execute()
        {
            Proc.StartInfo.FileName = Command;
            Proc.StartInfo.Arguments = (Args is null) ? null : string.Join(' ', Args);

            Proc.Start();

            if (Proc.StartInfo.RedirectStandardError)
                Stderr = Proc.StandardError.ReadToEnd();

            if (Proc.StartInfo.RedirectStandardOutput)
                Stdout = Proc.StandardOutput.ReadToEnd();

            Proc.WaitForExit();
            ExitCode = Proc.ExitCode;
        }
    }
}