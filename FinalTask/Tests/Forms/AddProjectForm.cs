using OpenQA.Selenium;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
namespace FinalTask.Tests.Forms
{
    public class AddProjectForm : Form
    {
        private ITextBox ProjectNameTextBox => Form.ElementFactory.GetTextBox(By.Id("projectName"), "Project name");
        private IButton SaveProjectButton => Form.ElementFactory.GetButton(By.XPath("//button[contains(@class,'btn-primary')]"), "Save project");
        private ILabel Label(By locator, string name) => Form.ElementFactory.GetLabel(locator, name);
        public AddProjectForm(By locator, string name) : base(locator, name)
        {
        }

        public void SendKeysToProjectNameTextBox(string projectName) => ProjectNameTextBox.SendKeys(projectName);

        public void ClickSaveProjectButton() => SaveProjectButton.ClickAndWait();

        public bool IsSavedNewProject() => Label(By.XPath("//div[contains(@class,'alert-success')]"), "Message of saving result").State.IsDisplayed;
    }
}
