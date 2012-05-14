using System;
using System.Collections.Generic;
using System.Text;

namespace SharpSiege
{
    public class LengthForamt
    {
       //const long MAX_BYTE = 1024;
       const long MIN_KB = 1024;
       const long MIN_MB = 1024 * 1024;
       const long MIN_GB = 1024 * 1024 * 1024;
       //const long MIN_TB = 1024 * 1024 * 1024 * 1024;
       const double KB = 1024.0;
       const double MB = 1024 * 1024.0;
       const double GB = 1024 * 1024 * 1024.0;
       const double TB = 1024 * 1024 * 1024 * 1024.0;

        public static string GetString(long length)
        {
            string number; string suffix;
            if (length < MIN_KB)
            {
                suffix="Byte";
                number= length.ToString();
            }
            else if (length >= MIN_KB && length < MIN_MB)
            {
                //return (length / KB).ToString("0.000") + " KB";
                suffix = "KB";
                number = (length / KB).ToString("0.000");

            }
            else if (length >= MIN_MB && length < MIN_GB)
            {
                //return (length / MB).ToString("0.000") + " MB";
                suffix = "MB";
                number = (length / MB).ToString("0.000");

            }
            else// if (length >= MIN_MB)
            {
                //return (length / GB).ToString("0.000") + " GB";
                suffix = "GB";
                number = (length / GB).ToString("0.000");

            }

            if (number.EndsWith(".000"))
            {
                return number.Substring(0, number.IndexOf(".")) + " " + suffix;
            }
            else
            {
                return number + " " + suffix;
            }
        }

        private static void Test()
        {
            Console.WriteLine(GetString(100));
            Console.WriteLine(GetString(1024));
            Console.WriteLine(GetString(10240));
            Console.WriteLine(GetString(10241));
            Console.WriteLine(GetString(1024 * 1024));
            Console.WriteLine(GetString(1024*1024+1024));
            Console.WriteLine(GetString(1024 * 1024 * 1024));
            Console.WriteLine(GetString(1024 * 1024 * 1024 + 1024 * 1024));
            //Console.WriteLine(GetString(100));
        }
    }
}
