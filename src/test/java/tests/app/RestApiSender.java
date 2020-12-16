package tests.app;

import com.fasterxml.jackson.databind.ObjectMapper;
import framework.configuration.Configuration;
import framework.logging.Log;
import framework.restapi.Request;
import framework.utilities.StringHelper;
import tests.models.Post;

import java.util.ArrayList;
import java.util.List;

public class RestApiSender {

    private static String postsUrn = Configuration.getData().read("$.posts.urn");

    private static boolean isJsonFormat;
    private static boolean isEmptyJsonString;
    public static List<Post> getPosts(){
        try{
            Log.info("Sent the request to get the list of all posts");
            ObjectMapper objectMapper = new ObjectMapper();
            String postsResponse = Request.getArray(buildUrl(postsUrn));
            //Log.info("Check response that has JSON format");
            //isJsonFormat = StringHelper.isJsonFormat(postsResponse);
            CheckJsonFormat(postsResponse);
            CheckJsonBodyIsNull(postsResponse);
            //boolean d = StringHelper.isNullJsonBody(postsResponse);
            return objectMapper.readValue(postsResponse,
                    new ObjectMapper().getTypeFactory().constructParametricType(ArrayList.class,
                            Post.class));
        }
        catch (Exception ex){
            Log.error("An error occurred during getting the list of all posts", ex);
            return new ArrayList<Post>();
        }
    }

    public static Post getPostById(int postId){
        Log.info("Sent the request to get the post by id=" + postId);
        ObjectMapper objectMapper = new ObjectMapper();
        try{
        String postsResponse = Request.get(buildUrl(postsUrn, postId));
        CheckJsonFormat(postsResponse);
        CheckJsonBodyIsNull(postsResponse);
        return objectMapper.readValue(postsResponse, Post.class);
        }
        catch (Exception ex){
            Log.error("An error occurred during getting the post by id=" + postId, ex);
            return new Post();
        }
    }

    private static void CheckJsonFormat(String jsonString){
        Log.info("Check response that has JSON format");
        isJsonFormat = StringHelper.isJsonFormat(jsonString);
    }

    private static void CheckJsonBodyIsNull(String jsonString){
        Log.info("Check JSON string is empty.");
        isEmptyJsonString = StringHelper.isNullJsonBody(jsonString);
    }

    public static int getStatusCode(){
        Log.info(String.format("Get a response status code. The status code was %d",
                Request.getStatusCode()));
        return Request.getStatusCode();
    }

    /*public static String getContentType(){
        String st = Request.getContentType();
        return null;
    }*/

    public static boolean isJsonFormat(){
        return isJsonFormat;
    }

    public static boolean isIsEmptyJsonString() {
        return isEmptyJsonString;
    }

    public static boolean isDesc(List<Post> postsList){
        Log.info("Check the list of post that has DESC sorting.");
        int minimumId = 0;
        for (var post : postsList) {
            if(minimumId > post.getId()){
                return false;
            }
            minimumId = post.getId();
        }
        return true;
    }

    private static String buildUrl(String urn){
        return String.format("%s%s", Configuration.getConfig().read("$.url"),urn);
    }

    private static String buildUrl(String urn, int id){
        return String.format("%s%s/%d", Configuration.getConfig().read("$.url"),urn,id);
    }
}
