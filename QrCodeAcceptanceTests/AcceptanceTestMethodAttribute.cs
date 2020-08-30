using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QrCodeAcceptanceTests
{
    class AcceptanceTestMethodAttribute : TestMethodAttribute
    {
        public override TestResult[] Execute(ITestMethod testMethod)
        {
            var testResults = base.Execute(testMethod);

            if (testResults.Any((testResult) => testResult.Outcome != UnitTestOutcome.Passed))
            {
                var screenshotName = $"{testMethod.MethodInfo.DeclaringType.FullName}.{testMethod.TestMethodName}.png";
                Browser.TakeScreenshot(screenshotName);
            }

            return testResults;
        }
    }
}
