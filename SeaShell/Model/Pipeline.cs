namespace SeaShell.Model
{
    public class Pipeline
    {
        public Instruction[] Instructions { get; }
        public int Length { get; }

        public Pipeline(Instruction[] instructions)
        {
            Instructions = instructions;
            Length = instructions.Length;
        }

        public void Execute()
        {
            if (Length > 1)
                Instructions[0].Proc.StartInfo.RedirectStandardOutput = true;

            Instructions[0].Execute();

            for (int i = 1; i < this.Length; i++)
            {
                if (Instructions[i-1].ExitCode == 0)
                {
                    if (i < Length - 1)
                    {
                        Instructions[i].Proc.StartInfo.RedirectStandardOutput = true;
                    }
                    //this.Instructions[i].Args += (" " + this.Instructions[i-1].Stdout);

                    Instructions[i].Execute();
                }
            }
        }
    }
}