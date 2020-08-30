using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Threading.Tasks;

namespace QrCodeAcceptanceTests
{
    [TestClass]
    public class ChromeDriverTest
    {
        // In order to run the below test(s), 
        // please follow the instructions from http://go.microsoft.com/fwlink/?LinkId=619687
        // to install Microsoft WebDriver.

        private ChromeDriver _driver;
        private CancellationTokenSource _serverCancellationTokenSource;

        [TestInitialize]
        public void ChromeDriverInitialize()
        {
            // Initialize edge driver 
            var options = new ChromeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };
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
        public void EdgeDriverCleanup()
        {
            _driver.Quit();
            _serverCancellationTokenSource.Cancel();
        }
    }
}
