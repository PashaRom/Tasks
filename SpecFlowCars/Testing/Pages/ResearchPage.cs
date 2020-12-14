using OpenQA.Selenium;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Browsers;
using SpecFlowCars.Forms;
namespace SpecFlowCars.Pages
{
    public class ResearchPage
    {
        private ILabel TitleLabel => AqualityServices.Get<IElementFactory>().GetLabel(By.XPath("//title[text()[contains(.,'Research')]]"), "Research page title");
        private SearchForm searchForm = new SearchForm();
        private ILink SideBySideComparisonsLink => AqualityServices.Get<IElementFactory>().GetLink(By.XPath("//a[@data-linkname='compare-cars']"),"Side-by-side Comparisons");
        public bool IsOpened => TitleLabel.State.IsExist;

        public void SelectCarData(string brend, string model, int year) => searchForm.SelectCarData(brend, model, year);

        public (string Brand, string Model, int prodYear) GetDataFromInputField() => (searchForm.BrandComboBoxSelectedText, searchForm.ModelComboBoxSelectedText, int.Parse(searchForm.YearComboboxSelectedText));

        public void OpenCarDescription() => searchForm.ClickSearchButton();

        public void ClickSideBySideComparisonsLink() => SideBySideComparisonsLink.Click();
    }
}
