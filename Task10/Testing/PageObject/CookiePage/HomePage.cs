using OpenQA.Selenium;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
namespace Task10.Testing.PageObject.CookiePage
{
    public class HomePage
    {        
        public bool ExistHeaderLabel => AqualityServices.Get<IElementFactory>().GetLabel(By.XPath(".//div/h1"),"Header page").State.IsExist;        
    }
}
