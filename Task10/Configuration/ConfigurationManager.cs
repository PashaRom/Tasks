using System;
using System.IO;
using Test.Configuration.Common;
using Aquality.Selenium.Browsers;
namespace Test.Configuration
{
    public static class ConfigurationManager
    {
        public static ConfigurationGetter Configuration;
        public static ConfigurationGetter TestingData;
        
        static ConfigurationManager() 
        {
            try 
            {                
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
