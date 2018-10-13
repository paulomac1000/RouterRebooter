using System;
using System.Configuration;

namespace RouterRebooter.Helpers
{
    public static class AppSettings
    {
        public static bool IsDebug => Convert.ToBoolean(ConfigurationManager.AppSettings["IsDebug"]);
        public static string RouterIp => ConfigurationManager.AppSettings["RouterIp"];
        public static string RouterLogin => ConfigurationManager.AppSettings["RouterLogin"];
        public static string RouterPassword => ConfigurationManager.AppSettings["RouterPassword"];
        public static int CheckingIntervalInMinutes => Convert.ToInt32(ConfigurationManager.AppSettings["CheckingIntervalInMinutes"]);
    }
}