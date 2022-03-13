namespace SeaShell.Model
{
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
}