using System.Collections.Generic;
using System.Text.Json.Serialization;
using Task9VK.Models.ResponseModels;
namespace Task9VK.Models
{
    public class VkResponsePhotoList
    {
        [JsonPropertyName("response")]
        public List<Photo> Photos { get; set; }
    }
}
