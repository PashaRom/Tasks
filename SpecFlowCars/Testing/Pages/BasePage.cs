using OpenQA.Selenium;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Browsers;
namespace SpecFlowCars.Pages
{
    public abstract class BasePage
    {
        protected ILink NavLogoLink => AqualityServices.Get<IElementFactory>().GetLink(By.XPath("//a[@data-linkname='header-home']"), "Navigation logo link");
        public void GoToHomePage() => NavLogoLink.Click();
    }
}
