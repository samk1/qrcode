using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QrCodeAcceptanceTests
{
    class Browser
    {
        public static ChromeDriver CurrentDriver { get; private set; }

        private static string ScreenshotPath = BuildScreenshotPath();

        private static bool RunningInCI => Environment.GetEnvironmentVariable("CI") == "true";

        private static string BuildScreenshotPath()
        {
            if (RunningInCI)
            {
                return Path.Combine(Environment.GetEnvironmentVariable("HOME"), "screenshots");
            }
            else
            {
                return Path.Combine(Environment.CurrentDirectory, "screenshots");
            }
        }

        public static void TakeScreenshot(string screenshotName)
        {
            var screenshotPath = Path.Combine(ScreenshotPath, screenshotName);
            File.WriteAllText($"{screenshotPath}.html", CurrentDriver.PageSource);
            CurrentDriver.GetScreenshot().SaveAsFile(screenshotPath);
            Console.WriteLine($"screenshot saved to {screenshotPath}");
        }

        public static void Start()
        {
            if (!Directory.Exists(ScreenshotPath))
            {
                Directory.CreateDirectory(ScreenshotPath);
            }

            ChromeOptions options = new ChromeOptions();
            if (RunningInCI)
            {
                options.AddArgument("--remote-debugging-port=9222");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--headless");
            }

            CurrentDriver = new ChromeDriver(options);
        }
    }
}
