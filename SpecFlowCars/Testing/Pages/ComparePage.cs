using OpenQA.Selenium;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Browsers;
namespace SpecFlowCars.Pages
{
    public class ComparePage
    {
        private ILabel CompareLabel => AqualityServices.Get<IElementFactory>().GetLabel(By.Id("compare"),"Compare on breadcrumbs");
        public bool IsOpened => CompareLabel.State.IsExist;
    }
}
