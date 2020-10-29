using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Aquality.Selenium.Browsers;
using Test.Configuration;
using Task10.Utilities;
using Task10.Testing.Models.BasicAuth;
namespace Task10.Testing.App
{
    public static class AppWebRequest
    {
        public static async Task<AuthResponse> BasicAutorization()
        {
            AuthResponse authResponse = new AuthResponse();
            try
            {
                (Stream Stream, HttpStatusCode StatusCode, long ContentLenght) response = HttpWebRequestUtil.BasicAuthorization(
                    ConfigurationManager.Configuration.Get<string>("basicAuth:url"),
                    ConfigurationManager.Configuration.Get<string>("basicAuth:cred:login"),
                    ConfigurationManager.Configuration.Get<string>("basicAuth:cred:password")
                    );
                authResponse = await JsonSerializer.DeserializeAsync<AuthResponse>(response.Stream);
                return authResponse;

            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Fatal("The error appeared during basic authorization.", ex);
                return authResponse;
            }
        }
    }
}
