using System;
using System.Collections.Generic;
using Aquality.Selenium.Browsers;
using UnionReporting;
using Configuration;
using OpenQA.Selenium;
using FinalTask.Tests.Models;
using NUnit.Framework.Internal;
using Utilities;
namespace FinalTask.Tests.App
{
    public static class AppUnionReporting
    {
        private static UnionReportingClient unionReportingApiClient; 
        public static int ProjectId { get; set; }
        static AppUnionReporting()
        {
            try
            {
                unionReportingApiClient = new UnionReportingClient(
                ConfigurationManager.Configuration.Get<string>("unionReporting:apiUrl"),
                ConfigurationManager.CredOfUser.Get<string>("cred:unionReporting:login"),
                ConfigurationManager.CredOfUser.Get<string>("cred:unionReporting:password"));
            }
            catch(Exception ex)
            {
                AqualityServices.Logger.Error($"The error occurred while creating UnionReportingClient. Message: \"{ex.Message}\".");
            }
        }
        public static string GetToken()
        {
            try
            {
                AqualityServices.Logger.Info("Get token.");
                return unionReportingApiClient.GetToken(ConfigurationManager.TestingData.Get<int>("unionReporting:variant"));
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Error($"The error occurred while getting token. Message: \"{ex.Message}\".");
                return null;
            }
        }

        public static void SetCookie(string paramName, string paramValue)
        {
            try 
            {
                AqualityServices.Logger.Info($"Set the cookie name={paramName} value={paramValue}.");
                AqualityServices.Browser.Driver.Manage().Cookies.AddCookie(new Cookie(paramName, paramValue));
            }
            catch(Exception ex)
            {
                AqualityServices.Logger.Error($"The error occurred while setting cookie with name={paramName} value={paramValue}. Message: \"{ex.Message}\".");
            }
        }

        public static (List<Models.Test> Tests, string ErrorMessage) GetTestsList()
        {
            try
            {               
                return (Utilities
                    .StringUtil.
                    ConvertXmlStringToObject<List<Models.Test>>(
                    unionReportingApiClient
                    .GetTests(
                        ProjectId, 
                        ConfigurationManager.TestingData.Get<string>("unionReporting:formatResponseGetTests")),
                    "tests"),String.Empty);
            }
            catch(Exception ex)
            {
                AqualityServices.Logger.Error($"The error occurred while getting the tests list in the project id={ProjectId}. Message: \"{ex.Message}\".");
                return (new List<Models.Test>(), ex.Message);
            }
        }
    }
}
