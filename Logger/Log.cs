using NLog;
using System;

namespace LogRecorder
{
    public static class Log
    {
        public static Logger log;

        [STAThread]
        static void Main()
        {
            try
            {
                log = LogManager.GetCurrentClassLogger();

                log.Trace("Version: {0}", Environment.Version.ToString());
                log.Trace("OS: {0}", Environment.OSVersion.ToString());
                log.Trace("Command: {0}", Environment.CommandLine.ToString());

                NLog.Targets.FileTarget tar = (NLog.Targets.FileTarget)LogManager.Configuration.FindTargetByName("run_log");
                tar.DeleteOldFileOnStartup = false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка работы с логом!n" + e.Message);
            }

        }
    }
}
