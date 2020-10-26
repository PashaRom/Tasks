using System;
using OpenQA.Selenium;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Test.Configuration;
using Utilities;
namespace Task9VK.PageObject
{
    public class MyPage
    {
        private IElementFactory elementFactory = AqualityServices.Get<IElementFactory>();       

        public string GetPostMessage(int? postId)        
        {                      
            AqualityServices.Logger.Info($"Get the message with the post id = {postId}.");            
            return elementFactory.GetLabel(By.Id($"wpt{ConfigurationManager.Configuration.Get<int>("userId")}_{postId}"), "Posted message").Text; ;
        } 
        public int GetPostAutorId(int? postId)
        {
            AqualityServices.Logger.Info($"Get the post author by post id = {postId}.");
            string authorId = elementFactory.GetLabel(By.XPath($".//div[@id='post{ConfigurationManager.Configuration.Get<int>("userId")}_{postId}']/div/div/div/h5[@class='post_author']/a[@class='author']"), "Author Id").GetAttribute("href");
            return StringUtil.GetAuthorPostIdVk(authorId);
        }
        
        public int GetPostImageId(int? postId, int? photoId)
        {
            AqualityServices.Logger.Info($"Get the post image by post id = {postId}.");
            string imageId = elementFactory.GetLabel(By.XPath($".//div[@id='wpt{ConfigurationManager.Configuration.Get<int>("userId")}_{postId}']/div/a[@data-photo-id='{ConfigurationManager.Configuration.Get<int>("userId")}_{photoId}']"), "Post image").GetAttribute("href");
            return StringUtil.GetIdVkByChar(imageId,'_');
        }

        public void ClickLinkShowComment(int? postId)
        {
            AqualityServices.Logger.Info($"Click the link \"Show comment\" by post id = {postId}.");
            ILink showComment = elementFactory.GetLink(By.XPath($".//div[@id='replies{ConfigurationManager.Configuration.Get<int>("userId")}_{postId}']/a[contains(@class,'replies_next')]"), "Show comments");
            showComment.Click();
        }
        
        public int GetPostCommentAuthor(int? commentId)
        {
            AqualityServices.Logger.Info($"Get the post comment autor by comment id = {commentId}.");            
            string authorId = elementFactory.GetLabel(By.XPath($".//div[@data-post-id='{ConfigurationManager.Configuration.Get<int>("userId")}_{commentId}'][contains(@class,'reply')]/div/div[@class='reply_content']/div[@class='reply_author']/a[@class='author']"), "The post comment autor").GetAttribute("data-from-id");
            return Convert.ToInt32(authorId);
        }

        public string GetPostCommentMessage(int? commentId)
        {
            AqualityServices.Logger.Info($"Get the post comment message by post id = {commentId}.");            
            string commentMessage = elementFactory.GetLabel(By.XPath($".//div[@data-post-id='{ConfigurationManager.Configuration.Get<int>("userId")}_{commentId}'][contains(@class,'reply')]/div/div[@class='reply_content']/div[@class='reply_text']/div/div[@class='wall_reply_text']"), "Comment message").Text;
            return commentMessage;
        }

        public void SetPostLike(int? postId)
        {
            IButton likeButton = elementFactory.GetButton(By.XPath($".//div[@data-post-id='{ConfigurationManager.Configuration.Get<int>("userId")}_{postId}']/div/div/div/div[contains(@class,'like_wrap')]/div/div[@class='like_btns']/a[contains(@class,'like_btn')]"),"Like's button");
            AqualityServices.Logger.Info($"Click the button\"{likeButton.Name}\".");
            likeButton.Click();
        }

        public bool IsDeletedPost(int? postId)
        {
            bool isExist = elementFactory.GetLabel(By.XPath($".//div[@data-post-id='{ConfigurationManager.Configuration.Get<int>("userId")}_{postId}']/div/div/div/h5[@class='post_author']/a[@class='author']"), "Autor").State.WaitForNotExist(TimeSpan.FromSeconds(5));
            if (!isExist)
                AqualityServices.Logger.Info($"The post \"{postId}\" is unexist.");
            else
                AqualityServices.Logger.Info($"The post \"{postId}\" is exist.");
            return isExist;
        }
    }
}
