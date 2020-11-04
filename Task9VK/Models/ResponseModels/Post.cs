using System.Text.Json.Serialization;
using System.Text;
namespace Task9VK.Models.ResponseModels
{
    public class Post
    {
        [JsonPropertyName("post_id")]
        public int? Id { get; set; }

        [JsonIgnore]
        public string Message { get; set; }

        public static Post Convert(Response response)
        {
            Post post = new Post();
            post.Id = response.PostId;
            return post;
        }
        public override bool Equals(object obj)
        {
            Post post = obj as Post;
            if (post.Id == this.Id && post.Message.Equals(this.Message))
                return true;
            else
                return false;
        }
        public override string ToString()
        {
            StringBuilder postStringBuilder = new StringBuilder();
            postStringBuilder.Append("Post{");
            postStringBuilder.Append($"Id={this.Id}, ");
            postStringBuilder.Append($"Message={this.Message}");
            postStringBuilder.Append("}");
            return postStringBuilder.ToString();
        }
    }
}
