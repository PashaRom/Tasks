using OpenQA.Selenium;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
namespace Task9VK.Forms
{
    public class AutorizationForm
    {       
        public ITextBox Login => AqualityServices.Get<IElementFactory>().GetTextBox(By.Id("index_email"), "Login");
        public ITextBox Password => AqualityServices.Get<IElementFactory>().GetTextBox(By.Id("index_pass"), "Password");
        public IButton SendDataAutorization => AqualityServices.Get<IElementFactory>().GetButton(By.Id("index_login_button"), "Send autorization data");
    }
}
