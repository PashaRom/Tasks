package tests;

import org.testng.annotations.*;
import framework.logging.Log;
import org.testng.asserts.SoftAssert;
import tests.app.RestApiSender;
import tests.models.Post;
import tests.models.User;
import tests.steps.RestApiSteps;
import java.util.ArrayList;
import java.util.List;

public class RestApiTests {
    private static final String POSTS = "$.posts.";
    private static final String POSTS_URN = POSTS + "urn";
    private static final String POSTS_POSITIVE = POSTS + "positive.";
    private static final String POSTS_POSITIVE_ID = POSTS_POSITIVE + "id";
    private static final String POSTS_NEW_POST = POSTS + "newPost.";
    private static final String POSTS_NEW_POST_USER_ID = POSTS_NEW_POST + "userId";
    private static final String POST_NEGATIVE = POSTS + "negative.";
    private static final String POST_NEGATIVE_ID = POST_NEGATIVE + "id";
    private static final String USERS = "$.users.";
    private static final String USERS_URN = USERS + "urn";

    @Test(groups = { "first-group" })
    public void FirstTest(){
        SoftAssert softAssert = new SoftAssert();
        Log.step(1,"Send a GET request to get list of all posts");
        List<Post> posts = new ArrayList<Post>();
        posts = RestApiSender.getEntities(RestApiSteps.getTestingData(POSTS_URN),
                RestApiSteps.typeFactory(ArrayList.class, Post.class));
        softAssert.assertEquals(RestApiSender.getStatusCode(),200,
                "Response did not get the list of all posts.");
        softAssert.assertTrue(RestApiSender.isJsonFormat(),
                "Response \"/" + RestApiSteps.getTestingData(POSTS_POSITIVE_ID) +
                        "\" was not JSON format.");
        softAssert.assertTrue(RestApiSender.isDesc(posts),
                "The list of posts do not have DESC sort.");
        Log.step(2,"Send a GET request to get the post by id " +
                RestApiSteps.getTestingData(POSTS_POSITIVE + "id"));
        Post positivePostById = RestApiSender.<Post>getEntityById(RestApiSteps.
                        getTestingData(POSTS_URN),
                Integer.parseInt(RestApiSteps.getTestingData(POSTS_POSITIVE_ID)),
                Post.class);
        softAssert.assertEquals(RestApiSender.getStatusCode(),200,
                "Response did not get the post " + RestApiSteps.
                        getTestingData(POSTS_POSITIVE_ID) + ".");
        softAssert.assertEquals(positivePostById.getUserId(), Integer.parseInt(RestApiSteps.
                        getTestingData( POSTS_POSITIVE + "userId")),
                "The user id was different.");
        softAssert.assertEquals(positivePostById.getId(), Integer.parseInt(RestApiSteps.
                        getTestingData(POSTS_POSITIVE_ID)),
                "The post id was different.");
        softAssert.assertNotNull(positivePostById.getTitle(),
                "The title of post has null value");
        softAssert.assertNotNull(positivePostById.getBody(),
                "The body of post has null value");
        Log.step(3,"Send a GET request to get post by id " + RestApiSteps.
                getTestingData(POST_NEGATIVE_ID));
        Post negativePostById = RestApiSender.<Post>getEntityById(RestApiSteps.
                        getTestingData(POSTS_URN),
                Integer.parseInt(RestApiSteps.getTestingData(POST_NEGATIVE_ID)),
                Post.class);
        softAssert.assertEquals(RestApiSender.getStatusCode(), 404,
                "The post must be unexpected.");
        softAssert.assertTrue(RestApiSender.isIsEmptyJsonString(),
                "The json string has values.");
        Log.step(4, "Create a post by userId=" +
                RestApiSteps.getTestingData(POSTS_NEW_POST_USER_ID));
        Post expectedNewPost = RestApiSteps.createPost();
        Post actualNewPost = RestApiSender.createPost(RestApiSteps.getTestingData(POSTS_URN),
                expectedNewPost, Post.class);
        softAssert.assertEquals(RestApiSender.getStatusCode(), 201,
                "New post was not created.");
        softAssert.assertEquals(actualNewPost.getTitle(), expectedNewPost.getTitle(),
                "The titles new post were different.");
        softAssert.assertEquals(actualNewPost.getBody(), expectedNewPost.getBody(),
                "The bodies new post were different.");
        softAssert.assertNotEquals(actualNewPost.getId(), 0,
                "The id new post must not have value equal 0");
        Log.step(5, "Get the list of all users.");
        List<User> users = new ArrayList<>();
        users = RestApiSender.getEntities(RestApiSteps.getTestingData(USERS_URN),
                RestApiSteps.typeFactory(ArrayList.class, User.class));
        User expectedUser = RestApiSteps.getExpectedUser();
        User actualUser = users.stream().filter(us -> us.getId() == 5).findFirst().orElse(null);
        softAssert.assertEquals(RestApiSender.getStatusCode(), 200,
                "Did not get the list of all users.");
        softAssert.assertTrue(RestApiSender.isJsonFormat(),
                "The request to get the list of all users was not JSON format.");
        softAssert.assertEquals(expectedUser, actualUser,
                "Users were different.");
        Log.step(6,"Send request to get user by id=" + expectedUser.getId());
        User actualUserById = RestApiSender.getEntityById(RestApiSteps.getTestingData(USERS_URN),
                expectedUser.getId(),
                User.class);
        softAssert.assertEquals(RestApiSender.getStatusCode(), 200,
                "Response did not get user bu id " + expectedUser.getId());
        softAssert.assertEquals(actualUser, actualUserById,
                "Users data were different.");
        softAssert.assertTrue(true);
        softAssert.assertAll();
    }
}
