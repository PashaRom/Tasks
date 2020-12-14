using NUnit.Framework;
using Aquality.Selenium.Browsers;
using TechTalk.SpecFlow;
using SpecFlowCars.Pages;
using SpecFlowCars.Models;
namespace SpecFlowCars.Steps
{
    [Binding]
    public class CarsThenSteps
    {
        private static HomePage homePage;
        private ResearchPage researchPage;
        private DescriptionOfCarPage descriptionOfCarPage;
        private TrimsPage trimsPage;
        private ComparePage comparePage;
        private ScenarioContext scenarioContext;
        public CarsThenSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }  

        [Then(@"Research page is opened")]
        public void ThenResearchPageIsOpened()
        {
            researchPage = new ResearchPage();
            AqualityServices.Browser.WaitForPageToLoad();
            Assert.IsTrue(
                researchPage.IsOpened,
                "Research page has not been opened.");
        }
        
        [Then(@"Fields search form have values brand '(.*)' model '(.*)' production year '(.*)'")]
        public void ThenFieldsSearchFormHaveValuesBrandModelProductionYear(string brand, string model, int year)
        {
            (string Brand, string Model, int ProdYear) = researchPage.GetDataFromInputField();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(
                    brand,
                    Brand.Trim(),
                    "Brands are not equal.");
                Assert.AreEqual(
                    model,
                    Model.Trim(),
                    "Models are not equal.");
                Assert.AreEqual(
                    year,
                    ProdYear,
                    "Year data are not equal");
            });
        }

        [Then(@"The description of car page is opened")]
        public void ThenTheDescriptionOfCarPageIsOpened()
        {
            researchPage.OpenCarDescription();
            descriptionOfCarPage = new DescriptionOfCarPage();
            Assert.IsTrue(
                descriptionOfCarPage.IsOpened,
                "The description of car page has not been opened.");
        }

        [Then(@"Target options of car page is opened with brand '(.*)' model '(.*)' production year '(.*)'")]
        public void ThenTargetOptionsOfCarPageIsOpenedWithBrandModelProductionYear(string brand, string model, int year)
        {
            trimsPage = new TrimsPage();
            string text = trimsPage.BreadCrumbLinkText;
            Assert.AreEqual(
                $"{year} {brand} {model}",
                trimsPage.BreadCrumbLinkText,
                "Header description options of car page was not equal search oprions");
        }

        [Then(@"Home page is opened")]
        public void ThenHomePageIsOpened()
        {
            homePage = new HomePage();
            Assert.IsTrue(
                homePage.IsLoaded,
                "Home page has not been opened.");
        }
        
        [Then(@"Compare page is opened")]
        public void ThenComparePageIsOpened()
        {
            comparePage = new ComparePage();
            Assert.IsTrue(
                comparePage.IsOpened,
                "Compare page has not been opened");
        }
        
        [Then(@"Chosen car page is opened")]
        public void ThenChosenCarPageIsOpened()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"Models have been successfully selected for comparison")]
        public void ThenModelsHaveBeenSuccessfullySelectedForComparison()
        {
            ScenarioContext.Current.Pending();
        }     

        [Then(@"Options of cars on Compare page are equal with options of cars have got previous steps: '(.*)', '(.*)', '(.*)', '(.*)', '(.*)', '(.*)'")]
        public void ThenOptionsOfCarsOnComparePageAreEqualWithOptionsOfCarsHaveGotPreviousSteps(string firstCarBrand, string firstCarModel, int firstCarYear, string secondCarBrand, string secondCarModel, int secondCarYear)
        {
            Car firstCar = scenarioContext[$"{firstCarBrand}{firstCarModel}{firstCarYear}"] as Car;
            Car secondCar = scenarioContext[$"{secondCarBrand}{secondCarModel}{secondCarYear}"] as Car;
            ScenarioContext.Current.Pending();
        }
    }
}
