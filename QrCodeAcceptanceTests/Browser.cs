using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrCodeAcceptanceTests
{
    class Browser
    {
        public static ChromeDriver CurrentDriver { get; private set; }

        public static void Start()
        {
            ChromeOptions options = new ChromeOptions();
            if (System.Environment.GetEnvironmentVariable("CI") == "true")
            {
                options.AddArgument("--remote-debugging-port=9222");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--headless");
            }

            CurrentDriver = new ChromeDriver(options);
        }
    }
}
