package tests.app;

import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.ObjectMapper;
import framework.configuration.Configuration;
import framework.logging.Log;
import framework.restapi.Request;
import framework.utilities.StringHelper;
import tests.models.Post;
import java.util.ArrayList;
import java.util.List;

public class RestApiSender {
    private static boolean isJsonFormat;
    private static boolean isEmptyJsonString;

    public static <T> List<T> getEntities(String urn, JavaType typeValue){
        try{
            Log.info("Sent the request to get the list of all "+ typeValue.toString());
            ObjectMapper objectMapper = new ObjectMapper();
            String postsResponse = Request.getArray(buildUrl(urn));
            CheckData(postsResponse);

            return objectMapper.readValue(postsResponse, typeValue);
        }
        catch (Exception ex){
            Log.error("An error occurred during getting the list of all "+ typeValue.toString(), ex);
            return new ArrayList<T>();
        }
    }

    public static <T> T getEntityById(String urn, int entityId, Class<T> typeValue){
        Log.info("Sent the request to get the "+ typeValue.toString() + " by id=" + entityId);
        ObjectMapper objectMapper = new ObjectMapper();
        try{
            String postsResponse = Request.get(buildUrl(urn, entityId));

            CheckData(postsResponse);
            return objectMapper.readValue(postsResponse, typeValue);
        }
        catch (Exception ex){
            Log.error("An error occurred during getting the " + typeValue.toString() + " by id=" + entityId, ex);
            return null;
        }
    }

    public static <T> T createPost(String urn ,T entity, Class<T> typeValue){
        Log.info("Sent the POST request to create new post :" + entity);
        ObjectMapper objectMapperToJson = new ObjectMapper();
        ObjectMapper objectMapperToPost = new ObjectMapper();
        try{
            String createdPostResponse = Request.sendPost(buildUrl(urn),
                    objectMapperToJson.writeValueAsString(entity));
            CheckData(createdPostResponse);
            return objectMapperToPost.readValue(createdPostResponse, typeValue);
        }
        catch (Exception ex){
            Log.error("An error occurred during creating new post " + entity, ex);
        }
        return null;
    }

    private static void CheckData(String postsResponse){
        CheckJsonFormat(postsResponse);
        CheckJsonBodyIsNull(postsResponse);
    }

    private static void CheckJsonFormat(String jsonString){
        Log.info("Check response that has JSON format");
        isJsonFormat = StringHelper.isJsonFormat(jsonString);
        Log.info("CheckJsonFormat: " + isJsonFormat);
    }

    private static void CheckJsonBodyIsNull(String jsonString){
        Log.info("Check JSON string is empty.");
        isEmptyJsonString = StringHelper.isNullJsonBody(jsonString);
        Log.info("CheckJsonBodyIsNull: " + isEmptyJsonString);
    }

    public static int getStatusCode(){
        Log.info(String.format("Get a response status code. The status code was %d",
                Request.getStatusCode()));
        return Request.getStatusCode();
    }

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
