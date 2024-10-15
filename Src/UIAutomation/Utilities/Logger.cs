using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.IO;

namespace UIAutomation.Utilities
{
    public class Logger
    {
        public Logger(TestContext testContext)
        {
            var fileUtil = new FileUtil();
            Context = testContext;
            LogPath = $"{fileUtil.GetBasePath()}/Resources/Logs/[LOG]_{testContext.TestName}_{DateTime.Now:yyyy-MM-dd_HH.mm.ss}.log";
        }

        public Logger(string logPath)
        {
            LogPath = logPath;
        }

        public TestContext Context { get; set; }
        public string LogPath { get; set; }

        public void Info(string message)
        {
            var fileStream = File.AppendText(LogPath);
            fileStream.WriteLine($"{DateTime.Now.ToString(CultureInfo.InvariantCulture)} | INFO | {message}");
            fileStream.Close();
        }

        public void Warning(string message)
        {
            var fileStream = File.AppendText(LogPath);
            fileStream.WriteLine($"{DateTime.Now.ToString(CultureInfo.InvariantCulture)} | WARNING | {message}");
            fileStream.Close();
        }

        public void Error(Exception e)
        {
            var fileStream = File.AppendText(LogPath);
            var timeStamp = DateTime.Now.ToString(CultureInfo.InvariantCulture);

            fileStream.WriteLine($"{timeStamp} | ERROR | {e.Message}");
            if (e.InnerException != null)
            {
                fileStream.WriteLine("Inner Exception: " + e.InnerException.Message);
            }
            fileStream.Close();
        }
    }
}
