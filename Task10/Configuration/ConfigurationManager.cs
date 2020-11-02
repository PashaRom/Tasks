using System;
using System.IO;
using Testing.Configuration.Common;
using Aquality.Selenium.Browsers;
namespace Testing.Configuration
{
    public static class ConfigurationManager
    {
        public static ConfigurationGetter Configuration;
        public static ConfigurationGetter TestingData;
        
        static ConfigurationManager() 
        {
            try 
            {
                bool file = File.Exists($"{Directory.GetCurrentDirectory()}\\Source\\testconfig.json");
                Configuration = new ConfigurationGetter($"{Directory.GetCurrentDirectory()}\\Source\\testconfig.json");            
                TestingData = new ConfigurationGetter($"{Directory.GetCurrentDirectory()}\\Source\\testdata.json");
            }
            catch(Exception ex)
            {
                AqualityServices.Logger.Fatal("Unexpected error occurred during creating ConfigurationManager.", ex);
            }
        }        
    }   
}
