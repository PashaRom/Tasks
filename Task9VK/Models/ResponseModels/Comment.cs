using System.Text.Json.Serialization;
namespace Task9VK.Models.ResponseModels
{
    public class Comment
    {
        [JsonPropertyName("comment_id")]
        public int? Id { get; set; }
        [JsonIgnore]
        public string Message { get; set; }

        public static Comment Convert(Response response)
        {
            Comment comment = new Comment();
            comment.Id = response.CommentId;
            return comment;
        }
    }
}
