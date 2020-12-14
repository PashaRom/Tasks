using OpenQA.Selenium;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Browsers;
namespace SpecFlowCars.Forms
{
    public class TopMenuForm
    {
        private ILink ResearchLink => AqualityServices.Get<IElementFactory>().GetLink(By.XPath("//a[@data-linkname='header-research']"), "Research link on top menu");       
        public void ClickOn(string itemOfMenu)
        {
            switch (itemOfMenu)
            {
                case "Research" : 
                    ResearchLink.Click();
                    break;
            }
        }
    }
}
