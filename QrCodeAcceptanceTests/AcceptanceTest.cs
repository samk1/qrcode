using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Threading.Tasks;

namespace QrCodeAcceptanceTests
{
    [TestClass]
    public class AcceptanceTest
    {
        // In order to run the below test(s), 
        // please follow the instructions from http://go.microsoft.com/fwlink/?LinkId=619687
        // to install Microsoft WebDriver.

        private ChromeDriver _driver;
        private CancellationTokenSource _serverCancellationTokenSource;

        [TestInitialize]
        public void AcceptanceTestInitialize()
        {
            // Initialize edge driver 
            var options = new ChromeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };

            if (System.Environment.GetEnvironmentVariable("CI") == "true")
            {
                options.BinaryLocation = System.Environment.GetEnvironmentVariable("CHROMEWEBDRIVER");
                options.AddArgument("--remote-debugging-port=9222");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--headless");
            }

            _driver = new ChromeDriver(options);

            _serverCancellationTokenSource = new CancellationTokenSource();
            QrCode.Program.CreateHostBuilder(null).Build().RunAsync(_serverCancellationTokenSource.Token);
        }

        [TestMethod]
        public void VerifyPageTitle()
        {
            // Replace with your own test logic
            _driver.Url = "https://localhost:5001";
            Assert.AreEqual("QrCode", _driver.Title);
        }

        [TestCleanup]
        public void AcceptanceTestCleanup()
        {
            _driver.Quit();
            _serverCancellationTokenSource.Cancel();
        }
    }
}
