using System;
using System.Linq;
using System.Collections.Generic;
using Aquality.Selenium.Browsers;
using Testing.Configuration;
using Task10.TestRail;
using Task10.TestRail.Models;
namespace Task10.Testing.App
{
    public static class AppTestRail
    {
        private static TestRailClient railClient;
        static AppTestRail()
        {
            railClient = new TestRailClient(
                ConfigurationManager.Configuration.Get<string>("testRail:pathUrlBeforeMethod"),
                ConfigurationManager.Configuration.Get<string>("testRail:cred:login"),
                ConfigurationManager.Configuration.Get<string>("testRail:cred:password")); 
        }
    
        public static void SendResult(string comment, string defect, string testStatus, Dictionary<string,string> testSteps, string screenShotPath)
        {
            AqualityServices.Logger.Info($"Send the result of test to TestRail.");
            try { 
                int projectId = ConfigurationManager.Configuration.Get<int>("testRail:projectId");
                int templateId = ConfigurationManager.Configuration.Get<int>("testRail:templateId");
                int typeId = ConfigurationManager.Configuration.Get<int>("testRail:typeId");
                int priorityId = ConfigurationManager.Configuration.Get<int>("testRail:priorityId");
                AqualityServices.Logger.Info($"Get project with id = {projectId}");
                Project project = railClient.GetProject(projectId);
                AqualityServices.Logger.Info($"Add suit");
                Suite suite = railClient.AddSuit("Modal windows","Verify modal windows such as alert, confirm, prompt in browsers", projectId);
                AqualityServices.Logger.Info($"Add section.");
                SectionCreatingRequest sectionCreating = new SectionCreatingRequest
                {
                    Description = "The section save the case for modal windows",
                    Name = "Verify Windows",
                    SuiteId = suite.Id
                };
                Section section = railClient.AddSection(sectionCreating, projectId);
                AqualityServices.Logger.Info($"Add case.");
                List<Step> steps = new List<Step>();
                foreach(KeyValuePair<string,string> testStep in testSteps)
                {
                    steps.Add(new Step { Content = testStep.Key, Expected = testStep.Value});
                }
                CaseCreatingRequest caseCreating = new CaseCreatingRequest
                {
                    Title = "Verify modal windows in browser.",
                    TemplateId = ConfigurationManager.Configuration.Get<int>("testRail:templateId"),
                    TypeId = ConfigurationManager.Configuration.Get<int>("testRail:typeId"),
                    PriorityId = ConfigurationManager.Configuration.Get<int>("testRail:priorityId"),
                    CustomStepsSeparated = steps
                };
                Case createdCase = railClient.AddCase(caseCreating, section.Id);
                AqualityServices.Logger.Info($"Add run.");
                RunCreatingRequest runCreating = new RunCreatingRequest
                {
                    SuiteId = suite.Id,
                    Name = "Verify modal windows in browser",
                    Description = "Verify modal windows in browser such us alert, comfirm, prompt",
                    AssignedtoId = createdCase.CreatedBy,
                    CaseIds = new List<int?> { createdCase.Id }
                };
                Run run = railClient.AddRun(runCreating, projectId);
                AqualityServices.Logger.Info($"Get the tests list");
                List<Test> tests = railClient.GetTests(run.Id);
                AqualityServices.Logger.Info($"Get the statuses list");
                List<Status> statuses = railClient.GetStatuses();
                Test test = tests.FirstOrDefault();
                Status statusInput = statuses.Where(status => status.Name.Equals(testStatus.ToLower())).FirstOrDefault();
                AqualityServices.Logger.Info($"Add result.");
                ResultCreatingRequest resultCreating = new ResultCreatingRequest
                {
                    StatusId = statusInput.Id,
                    Comment = $"This test {comment}",                    
                    AssignedtoId = createdCase.CreatedBy,
                    Defects = $"{defect}"
                };
                Result result = railClient.AddResult(resultCreating, test.Id);
                AqualityServices.Logger.Info($"Attach the final screenshot.");
                AttachmentResponse attachmentResponse = railClient.AddAttachmentToResult(screenShotPath, result.Id);
                if(attachmentResponse.AttachmentId != null)
                    AqualityServices.Logger.Info($"The result of test was added to TestRail.");
            }
            catch(Exception ex)
            {
                AqualityServices.Logger.Fatal("The error appeared during send test result.",ex);
            }
        }
    }
}
