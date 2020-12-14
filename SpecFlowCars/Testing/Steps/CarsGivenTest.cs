using Aquality.Selenium.Browsers;
using TechTalk.SpecFlow;
namespace SpecFlowCars.Testing.Steps
{
    [Binding]
    public class CarsGivenTest
    {
        [Given(@"I have launch wep-application by '(.*)'")]
        public void GivenIHaveLaunchWep_ApplicationBy(string url)
        {
            AqualityServices.Browser.GoTo(url);            
        }
    }
}
