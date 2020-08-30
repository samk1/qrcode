using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
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


        private static CancellationTokenSource _serverCancellationTokenSource;
        private ChromeDriver _driver;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            Browser.Start();

            _serverCancellationTokenSource = new CancellationTokenSource();
            QrCode.Program.CreateHostBuilder(null).Build().RunAsync(_serverCancellationTokenSource.Token);
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            Browser.CurrentDriver.Quit();
            _serverCancellationTokenSource.Cancel();
        }

        [TestInitialize]
        public void AcceptanceTestInitialize()
        {
            _driver = Browser.CurrentDriver;
        }

        [AcceptanceTestMethod]
        public void VerifyPageTitle()
        {
            // Replace with your own test logic
            _driver.Url = "https://localhost:5001";
            Assert.AreEqual("QrCode", _driver.Title);
        }
    }
}
