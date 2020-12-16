package tests;


import framework.configuration.Configuration;
import framework.utilities.FileHelper;
import framework.utilities.StringHelper;
import org.testng.annotations.*;
import framework.logging.Log;

import org.testng.asserts.SoftAssert;
import tests.app.RestApiSender;
import tests.models.Post;

import java.util.ArrayList;
import java.util.List;


public class RestApiTests {

    //private static final Logger logger = LogManager.getLogger(RestApiTests.class);
    private List<Post> posts = new ArrayList<Post>();

    @BeforeClass
    public void setUp(){
        //RestApiHelper.Get();
        //String conf = System.getProperty("log4j.configurationFile");
        //if(conf == null)
        //System.setProperty("log4j.configurationFile","src\\test\\java\\resources\\log4j2.xml");
    }
    @Test
    public void Debuging(){
        //WriteFromJsonFile();
        //RestApiSender.getContentType();
        FileHelper.getJsonContextFomFile(null);
    }

    @Test(groups = { "first-group" })
    public void FirstTest(){
        //System.out.println(Paths.get("").toAbsolutePath().normalize().toString());
        SoftAssert softAssert = new SoftAssert();
        Log.step(1,"Send a GET request to get list of all posts");
        posts = RestApiSender.getPosts();
        softAssert.assertEquals(RestApiSender.getStatusCode(),200,
                "Response did not get the list of all posts.");

        softAssert.assertTrue(RestApiSender.isJsonFormat(),
                "Response \"/" + Configuration.getData().read("$.posts.positivePost.id") +
                        "\" was not JSON format.");
        softAssert.assertTrue(RestApiSender.isDesc(posts),
                "The list of posts do not have DESC sort.");
        Log.step(2,"Send a GET request to get the post by id " +
                Configuration.getData().read("$.posts.positivePost.id"));
        Post positivePostById = RestApiSender.getPostById(Configuration.getData().
                read("$.posts.positivePost.id"));
        softAssert.assertEquals(RestApiSender.getStatusCode(),200,
                "Response did not get the post " + Configuration.getData().
                        read("$.posts.positivePost.id") + ".");
        softAssert.assertEquals(positivePostById.getUserId(), (int)Configuration.getData().
                read("$.posts.positivePost.userId"),
                "The user id was different.");
        softAssert.assertEquals(positivePostById.getId(), (int)Configuration.getData().
                read("$.posts.positivePost.id"),
                "The post id was different.");
        softAssert.assertNotNull(positivePostById.getTitle(),
                "The title of post has null value");
        softAssert.assertNotNull(positivePostById.getBody(),
                "The body of post has null value");
        Log.step(3,"Send a GET request to get post by id " + Configuration.getData()
                .read("$.posts.negativeId.id"));
        Post negativePostById = RestApiSender.getPostById(Configuration.getData().
                read("$.posts.negativeId.id"));
        softAssert.assertEquals(RestApiSender.getStatusCode(), 404,
                "The post must be unexpected.");
        softAssert.assertTrue(RestApiSender.isIsEmptyJsonString(),
                "The json string has values.");
        Log.step(4, "Create a post by userId=" + Configuration.getData().read("$.posts.newPost.userId"));
        Post expectedNewPost = new Post();

        softAssert.assertTrue(true);
        softAssert.assertAll();
    }
}
