using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace QrCodeAcceptanceTests
{
    class TestServer
    {
        private static CancellationTokenSource _serverCancellationTokenSource;

        public static void Start()
        {
            _serverCancellationTokenSource = new CancellationTokenSource();
            QrCode.Program.CreateHostBuilder(null)
                .Build().RunAsync(_serverCancellationTokenSource.Token);
        }

        public static void Stop()
        {
            _serverCancellationTokenSource.Cancel();
        }
    }
}
