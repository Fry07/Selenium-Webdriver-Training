using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using System;
using System.IO;

namespace Selenium
{
    public class WebDriverEvents
    {
        public static void firingDriver_ExceptionThrown(object sender, WebDriverExceptionEventArgs e)
        {
            var screenshot = ((ITakesScreenshot)e.Driver).GetScreenshot();
            var path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Screenshots\\")) + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".jpg";
            screenshot.SaveAsFile(path , ScreenshotImageFormat.Jpeg);
            File.AppendAllText(AdminTest.logPath, "Exception thrown: " + e.ThrownException.Message.ToString() + "\n\n");
        }

        public static void firingDriver_ElementClicked(object sender, WebElementEventArgs e)
        {
            File.AppendAllText(AdminTest.logPath, "Click on element: " + e.Element.ToString() + "\n\n");
        }

        public static void firingDriver_FindElementCompleted(object sender, FindElementEventArgs e)
        {
            File.AppendAllText(AdminTest.logPath, "Found element: " + e.FindMethod.ToString() + "\n\n");
        }
    }
}
