using System;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using Aquality.Selenium.Browsers;
using Configuration;
using TestRail;
using TestRail.Models;
namespace Tests.App
{
    public static class AppTestRail
    {
        private static TestRailClient railClient;
        private static int countAssert = 0, countStep = 0;
        private static List<Status> Statuses { get; set; }        
        public static Case CurrentCase => railClient.GetCase(ConfigurationManager.Configuration.Get<int>("testRail:caseId"));
        private const string passedStatus = "Passed", failedStatus = "Failed";

        public static List<StepResult> StepResults { get; set; } = new List<StepResult>();
        static AppTestRail()
        {
            try
            {               
                railClient = new TestRailClient(
                    ConfigurationManager.Configuration.Get<string>("testRail:pathUrlBeforeMethod"),
                    ConfigurationManager.CredOfUser.Get<string>("cred:testRail:login"),
                    ConfigurationManager.CredOfUser.Get<string>("cred:testRail:password"));
                Statuses = railClient.GetStatuses();
            }
            catch(Exception ex)
            {
                AqualityServices.Logger.Fatal($"The error appeared during creating {nameof(TestRailClient)} \"{ex.Message}\"", ex);
            }
        }
    
        public static void SendResult(string comment, string defect, string testStatus, Case currentCase, string screenShotPath)
        {
            AqualityServices.Logger.Info($"Send the result of test to TestRail.");
            try 
            {
                defect = defect != null ? (defect.Length >= 250 ? String.Format(@"{0}... More information see the log file.", defect.Remove(200, defect.Length - 201)) : $"{defect}") : null;               
                AqualityServices.Logger.Info($"Add run.");
                RunCreatingRequest runCreating = new RunCreatingRequest
                {
                    SuiteId = currentCase.SuiteId,
                    Name = "Union reporting",
                    Description = "Verify unit testing web and api services.",
                    AssignedtoId = railClient.GetUser(ConfigurationManager.CredOfUser.Get<string>("cred:testRail:email")).Id,
                    CaseIds = new List<int?> { currentCase.Id }
                };
                Run run = railClient.AddRun(runCreating, railClient.GetSuite(currentCase.SuiteId).ProjectId);
                AqualityServices.Logger.Info($"Get the tests list");
                List<Test> tests = railClient.GetTests(run.Id);
                AqualityServices.Logger.Info($"Get the statuses list");
                List<Status> statuses = railClient.GetStatuses();
                Test test = tests.FirstOrDefault();
                Status statusInput = Statuses.Where(status => status.Name.Equals(testStatus.ToLower())).FirstOrDefault();
                AqualityServices.Logger.Info($"Add result.");
                if (statusInput.Name.ToLower().Equals(failedStatus.ToLower()))
                    AddStepResult(countStep + 1, currentCase.CustomStepsSeparated[countStep]);
                ResultCreatingRequest resultCreating = new ResultCreatingRequest
                {
                    StatusId = statusInput.Id,
                    Comment = String.Format("This test {0}", comment),
                    AssignedtoId = runCreating.AssignedtoId,
                    Defects = defect,
                    CustomStepResults = StepResults
                };
                Result result = railClient.AddResult(resultCreating, test.Id);
                AqualityServices.Logger.Info($"Attach the final screenshot.");
                AttachmentResponse attachmentResponse = railClient.AddAttachmentToResult(screenShotPath, result.Id);
                if(attachmentResponse.AttachmentId != null)
                    AqualityServices.Logger.Info($"The result of test was added to TestRail.");
            }
            catch(Exception ex)
            {
                AqualityServices.Logger.Error($"The error appeared during send test result. Message: \"{ex.Message}\".");
            }
            finally
            {
                countAssert = 0;
                Statuses = new List<Status>();
            }
        }

        public static void AddStepResult(int numberOfStep, Step step)
        {            
            countStep = numberOfStep;
            string errorMessage = null;
            Status status = new Status();
            try
            {
                if (countAssert == TestContext.CurrentContext.Result.Assertions.Count())
                {
                    errorMessage = null;
                    status = Statuses.Where(statusTest => statusTest.Name.ToLower().Equals(passedStatus.ToLower())).FirstOrDefault();
                }
                else
                {
                    errorMessage = TestContext.CurrentContext.Result.Assertions.LastOrDefault().Message;
                    status = Statuses.Where(statusTest => statusTest.Name.ToLower().Equals(TestContext.CurrentContext.Result.Assertions.LastOrDefault().Status.ToString().ToLower())).FirstOrDefault();
                    countAssert++;
                }
                StepResults.Add(new StepResult
                {
                    Content = step.Content,
                    Expected = step.Expected,
                    Actual = errorMessage,
                    StatusId = status.Id
                });
            }
            catch(Exception ex)
            {
                AqualityServices.Logger.Error($"The error appeared during send test result. Message: \"{ex.Message}\".");
            }
        }
    }
}
