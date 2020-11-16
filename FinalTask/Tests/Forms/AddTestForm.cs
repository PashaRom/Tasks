using System;
using OpenQA.Selenium;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using FinalTask.Tests.Models;
using Utilities;
namespace FinalTask.Tests.Forms
{
    public class AddTestForm : Form
    {
        private ITextBox TestNameTextBox = Form.ElementFactory.GetTextBox(By.Id("testName"), "Test name");
        private IComboBox StatusComboBox = Form.ElementFactory.GetComboBox(By.Id("testStatus"), "Status");
        private ITextBox TestMethodTextBox = Form.ElementFactory.GetTextBox(By.Id("testMethod"), "Test method");
        private ITextBox StartTimeTextBox = Form.ElementFactory.GetTextBox(By.Id("startTime"), "Start time");
        private ITextBox EndTimeTextBox = Form.ElementFactory.GetTextBox(By.Id("endTime"), "End time");
        private ITextBox EnvironmetTextBox = Form.ElementFactory.GetTextBox(By.Id("environment"), "Environment");
        private ITextBox BrowserTextBox = Form.ElementFactory.GetTextBox(By.Id("browser"), "Browser");
        private ITextBox LogTextBox = Form.ElementFactory.GetTextBox(By.Id("log"), "Log");
        private IButton SelectFileButton = Form.ElementFactory.GetButton(By.Id("attachment"), "Attachment");
        private IButton SaveTestButton = Form.ElementFactory.GetButton(By.XPath("//form[@id='addTestForm']/button[contains(@class,'btn-primary')]"),"Save test");
        private ILabel SuccessLabel = Form.ElementFactory.GetLabel(By.Id("success"), "Test saved");        
        public AddTestForm(By locator, string name) : base(locator, name)
        {
        }

        public bool IsDisplayedSuccessLabel() => SuccessLabel.GetCssValue("display").Equals("block");  
        
        public string AddNewTest(UIAddedTest uiAddedTest, string outDirectory, string screenShotFileName)
        {            
            try
            {
                TestNameTextBox.SendKeys(uiAddedTest.Name);                
                StatusComboBox.SelectByText(uiAddedTest.Status.ToUpper().Trim());                
                TestMethodTextBox.SendKeys(uiAddedTest.Method);                
                StartTimeTextBox.SendKeys(uiAddedTest.StartTime);               
                EndTimeTextBox.SendKeys(uiAddedTest.EndTime);               
                EnvironmetTextBox.SendKeys(uiAddedTest.Environment);                
                BrowserTextBox.SendKeys(uiAddedTest.Browser);                
                LogTextBox.SendKeys(uiAddedTest.Log);
                string pathScreenShot = FileUtil.TakeScreenshot(outDirectory, screenShotFileName);                
                SelectFileButton.SendKeys(pathScreenShot);
                SaveTestButton.ClickAndWait();                
                return pathScreenShot;
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Error($"The error occurred while added new test on the added test form. Message: {ex.Message}");
                return String.Empty;
            }
        }
    }
}
