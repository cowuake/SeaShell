using System.Text;
using System.Linq;

namespace SeaShell.Utilities
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

        public static bool IsAllUpper(this string str)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
            {
                return false;
            }

            return str.Where(c => char.IsLetter(c)).All(c => char.IsUpper(c));
        }

        public static bool IsAllLower(this string str)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
            {
                return false;
            }

            return str.Where(c => char.IsLetter(c)).All(c => char.IsLower(c));
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

            for (int i = 0; i < strArray.Length; i++)
            {
                result.Append(ReverseString(strArray[i]));
                result.Append(i < strArray.Length - 1 ? " " : null);
            }

            return result.ToString();
        }

        public static T[] Rotate<T>(this T[] array, int offset, string direction = "left")
        {
            if (array.Length == 0) { return array; }

            var newArray = (T[]) Array.CreateInstance(typeof(T), array.Length);
            Array.Copy(array, newArray, array.Length);
            newArray.RotateInPlace(offset, direction);

            return newArray;
        }

        public static void RotateInPlace<T>(this T[] array, int offset = 1, string direction = "left")
        {
            if (array.Length == 0) { return; }

            if (direction.ToLower() != "left" && direction.ToLower() != "right") {
                return;
            }

            if (direction == "left")
            {
                T temp = array[0];
                for (int i = 0; i < array.Length - 1; i++)
                {
                    array[i] = array[i + 1];
                }
                array[array.Length-1] = temp;
            } else {
                T temp = array[array.Length - 1];
                for (int i = array.Length - 1; i > 0; i--)
                {
                    array[i] = array[i - 1];
                }
                array[0] = temp;
            }

            if (offset > 1)
            {
                array.RotateInPlace(offset - 1, direction);
            }
        }
    }
}