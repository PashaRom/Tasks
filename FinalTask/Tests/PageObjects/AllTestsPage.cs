using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using FinalTask.Tests.Models;
using OpenQA.Selenium;
namespace FinalTask.Tests.PageObjects
{
    public class AllTestsPage
    {
        private ReadOnlyCollection<IWebElement> ListOfTestsPage => AqualityServices.Browser.Driver.FindElementsByXPath(".//table[@class='table']/tbody/tr");
        public List<Test> ListOfTests => GetTests();

        private IButton AddTestButton => AqualityServices.Get<IElementFactory>().GetButton(By.XPath("//button[contains(@class,'btn-primary') and contains(@class,'pull-right')]"), "Add test button");
        private ILink Link(By locator, string name) => AqualityServices.Get<IElementFactory>().GetLink(locator, name);
        public void ClickAddTestButton() => AddTestButton.ClickAndWait();
        private const string addedTestLocator = "//table[@id='allTests']//a[text()[contains(.,'{0}')]]";
        
        public bool IsAddedTestInTestsList(string testName) => AqualityServices.ConditionalWait.WaitFor(()=> Link(By.XPath(String.Format(addedTestLocator, testName)), testName).State.IsExist, TimeSpan.FromSeconds(10));
       
        public void ClickAddedTestInTestsList(string testName) => Link(By.XPath(String.Format(addedTestLocator, testName)), testName).Click();
        private List<Test> GetTests() 
        {
            AqualityServices.Logger.Info("Get the list of test has been loaded on the allTests page.");
            if (AqualityServices.ConditionalWait.WaitFor((driver)=> driver.FindElement(By.XPath(".//table[@class='table']/tbody/tr/td/a")),TimeSpan.FromSeconds(240)) != null)
                AqualityServices.Logger.Info("The list of test has been loaded on the allTests page.");
            List<Test> listOfTest = new List<Test>();
            int firstStep = 0; 
            try 
            { 
                foreach(var test in ListOfTestsPage)
                {
                    if(firstStep != 1)
                    {
                        firstStep++;
                        continue;
                    }                    
                    ReadOnlyCollection<IWebElement> fieldsOfTest = test.FindElements(By.TagName("td"));
                    Test additionTest = new Test();
                    for (int i = 0; i < fieldsOfTest.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                additionTest.Name = fieldsOfTest[i].FindElement(By.TagName("a")).Text;
                                break;
                            case 1:
                                additionTest.Method = fieldsOfTest[i].Text;
                                break;
                            case 2:
                                additionTest.Status = fieldsOfTest[i].FindElement(By.TagName("span")).Text;
                                break;
                            case 3:
                                additionTest.StartTime = fieldsOfTest[i].Text;
                                break;
                            case 4:
                                additionTest.EndTime = String.IsNullOrEmpty(fieldsOfTest[i].Text) ? null : fieldsOfTest[i].Text;
                                break;
                            case 5:
                                additionTest.Duration = fieldsOfTest[i].Text;
                                break;
                        }
                    }
                    listOfTest.Add(additionTest);
                }
                return listOfTest;
            }
            catch(Exception ex)
            {
                AqualityServices.Logger.Error($"The error occurred while getting list of test from the all tests page. Message: {ex.Message}");
                return listOfTest;
            }
        }        
    }
}
