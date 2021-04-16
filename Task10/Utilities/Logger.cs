using Aquality.Selenium.Browsers;
namespace Utilities
{
    public static class Logger
    {
        public static void Step(int numberOfStep, string description)
        {
            string infoMessage = $"STEP: {numberOfStep}\tDescription: {description}";
            AqualityServices.Logger.Info(infoMessage);
        }
    }
}
