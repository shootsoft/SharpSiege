using System;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.IO;

namespace SharpSiege
{
    public class Siege
    {
        public int Retry { get; set; }
        //public int Time { get; set; }
        //public string File { get; set; }
        public string Url { get; set; }
        //public int Thread { get; set; }
        //public int Delay { get; set; }

        protected bool isRunning;
        protected Stopwatch watch;

        public TimeSpan Cost
        {
            get
            {
                if (watch != null)
                {
                    return watch.Elapsed;
                }
                else
                {
                    return TimeSpan.Zero;
                }

            }
        }
        public double MinResponseWait { get; protected set; }
        public double MaxResponseWait { get; protected set; }
        public long ByteReads { get; protected set; }
        public int SuccessCount { get; protected set; }
        public int FailedCount { get; protected set; }

        public Siege()
        {
            watch = new Stopwatch();
            isRunning = false;
            //Thread = 1;
            //Delay = 0;
        }


        public void Run()
        {
            if (!isRunning)
            {
                isRunning = true;
                ByteReads=0;
                
                Console.Write(
@"** SHARP SIEGE 0.1
** Preparing 1 concurrent users for battle.
The server is now under sharp siege...
Lifting the server sharp siege...      ");
                watch.Start();

                for (int i = 0; i < Retry; i++)
                {
                    Stopwatch perRequest = new Stopwatch();
                    perRequest.Start();
                    try
                    {
                        HttpWebRequest req = WebRequest.Create(Url) as HttpWebRequest;
                        HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
                        Stream stream = resp.GetResponseStream();
                        byte[] buff = new byte[102400];
                        int len = stream.Read(buff, 0, buff.Length);
                        while (len > 0)
                        {
                            ByteReads += len;
                            len = stream.Read(buff, 0, buff.Length);
                        }
                        resp.Close();
                        SuccessCount++;
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString());
                        FailedCount++;
                    }
                    perRequest.Stop();
                    TimeSpan ts = perRequest.Elapsed;
                    if (ts.TotalMilliseconds < MinResponseWait)
                    {
                        MinResponseWait = ts.TotalMilliseconds;
                    }
                    if (ts.TotalMilliseconds > MaxResponseWait)
                    {
                        MaxResponseWait = ts.TotalMilliseconds;
                    }
                    //perRequest.Reset();
                }
                watch.Stop();
                Console.Write("Done"+Environment.NewLine);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("=============================");
                sb.AppendLine("Total: " + watch.Elapsed);
                sb.AppendLine("Requests: " + Retry);
                sb.AppendLine("Requests Per Second: " + (Retry / watch.Elapsed.TotalSeconds).ToString("0.00"));
                sb.AppendLine("Min Response Wait(ms): " + MinResponseWait);
                sb.AppendLine("Max Response Wait(ms): " + MaxResponseWait);
                sb.AppendLine("Read Bytes: " + LengthForamt.GetString(ByteReads));
                sb.AppendLine("Success Count: " + SuccessCount);
                sb.AppendLine("Failed Count: " + FailedCount);
                Logger.Log(sb.ToString());
                isRunning = false;
            }
        }

        public void TaskRun()
        {

        }
    }
}
