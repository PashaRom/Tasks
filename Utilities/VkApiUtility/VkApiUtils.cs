using System;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Aquality.Selenium.Browsers;
using Utilities.VkApiUtility.Models;
namespace Utilities.VkApiUtility
{
    public static class VkApiUtils
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static HttpStatusCode statusCode = 0;
        public static HttpStatusCode StatusCode => statusCode;
        private static string mediaType = String.Empty;
        public static string MediaType => mediaType;
        private static long? contentLenght = 0;
        public static long? ContentLenght => contentLenght;
        public static bool IsNullResponseError => VkResponseError.Error == null;
        public static VkResponseError VkResponseError = new VkResponseError();

        public static void Initialization(string uri)
        {
            httpClient.BaseAddress = new Uri(uri);
            httpClient.DefaultRequestHeaders.Accept.Clear();            
            httpClient.DefaultRequestHeaders.Add("charset", "utf-8");            
        }
        public static async Task<T> GetTAsync<T>(string urn)
            where T : class            
        {            
            try
            {                
                HttpResponseMessage streamTask = await httpClient.GetAsync(urn);
                statusCode = streamTask.StatusCode;
                mediaType = streamTask.Content.Headers.ContentType.MediaType;
                contentLenght = streamTask.Content.Headers.ContentLength;                
                VkResponseError.Error = null;
                VkResponseError = JsonSerializer.Deserialize<VkResponseError>(streamTask.Content.ReadAsStringAsync().Result);                
                return await JsonSerializer.DeserializeAsync<T>(await streamTask.Content.ReadAsStreamAsync());
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Fatal($"Unexpected error occurred during getting data{typeof(T)} on urn: \"{urn}\".", ex);
                return null;
            }
        }
        public static async Task<T> PostImage<T>(string url, string filePath, string mediaType)
        {            
            MultipartFormDataContent multipartFormData = new MultipartFormDataContent();
            FileStream fileStream = File.OpenRead(filePath);
            var streamContent = new StreamContent(fileStream);
            var imageContent = new ByteArrayContent(streamContent.ReadAsByteArrayAsync().Result);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(mediaType);
            multipartFormData.Add(imageContent, "photo", Path.GetFileName(filePath));
            HttpResponseMessage response = await httpClient.PostAsync(url, multipartFormData);
            statusCode = response.StatusCode;
            mediaType = response.Content.Headers.ContentType.MediaType;            
            VkResponseError.Error = null;
            var vkResponseErrorStream = await response.Content.ReadAsStreamAsync();
            var VkResponseErrorDesTask = JsonSerializer.DeserializeAsync<VkResponseError>(vkResponseErrorStream);
            VkResponseError = VkResponseErrorDesTask.Result;
            vkResponseErrorStream.Position = 0;
            return await JsonSerializer.DeserializeAsync<T>(vkResponseErrorStream);//await response.Content.ReadAsStreamAsync()
        }
    }
}
