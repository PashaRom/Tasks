using NUnit.Framework;
using Task7.Tests.Models;
using Task7.Tests;
using Task7.Utilities.Configuration;
namespace Task7
{
    public class SqlTests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            AppSqlRequest.RespondWriteToFiles();
        }
        [Test]        
        public void GetMinWorkingTimeTest()
        {            
            CollectionAssert.AreEqual(
                Utilities.JsonUtil.Read<WorkingTime>(ConfigurationManager.Configuration.Get<string>("files:json:minWorkingTime")), 
                AppSqlRequest.GetMinWorkingTimeTest(),
                "The request to MySQL for getting minimum test working times has been filed.");           
        }

        [Test]       
        public void GetNumberOfTestsInProject()
        {            
            CollectionAssert.AreEqual(
                Utilities.JsonUtil.Read<NumberOfTestsInProject>(ConfigurationManager.Configuration.Get<string>("files:json:countTests")),
                AppSqlRequest.GetNumberOfTestsInProject(),
                "The request to MySQL for getting number of tests in the project has been filed.");
        }

        [Test]        
        public void GetTestExecutedAfterParticularDate()
        {
            CollectionAssert.AreEqual(
                Utilities.JsonUtil.Read<TestsInProject>(ConfigurationManager.Configuration.Get<string>("files:json:testsInProject")),
                AppSqlRequest.GetExecutedTestsAfterParticularDate(ConfigurationManager.TestData.Get<string>("dateFrom")),
                $"The request to MySQL for getting executed tests after \"{ConfigurationManager.TestData.Get<string>("dateFrom")}\" has been filed.");
        }

        [Test]        
        public void GetNumberOfTestExecutedParticularBrowsers()
        {
            CollectionAssert.AreEqual(
                Utilities.JsonUtil.Read<NumberOfBrowsers>(ConfigurationManager.Configuration.Get<string>("files:json:numberOfBrowsers")),
                AppSqlRequest.GetNumberOfTestExecutedParticularBrowsers(ConfigurationManager.TestData.GetSectionWithArray<string>("browsers")),
                $"The request to MySQL for getting number of executed tests particular in browsers has been filed.");
        }
    }
}