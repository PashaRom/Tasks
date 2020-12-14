using TechTalk.SpecFlow;
using SpecFlowCars.Pages;
using SpecFlowCars.Models;
namespace SpecFlowCars.Testing.Steps
{
    [Binding]
    public class CarsWhenTest
    {
        private static HomePage homePage;
        private ResearchPage researchPage;
        private DescriptionOfCarPage descriptionOfCarPage;
        private ScenarioContext scenarioContext;
        private TrimsPage trimsPage;
        public CarsWhenTest(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [When(@"I click on the top menu link '(.*)'")]
        public void WhenIClickOnTheTopMenuLink(string itemOfMenu)
        {
            homePage = new HomePage();
            homePage.ClickOnItemOfMenu(itemOfMenu);
        }

        [When(@"I search car brand '(.*)' model '(.*)' and production year '(.*)'")]
        public void WhenISearchCarBrandModelAndProductionYear(string brand, string model, int year)
        {
            researchPage = new ResearchPage();
            researchPage.SelectCarData(brand, model, year);
        }

        [When(@"I click on link CompareTrim")]
        public void WhenIClickOnLinkCompareTrim()
        {
            descriptionOfCarPage = new DescriptionOfCarPage();
            descriptionOfCarPage.ClickCompareTrimLink();
        }

        [When(@"I save options of car: brand '(.*)', model '(.*)', production year, '(.*)' available engines, transmissions")]
        public void WhenISaveOptionsOfCarBrandModelProductionYearAvailableEnginesTransmissions(string brand, string model, int year)
        {
            trimsPage = new TrimsPage();
            (string EnginValue, string TransValue) = trimsPage.GetDataCarOptions();
            scenarioContext.Add(
                $"{brand}{model}{year}",
                new Car
                {
                    Model = model,
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

        [When(@"Choose first car for comparing: brand '(.*)', model '(.*)', production year, '(.*)'")]
        public void WhenChooseFirstCarForComparingBrandModelProductionYear(string brand, string model, int year)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I use Add button to add for comparison secomd car: brand '(.*)', model '(.*)', production year '(.*)'")]
        public void WhenIUseAddButtonToAddForComparisonSecomdCarBrandModelProductionYear(string brand, string model, int year)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"Check Compare page for both cars")]
        public void WhenCheckComparePageForBothCars()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
