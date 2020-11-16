using System;
using System.IO;
using Aquality.Selenium.Browsers;
using Configuration.Common;
namespace Configuration
{
    public static class ConfigurationManager
    {
        private static readonly string fileConfigPath = $"{Directory.GetCurrentDirectory()}\\Source\\testconfig.json";
        private static readonly string fileDataPath = $"{Directory.GetCurrentDirectory()}\\Source\\testdata.json";
        private static readonly string fileUserCredPath = $"{Directory.GetCurrentDirectory()}\\Source\\usercred.json";
        public static readonly ConfigurationGetter Configuration;
        public static readonly ConfigurationGetter TestingData;
        public static readonly ConfigurationGetter CredOfUser;

        static ConfigurationManager()
        {
            try 
            { 
                Configuration = !File.Exists($"{fileConfigPath}") ?
                    throw new FileNotFoundException($"The file which path \"{fileConfigPath}\" did not find.") :
                    new ConfigurationGetter($"{fileConfigPath}");
                TestingData = !File.Exists($"{fileDataPath}") ?
                    throw new FileNotFoundException($"The file which path \"{fileDataPath}\" did not find.") :
                    new ConfigurationGetter($"{fileDataPath}");
                CredOfUser = !File.Exists($"{fileUserCredPath}") ?
                    throw new FileNotFoundException($"The file which path \"{fileUserCredPath}\" did not find.") :
                    new ConfigurationGetter($"{fileUserCredPath}");
            }
            catch(Exception ex)
            {
                AqualityServices.Logger.Error($"An error occured while created one of setting files. The error message: {ex.Message}");
            }
        }
    }   
}
