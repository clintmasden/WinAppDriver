using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomationClient;

namespace AutomationCom.PerformanceUnitTests
{
    /// <summary>
    /// Launch Windows 10 + WinAppDriver.GridPerformanceUi.exe before leveraging.
    /// </summary>
    /// <remarks>
    /// C:\...\WinAppDriver.GridPerformanceUi\bin\Debug\WinAppDriver.GridPerformanceUi.exe
    /// </remarks>
    [TestClass]
    public class PerformanceTests
    {
        /// <summary>
        /// Finds StartupForm + Finds Row 1000
        /// </summary>
        /// <remarks>
        /// Form Found: 0.0540125 ~
        /// Row Found: 2.8856485 ~
        /// </remarks>
        [TestMethod]
        public void TestStartupFormGridViewPerformance()
        {
            var cUIAutomation8 = new CUIAutomation8();
            cUIAutomation8.TransactionTimeout = 60000;

            var startTime = DateTime.Now;
            var startupFormElement = cUIAutomation8.GetRootElement().FindFirst(TreeScope.TreeScope_Children, cUIAutomation8.CreatePropertyCondition(30011, "StartupForm"));
            var startupFormElementDetails = GetElementDetailsByIUIAutomationElement(startupFormElement);
            Console.WriteLine($"Form Found: {(DateTime.Now - startTime).TotalSeconds}");

            Assert.IsNotNull(startupFormElement);

            startTime = DateTime.Now;
            var startupFormPerformanceDataGridViewRowItemElement = startupFormElement.FindFirst(TreeScope.TreeScope_Descendants, cUIAutomation8.CreatePropertyCondition(30005, "Row 1000"));
            var startupFormPerformanceDataGridViewRowItemElementDetails = GetElementDetailsByIUIAutomationElement(startupFormPerformanceDataGridViewRowItemElement);
            Console.WriteLine($"Row Found: {(DateTime.Now - startTime).TotalSeconds}");

            Assert.IsNotNull(startupFormPerformanceDataGridViewRowItemElement);
        }

        /// <summary>
        /// For readability
        /// </summary>
        /// <param name="automationElement"></param>
        /// <returns></returns>
        private string GetElementDetailsByIUIAutomationElement(IUIAutomationElement automationElement)
        {
            if (automationElement == null)
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"LocalizedControl: {automationElement.CurrentLocalizedControlType}");
            stringBuilder.AppendLine($"ClassName: {automationElement.CurrentClassName}");
            stringBuilder.AppendLine($"Name: {automationElement.CurrentName}");
            stringBuilder.AppendLine($"AutomationId: {automationElement.CurrentAutomationId}");

            return stringBuilder.ToString();
        }
    }
}