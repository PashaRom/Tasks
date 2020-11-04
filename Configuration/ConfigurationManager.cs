using System.IO;
using Test.Configuration.Common;
namespace Test.Configuration
{
    public static class ConfigurationManager
    {
        private static readonly string fileConfigPath = $"{Directory.GetCurrentDirectory()}\\Source\\testconfig.json";
        private static readonly string fileDataPath = $"{Directory.GetCurrentDirectory()}\\Source\\testdata.json";
        private static readonly string fileUserCredPath = $"{Directory.GetCurrentDirectory()}\\Source\\usercred.json";
        public static readonly ConfigurationGetter Configuration;
        public static readonly ConfigurationGetter TestingData;
        public static readonly  ConfigurationGetter CredOfUser;

        static ConfigurationManager()
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
    }   
}
