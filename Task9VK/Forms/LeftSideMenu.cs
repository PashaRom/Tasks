using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
namespace Task9VK.Forms
{
    public class LeftSideMenu
    {        
        public ILink MyPage => AqualityServices.Get<IElementFactory>().GetLink(By.XPath(".//div[@id='side_bar_inner']/nav/ol/li[@id='l_pr']/a"), "My page");
    }
}
