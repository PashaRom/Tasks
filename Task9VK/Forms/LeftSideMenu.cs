using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
namespace Task9VK.Forms
{
    public class LeftSideMenu
    {        
        private ILink MyPage => AqualityServices.Get<IElementFactory>().GetLink(By.XPath(".//div[@id='side_bar_inner']//li[@id='l_pr']/a"), "My page");
        public string MyPageLinkName => MyPage.Name;
        public void ClickAndWaitMyPageLink()
        {
            MyPage.ClickAndWait();
        }
    }
}
