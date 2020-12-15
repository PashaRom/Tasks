package tests.app;

import com.fasterxml.jackson.databind.ObjectMapper;
import framework.configuration.Configuration;
import framework.logging.Log;
import framework.restApi.Request;
import tests.models.Post;
import java.util.ArrayList;
import java.util.List;

public class RestApiSender {

    public static List<Post> getPosts(){
        try{
            Log.info("Sent the request to get the list of all posts");
            return Request.<ArrayList<Post>>getArray(String.format("%s%s",
                    Configuration.getConfig().getUrl(), Configuration.getData().getPost().getUrn()),
                    new ObjectMapper().getTypeFactory().constructParametricType(ArrayList.class, Post.class));
        }
        catch (Exception es){
            Log.error("An error occurred during getting the list of all posts");
            return new ArrayList<Post>();
        }
    }

    public static int getStatusCode(){
        return Request.getStatusCode();
    }

    public static String getContentType(){
        String st = Request.getContentType();
        return null;
    }
}
