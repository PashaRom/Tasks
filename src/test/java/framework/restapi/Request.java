package framework.restapi;

import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.apache.http.HttpEntity;
import org.apache.http.HttpHeaders;
import org.apache.http.client.methods.CloseableHttpResponse;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClients;
import org.apache.http.util.EntityUtils;
import framework.logging.Log;
import java.io.IOException;

public class Request {
    private static int statusCode;
    private static String contentType;

    public static String get(String url){
        CloseableHttpClient httpClient = HttpClients.createDefault();
        try{
            HttpGet request = new HttpGet(url);
            request.addHeader(HttpHeaders.CONTENT_TYPE, "application/json");
            CloseableHttpResponse response = httpClient.execute(request);
            return EntityUtils.toString(setResponseData(response));
        }
        catch (Exception ex){
            Log.error("An error occurred during to send get request. Status code was " +
                    statusCode, ex);
        }
        return null;
    }

    public static <T> T getArray(String url, JavaType valueType) throws IOException {
        CloseableHttpClient httpClient = HttpClients.createDefault();
        try{
            ObjectMapper objectMapper = new ObjectMapper();
            HttpGet request = new HttpGet(url);
            request.addHeader(HttpHeaders.CONTENT_TYPE, "application/json");
            CloseableHttpResponse response = httpClient.execute(request);
            return objectMapper.readValue(setResponseData(response).getContent(), valueType);
        }
        catch (Exception ex){
            Log.error("An error occurred during to send get request. Status code was " +
                    statusCode, ex);
        }
        finally {
            httpClient.close();
        }
        return null;
    }

    public static String getArray(String url) throws IOException {
        CloseableHttpClient httpClient = HttpClients.createDefault();
        try{
            HttpGet request = new HttpGet(url);
            request.addHeader(HttpHeaders.CONTENT_TYPE, "application/json");
            CloseableHttpResponse response = httpClient.execute(request);
            return EntityUtils.toString(setResponseData(response));
        }
        catch (Exception ex){
            Log.error("An error occurred during to send GET request. Status code was " +
                    statusCode, ex);
        }
        finally {
            httpClient.close();
        }
        return null;
    }

    public static String sendPost (String url, String json){
        try(CloseableHttpClient httpClient = HttpClients.createDefault()) {
            HttpPost httpPost = new HttpPost(url);
            httpPost.addHeader("content-type", "application/json");
            httpPost.setEntity(new StringEntity(json));
            CloseableHttpResponse response = httpClient.execute(httpPost);
            return EntityUtils.toString(setResponseData(response));
        }
        catch (Exception ex){
            Log.error("An error occurred during to send POST request. Status code was " +
                    statusCode, ex);
        }
        return null;
    }

    private static HttpEntity setResponseData(CloseableHttpResponse response) throws IOException{
        statusCode = response.getStatusLine().getStatusCode();
        HttpEntity entity = response.getEntity();
        if (entity != null) {
            contentType = entity.getContentType().toString();
            return entity;
        }
        return null;
    }

    public static int getStatusCode(){
        return statusCode;
    }

    public static String getContentType() {
        return contentType;
    }
}
