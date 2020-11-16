using OpenQA.Selenium;
using FinalTask.Tests.Forms;
namespace FinalTask.Tests.PageObjects
{
    public class AddProjectPage
    {
        public string Name => "Add project page";

        private AddProjectForm addProjectForm = new AddProjectForm(By.Id("addProjectForm"), "Add progect");
        
        public bool IsLoadedPage => addProjectForm.State.IsDisplayed;

        public bool CreateNewProject(string projectName)
        {
            addProjectForm.SendKeysToProjectNameTextBox(projectName);
            addProjectForm.ClickSaveProjectButton();
            return addProjectForm.IsSavedNewProject();
        }        
    }
}
