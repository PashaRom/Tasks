using System;
using System.IO;
using OpenQA.Selenium;
using Aquality.Selenium.Browsers;
namespace Utilities
{
    public static class FileUtil
    {
        public static string TakeScreenshot(IWebDriver driver, string fileName)
        {
            string pathToFile = String.Empty;
            try
            {                
                string outDirectory = $"{Directory.GetCurrentDirectory()}\\out";
                pathToFile = $"{outDirectory}\\{fileName}";
                if (!Directory.Exists(outDirectory))
                    Directory.CreateDirectory(outDirectory);
                if (File.Exists(pathToFile))
                    File.Delete(pathToFile);
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(pathToFile);
                return pathToFile;
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Fatal("The error appeared during taking the screenshot.", ex);
                return pathToFile;
            }
        }
    }
}
