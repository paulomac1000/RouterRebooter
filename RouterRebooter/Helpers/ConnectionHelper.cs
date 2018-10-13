using PrimS.Telnet;
using Renci.SshNet;
using System.Net.NetworkInformation;
using System.Threading;

namespace RouterRebooter.Helpers
{
    public static class ConnectionHelper
    {
        public static bool CheckSshCanConnect()
        {
            if (!TryPingIp(AppSettings.RouterIp)) return false;
            using (var sshClient =
                new SshClient(AppSettings.RouterIp, AppSettings.RouterLogin, AppSettings.RouterPassword))
            {
                try
                {
                    sshClient.Connect();
                }
                catch
                {
                    return false;
                }

                if (!sshClient.IsConnected) return false;

                sshClient.Disconnect();
                return true;
            }
        }

        public static bool CheckTelnetCanConnect()
        {
            using (var telnetClient = new Client(AppSettings.RouterIp, 23, new CancellationToken()))
            {
                telnetClient.TryLoginAsync(AppSettings.RouterLogin, AppSettings.RouterPassword, loginTimeoutMs: 15).GetAwaiter().GetResult();

                if (!telnetClient.IsConnected) return false;

                telnetClient.Dispose();
                return true;
            }
        }

        public static void TelnetSendReboot()
        {
            using (var telnetClient = new Client(AppSettings.RouterIp, 23, new CancellationToken()))
            {
                telnetClient.TryLoginAsync(AppSettings.RouterLogin, AppSettings.RouterPassword, loginTimeoutMs: 15).GetAwaiter().GetResult();
                telnetClient.WriteLine("reboot").GetAwaiter().GetResult();
            }
        }

        #region private methods

        private static bool TryPingIp(string ipAdress)
        {
            if (string.IsNullOrEmpty(ipAdress)) return false;

            using (var ping = new Ping())
            {
                var pingReply = ping.Send(ipAdress);
                if (pingReply == null || pingReply.Status != IPStatus.Success)
                    return false;
            }

            return true;
        }

        #endregion private methods
    }
}