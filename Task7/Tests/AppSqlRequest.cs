using System;
using System.Linq;
using System.Collections.Generic;
using Task7.Tests.Models;
using Task7.Context.AppDbContext;
using Task7.Utilities;
using Task7.Utilities.Logging;
using Task7.Utilities.Configuration;
namespace Task7.Tests
{
    public class AppSqlRequest
    {       
        public static List<WorkingTime> GetMinWorkingTimeTest()
        {
            AppDbContext context = new AppDbContext();
            try 
            { 
                var workingTimes = context.Projects.Join(context.Tests,
                    project => project.Id,
                    test => test.ProjectId,
                    (project, test) => new WorkingTime
                    {
                        Project = project.Name,
                        Test = test.Name,
                        MinimumWorkingTime = (test.EndTime.HasValue && test.StartTime.HasValue) ? (test.EndTime - test.StartTime).Value.TotalSeconds.ToString() : ""
                    })
                    .ToList()
                    .Where(workingTime => !workingTime.MinimumWorkingTime.Equals(""))
                    .OrderBy(workingTime => workingTime.Project)
                    .ThenBy(workingTime => workingTime.Test)
                    .ToList();                
                return workingTimes;
            }
            catch(Exception ex)
            {
                Log.Error(ex, "Unexpected error occurred during executing AppSqlRequest.GetMinWorkingTimeTest().");
                return null;
            }
        }

        public static List<NumberOfTestsInProject> GetNumberOfTestsInProject()
        {
            AppDbContext context = new AppDbContext();
            try 
            { 
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
                    .OrderBy(numberOfTestsInProject => numberOfTestsInProject.TestsCount)
                    .ToList();                
                return countTests;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error occurred during executing AppSqlRequest.NumberOfTestsInProject().");
                return null;
            }
        }

        public static List<TestsInProject> GetExecutedTestsAfterParticularDate(string dateFrom)
        {
            AppDbContext context = new AppDbContext();
            try 
            { 
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
                    .ThenBy(testProject => testProject.Test)
                    .ToList();                
                return testsInProjects;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error occurred during executing AppSqlRequest.TestExecutedAfterParticularDate.");
                return null;
            }
        }

        public static List<NumberOfBrowsers> GetNumberOfTestExecutedParticularBrowsers(List<string> browsersName)
        {
            AppDbContext context = new AppDbContext();            
            try 
            {
                List<NumberOfBrowsers> testExecuteBrowser = context.Projects.Join(context.Tests,
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
                    })
                    .ToList();
                return testExecuteBrowser;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error occurred during executing AppSqlRequest.TestExecutedAfterParticularDate.");
                return null;
            }
        }

        public static void RespondWriteToFiles()
        {
            ExcelUtil.Write<WorkingTime>(GetMinWorkingTimeTest(), 1, "Minimum working test's time");            
            ExcelUtil.Write<NumberOfTestsInProject>(GetNumberOfTestsInProject(), 2, "Number of test in project");            
            ExcelUtil.Write<TestsInProject>(GetExecutedTestsAfterParticularDate(ConfigurationManager.TestData.Get<string>("dateFrom")), 3, "Tests in project");            
            ExcelUtil.Write<NumberOfBrowsers>(GetNumberOfTestExecutedParticularBrowsers(ConfigurationManager.TestData.GetSectionWithArray<string>("browsers")), 4, "Number of browsers");            
        }
    }
}
