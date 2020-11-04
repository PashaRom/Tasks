using Task9VK.Forms;
namespace Task9VK.PageObject
{
    public class LoginPage
    {
        private AutorizationForm autorizationForm = new AutorizationForm();
        
        public void Autorization(string login, string password)
        {
            autorizationForm.Autorization(login, password);
        }
    }
}
