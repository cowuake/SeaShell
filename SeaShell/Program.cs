using SeaShell.Model;

namespace SeaShell
{
    class Program
    {
        static void Main(string[] args)
        {
            var shell = new Shell();

            try {
                shell.Run();
            }
            catch (Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }
    }
}