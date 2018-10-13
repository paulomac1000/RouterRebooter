using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimS.Telnet;
using Renci.SshNet;
using RouterRebooter.Helpers;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ConnectSsh_Succes()
        {
            var sshclient = new SshClient(AppSettings.RouterIp, AppSettings.RouterLogin, AppSettings.RouterPassword);
            sshclient.Connect();
            sshclient.Disconnect();
        }

        [TestMethod]
        [ExpectedException(typeof(SocketException))]
        public void ConnectSsh_Failed()
        {
            var sshclient = new SshClient("195.184.36.47", AppSettings.RouterLogin, AppSettings.RouterPassword);
            sshclient.Connect();
        }

        [TestMethod]
        public async Task ConnectTelnet_Succes()
        {
            using (var client = new Client(AppSettings.RouterIp, 23, new CancellationToken()))
            {
                await client.TryLoginAsync(AppSettings.RouterLogin, AppSettings.RouterPassword, loginTimeoutMs: 15);
                Assert.IsTrue(client.IsConnected);
            }
        }
    }
}