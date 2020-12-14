using Aquality.Selenium.Browsers;
using TechTalk.SpecFlow;
namespace CarsSpecFlow.Hooks
{
    [Binding]
    public sealed class CarsHooks
    {       

        [BeforeFeature]
        public static void BeforeFeature()
        {
            AqualityServices.Browser.Maximize();
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            AqualityServices.Browser.Quit();
        }
    }
}
