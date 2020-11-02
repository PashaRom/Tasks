using NUnit.Framework;
using Aquality.Selenium.Browsers;
using Testing.Configuration;
using Task10.Testing.App;
using Utilities;
using Task10.Testing.PageObject.MadalWindows;
using System.Collections.Generic;
namespace Task10
{
    public class AllTests
    {
        private static Dictionary<string, string> testSteps = new Dictionary<string, string>();
        [SetUp]
        public void Setup()
        {
            testSteps.Clear();
            AqualityServices.Browser.Maximize();
        }

        [Test]
        public void ModalWindows()
        {
            AqualityServices.Browser.Maximize();
            AqualityServices.Browser.GoTo(ConfigurationManager.Configuration.Get<string>("modalWindows:url"));
            AqualityServices.Browser.WaitForPageToLoad();
            JavaScriptAlertsPage jsAlertsPage = new JavaScriptAlertsPage();
            Logger.Step(1, $"Click the button \"{jsAlertsPage.jsAlertButton.Name}\".");
            testSteps.Add($"Click the button \"{jsAlertsPage.jsAlertButton.Name}\"", "The alert modal window has the text \"I am a JS Alert\"");
            jsAlertsPage.jsAlertButton.Click();
            jsAlertsPage.SwitchToModalWindow();            
            Assert.AreEqual(
                ConfigurationManager.TestingData.Get<string>("modalWindows:alert:text"),
                jsAlertsPage.ModalWindow.Text,
                "The text in the alert modal window was differetn.");            
            Logger.Step(2, "Click the button \"OK\" in the alert modal window.");
            testSteps.Add("Click the button \"OK\" in the alert modal window.", "The alert modal window has closed and the text \"You successfuly clicked an alert\" has been appeared in the aria \"Result\".");
            jsAlertsPage.ModalWindow.Accept();
            jsAlertsPage.SwitchToModalWindow();
            Assert.IsNull(
                jsAlertsPage.ModalWindow,
                "The alert modal window has not been clossed.");            
            Assert.AreEqual(
                ConfigurationManager.TestingData.Get<string>("modalWindows:textResult:alert"),
                jsAlertsPage.GetTextResult(),
                "The text of result was different.");            
            Logger.Step(3, $"Click the button \"{jsAlertsPage.jsConfirmButton.Name}\".");
            testSteps.Add($"Click the button \"{jsAlertsPage.jsConfirmButton.Name}\"", "The confirm modal window has the text \"I am a JS Confirm\"");
            jsAlertsPage.jsConfirmButton.Click();
            jsAlertsPage.SwitchToModalWindow();
            Assert.AreEqual(
                ConfigurationManager.TestingData.Get<string>("modalWindows:confirm:text"),
                jsAlertsPage.ModalWindow.Text,
                "The text in the confirm modal window was different.");
            Logger.Step(4, $"Click the button \"OK\" in the confirm modal window.");
            testSteps.Add("Click the button \"OK\" in the confirm modal window.", "The confirm modal window has closed and the text \"You clicked: Ok\" has been appeared in the aria \"Result\".");
            jsAlertsPage.ModalWindow.Accept();
            jsAlertsPage.SwitchToModalWindow();
            Assert.IsNull(
                jsAlertsPage.ModalWindow,
                "The confirm modal window has not been clossed.");
            Assert.AreEqual(
                ConfigurationManager.TestingData.Get<string>("modalWindows:textResult:confirm"),
                jsAlertsPage.GetTextResult(),
                "The text of result was different.");
            Logger.Step(5, $"Click the button \"{jsAlertsPage.jsPromptButton.Name}\".");
            Logger.Step(3, $"Click the button \"{jsAlertsPage.jsPromptButton.Name}\".");
            jsAlertsPage.jsPromptButton.Click();
            jsAlertsPage.SwitchToModalWindow();
            Assert.AreEqual(
                ConfigurationManager.TestingData.Get<string>("modalWindows:prompt:text"),
                jsAlertsPage.ModalWindow.Text,
                "The text in the prompt modal window was different.");
            Logger.Step(6, "Send the text to the prompt modal window and click the button \"OK\".");
            string generatedText = StringUtil.GeneraterText(15);
            testSteps.Add("Click the button \"OK\" in the prompt modal window.", $"The prompt modal window has closed and the text \"You entered: {generatedText}\" has been appeared in the aria \"Result\".");            
            jsAlertsPage.ModalWindow.SendKeys(generatedText);
            jsAlertsPage.ModalWindow.Accept();
            jsAlertsPage.SwitchToModalWindow();
            Assert.IsNull(
               jsAlertsPage.ModalWindow,
               "The prompt modal window has not been clossed.");
            Assert.AreEqual(
                $"{ConfigurationManager.TestingData.Get<string>("modalWindows:textResult:prompt")}{generatedText}",
                jsAlertsPage.GetTextResult(),
                "The text of result was different.");
        }
        
        [TearDown]
        public void TearDown()
        {
            AppTestRail.SendResult(
                TestContext.CurrentContext.Test.Name,
                TestContext.CurrentContext.Result.Message,
                TestContext.CurrentContext.Result.Outcome.Status.ToString(),
                testSteps,
                FileUtil.TakeScreenshot(AqualityServices.Browser.Driver, ConfigurationManager.Configuration.Get<string>("modalWindows:screenshotName")));            
            AqualityServices.Browser.Quit();
        }
    }
}