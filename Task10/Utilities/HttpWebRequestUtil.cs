using System;
using System.Text;
using System.Net;
using Aquality.Selenium.Browsers;
using System.Net.Http;
using System.IO;
namespace Task10.Utilities
{
    public static class HttpWebRequestUtil
    {
        public static (Stream webResponseStream, HttpStatusCode responseStatusCode, long responseContentLeinght) BasicAuthorization(string url, string login, string password)
        {
            try
            {
                string authBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{login}:{password}"));
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = HttpMethod.Get.Method;
                webRequest.ContentType = "application/json; charset=utf-8";
                webRequest.Headers["Authorization"] = $"Basic {authBase64}";
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();                
                return (webResponse.GetResponseStream(), webResponse.StatusCode, webResponse.ContentLength);
            }
            catch(Exception ex)
            {
                AqualityServices.Logger.Fatal("The error appeared during basic authorization.", ex);
                return (null, 0, 0);
            }
        }
    }
}
