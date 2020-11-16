using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.IO;
namespace UnionReporting
{
    public static class UnionReportingUtils
    {
        public static HttpStatusCode ResponseStatusCode { get; set; } 
        public static long ResponseContentLenght { get; set; }
        public static string ResponsTextError { get; set; }
        public static string Post(string uri, string authBase64, string param)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(param);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.Method = HttpMethod.Post.Method;
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = byteArray.Length;
            webRequest.Headers["Authorization"] = $"Basic {authBase64}";
            Stream stream = webRequest.GetRequestStream();            
            stream.Write(byteArray,0, byteArray.Length);
            stream.Close();
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            ResponseStatusCode = webResponse.StatusCode;
            ResponseContentLenght = webResponse.ContentLength;           
            string responseFromServer;
            using (stream = webResponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream);                
                responseFromServer = reader.ReadToEnd();                
            }                     
            ResponsTextError = webResponse.StatusCode != HttpStatusCode.OK ? responseFromServer : String.Empty;
            webResponse.Close();
            return responseFromServer;
        }        
    }
}
