using OpenQA.Selenium;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using System;
using FinalTask.Tests.Models;
using Configuration;
using Utilities;
namespace FinalTask.Tests.PageObjects
{
    public class TestInfoPage
    {
        private ILabel ProjectNameLabel => AqualityServices.Get<IElementFactory>().GetLabel(By.XPath("//div[contains(@class,'col-md-4')]/div[contains(@class,'panel-default') and not(contains(@class,'fail-reason-block'))]//h4[text()[contains(.,'Project name')]]/following::p"), "Project name");
        private ILabel TestNameLabel => AqualityServices.Get<IElementFactory>().GetLabel(By.XPath("//div[contains(@class,'col-md-4')]/div[contains(@class,'panel-default') and not(contains(@class,'fail-reason-block'))]//h4[text()[contains(.,'Test name')]]/following::p"), "Test name");
        private ILabel TestMethodNameLabel => AqualityServices.Get<IElementFactory>().GetLabel(By.XPath("//div[contains(@class,'col-md-4')]/div[contains(@class,'panel-default') and not(contains(@class,'fail-reason-block'))]//h4[text()[contains(.,'Test method name')]]/following::p"), "Test method name");
        private ILabel StatusLabel => AqualityServices.Get<IElementFactory>().GetLabel(By.XPath("//div[contains(@class,'col-md-4')]/div[contains(@class,'panel-default') and not(contains(@class,'fail-reason-block'))]//h4[text()[contains(.,'Status')]]/following::p/span"), "Status");
        private ILabel EnvironmentLabel => AqualityServices.Get<IElementFactory>().GetLabel(By.XPath("//div[contains(@class,'col-md-4')]/div[contains(@class,'panel-default') and not(contains(@class,'fail-reason-block'))]//h4[text()[contains(.,'Environment')]]/following::p"), "Environment");
        private ILabel BrowserLabel => AqualityServices.Get<IElementFactory>().GetLabel(By.XPath("//div[contains(@class,'col-md-4')]/div[contains(@class,'panel-default') and not(contains(@class,'fail-reason-block'))]//h4[text()[contains(.,'Browser')]]/following::p"), "Browser");
        private ILabel StartTimeLabel => AqualityServices.Get<IElementFactory>().GetLabel(By.XPath("//div[contains(@class,'col-md-4')]/div[contains(@class,'panel-default') and not(contains(@class,'fail-reason-block'))]//h4[text()[contains(.,'Time info')]]/following::p[text()[contains(.,'Start time')]]"), "Start time");
        private ILabel EndTimeLabel => AqualityServices.Get<IElementFactory>().GetLabel(By.XPath("//div[contains(@class,'col-md-4')]/div[contains(@class,'panel-default') and not(contains(@class,'fail-reason-block'))]//h4[text()[contains(.,'Time info')]]/following::p[text()[contains(.,'End time')]]"), "End time");
        private ILabel LogTextLabel => AqualityServices.Get<IElementFactory>().GetLabel(By.XPath("//div[contains(@class,'col-md-8')]/div[contains(@class,'panel-default') and not(contains(@class,'fail-reason-block'))]/div[contains(@class,'panel-heading') and text()[contains(.,'Logs')]]/following::table"), "Log");
        private ILink ScreenShotLink => AqualityServices.Get<IElementFactory>().GetLink(By.XPath("//div[contains(@class,'col-md-8')]/div[contains(@class,'panel-default') and not(contains(@class,'fail-reason-block'))]/div[contains(@class,'panel-heading') and text()[contains(.,'Attachments')]]/following::table//img"), "Attachments");

        public (string ProjectName,UIAddedTest UIAddedTest) GetTestData()
        {
            AqualityServices.Logger.Info("Get data of the saved test by UI on the page info.");
            try 
            {                 
                UIAddedTest uiAddedTest = new UIAddedTest();
                uiAddedTest.Name = TestNameLabel.Text;
                uiAddedTest.Method = TestMethodNameLabel.Text;
                uiAddedTest.Status = StatusLabel.Text;
                uiAddedTest.Environment = EnvironmentLabel.Text;
                uiAddedTest.Browser = BrowserLabel.Text;
                uiAddedTest.StartTime = StringUtil.GetPartFromString(": ", StartTimeLabel.Text);
                uiAddedTest.EndTime = StringUtil.GetPartFromString(": ", EndTimeLabel.Text);
                uiAddedTest.Log = LogTextLabel.GetText();                
                return (ProjectNameLabel.Text, uiAddedTest);
            }
            catch(Exception ex)
            {
                AqualityServices.Logger.Error($"The error occurred while getting data of the added test. Message: {ex.Message}");
                return (String.Empty, new UIAddedTest());
            }
        }

        public string GetScreenshot()
        {
            try 
            { 
                string linkImagesBase64 = ScreenShotLink.GetAttribute("src");
                return FileUtil.SaveImagePngFromBase64(
                    ConfigurationManager.Configuration.Get<string>("outDirectory:download"),
                    String.Format(ConfigurationManager.Configuration.Get<string>("finalScreenshotName"),typeof(TestInfoPage).Name),
                    linkImagesBase64,
                    "data:image/png;base64,");
            }
            catch(Exception ex)
            {
                AqualityServices.Logger.Error($"The error occurred while getting screenshot from the info test page. Message: {ex.Message}");
                return String.Empty;
            }
        }
    }
}
