using System;
using System.Text;
using System.IO;

namespace SharpSiege
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = string.Empty;
            int retry = 10;

            #region Parse Parameters
            for (int i = 0; args != null && i < args.Length; i++)
            {
                if (i + 1 < args.Length)
                { 
                    switch (args[i].ToLower())
                    {
                        case "-r":                        
                            if (!int.TryParse(args[++i], out retry))
                            {
                                retry = 10;
                            }
                            if (retry <= 0) retry = 10;
                            break;

                        case "-u":
                            try
                            {
                                Uri u = new Uri(args[++i]);
                                url = u.ToString();
                            }
                            catch (Exception ex)
                            {
                                Logger.Log(ex.ToString());
                            }
                            break;
                    }
                }
            } 
            #endregion
            if (!string.IsNullOrEmpty(url))
            {
                Siege s = new Siege() { Url = url, Retry = retry };
                s.Run();
            }
            else
            {
                Help();
            }
            Console.Read();
            Logger.Close();
        }

        static void Help()
        {
            if (File.Exists("HELP.md"))
            {
                Console.WriteLine(File.ReadAllText("HELP.md", Encoding.UTF8));
            }
            else
            {
                Console.WriteLine("Missing README!");
            }           
        }

    }
}
