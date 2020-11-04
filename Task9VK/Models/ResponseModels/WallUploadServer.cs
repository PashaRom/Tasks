namespace Task9VK.Models.ResponseModels
{
    public class WallUploadServer
    {
        public string UploadUrl { get; set; }
        public int? AlbumId { get; set; }
        public int? UserId { get; set; }
        public static WallUploadServer Convert(Response response)
        {
            WallUploadServer wallUploadServer = new WallUploadServer();
            wallUploadServer.AlbumId = response.AlbumId;
            wallUploadServer.UploadUrl = response.UploadUrl;
            wallUploadServer.UserId = response.UserId;
            return wallUploadServer;
        }
    }
}
