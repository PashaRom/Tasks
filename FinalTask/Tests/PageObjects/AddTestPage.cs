using System;
using System.Drawing;
using OpenQA.Selenium;
using Aquality.Selenium.Browsers;
using FinalTask.Tests.Forms;
using FinalTask.Tests.Models;
using Configuration;
using Utilities;
namespace FinalTask.Tests.PageObjects
{
    public class AddTestPage
    {
        private AddTestForm AddTestForm(By locator, string name) => new AddTestForm(locator, name);        
        public (UIAddedTest UIAddedTest, string PathScreenShot, bool SavedTestResult) AddTest()
        {
            AqualityServices.Logger.Info("Add new test.");
            try
            {                
                UIAddedTest uiAddedTest = ConfigurationManager.TestingData.GetObjectParam<UIAddedTest>("unionReporting:newTest");
                uiAddedTest.StartTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.0");
                uiAddedTest.EndTime = DateTime.Now.AddSeconds(2).ToString("yyyy-MM-dd hh:mm:ss.0");
                uiAddedTest.Environment = System.Environment.OSVersion.VersionString;
                uiAddedTest.Browser = $"{AqualityServices.Browser.Driver.Capabilities["browserName"]} {AqualityServices.Browser.Driver.Capabilities["browserVersion"]}";
                uiAddedTest.Log = StringUtil.GeneraterText(200);
                AddTestForm addTestForm = AddTestForm(By.Id("addTestForm"), "Add test form");
                string pathScreenShot = addTestForm.AddNewTest(uiAddedTest, ConfigurationManager.Configuration.Get<string>("outDirectory:upload"), String.Format(ConfigurationManager.Configuration.Get<string>("finalScreenshotName"), typeof(AddTestPage).Name));               
                Size size = addTestForm.Size;
                bool savedResult = addTestForm.IsDisplayedSuccessLabel();                
                CloseAddTestForm();
                return (uiAddedTest, pathScreenShot, savedResult);
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Error($"The error occurred while added new test on the added test form. Message: {ex.Message}");
                return (new UIAddedTest(), String.Empty, false);
            }
        }
        public void CloseAddTestForm()
        {
            AqualityServices.Logger.Info("Close Add test form.");
            try 
            {                
                AqualityServices.Browser.ExecuteScript(FileUtil.GetJavaScriptFromFile("CloseAddTestForm.js"));                
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Error($"The error occurred while added new test on the added test form. Message: {ex.Message}");                
            }
        }
    }
}
