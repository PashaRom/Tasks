using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Aquality.Selenium.Browsers;
using Test.Configuration;
using Task10.Testing.Models.Cookies;
namespace Task10.Testing.App
{
    public static class AppWorkingCookies
    {
        public static List<AppCookie> AppCookies { get; set; } = ConfigurationManager.TestingData.GetSectionWithArray<AppCookie>("cookies");
        public static void SetCookies()
        {
            foreach (AppCookie appCookie in AppCookies)
                AqualityServices.Browser.Driver.Manage().Cookies.AddCookie(new OpenQA.Selenium.Cookie(appCookie.Name, appCookie.Value));
        }

        public static List<AppCookie> GetCookies ()
        {
            IEnumerable<Cookie> gettingCookies = AqualityServices.Browser.Driver.Manage().Cookies.AllCookies;
            List<AppCookie> cookies = new List<AppCookie>();
            foreach (var gettingCokie in gettingCookies)
                cookies.Add((AppCookie)gettingCokie);
            cookies.Reverse();
            return cookies;
        }

        public static void DeleteCookieNamed()
        {
            AppCookie appCookie = AppCookies.FirstOrDefault(deleteAppCookie => deleteAppCookie.Name.Equals(ConfigurationManager.TestingData.Get<string>("deleteCookie")));
            IEnumerable<Cookie> gettingCookies = AqualityServices.Browser.Driver.Manage().Cookies.AllCookies;
            Cookie deleteCookie = gettingCookies.FirstOrDefault(delCookie => delCookie.Name.Equals(appCookie.Name));
            AppCookies.RemoveAll(delAppCookie => delAppCookie.Name.Equals(appCookie.Name));
            AqualityServices.Browser.Driver.Manage().Cookies.DeleteCookie(deleteCookie);
        }

        public static (string ExpetedValue, string ActualValue) AddValueCookie()
        {
            AppCookie addValueCookie = ConfigurationManager.TestingData.GetObjectParam<AppCookie>("addValueCookie");
            AppCookie changedCookie = AppCookies.FirstOrDefault(appCookie => appCookie.Name.Equals(addValueCookie.Name));
            changedCookie.Value = $"{changedCookie.Value}{addValueCookie.Value}";            
            AqualityServices.Browser.Driver.Manage().Cookies.DeleteCookieNamed(changedCookie.Name);
            AqualityServices.Browser.Driver.Manage().Cookies.AddCookie(new Cookie(changedCookie.Name,changedCookie.Value));
            return (changedCookie.Value, AqualityServices.Browser.Driver.Manage().Cookies.GetCookieNamed(changedCookie.Name).Value);
        }

        public static bool DeleteCookies()
        {
            AqualityServices.Browser.Driver.Manage().Cookies.DeleteAllCookies();
            int countCookies = AqualityServices.Browser.Driver.Manage().Cookies.AllCookies.Count();
            if (countCookies == 0)
                return true;
            else
                return false;
        }
    }
}
