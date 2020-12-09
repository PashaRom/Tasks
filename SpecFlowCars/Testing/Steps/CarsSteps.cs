using NUnit.Framework;
using Aquality.Selenium.Browsers;
using TechTalk.SpecFlow;
using SpecFlowCars.Pages;
using SpecFlowCars.Models;
namespace SpecFlowCars.Steps
{
    [Binding]
    public class CarsSteps
    {
        private static HomePage homePage;
        private ResearchPage researchPage;
        private DescriptionOfCarPage descriptionOfCarPage;
        private TrimsPage trimsPage;
        private ComparePage comparePage;
        private ScenarioContext scenarioContext;
        public CarsSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Given(@"I have launch wep-application by '(.*)'")]
        public void GivenIHaveLaunchWep_ApplicationBy(string url)
        {
            AqualityServices.Browser.GoTo(url);
            homePage = new HomePage();            
        }

        [When(@"I click on the top menu link '(.*)'")]
        public void WhenIClickOnTheTopMenuLink(string itemOfMenu)
        {
            homePage.ClickOnItemOfMenu(itemOfMenu);
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

        [When(@"I search car brand '(.*)' model '(.*)' and production year '(.*)'")]
        public void WhenISearchCarBrandModelAndProductionYear(string brand, string model, int year)
        {
            researchPage.SelectCarData(brand, model, year);
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

        [When(@"I click on link CompareTrim")]
        public void WhenIClickOnLinkCompareTrim()
        {
            descriptionOfCarPage.ClickCompareTrimLink();
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

        [When(@"I save options of car: brand '(.*)', model '(.*)', production year, '(.*)' available engines, transmissions")]
        public void WhenISaveOptionsOfCarBrandModelProductionYearAvailableEnginesTransmissions(string brand, string model, int year)
        {
            (string EnginValue, string TransValue) = trimsPage.GetDataCarOptions();
            scenarioContext.Add(
                $"{brand}{model}{year}",
                new Car {
                    Model=model, 
                    Brand = brand,
                    Year = year,
                    AvalibleEngines = EnginValue,
                    Transmission = TransValue
                });
        }

        [When(@"I am back to Home page")]
        public void WhenIAmBackToHomePage()
        {
            trimsPage.GoToHomePage();
        }

        [Then(@"Home page is opened")]
        public void ThenHomePageIsOpened()
        {
            Assert.IsTrue(
                homePage.IsLoaded,
                "Home page has not been opened.");
        }

        [When(@"I click on the top menu link '(.*)' on Trims page")]
        public void WhenIClickOnTheTopMenuLinkOnTrimsPage(string itemOfMenu)
        {
            trimsPage.ClickOnItemOfMenu(itemOfMenu);
        }

        [When(@"I click on the Side-by-side Comparisons block")]
        public void WhenIClickOnTheSide_By_SideComparisonsBlock()
        {            
            researchPage.ClickSideBySideComparisonsLink();
        }

        [Then(@"Compare page is opened")]
        public void ThenComparePageIsOpened()
        {
            comparePage = new ComparePage();
            Assert.IsTrue(
                comparePage.IsOpened,
                "Compare page has not been opened");
        }

        [When(@"Choose first car for comparing: brand '(.*)', model '(.*)', production year, '(.*)'")]
        public void WhenChooseFirstCarForComparingBrandModelProductionYear(string brand, string model, int year)
        {                      
            ScenarioContext.Current.Pending();
        }

        [Then(@"Chosen car page is opened")]
        public void ThenChosenCarPageIsOpened()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I use Add button to add for comparison secomd car: brand '(.*)', model '(.*)', production year '(.*)'")]
        public void WhenIUseAddButtonToAddForComparisonSecomdCarBrandModelProductionYear(string brand, string model, int year)
        {            
            ScenarioContext.Current.Pending();
        }

        [Then(@"Models have been successfully selected for comparison")]
        public void ThenModelsHaveBeenSuccessfullySelectedForComparison()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"Check Compare page for both cars")]
        public void WhenCheckComparePageForBothCars()
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
