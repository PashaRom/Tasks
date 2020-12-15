package framework.restApi;

import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.apache.http.HttpEntity;
import org.apache.http.HttpHeaders;
import org.apache.http.client.methods.CloseableHttpResponse;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClients;
import org.apache.http.util.EntityUtils;
import framework.logging.Log;

import java.io.IOException;
import java.io.InputStream;

public class Request {
    private static int statusCode;
    private static String contentType;
    public static <T> T get(String url, final Class<T> valueType){
        CloseableHttpClient httpClient = HttpClients.createDefault();
        try{
            ObjectMapper objectMapper = new ObjectMapper();
            HttpGet request = new HttpGet("https://httpbin.org/get");
            request.addHeader(HttpHeaders.CONTENT_TYPE, "application/json");
            CloseableHttpResponse response = httpClient.execute(request);
            statusCode = response.getStatusLine().getStatusCode();
            HttpEntity entity = response.getEntity();
            if (entity != null) {
                contentType = entity.getContentType().toString();
                String result = EntityUtils.toString(entity);
                return objectMapper.readValue(result, valueType);
            }
        }
        catch (Exception ex){
            Log.error("An error occurred during to send get request", ex);
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
            statusCode = response.getStatusLine().getStatusCode();
            HttpEntity entity = response.getEntity();
            if (entity != null) {
                contentType = entity.getContentType().toString();
                //InputStream st = entity.getContent();
                //String result = EntityUtils.toString(entity);
                return objectMapper.readValue(entity.getContent(), valueType);
            }
        }
        catch (Exception ex){
            Log.error("An error occurred during to send get request", ex);
        }
        finally {
            httpClient.close();
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
