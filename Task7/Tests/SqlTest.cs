using NUnit.Framework;
using Task7.Tests;
using Task7.Utilities.Configuration;
namespace Task7
{
    public class SqlTests
    {
        [Test]        
        public void GetMinWorkingTimeTest()
        {
            Assert.IsTrue(AppSqlRequest.GetMinWorkingTimeTest(),
                "The request to MySQL for getting minimum test working times has been filed.");           
        }

        [Test]       
        public void GetNumberOfTestsInProject()
        {
            Assert.IsTrue(AppSqlRequest.GetNumberOfTestsInProject(),
                "The request to MySQL for getting number of tests in the project has been filed.");
        }

        [Test]        
        public void GetTestExecutedAfterParticularDate()
        {
            Assert.IsTrue(AppSqlRequest.GetExecutedTestsAfterParticularDate(ConfigurationManager.TestData.Get<string>("dateFrom")),
                $"The request to MySQL for getting executed tests after \"{ConfigurationManager.TestData.Get<string>("dateFrom")}\" has been filed.");
        }

        [Test]        
        public void GetNumberOfTestExecutedParticularBrowsers()
        {
            Assert.IsTrue(AppSqlRequest.GetNumberOfTestExecutedParticularBrowsers(ConfigurationManager.TestData.GetSectionWithArray<string>("browsers")),
                $"The request to MySQL for getting number of executed tests particular in browsers has been filed.");
        }
    }
}