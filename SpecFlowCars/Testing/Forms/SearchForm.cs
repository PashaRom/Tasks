using OpenQA.Selenium;
using Aquality.Selenium.Forms;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
namespace SpecFlowCars.Forms
{
    public class SearchForm : Form
    {
        private const string xPathToSearchForm = "//form[@class='_24sct']";
        private IComboBox BrandComboBox => AqualityServices.Get<IElementFactory>().GetComboBox(By.XPath($"{xPathToSearchForm}//select[@name='makeId']"), "Brand");
        private IComboBox ModelComboBox => AqualityServices.Get<IElementFactory>().GetComboBox(By.XPath($"{xPathToSearchForm}//select[@name='modelId']"), "Model");
        private IComboBox YearCombobox => AqualityServices.Get<IElementFactory>().GetComboBox(By.XPath($"{xPathToSearchForm}//select[@name='year']"),"Year");
        private IButton SearchButton => AqualityServices.Get<IElementFactory>().GetButton(By.XPath($"{xPathToSearchForm}//input[@type='submit']"), "Search");
        public SearchForm():base(By.XPath(xPathToSearchForm), "Search") { }

        public void SelectCarData(string brand, string model, int productionYear)
        {
            BrandComboBox.SelectByText(brand);
            ModelComboBox.SelectByText(model);
            YearCombobox.SelectByText(productionYear.ToString());          
        }

        public string BrandComboBoxSelectedText => BrandComboBox.SelectedText;
        public string ModelComboBoxSelectedText => ModelComboBox.SelectedText;
        public string YearComboboxSelectedText => YearCombobox.SelectedText;
        public void ClickSearchButton() => SearchButton.Click();
    }
}
