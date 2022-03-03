using System.Text;
using System.Linq;

namespace SeaShellUtilities
{
    public static class Utilities
    {
        // Allow for the use of BuildString objects on Linux/Unix
        //  without fearing carriage returns
        public static void AppendUnixLine(this StringBuilder sb, string str)
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
        public static bool IsWithinBufferWidth(this string str)
        {
            return (str.Length <= Console.BufferWidth) ? true : false;
        }

        public static string ReverseString(string s)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
            {
                return s;
            }

            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);

            return new string(charArray);
        }

        public static string ReverseWordsInString(string s)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
            {
                return s;
            }

            string[] strArray = s.Split(' ');
            var result = new StringBuilder();

            for (var i = 0; i < strArray.Length; i++)
            {
                result.Append(ReverseString(strArray[i]));
                result.Append(i < strArray.Length - 1 ? " " : "");
            }

            return result.ToString();
        }
    }
}