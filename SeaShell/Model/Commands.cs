namespace SeaShell.Commands
{
    public static class BaseCommands
    {
        public static void Beep()
        {
            Console.Beep();
        }

        public static void Cow()
        {
            Console.WriteLine("🐮 There is no cow level 🐮");
        }

        public static void Exit()
        {
            Environment.Exit(0);
        }

        public static void Hostname()
        {
            Console.WriteLine(Environment.MachineName);
        }

        public static void Username()
        {
            Console.WriteLine(Environment.UserName);
        }
    }
}