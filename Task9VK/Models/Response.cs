using System.Text.Json.Serialization;
using System.Collections.Generic;
using Task9VK.Models.ResponseModels;
namespace Task9VK.Models
{
    public class Response
    {
        [JsonPropertyName("post_id")]
        public int PostId { get; set; }
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }
        [JsonPropertyName("upload_url")]
        public string UploadUrl { get; set; }
        [JsonPropertyName("album_id")]
        public int? AlbumId { get; set; }
        [JsonPropertyName("user_id")]
        public int? UserId { get; set; }
        [JsonPropertyName("server")]
        public int? Server { get; set; }
        [JsonPropertyName("photo")]
        public string Photo { get; set; }
        [JsonPropertyName("hash")]
        public string Hash { get; set; }
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("owner_id")]
        public int? OwnerId { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("date")]
        public int? Date { get; set; }
        [JsonPropertyName("width")]
        public int? Width { get; set; }
        [JsonPropertyName("height")]
        public int? Height { get; set; }
        [JsonPropertyName("sizes")]
        public List<Size> Sizes { get; set; }
        
        [JsonPropertyName("comment_id")]
        public int? CommentId { get; set; }
        [JsonPropertyName("liked")]
        public int? Liked { get; set; }
        [JsonPropertyName("copied")]
        public int? Copied { get; set; }
    }
}
