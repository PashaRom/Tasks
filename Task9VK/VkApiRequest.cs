using System;
using System.Linq;
using Utilities;
using Utilities.VkApiUtility;
using Test.Configuration;
using Task9VK.Models;
using Task9VK.Models.ResponseModels;
using Aquality.Selenium.Browsers;
using System.IO;
using Polly;
using Polly.Retry;
namespace Task9VK
{
    public static class VkApiRequest
    {       
        private static string RerequiredParam => $"&access_token={ConfigurationManager.Configuration.Get<string>("autorizationData:token")}" +
            $"&v={ConfigurationManager.Configuration.Get<string>("versionVkApi")}";
        static VkApiRequest()
        {
            VkApiUtils.Initialization(ConfigurationManager.Configuration.Get<string>("apiUrl"));
        }

        public static Post CreatePost()
        {
            Post createdPost = new Post();
            try 
            { 
                createdPost.Message = StringUtil.GeneraterText(50);            
                string urnCreatePost = $"wall.post?" +
                    $"message={createdPost.Message}" +
                    $"{RerequiredParam}";
                AqualityServices.Logger.Info("Send request creating post.");
                var vkResponseTask = VkApiUtils.GetTAsync<VkResponse>(urnCreatePost);
                vkResponseTask.Wait();
                AqualityServices.Logger.Info($"The created status code {Convert.ToInt32(VkApiUtils.StatusCode)} and the respons lenght = {VkApiUtils.ContentLenght}");
                VerifyVkApiResponseError();
                VkResponse vkResponse = vkResponseTask.Result;
                if (vkResponse.Response == null)
                    AqualityServices.Logger.Error($"The respons has not been correct during creating post.");
                createdPost.Id = vkResponse.Response.PostId;
                return createdPost;
            }
            catch(Exception ex)
            {
                AqualityServices.Logger.Fatal("The error appeared during creating post by API request.",ex);
                return createdPost;
            }
        }

        public static Photo UploadPhoto()
        {
            Photo photo = new Photo();
            try 
            { 
                string urnUploadAddress = $"photos.getWallUploadServer?" +
                    $"{RerequiredParam}";
                AqualityServices.Logger.Info($"Get the download photos address by urn : \"{urnUploadAddress}\".");
                var vkResponseUploadUrlTask = VkApiUtils.GetTAsync<VkResponse>(urnUploadAddress);
                vkResponseUploadUrlTask.Wait();
                AqualityServices.Logger.Info($"The photos download address returned status code {Convert.ToInt32(VkApiUtils.StatusCode)} and the respons lenght = {VkApiUtils.ContentLenght}");
                VerifyVkApiResponseError();
                WallUploadServer wallUploadServer = WallUploadServer.Convert(vkResponseUploadUrlTask.Result.Response);                
                var vkResponseUpLoadPhotoTask = VkApiUtils.PostImage<UploadServer>(
                     wallUploadServer.UploadUrl, 
                    $"{Directory.GetCurrentDirectory()}\\Source\\TestingFiles\\{ConfigurationManager.TestingData.Get<string>("files:img")}",
                    "multipart/form-data");
                vkResponseUpLoadPhotoTask.Wait();
                AqualityServices.Logger.Info($"The server download photo returned status code {Convert.ToInt32(VkApiUtils.StatusCode)} and the respons lenght = {VkApiUtils.ContentLenght}");
                VerifyVkApiResponseError();
                UploadServer uploadServer = vkResponseUpLoadPhotoTask.Result;                
                string urnSavePhoto = $"photos.saveWallPhoto?" +
                    $"user_id={ConfigurationManager.Configuration.Get<int>("userId")}" +
                    $"&server={uploadServer.Server}" +
                    $"&photo={uploadServer.Photo}" +
                    $"&hash={uploadServer.Hash}" +
                    $"{RerequiredParam}";
                var vkResponsSavePhotoTask = VkApiUtils.GetTAsync<VkResponsePhotoList>(urnSavePhoto);
                vkResponsSavePhotoTask.Wait();
                AqualityServices.Logger.Info($"The seved photo request returned status code {Convert.ToInt32(VkApiUtils.StatusCode)} and the respons lenght = {VkApiUtils.ContentLenght}");
                VerifyVkApiResponseError();
                VkResponsePhotoList vkResponsePhoto = vkResponsSavePhotoTask.Result;
                return vkResponsePhoto.Photos.FirstOrDefault();
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Fatal("The error appeared during uploading photo by API request.", ex);
                return photo;
            }
        }

        public static Post EditPost(int? postId, Photo uploadPhoto)
        {
            Post wallPost = new Post();
            try 
            { 
                wallPost.Message = StringUtil.GeneraterText(15);
                string urnEditPost = $"wall.edit?" +
                    $"post_id={postId}" +
                    $"&friends_only=0" +
                    $"&message={wallPost.Message}" +
                    $"&attachments=photo{uploadPhoto.OwnerId}_{uploadPhoto.Id}" +
                    $"{RerequiredParam}";
                var vkResponseEditPost = VkApiUtils.GetTAsync<VkResponse>(urnEditPost);
                vkResponseEditPost.Wait();
                AqualityServices.Logger.Info($"The edited post request returned status code {Convert.ToInt32(VkApiUtils.StatusCode)} and the respons lenght = {VkApiUtils.ContentLenght}");
                VerifyVkApiResponseError();
                Post editedPost = Post.Convert(vkResponseEditPost.Result.Response);
                wallPost.Id = editedPost.Id;
                return wallPost;
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Fatal("The error appeared during editing post by API request.", ex);
                return wallPost;
            }
        }
        
        public static Comment CreateComment(int? postId)
        {
            Comment createdComment = new Comment();
            try 
            { 
                string generateText = StringUtil.GeneraterText(20);
                string urnCreateComment = $"wall.createComment?" +
                    $"post_id={postId}" +
                    $"&message={generateText}" +
                    $"{RerequiredParam}";
                var vkResponseCreateCommentTask = VkApiUtils.GetTAsync<VkResponse>(urnCreateComment);
                vkResponseCreateCommentTask.Wait();
                AqualityServices.Logger.Info($"The created status code {Convert.ToInt32(VkApiUtils.StatusCode)} and the respons lenght = {VkApiUtils.ContentLenght}");
                VerifyVkApiResponseError();
                createdComment = Comment.Convert(vkResponseCreateCommentTask.Result.Response);
                createdComment.Message = generateText;
                return createdComment;
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Fatal("The error appeared during creating comment by API request.", ex);
                return createdComment;
            }
        }

        public static bool IsPostLikeAuthor(int? postId)
        {
            try
            {
                string urnIsLike = $"likes.isLiked?" +
                    $"user_id={ConfigurationManager.Configuration.Get<int>("userId")}" +
                    $"&type=post" +
                    $"&item_id={postId}" +
                    $"{RerequiredParam}";
                IsLiked liked = new IsLiked();
                RetryPolicy policy = Policy
                .Handle<NullReferenceException>()
                .WaitAndRetry(1, retryAttempt => TimeSpan.FromSeconds(5)                
                );
                liked = policy.Execute(() =>
                {
                    var vkResponseIsLikedTask = VkApiUtils.GetTAsync<VkResponse>(urnIsLike);
                    vkResponseIsLikedTask.Wait();
                    AqualityServices.Logger.Info($"The created status code {Convert.ToInt32(VkApiUtils.StatusCode)} and the respons lenght = {VkApiUtils.ContentLenght}");
                    VerifyVkApiResponseError();
                    liked = IsLiked.Convert(vkResponseIsLikedTask.Result.Response);
                    if (liked.Liked == 1)
                        return liked;
                    else
                        throw new NullReferenceException();
                });                
                if (liked.Liked == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Fatal("The error appeared during verifying comment author by API request.", ex);
                return false;
            }
        }

        public static void DeletePost(int? postId)
        {
            try 
            { 
                string urnDeletePost = $"wall.delete?" +
                    $"post_id={postId}" +
                    $"{RerequiredParam}";
                var vkResponseIntTask = VkApiUtils.GetTAsync<VkResponseInt>(urnDeletePost);
                vkResponseIntTask.Wait();
                AqualityServices.Logger.Info($"The created status code {Convert.ToInt32(VkApiUtils.StatusCode)} and the respons lenght = {VkApiUtils.ContentLenght}");
                VerifyVkApiResponseError();
                VkResponseInt vkResponseInt = vkResponseIntTask.Result;
                if (vkResponseInt.Response == 1)
                    AqualityServices.Logger.Info($"The post \"{postId}\" has been deleted.");
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Fatal("The error appeared during deleting post by API request.", ex);                
            }
        }

        private static void VerifyVkApiResponseError()
        {
            if (!VkApiUtils.IsNullResponseError)
                AqualityServices.Logger.Error($"Vk API responsed error: {VkApiUtils.VkResponseError.ToString()}.");
        } 
    }
}
