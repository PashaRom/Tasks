using NUnit.Framework;
using Aquality.Selenium.Browsers;
using Test.Configuration;
using Task10.Testing.App;
using Task10.Testing.Models.BasicAuth;
using Task10.Testing.Models.Cookies;
using Task10.Testing.PageObject.CookiePage;
using Utilities;
using Task10.Testing.PageObject.MadalWindows;
namespace Task10
{
    public class AllTests
    {
        [SetUp]
        public void Setup()
        {
            AqualityServices.Browser.Maximize();
        }

        [Test]
        public void BasicAuthorization()
        {
            var authResponseTask = AppWebRequest.BasicAutorization();
            authResponseTask.Wait();
            AuthResponse authResponse = authResponseTask.Result;            
            Assert.AreEqual(
                ConfigurationManager.TestingData.Get<bool>("basicAuth:response:authenticated"),
                authResponse.Authenticated,
                $"The param \"authenticated\" has different value.");
            Assert.AreEqual(
                ConfigurationManager.TestingData.Get<string>("basicAuth:response:user"),
                authResponse.User,
                $"The param \"user\" has different value.");            
        }

        [Test]
        public void WorkingWithCookie()
        {            
            Logger.Step(1, $"Go to the web resource \"{ConfigurationManager.Configuration.Get<string>("cookie:url")}\".");
            AqualityServices.Browser.GoTo(ConfigurationManager.Configuration.Get<string>("cookie:url"));
            AqualityServices.Browser.WaitForPageToLoad();
            HomePage homePage = new HomePage();
            Assert.IsTrue(
                homePage.ExistHeaderLabel,
                $"Home page \"{ConfigurationManager.Configuration.Get<string>("cookie:url")}\" has not been loaded.");
            Logger.Step(2, "Add cookies");
            AppWorkingCookies.SetCookies();
            CollectionAssert.AreEqual(
                AppWorkingCookies.AppCookies,
                AppWorkingCookies.GetCookies(),
                "Cookies have not been added.");
            Logger.Step(3, $"Delete cookie with key=.{(ConfigurationManager.TestingData.Get<string>("deleteCookie"))}");
            AppWorkingCookies.DeleteCookieNamed();
            CollectionAssert.AreEqual(
                AppWorkingCookies.AppCookies,
                AppWorkingCookies.GetCookies(),
                "Cookie has not been deleted.");
            Logger.Step(4, $"Add value into Cookie ={ConfigurationManager.TestingData.GetObjectParam<AppCookie>("addValueCookie")}");
            (string Expected, string Actual) cookiesValue = AppWorkingCookies.AddValueCookie();
            Assert.AreEqual(
                cookiesValue.Expected,
                cookiesValue.Actual,
                $"The cookie's value has not been added.");
            Logger.Step(5, "Delete cookies");
            Assert.IsTrue(
                AppWorkingCookies.DeleteCookies(),
                $"The cookies have not been deleted");
        }

        [Test]
        public void ModalWindows()
        {
            AqualityServices.Browser.Maximize();
            AqualityServices.Browser.GoTo(ConfigurationManager.Configuration.Get<string>("modalWindows:url"));
            AqualityServices.Browser.WaitForPageToLoad();
            JavaScriptAlertsPage jsAlertsPage = new JavaScriptAlertsPage();
            Logger.Step(1, $"Click the button \"{jsAlertsPage.jsAlertButton.Name}\".");            
            jsAlertsPage.jsAlertButton.Click();
            jsAlertsPage.SwitchToModalWindow();            
            Assert.AreEqual(
                ConfigurationManager.TestingData.Get<string>("modalWindows:alert:text"),
                jsAlertsPage.ModalWindow.Text,
                "The text in the alert modal window was differetn.");
            Logger.Step(2, $"Click the button \"OK\" in the alert modal window.");            
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
            jsAlertsPage.jsConfirmButton.Click();
            jsAlertsPage.SwitchToModalWindow();
            Assert.AreEqual(
                ConfigurationManager.TestingData.Get<string>("modalWindows:confirm:text"),
                jsAlertsPage.ModalWindow.Text,
                "The text in the confirm modal window was different.");
            Logger.Step(4, $"Click the button \"OK\" in the confirm modal window.");
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
            jsAlertsPage.jsPromptButton.Click();
            jsAlertsPage.SwitchToModalWindow();
            Assert.AreEqual(
                ConfigurationManager.TestingData.Get<string>("modalWindows:prompt:text"),
                jsAlertsPage.ModalWindow.Text,
                "The text in the prompt modal window was different.");
            Logger.Step(6, "Send the text to the prompt modal window and click the button \"OK\".");
            string generatedText = StringUtil.GeneraterText(15);
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
            AqualityServices.Browser.Quit();
        }
    }
}