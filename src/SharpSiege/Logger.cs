using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SharpSiege
{
    public class Logger
    {
        static FileStream fs;
        static StreamWriter sw = null;

        static Logger()
        {
            try
            {
                fs = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sharpsiege.log"), FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                sw = new StreamWriter(fs);
                sw.AutoFlush=true;
      
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Log(string msg)
        {
            try
            {
                if (sw != null)
                {
                    fs.Seek(0, SeekOrigin.End);
                    string log = string.Format("[{0}]\t{1}", DateTime.Now.ToString(), msg);
                    sw.WriteLine(log);
                    Console.WriteLine(log);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public static void Close()
        {
            try
            {
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

    }
}
