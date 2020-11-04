using System.IO;
using NUnit.Framework;
using Test.Configuration;
using Aquality.Selenium.Browsers;
using Utilities;
using Task9VK.PageObject;
using Task9VK.Models.ResponseModels;
namespace Task9VK
{
    public class VKTest
    { 
        [OneTimeSetUp]
        public void OneTimeSetUp() 
        {
            //FileUtil.ComparePhoto(@"D:\A1QE\Projects\Task_9_VK\Tasks\Task9VK\bin\Debug\netcoreapp3.1\Source\TestingFiles\testing_bug.png", @"D:\A1QE\Projects\Task_9_VK\Tasks\Task9VK\bin\Debug\netcoreapp3.1\Out\images\downloadImages.png", "");
            AqualityServices.Browser.Maximize();            
        }

        [Test]
        public void WorkWithPostOnPublicWall()
        {
            Logger.Step(1, $"Go to {ConfigurationManager.Configuration.Get<string>("url")} by UI");
            AqualityServices.Browser.GoTo(ConfigurationManager.Configuration.Get<string>("url"));
            AqualityServices.Browser.WaitForPageToLoad();
            Logger.Step(2, "Autorization by UI");
            LoginPage loginPage = new LoginPage();
            loginPage.Autorization(
                ConfigurationManager.CredOfUser.Get<string>("autorizationData:email"),
                ConfigurationManager.CredOfUser.Get<string>("autorizationData:password"));
            Logger.Step(3, "Go to page: \"My page\" by UI.");
            HomePage homePage = new HomePage();
            homePage.GoToMyPage();
            Logger.Step(4, "Create post and get the post id by API.");
            Post createdPost = VkApiRequest.CreatePost(StringUtil.GeneraterText(50));
            Assert.IsNotNull(
                createdPost.Id,
                "The created post Id have got value null.");
            Logger.Step(5, "Get the message created post and the created user by UI.");
            MyPage myPage = new MyPage();            
            Assert.IsTrue(
                myPage.GetPostMessage(createdPost.Id).Equals(createdPost.Message),
                "The created post message is different the message from wall.");            
            Assert.AreEqual(
                myPage.GetUserId(), myPage.GetPostAutorId(createdPost.Id));
            Logger.Step(6, $"Edit the post \"{createdPost.Id}\" by API.");
            Photo uploadPhoto = VkApiRequest.UploadPhoto(myPage.GetUserId(),
                ConfigurationManager.TestingData.Get<string>("files:images:upload:directory"),
                ConfigurationManager.TestingData.Get<string>("files:images:upload:fileName"));
            Post editedPost = VkApiRequest.EditPost(createdPost.Id, uploadPhoto);
            Logger.Step(7, $"Check the edited post message and the uploaded post image by API.");            
            string editedPostMessage = myPage.GetPostMessage(createdPost.Id);            
            Assert.IsTrue(
                editedPost.Message.Equals(editedPostMessage),
                $"The post message has not been changed. The expected post message \"{editedPost.Message}\". The actual post message \"{editedPostMessage}\"");
            myPage.DownloadPostPhoto(createdPost.Id, uploadPhoto.Id);
            Assert.Greater(
                ConfigurationManager.TestingData.Get<int>("files:images:percentDifferentCompare"),
                FileUtil.ComparePhoto($"{Directory.GetCurrentDirectory()}{ConfigurationManager.TestingData.Get<string>("files:images:upload:directory")}\\{ConfigurationManager.TestingData.Get<string>("files:images:upload:fileName")}",
                    $"{Directory.GetCurrentDirectory()}{ConfigurationManager.TestingData.Get<string>("files:images:download:directory")}\\{ConfigurationManager.TestingData.Get<string>("files:images:download:fileName")}"),
                "The post image has not been upload or are different.");
            Logger.Step(8, "Add a comment to the post by API.");
            Comment createdComment = VkApiRequest.CreateComment(createdPost.Id);
            myPage.ClickLinkShowComment(createdPost.Id);            
            string createdPostCommentMessage = myPage.GetPostCommentMessage(createdComment.Id);
            Logger.Step(9, "Verify the created post comment and the comment author.");
            Assert.AreEqual(
                myPage.GetUserId(), 
                myPage.GetPostCommentAuthor(createdComment.Id),
                "The current authot is different the post comment author by UI.");
            Assert.IsTrue(
                createdComment.Message.Equals(createdPostCommentMessage),
                $"The generate post comment message \"{createdComment.Message}\" and the post comment message \"{createdPostCommentMessage}\" was different.");
            Logger.Step(10, "Set a like to the post by UI.");
            myPage.SetPostLike(createdPost.Id);
            Logger.Step(11, "Verify the like author by API.");            
            Assert.IsTrue(
                VkApiRequest.IsPostLikeAuthor(createdPost.Id, myPage.GetUserId()),
                $"The autor \"{myPage.GetUserId()}\" do not have a like to the post \"{61015}\"");
            Logger.Step(12, $"Delete the post \"{createdPost.Id}\".");
            VkApiRequest.DeletePost(createdPost.Id);
            Logger.Step(13, $"Veryfy the post \"{createdPost.Id}\" has been deleted.");            
            Assert.IsFalse(
                myPage.IsDeletedPost(createdPost.Id),
                $"The post \"{createdPost.Id}\" has not been delete.");           
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            AqualityServices.Browser.Quit();
        }
    }
}