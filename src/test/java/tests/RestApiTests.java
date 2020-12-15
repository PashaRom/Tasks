package tests;


import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.databind.type.TypeFactory;
import org.apache.logging.log4j.core.LoggerContext;
import org.testng.Assert;
import org.testng.annotations.*;
import static framework.utilities.FileHelper.*;
import framework.configuration.Configuration;
import framework.logging.Log;
import framework.restApi.Request;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.testng.asserts.SoftAssert;
import tests.app.RestApiSender;
import tests.models.Post;

import java.io.File;
import java.nio.file.Paths;
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
        RestApiSender.getContentType();

    }

    @Test(groups = { "first-group" })
    public void FirstTest(){
        //System.out.println(Paths.get("").toAbsolutePath().normalize().toString());
        SoftAssert softAssert = new SoftAssert();
        Log.step(1,"Send a GET request to get list of all posts");
        posts = RestApiSender.getPosts();
        softAssert.assertEquals(RestApiSender.getStatusCode(),200,
                "Response did not get the list of all posts");

        RestApiSender.getContentType();
        softAssert.assertTrue(true);
        softAssert.assertAll();
    }
}
