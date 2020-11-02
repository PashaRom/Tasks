using System;
using OpenQA.Selenium;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
namespace Task10.Testing.PageObject
{
    public class JavaScriptAlertsPage
    {       
        public IButton jsAlertButton => AqualityServices.Get<IElementFactory>().GetButton(By.XPath(".//div[@id='content']/div/ul/li/button[text()[contains(.,'Alert')]]"), "Click for JS Alert");
        public IButton jsConfirmButton => AqualityServices.Get<IElementFactory>().GetButton(By.XPath(".//div[@id='content']/div/ul/li/button[text()[contains(.,'Confirm')]]"), "Click for JS Confirm");
        public IButton jsPromptButton => AqualityServices.Get<IElementFactory>().GetButton(By.XPath(".//div[@id='content']/div/ul/li/button[text()[contains(.,'Prompt')]]"), "Click for JS Prompt");        
        public IAlert ModalWindow;
        public void SwitchToModalWindow()
        {
            try
            {           
                ModalWindow = AqualityServices
                    .ConditionalWait
                    .WaitFor<IAlert>(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());                
            }
            catch(WebDriverTimeoutException)
            {
                AqualityServices
                    .Logger
                    .Info("The modal info was not present.");
                ModalWindow = null;
            }
            catch (Exception ex)
            {
                AqualityServices
                    .Logger
                    .Fatal("The error appeared during switch to modal window.", ex);                
            }
        }
        
        public string GetTextResult()
        {
            return AqualityServices
                .Get<IElementFactory>()
                .GetLabel(By.Id("result"), "Text result").Text;
        }
    }
}
