using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Task9VK.Models.ResponseModels
{
    public class Photo
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("album_id")]
        public int? AlbumId { get; set; }
        [JsonPropertyName("owner_id")]
        public int? OwnerId { get; set; }
        [JsonPropertyName("user_id")]
        public int? UserId { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("date")]
        public int? Date { get; set; }
        [JsonPropertyName("sizes")]
        public List<Size> Sizes { get; set; }
        [JsonPropertyName("width")]
        public int? Width { get; set; }
        [JsonPropertyName("height")]
        public int? Height { get; set; }

        public static Photo Convert(Response response) 
        {
            Photo photo = new Photo();
            photo.Id = response.Id;
            photo.AlbumId = response.AlbumId;
            photo.OwnerId = response.OwnerId;
            photo.UserId = response.UserId;
            photo.Text = response.Text;
            photo.Date = response.Date;
            photo.Sizes = response.Sizes;
            photo.Width = response.Width;
            photo.Height = response.Height;
            return photo;
        }
    }
}
