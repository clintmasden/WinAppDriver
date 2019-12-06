using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;

namespace WinAppDriver.PerformanceUnitTests
{
    /// <summary>
    /// Launch WinAppDriver.exe + WinAppDriver.GridPerformanceUi.exe before leveraging.
    /// </summary>
    /// <remarks>
    /// C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe
    /// C:\...\WinAppDriver.GridPerformanceUi\bin\Debug\WinAppDriver.GridPerformanceUi.exe
    /// </remarks>
    [TestClass]
    public class PerformanceTests
    {
        private WindowsDriver<WindowsElement> GetWindowsApplicationRootDriver()
        {
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", "Root");
            appCapabilities.SetCapability("deviceName", "WindowsPC");

            var windowsApplicationRootDriver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723/"), appCapabilities, TimeSpan.FromSeconds(60));
            windowsApplicationRootDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(180);

            return windowsApplicationRootDriver;
        }

        /// <summary>
        /// Finds StartupForm + Finds Row 1000
        /// </summary>
        /// <remarks>
        /// Form Found: 0.102021~
        /// Row Found: 26.9488513~
        /// </remarks>
        [TestMethod]
        public void TestStartupFormGridViewPerformance()
        {
            var windowsApplicationRootDriver = GetWindowsApplicationRootDriver();

            var startTime = DateTime.Now;
            var startupFormWindowsElement = windowsApplicationRootDriver.FindElementByAccessibilityId("StartupForm");
            Console.WriteLine($"Form Found: {(DateTime.Now - startTime).TotalSeconds}");

            Assert.IsNotNull(startupFormWindowsElement);

            startTime = DateTime.Now;
            var startupFormPerformanceDataGridViewRowItemElement = startupFormWindowsElement.FindElementByName("Row 1000");
            Console.WriteLine($"Row Found: {(DateTime.Now - startTime).TotalSeconds}");

            Assert.IsNotNull(startupFormPerformanceDataGridViewRowItemElement);

            //startTime = DateTime.Now;
            //startupFormPerformanceDataGridViewRowItemElement.Click();
            //Console.WriteLine($"Row Click: {(DateTime.Now - startTime).TotalSeconds}");
        }
    }
}