using System.Text;

namespace SeaShellUtilities
{
    public static class Utilities
    {
        // Allow for the use of BuildString objects on Linux/Unix
        //  without fearing carriage returns
        internal static void AppendUnixLine(this StringBuilder sb, string str)
        {
            if (System.OperatingSystem.IsWindows())
            {
                sb.AppendLine(str);
            }
            else
            {
                sb.Append(str + "\n");
            }
        }

        // Check if a string can be print without exceeding the current buffer width
        internal static bool IsWithinBufferWidth(this string str)
        {
            return (str.Length <= Console.BufferWidth) ? true : false;
        }
    }
}