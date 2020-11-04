using System.Text.Json.Serialization;
namespace Task9VK.Models.ResponseModels
{
    public class IsLiked
    {
        [JsonPropertyName("liked")]
        public int? Liked { get; set; }
        [JsonPropertyName("copied")]
        public int? Copied { get; set; }

        public static IsLiked Convert(Response response)
        {
            IsLiked like = new IsLiked();
            like.Copied = response.Copied;
            like.Liked = response.Liked;
            return like;
        }
    }
}
