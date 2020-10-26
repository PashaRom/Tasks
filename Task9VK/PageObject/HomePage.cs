using Aquality.Selenium.Browsers;
using Task9VK.Forms;
namespace Task9VK.PageObject
{
    public class HomePage
    {
        private AutorizationForm autorizationForm = new AutorizationForm();
       
        public void Autorization(string login, string password)
        {
            AqualityServices.Logger.Info($"Send login {login}.");
            autorizationForm.Login.SendKeys(login);
            AqualityServices.Logger.Info($"Send password {password}.");
            autorizationForm.Password.SendKeys(password);
            AqualityServices.Logger.Info("Click the button.");
            autorizationForm.SendDataAutorization.Click();
        }

        public void GoToMyPage()
        {
            LeftSideMenu leftSideMenu = new LeftSideMenu();
            AqualityServices.Logger.Info($"Click the link \"{leftSideMenu.MyPage.Name}\".");
            leftSideMenu.MyPage.ClickAndWait();
        }
    }
}
