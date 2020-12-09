using OpenQA.Selenium;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Browsers;
namespace SpecFlowCars.Pages
{
    public class DescriptionOfCarPage
    {
        private ILink CompareTrimLink => AqualityServices.Get<IElementFactory>().GetLink(By.XPath("//a[@data-linkname='trim-compare']"), "Compare trim");
        private ILabel HeaderLabel => AqualityServices.Get<IElementFactory>().GetLabel(By.XPath("//h1[@class='cui-page-section__title']"), "Header page");
        public bool IsOpened => CompareTrimLink.State.IsExist;
        public void ClickCompareTrimLink() => CompareTrimLink.Click();

        public string HeaderLabelText => HeaderLabel.Text;
    }
}
