using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using TestRail.Models;
using TestRail.Utilities;
namespace TestRail
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
            var projectTask = TestRailApiUtil.GetAsync<Project>(BuildUri("get_project", id), authBase64);
            projectTask.Wait();
            if (TestRailApiUtil.ResponseStatusCode != HttpStatusCode.OK)
                throw new Exception($"The error occurred while getting the project whith id={id}. Message: {TestRailApiUtil.ResponseError.Error}");
            return projectTask.Result;            
        }

        public Suite AddSuit(string name, string description, int projectId)
        {
            SuiteCreatingRequest suitCreating = new SuiteCreatingRequest { Name = name, Description = description };
            var creartedSuitTask = TestRailApiUtil.PostAsync<SuiteCreatingRequest, Suite>(BuildUri("add_suite", projectId),authBase64, suitCreating);
            creartedSuitTask.Wait();
            if (TestRailApiUtil.ResponseStatusCode != HttpStatusCode.OK)
                throw new Exception($"The error occurred while adding the suit which has input param: name={name}, description={description}, projectId={projectId}. Message: {TestRailApiUtil.ResponseError.Error}");
            return creartedSuitTask.Result;
        }

        public Section AddSection(SectionCreatingRequest sectionCreating, int projectId)
        {
            var createdSectionTask = TestRailApiUtil.PostAsync<SectionCreatingRequest, Section>(BuildUri("add_section", projectId), authBase64, sectionCreating);
            createdSectionTask.Wait();
            if (TestRailApiUtil.ResponseStatusCode != HttpStatusCode.OK)
                throw new Exception($"The error occurred while adding the section which has input param: {sectionCreating}, projectId={projectId}. Message: {TestRailApiUtil.ResponseError.Error}");
            return createdSectionTask.Result;
        }

        public Case AddCase(CaseCreatingRequest caseCreating, int? sectionId)
        {
            var createdCaseTask = TestRailApiUtil.PostAsync<CaseCreatingRequest, Case>(BuildUri("add_case", sectionId), authBase64, caseCreating);
            createdCaseTask.Wait();
            if (TestRailApiUtil.ResponseStatusCode != HttpStatusCode.OK)
                throw new Exception($"The error occurred while adding the case which has input param: {caseCreating}, sectionId={sectionId}. Message: {TestRailApiUtil.ResponseError.Error}");
            return createdCaseTask.Result;
        }

        public Case GetCase(int caseId)
        {
            var getCaseTask = TestRailApiUtil.GetAsync<Case>(BuildUri("get_case", caseId),authBase64);
            getCaseTask.Wait();
            if (TestRailApiUtil.ResponseStatusCode != HttpStatusCode.OK)
                throw new Exception($"The error occurred while getting the case which has input param: caseId={caseId}. Message: {TestRailApiUtil.ResponseError.Error}");
            return getCaseTask.Result;
        }

        public List<Case> GetCases(int projectId)
        {
            var getCasesTask = TestRailApiUtil.GetAsync<List<Case>>(BuildUri("get_cases", projectId), authBase64);
            getCasesTask.Wait();
            if (TestRailApiUtil.ResponseStatusCode != HttpStatusCode.OK)
                throw new Exception($"The error occurred while getting cases which has input param: projectId={projectId}. Message: {TestRailApiUtil.ResponseError.Error}");
            return getCasesTask.Result;
        }
        public Run AddRun(RunCreatingRequest runCreating, int? projectId)
        {
            var createdRunTask = TestRailApiUtil.PostAsync<RunCreatingRequest, Run>(BuildUri("add_run", projectId), authBase64, runCreating);
            createdRunTask.Wait();
            if (TestRailApiUtil.ResponseStatusCode != HttpStatusCode.OK)
                throw new Exception($"The error occurred while adding run which has input param: {runCreating}, projectId={projectId}. Message: {TestRailApiUtil.ResponseError.Error}");
            return createdRunTask.Result;
        }
        
        public List<Test> GetTests(int? runId)
        {
            var gettingTestsTask = TestRailApiUtil.GetAsync<List<Test>>(BuildUri("get_tests", runId), authBase64);
            gettingTestsTask.Wait();
            if (TestRailApiUtil.ResponseStatusCode != HttpStatusCode.OK)
                throw new Exception($"The error occurred while getting test which has input param: runId={runId}. Message: {TestRailApiUtil.ResponseError.Error}");
            return gettingTestsTask.Result;
        }

        public List<Status> GetStatuses()
        {
            var gettingStatusesTask = TestRailApiUtil.GetAsync<List<Status>>(BuildUri("get_statuses"),authBase64);
            gettingStatusesTask.Wait();
            if (TestRailApiUtil.ResponseStatusCode != HttpStatusCode.OK)
                throw new Exception($"The error occurred while getting statuses. Message: {TestRailApiUtil.ResponseError.Error}");
            return gettingStatusesTask.Result;
        }
        
        public Result AddResult(ResultCreatingRequest resultCreating, int? testId)
        {
            var createdResultTask = TestRailApiUtil.PostAsync<ResultCreatingRequest, Result>(BuildUri("add_result", testId), authBase64, resultCreating);
            createdResultTask.Wait();
            if (TestRailApiUtil.ResponseStatusCode != HttpStatusCode.OK)
                throw new Exception($"The error occurred while adding result which has input param: {resultCreating}, testId={testId}. Message: {TestRailApiUtil.ResponseError.Error}");
            return createdResultTask.Result;
        }
        
        public AttachmentResponse AddAttachmentToResult(string filePath, int? resultId)
        {
            var attachmentTask = TestRailApiUtil.UploadImagePostAsync<AttachmentResponse>(BuildUri("add_attachment_to_result", resultId), authBase64, filePath);
            attachmentTask.Wait();
            if (TestRailApiUtil.ResponseStatusCode != HttpStatusCode.OK)
                throw new Exception($"The error occurred while adding attachment file to result which has input param: filePath={filePath}, resultId={resultId}. Message: {TestRailApiUtil.ResponseError.Error}");
            return attachmentTask.Result;
        }

        public User GetUser(string email)
        {
            var getUserTask = TestRailApiUtil.GetAsync<User>(BuildUri($"get_user_by_email&email={email}"),authBase64);
            getUserTask.Wait();
            if (TestRailApiUtil.ResponseStatusCode != HttpStatusCode.OK)
                throw new Exception($"The error occurred while getting user which has input param: email={email}. Message: {TestRailApiUtil.ResponseError.Error}");
            return getUserTask.Result;
        }

        public Suite GetSuite(int? id)
        {
            var getSuiteTask = TestRailApiUtil.GetAsync<Suite>(BuildUri("get_suite", id), authBase64);
            getSuiteTask.Wait();
            if (TestRailApiUtil.ResponseStatusCode != HttpStatusCode.OK)
                throw new Exception($"The error occurred while getting suite which has input param: suiteId={id}. Message: {TestRailApiUtil.ResponseError.Error}");
            return getSuiteTask.Result;
        }
    }
}
