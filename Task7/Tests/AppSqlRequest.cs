using System;
using System.Linq;
using System.Collections.Generic;
using Task7.Tests.Models;
using Task7.Context.AppDbContext;
using Task7.Utilities;
using Task7.Utilities.Logging;
namespace Task7.Tests
{
    public class AppSqlRequest
    {
        private static AppDbContext context = new AppDbContext();
        public static bool GetMinWorkingTimeTest()
        {
            try { 
                var workingTimes = context.Projects.Join(context.Tests,
                    project => project.Id,
                    test => test.ProjectId,
                    (project, test) => new WorkingTime
                    {
                        Project = project.Name,
                        Test = test.Name,
                        MinimumWorkingTime = (test.EndTime.HasValue && test.StartTime.HasValue) ? (test.EndTime - test.StartTime).Value.TotalSeconds : -1
                    })
                    .ToList()
                    .Where(workingTime => workingTime.MinimumWorkingTime != -1)
                    .OrderBy(workingTime => workingTime.Project)
                    .ThenBy(workingTime => workingTime.Test);           
                ExcelUtil.Write<WorkingTime>(workingTimes, 1, "Minimum working test's time");
                if (workingTimes.Count() > 0)
                    return true;
                else
                    return false;
            }
            catch(Exception ex)
            {
                Log.Error(ex, "Unexpected error occurred during executing AppSqlRequest.GetMinWorkingTimeTest().");
                return false;
            }
        }

        public static bool GetNumberOfTestsInProject()
        {
            try { 
                var countTests = context.Projects.Join(context.Tests,
                    project => project.Id,
                    test => test.ProjectId,
                    (project, test) => new
                    {
                        Project = project.Name 
                     })
                    .GroupBy(project => project.Project)
                    .Select(group => new NumberOfTestsInProject
                    {
                        Project = group.Key,
                        TestsCount = group.Count()
                    })
                    .OrderBy(numberOfTestsInProject => numberOfTestsInProject.TestsCount);
                ExcelUtil.Write<NumberOfTestsInProject>(countTests, 2, "Number of test in project");
                if (countTests.Count() > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error occurred during executing AppSqlRequest.NumberOfTestsInProject().");
                return false;
            }
        }

        public static bool GetExecutedTestsAfterParticularDate(string dateFrom)
        {
            try { 
                DateTime executedDate = DateTime.Parse(dateFrom);
                var testsInProjects = context.Projects.Join(context.Tests,
                    project => project.Id,
                    test => test.ProjectId,
                    (project, test) => new TestsInProject
                    {
                        Project = project.Name,
                        Test = test.Name,
                        Date = test.EndTime.HasValue ? test.EndTime : DateTime.MinValue
                    })
                    .Where(test => test.Date.Value.Date > executedDate.Date)                    
                    .OrderBy(testProject => testProject.Project)
                    .ThenBy(testProject => testProject.Test);
                ExcelUtil.Write<TestsInProject>(testsInProjects, 3, "Tests in project");
                if (testsInProjects.Count() > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error occurred during executing AppSqlRequest.TestExecutedAfterParticularDate.");
                return false;
            }
        }

        public static bool GetNumberOfTestExecutedParticularBrowsers(List<string> browsersName)
        {
            try { 
                var testExecuteBrowser = context.Projects.Join(context.Tests,
                    project => project.Id,
                    test => test.ProjectId,
                    (project, test) => new
                    {
                        Name = test.Browser
                    })
                    .Where(browser => browsersName.Contains(browser.Name))
                    .GroupBy(browser => browser.Name)
                    .Select(group => new NumberOfBrowsers
                    {
                        Browser = group.Count()
                    });                
                ExcelUtil.Write<NumberOfBrowsers>(testExecuteBrowser, 4, "Number of browsers");
                if (testExecuteBrowser.Count() > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error occurred during executing AppSqlRequest.TestExecutedAfterParticularDate.");
                return false;
            }
        }
    }
}
