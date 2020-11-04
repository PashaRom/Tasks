using OpenQA.Selenium;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
namespace Task9VK.Forms
{
    public class AutorizationForm
    {       
        private ITextBox Login => AqualityServices.Get<IElementFactory>().GetTextBox(By.Id("index_email"), "Login");
        private ITextBox Password => AqualityServices.Get<IElementFactory>().GetTextBox(By.Id("index_pass"), "Password");
        private IButton SendDataAutorization => AqualityServices.Get<IElementFactory>().GetButton(By.Id("index_login_button"), "Send autorization data");

        public void Autorization(string login, string password)
        {
            AqualityServices.Logger.Info($"Send login {login}.");
            Login.SendKeys(login);
            AqualityServices.Logger.Info($"Send password {password}.");
            Password.SendKeys(password);
            AqualityServices.Logger.Info("Click the button.");
            SendDataAutorization.Click();
        }
    }
}
