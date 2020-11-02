using System;
using System.Collections.Generic;
using System.Text;
using Task10.TestRail.Models;
using Task10.Utilities;
namespace Task10.TestRail
{
    public class TestRailClient
    {
        private readonly string url;
        private readonly string authBase64;
        public TestRailClient(string url, string login, string password)
        {
            this.url = url;
            this.authBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{login}:{password}"));
        }        
        private string BuildUri(string method, int? id = -1)
        {
            StringBuilder uriStringBuilder = new StringBuilder();
            uriStringBuilder.Append(url);
            uriStringBuilder.Append(method);
            if (id != -1)
                uriStringBuilder.Append($"/{id}");
            return uriStringBuilder.ToString();
        }

        public Project GetProject(int id)
        {
            var projectTask = TestRailApiUtil.Get<Project>(BuildUri("get_project", 140), authBase64);
            projectTask.Wait();
            return projectTask.Result;            
        }

        public Suite AddSuit(string name, string description, int projectId)
        {
            SuiteCreatingRequest suitCreating = new SuiteCreatingRequest { Name = name, Description = description };
            var creartedSuitTask = TestRailApiUtil.Post<SuiteCreatingRequest, Suite>(BuildUri("add_suite", projectId),authBase64, suitCreating);
            creartedSuitTask.Wait();            
            return creartedSuitTask.Result;
        }

        public Section AddSection(SectionCreatingRequest sectionCreating, int projectId)
        {
            var createdSectionTask = TestRailApiUtil.Post<SectionCreatingRequest, Section>(BuildUri("add_section", projectId), authBase64, sectionCreating);
            createdSectionTask.Wait();            
            return createdSectionTask.Result;
        }

        public Case AddCase(CaseCreatingRequest caseCreating, int? sectionId)
        {
            var createdCaseTask = TestRailApiUtil.Post<CaseCreatingRequest, Case>(BuildUri("add_case", sectionId), authBase64, caseCreating);
            createdCaseTask.Wait();            
            return createdCaseTask.Result;
        }
        public Run AddRun(RunCreatingRequest runCreating, int projectId)
        {
            var createdRunTask = TestRailApiUtil.Post<RunCreatingRequest, Run>(BuildUri("add_run", projectId), authBase64, runCreating);
            createdRunTask.Wait();            
            return createdRunTask.Result;
        }
        
        public List<Test> GetTests(int? runId)
        {
            var gettingTestsTask = TestRailApiUtil.Get<List<Test>>(BuildUri("get_tests", runId), authBase64);
            gettingTestsTask.Wait();           
            return gettingTestsTask.Result;
        }

        public List<Status> GetStatuses()
        {
            var gettingStatusesTask = TestRailApiUtil.Get<List<Status>>(BuildUri("get_statuses"),authBase64);
            gettingStatusesTask.Wait();            
            return gettingStatusesTask.Result;
        }
        
        public Result AddResult(ResultCreatingRequest resultCreating, int? testId)
        {
            var createdResultTask = TestRailApiUtil.Post<ResultCreatingRequest, Result>(BuildUri("add_result", testId), authBase64, resultCreating);
            createdResultTask.Wait();            
            return createdResultTask.Result;
        }
        
        public AttachmentResponse AddAttachmentToResult(string filePath, int? resultId)
        {
            var attachmentTask = TestRailApiUtil.UploadImagePost<AttachmentResponse>(BuildUri("add_attachment_to_result", resultId), authBase64, filePath);
            attachmentTask.Wait();            
            return attachmentTask.Result;
        }
    }
}
