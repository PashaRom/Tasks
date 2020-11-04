using Aquality.Selenium.Browsers;
using Task9VK.Forms;
namespace Task9VK.PageObject
{
    public class HomePage
    {
        public void GoToMyPage()
        {
            LeftSideMenu leftSideMenu = new LeftSideMenu();
            AqualityServices.Logger.Info($"Click the link \"{leftSideMenu.MyPageLinkName}\".");
            leftSideMenu.ClickAndWaitMyPageLink();
        }
    }
}
