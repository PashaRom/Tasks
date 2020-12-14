using SpecFlowCars.Forms;
namespace SpecFlowCars.Pages
{
    public class HomePage : BasePage
    {        
        private TopMenuForm topMenuForm = new TopMenuForm();
        public bool IsLoaded => NavLogoLink.State.IsDisplayed;
        public void ClickOnItemOfMenu(string itemOfMenu) => topMenuForm.ClickOn(itemOfMenu);
    }   
}
