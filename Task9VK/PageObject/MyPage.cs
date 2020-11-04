using System;
using OpenQA.Selenium;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Utilities;
using Test.Configuration;
using System.IO;
namespace Task9VK.PageObject
{
    public class MyPage
    {
        private ILabel Label(By locator, string name) => AqualityServices.Get<IElementFactory>().GetLabel(locator, name);
        private ILink Link(By locator, string name) => AqualityServices.Get<IElementFactory>().GetLink(locator, name);
        private IButton Button(By locator, string name) => AqualityServices.Get<IElementFactory>().GetButton(locator, name);
        public string GetPostMessage(int? postId)        
        {                      
            AqualityServices.Logger.Info($"Get the message with the post id = {postId}.");            
            return Label(By.Id($"wpt{GetUserId()}_{postId}"), "Posted message").Text;
        } 
        public int GetPostAutorId(int? postId)
        {
            AqualityServices.Logger.Info($"Get the post author by post id = {postId}.");
            string authorId = Label(By.XPath($".//div[@id='post{GetUserId()}_{postId}']//h5[@class='post_author']/a[@class='author']"), "Author Id").GetAttribute("href");
            return StringUtil.GetAuthorPostIdVk(authorId);
        }
        
        public int GetPostImageId(int? postId, int? photoId)
        {
            AqualityServices.Logger.Info($"Get the post image by post id = {postId}.");
            string imageId = Label(By.XPath($".//div[@id='wpt{GetUserId()}_{postId}']/div/a[@data-photo-id='{GetUserId()}_{photoId}']"), "Post image").GetAttribute("href");
            return StringUtil.GetIdVkByChar(imageId,'_');
        }

        public void ClickLinkShowComment(int? postId)
        {
            AqualityServices.Logger.Info($"Click the link \"Show comment\" by post id = {postId}.");
            ILink showComment = Link(By.XPath($".//div[@id='replies{GetUserId()}_{postId}']/a[contains(@class,'replies_next')]"), "Show comments");
            showComment.Click();
        }
        
        public int GetPostCommentAuthor(int? commentId)
        {
            AqualityServices.Logger.Info($"Get the post comment autor by comment id = {commentId}.");            
            string authorId = Label(By.XPath($".//div[@data-post-id='{GetUserId()}_{commentId}'][contains(@class,'reply')]/div/div[@class='reply_content']/div[@class='reply_author']/a[@class='author']"), "The post comment autor").GetAttribute("data-from-id");
            return Convert.ToInt32(authorId);
        }

        public string GetPostCommentMessage(int? commentId)
        {
            AqualityServices.Logger.Info($"Get the post comment message by post id = {commentId}.");            
            string commentMessage = Label(By.XPath($".//div[@data-post-id='{GetUserId()}_{commentId}'][contains(@class,'reply')]/div/div[@class='reply_content']/div[@class='reply_text']/div/div[@class='wall_reply_text']"), "Comment message").Text;
            return commentMessage;
        }

        public void SetPostLike(int? postId)
        {
            IButton likeButton = Button(By.XPath($".//div[@data-post-id='{GetUserId()}_{postId}']//div[contains(@class,'like_wrap')]/div/div[@class='like_btns']/a[contains(@class,'like_btn')]"),"Like's button");
            AqualityServices.Logger.Info($"Click the button\"{likeButton.Name}\".");
            likeButton.Click();
        }

        public bool IsDeletedPost(int? postId)
        {
            bool isExist = Label(By.XPath($".//div[@data-post-id='{GetUserId()}_{postId}']//h5[@class='post_author']/a[@class='author']"), "Autor").State.WaitForNotExist(TimeSpan.FromSeconds(5));
            if (!isExist)
                AqualityServices.Logger.Info($"The post \"{postId}\" is unexist.");
            else
                AqualityServices.Logger.Info($"The post \"{postId}\" is exist.");
            return isExist;
        }

        public int GetUserId()
        {
            try 
            { 
                string currentUrl = AqualityServices.Browser.CurrentUrl;
                return StringUtil.GetIdVkByString(currentUrl,"id");
            }
            catch(Exception ex)
            {
                AqualityServices.Logger.Error($"The error appeared during getting user id from current url. The error message: {ex.Message}.");
                return 0;
            }
        }

        public void DownloadPostPhoto(int? postId, int? photoId)
        {
            try 
            { 
                AqualityServices.Logger.Info("Download the image from the created post.");
                ClickPostPhoto(postId, photoId);
                MoveToLinkMoreAction();                
                if (FileUtil.DownloadFile(GetAttributeRealSizePhotoLink("href"), $"{Directory.GetCurrentDirectory()}{ConfigurationManager.TestingData.Get<string>("files:images:download:directory")}", $"{ConfigurationManager.TestingData.Get<string>("files:images:download:fileName")}"))
                    AqualityServices.Logger.Info("The photo has been downloaded.");
                else
                    AqualityServices.Logger.Error("The photo has not been downloaded.");                
                ClosePhotoModalWindow();
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Error($"The error appeared during downloading photo from the post on wall. Message: \"{ex.Message}\"");
            }
        }

        public void ClickPostPhoto(int? postId, int? photoId) 
        {
            try 
            { 
                ILink photoLink = Link(By.XPath($".//div[@data-post-id='{GetUserId()}_{postId}']//a[@data-photo-id='{GetUserId()}_{photoId}']"), "Post photo");
                AqualityServices.Logger.Info($"Click the link \"{photoLink.Name}\".");
                photoLink.ClickAndWait();
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Error($"The error appeared during clicking the photo on post. Message: \"{ex.Message}\"");
            }
        }
        public void MoveToLinkMoreAction()
        {
            try 
            { 
                ILink moreActions = Link(By.CssSelector(".pv_actions_more"), "More action");
                AqualityServices.Logger.Info($"Click the link \"{moreActions.Name}\".");
                moreActions.MouseActions.Click();
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Error($"The error appeared during moving to \"More action\" link. Message: \"{ex.Message}\"");
            }
        }

        public string GetAttributeRealSizePhotoLink(string attribute)
        {
            try 
            {         
                ILink openOrigin = Link(By.Id("pv_more_act_download"), "Show origin");
                AqualityServices.Logger.Info($"Get the \"href\" attribute from \"{openOrigin.Name}\" link.");
                return openOrigin.GetAttribute(attribute);
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Error($"The error appeared during getting the attribute \"{attribute}\" from \"Real size\" link. Message: \"{ex.Message}\"");
                return String.Empty;
            }
        }

        public void ClosePhotoModalWindow()
        {
            try 
            { 
                IButton closeModalWindow = Button(By.XPath("//div[@id='pv_box']/div[contains(@class,'pv_photo_wrap')]/div[@class='pv_close_btn']"), "Close");
                closeModalWindow.MouseActions.Click();
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Error($"The error appeared during closing the photo modal window. Message: \"{ex.Message}\"");
            }
        }
    }
}
