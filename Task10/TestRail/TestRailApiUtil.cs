using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
namespace Task10.Utilities
{
    public static class TestRailApiUtil
    {   
        public static async Task<T> Get<T>(string uri, string authBase64) 
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.Method = HttpMethod.Get.Method;
            webRequest.ContentType = "application/json";
            webRequest.Headers["Authorization"] = $"Basic {authBase64}";
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            return await JsonSerializer.DeserializeAsync<T>(webResponse.GetResponseStream());
        }

        public static async Task<K> Post<T,K>(string uri, string authBase64, T createObj)
        {
            string responsBody = JsonSerializer.Serialize(createObj);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.Method = HttpMethod.Post.Method;
            webRequest.ContentType = "application/json";
            webRequest.Headers["Authorization"] = $"Basic {authBase64}";
            using (StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                streamWriter.Write(responsBody);                
            }
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            return await JsonSerializer.DeserializeAsync<K>(webResponse.GetResponseStream());
        }

        public static async Task<T> UploadImagePost<T>(string uri, string authBase64, string filePath)
        {            
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authBase64);
            MultipartFormDataContent multipartFormData = new MultipartFormDataContent();
            FileStream fileStream = File.OpenRead(filePath);
            var streamContent = new StreamContent(fileStream);
            var imageContent = new ByteArrayContent(streamContent.ReadAsByteArrayAsync().Result);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            multipartFormData.Add(imageContent, "attachment", Path.GetFileName(filePath));
            HttpResponseMessage response = await httpClient.PostAsync(uri, multipartFormData);
            return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync());
        }
    }
}
