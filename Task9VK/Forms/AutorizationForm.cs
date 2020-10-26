using OpenQA.Selenium;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
namespace Task9VK.Forms
{
    public class AutorizationForm
    {
        private IElementFactory elementFactory = AqualityServices.Get<IElementFactory>();
        public ITextBox Login => elementFactory.GetTextBox(By.Id("index_email"), "Login");
        public ITextBox Password => elementFactory.GetTextBox(By.Id("index_pass"), "Password");
        public IButton SendDataAutorization => elementFactory.GetButton(By.Id("index_login_button"), "Send autorization data");

    }
}
