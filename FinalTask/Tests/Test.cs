using System;
using System.Collections.Generic;
using NUnit.Framework;
using Aquality.Selenium.Browsers;
using Tests.App;
using FinalTask.Tests.App;
using Utilities;
using TestRail.Models;
using Configuration;
using FinalTask.Tests.PageObjects;
using FinalTask.Tests.Extension;
using Aquality.Selenium.Configurations;
namespace FinalTask.Tests.FinalTask
{
    public class Tests
    {
        private Case currentCase;
        [SetUp]
        public void Setup()
        {            
            currentCase = AppTestRail.CurrentCase;            
            AqualityServices.Browser.Maximize();
        }

        [Test]
        public void Test1()
        {
            Logger.Step(1,currentCase.CustomStepsSeparated[0].Content);
            string token = AppUnionReporting.GetToken();
            Assert.IsNotNull(
                token,
                "Token has not been received.");
            AppTestRail.AddStepResult(1, currentCase.CustomStepsSeparated[0]);
            Logger.Step(2, currentCase.CustomStepsSeparated[1].Content);
            AqualityServices.Browser.GoTo(StringUtil.BuilAuthorizationLink(
                ConfigurationManager.Configuration.Get<string>("unionReporting:webUrl"),
                ConfigurationManager.CredOfUser.Get<string>("cred:unionReporting:login"),
                ConfigurationManager.CredOfUser.Get<string>("cred:unionReporting:password")));//@"http://login:password@192.168.99.100:8080/web"ConfigurationManager.Configuration.Get<string>("unionReporting:webUrl")
            AqualityServices.Browser.WaitForPageToLoad();
            HomePage homePage = new HomePage();
            Assert.AreEqual(
                ConfigurationManager.TestingData.Get<string>("unionReporting:pageIsLoadElementText").Trim().ToLower(),
                homePage.TextHomeLink.ToLower().Trim(),
                "The home page has not been loaded.");
            AppUnionReporting.SetCookie(
                ConfigurationManager.Configuration.Get<string>("unionReporting:cookies:tokenName"),
                token);            
            homePage.Refresh();
            Assert.IsTrue(
                homePage.TextVariantLabel.Contains(ConfigurationManager.TestingData.Get<int>("unionReporting:variant").ToString()),                
                $"The label variant did not contein expected variant = {ConfigurationManager.TestingData.Get<int>("unionReporting:variant")}. The actual rasult {homePage.TextVariantLabel}");
            AppTestRail.AddStepResult(2, currentCase.CustomStepsSeparated[1]);
            Logger.Step(3, currentCase.CustomStepsSeparated[2].Content);
            AppUnionReporting.ProjectId = homePage.GetProjectId(ConfigurationManager.TestingData.Get<string>("unionReporting:availableProjectLink"));
            homePage.ClickAvalibleProjectLink(ConfigurationManager.TestingData.Get<string>("unionReporting:availableProjectLink"));
            AllTestsPage allTestsPage = new AllTestsPage();
            Assert.IsTrue(
                allTestsPage.ListOfTests.IsTestsDesc(),
                "The list of tests has not DESC sorting on the all tests page.");            
            (List<Models.Test> Tests, string Error) testsApiResult = AppUnionReporting.GetTestsList();            
            Assert.That(
                allTestsPage.ListOfTests, 
                Is.SubsetOf(testsApiResult.Tests),
                $"Tests from the all tests page and test from API did not contain equal items. {testsApiResult.Error}");
            AppTestRail.AddStepResult(3, currentCase.CustomStepsSeparated[2]);
            Logger.Step(4, currentCase.CustomStepsSeparated[3].Content);
            AqualityServices.Browser.GoBack();
            homePage.ClickAddButton();
            string homeTabHandle = AqualityServices.Browser.Tabs().CurrentTabHandle;
            AqualityServices.Browser.Tabs().SwitchToLastTab();            
            AqualityServices.Browser.WaitForPageToLoad();            
            AddProjectPage addProjectPage = new AddProjectPage();
            Assert.IsTrue(
                addProjectPage.IsLoadedPage,
                $"The {addProjectPage.Name} has not been opened.");
            Assert.IsTrue(
                addProjectPage.CreateNewProject(ConfigurationManager.TestingData.Get<string>("unionReporting:newProjectName")),
                $"New project was not created.");
            string newProjectTabTabHandle = AqualityServices.Browser.Tabs().CurrentTabHandle;
            AqualityServices.Browser.Tabs().CloseTab();            
            Assert.That(
                newProjectTabTabHandle,
                Is.Not.SubsetOf(AqualityServices.Browser.Tabs().TabHandles));
            AqualityServices.Browser.Tabs().SwitchToTab(homeTabHandle);            
            AqualityServices.Browser.Refresh();
            Assert.IsTrue(
                homePage.IsAddedNewProject(ConfigurationManager.TestingData.Get<string>("unionReporting:newProjectName")),
                $"The new project \"{ConfigurationManager.TestingData.Get<string>("unionReporting:newProjectName")}\" has not been added to projects list");
            AppTestRail.AddStepResult(4, currentCase.CustomStepsSeparated[3]);
            Logger.Step(5, currentCase.CustomStepsSeparated[4].Content);
            homePage.ClickAvalibleProjectLink(ConfigurationManager.TestingData.Get<string>("unionReporting:newProjectName"));
            allTestsPage.ClickAddTestButton();
            AddTestPage addTestPage = new AddTestPage();
            (Models.UIAddedTest UIAddedTest, string PathScreenShot, bool SavedTestResult) addedTestResult = addTestPage.AddTest();            
            Assert.IsTrue(
                addedTestResult.SavedTestResult,
                "New test has not been saved.");            
            Assert.IsTrue(
                allTestsPage.IsAddedTestInTestsList(addedTestResult.UIAddedTest.Name),
                $"The test \"{addedTestResult.UIAddedTest.Name}\" has not found to the tests list on the all tests page.");
            AppTestRail.AddStepResult(5, currentCase.CustomStepsSeparated[4]);
            Logger.Step(6, currentCase.CustomStepsSeparated[5].Content);
            allTestsPage.ClickAddedTestInTestsList(addedTestResult.UIAddedTest.Name);
            TestInfoPage testInfoPage = new TestInfoPage();
            (string ProjectName, Models.UIAddedTest UIAddedTest) savedTestData = testInfoPage.GetTestData();
            Assert.AreEqual(
                ConfigurationManager.TestingData.Get<string>("unionReporting:newProjectName"),
                savedTestData.ProjectName,
                "The project names were not equal.");
            Assert.AreEqual(
                addedTestResult.UIAddedTest,
                savedTestData.UIAddedTest,
                $"Data of tests were not equal.");            
            Assert.Greater(
                ConfigurationManager.TestingData.Get<int>("unionReporting:files:images:percentDifferentCompare"),
                FileUtil.CompareImages(addedTestResult.PathScreenShot, testInfoPage.GetScreenshot()),
                "Screenshots are different.");
            AppTestRail.AddStepResult(6, currentCase.CustomStepsSeparated[5]);            
        }

        [TearDown]
        public void TearDown()
        {
            AppTestRail.SendResult(
               String.Format(
                   "\"{0}\" was runned on OS: \"{1}\", Browser: \"{2} {3}\" and with param isRemote: \"{4}\"" ,
                   TestContext.CurrentContext.Test.Name, 
                   Environment.OSVersion.VersionString,
                   AqualityServices.Browser.Driver.Capabilities["browserName"],
                   AqualityServices.Browser.Driver.Capabilities["browserVersion"],
                   AqualityServices.Get<IBrowserProfile>().IsRemote),
               TestContext.CurrentContext.Result.Message,
               TestContext.CurrentContext.Result.Outcome.Status.ToString(),
               currentCase,
               FileUtil.TakeScreenshot(
                   ConfigurationManager.Configuration.Get<string>("outDirectory:upload"),
                   String.Format(ConfigurationManager.Configuration.Get<string>("finalScreenshotName"), typeof(AppTestRail).Name)));            
            AqualityServices.Browser.Quit();
        }
    }
}