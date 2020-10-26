using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
namespace Task9VK.Forms
{
    public class LeftSideMenu
    {
        private IElementFactory elementFactory = AqualityServices.Get<IElementFactory>();
        public ILink MyPage => elementFactory.GetLink(By.XPath(".//div[@id='side_bar_inner']/nav/ol/li[@id='l_pr']/a"), "My page");
    }
}
