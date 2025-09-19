using System;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace SimpleService
{
    public class SimpleService : ServiceBase
    {
        public const string SvcName = "SimpleServiceDemo";
        private Timer _timer;
        private readonly string _logDir = @"C:\Users\Public\";
        private string _logFile { get { return Path.Combine(_logDir, "service.log"); } }

        public SimpleService()
        {
            ServiceName = SvcName;
            CanStop = true;
            CanPauseAndContinue = false;
            AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            Directory.CreateDirectory(_logDir);
            File.AppendAllText(_logFile, string.Format("[{0:u}] Service starting...\r\n", DateTime.Now));

            _timer = new Timer(5000); // every 5 seconds
            _timer.Elapsed += new ElapsedEventHandler(OnTick);
            _timer.AutoReset = true;
            _timer.Start();

            File.AppendAllText(_logFile, string.Format("[{0:u}] Service started.\r\n", DateTime.Now));
        }

        private void OnTick(object sender, ElapsedEventArgs e)
        {
            File.AppendAllText(_logFile, string.Format("[{0:u}] heartbeat\r\n", DateTime.Now));
        }

        protected override void OnStop()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
            }
            File.AppendAllText(_logFile, string.Format("[{0:u}] Service stopped.\r\n", DateTime.Now));
        }

        // console-mode helper
        public void TestRun()
        {
            OnStart(new string[0]);
            Console.WriteLine("Running in console mode. Press Enter to stop...");
            Console.ReadLine();
            OnStop();
        }
    }
}
