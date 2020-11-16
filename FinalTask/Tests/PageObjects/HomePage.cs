using System;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using Utilities;
namespace FinalTask.Tests.PageObjects
{
    public class HomePage
    {
        private ILabel Label(By locator, string name) => AqualityServices.Get<IElementFactory>().GetLabel(locator, name);
        private ILink HomeLink => AqualityServices.Get<IElementFactory>().GetLink(By.XPath(".//ol[@class='breadcrumb']//a"), "Home");
        private ILink AvalibleProjectLink(string projectName) => AqualityServices.Get<IElementFactory>().GetLink(By.XPath($".//div[@class='list-group']/a[text()[contains(.,'{projectName}')]]"), projectName);
        private IButton AddButton => AqualityServices.Get<IElementFactory>().GetButton(By.XPath(".//div[@class='panel-heading']/*[contains(@class,btn)]"), "Add new project");
        public string TextHomeLink => HomeLink.Text;
        public string TextVariantLabel => Label(By.XPath(".//footer/div[@class='container']//span"), "Variant").Text;
        public void Refresh()
        {
            AqualityServices.Browser.Refresh();
            AqualityServices.Browser.WaitForPageToLoad();
        }

        public void ClickAvalibleProjectLink(string projectName) => AvalibleProjectLink(projectName).ClickAndWait();
        
        public int GetProjectId(string projectName)
        {
            AqualityServices.Logger.Info("Get the avalible project id.");
            try
            {
                return StringUtil.GetIdFromString("=", AvalibleProjectLink(projectName).GetAttribute("href"));
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Error($"The error occurred while getting the avalible project id. Message: {ex.Message}");
                return -1;
            }
        }

        public void ClickAddButton() 
        {
            AqualityServices.Browser.ExecuteScript(FileUtil.GetJavaScriptFromFile("ScrollToTop.js"));            
            AqualityServices.ConditionalWait.WaitFor(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(AddButton.Locator), TimeSpan.FromSeconds(7));
            AddButton.MouseActions.MoveToElement();
            AddButton.Click(); 
        }

        public bool IsAddedNewProject(string projectName) => AvalibleProjectLink(projectName).State.IsExist;
    }
}
