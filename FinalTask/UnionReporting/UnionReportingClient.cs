using System;
using System.Net;
using System.Text;
namespace UnionReporting
{
    public class UnionReportingClient
    {
        private readonly string url;
        private readonly string authBase64;
        public UnionReportingClient(string url, string login, string password)
        {
            this.url = url;
            this.authBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{login}:{password}"));
        }

        public string GetToken(int variant)
        {
            string urn = "/token/get";
            var token = UnionReportingUtils.Post(BuildUrl(urn), authBase64, $"variant={variant}");
            if (UnionReportingUtils.ResponseStatusCode != HttpStatusCode.OK)
                throw new Exception($"The post response \"{urn}\" returned status code {Convert.ToInt32(UnionReportingUtils.ResponseStatusCode)} and had the body {UnionReportingUtils.ResponsTextError}");
            return token;
        }

        public string GetTests(int projectId, string format)
        {
            string urn = $"/test/get/{format}";
            var tests = UnionReportingUtils.Post(BuildUrl(urn), authBase64, $"projectId={projectId}");
            if (UnionReportingUtils.ResponseStatusCode != HttpStatusCode.OK)
                throw new Exception($"The post response \"{urn}\" returned status code {Convert.ToInt32(UnionReportingUtils.ResponseStatusCode)} and had the body {UnionReportingUtils.ResponsTextError}");            
            return tests;
        }

        private string BuildUrl(string urn)
        {
            return $"{url}{urn}";
        }
    }
}
