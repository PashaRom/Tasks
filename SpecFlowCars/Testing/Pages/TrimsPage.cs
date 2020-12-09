using System.Collections.ObjectModel;
using OpenQA.Selenium;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Browsers;
using SpecFlowCars.Forms;
namespace SpecFlowCars.Pages
{
    public class TrimsPage : BasePage
    {
        private ILink BreadCrumbLink => AqualityServices.Get<IElementFactory>().GetLink(By.XPath("//a[@data-linkname='research-mmy']"),"Bread crumb");
        private IWebElement TrimTable => AqualityServices.Browser.Driver.FindElementById("trim-table");
        private IWebElement HeaderOptionsTable => TrimTable.FindElement(By.XPath("//div[@id='labels-row']"));
        private ReadOnlyCollection<IWebElement> HeaderCells => HeaderOptionsTable.FindElements(By.XPath("./div[contains(@class,'cell')]"));
        private ReadOnlyCollection<IWebElement> ValueCells => TrimTable.FindElements(By.XPath("./div[contains(@class,'trim-details')]//div[contains(@class,'cell')]"));
        private TopMenuForm topMenuForm = new TopMenuForm();
        public string BreadCrumbLinkText => BreadCrumbLink.Text;

        public void ClickOnItemOfMenu(string itemOfMenu) => topMenuForm.ClickOn(itemOfMenu);

        public (string EnginValue, string TransValue) GetDataCarOptions() => (ValueCells[GetIndexHeader("engine")].Text, ValueCells[GetIndexHeader("trans")].Text);
        
        private int GetIndexHeader(string headerName)
        {
            int index = 0;
            foreach (IWebElement headerElement in HeaderCells)
            {
                if (headerElement.Text.Trim().ToLower().Equals(headerName))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }
    }
}
