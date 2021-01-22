package tests.steps;

import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.ObjectMapper;
import framework.configuration.Configuration;
import framework.logging.Log;
import framework.utilities.StringHelper;
import tests.models.*;

public class RestApiSteps {
    private static final String USER_PATH = "$.users.user.";
    private static final String USER_ADDRESS = USER_PATH + "address.";
    private static final String USER_COMPANY = USER_PATH + "company.";
    private static final String USER_ADDRESS_GEO = USER_ADDRESS + "geo.";

    public  static Post createPost(){
        Log.info("Create new post.");
        Post post = new Post();
        post.setUserId(Configuration.getData().read("$.posts.newPost.userId"));
        post.setTitle(StringHelper.GenerateString(15));
        post.setBody(StringHelper.GenerateString(50));
        return post;
    }

    public static User getExpectedUser(){
        Log.info("Get expected user data from the data file.");
        try{
            User user = new User();
            Address address = new Address();
            Company company = new Company();
            Geo geo = new Geo();
            geo.setLat(Configuration.getData().read(USER_ADDRESS_GEO + "lat"));
            geo.setLng(Configuration.getData().read(USER_ADDRESS_GEO + "lng"));
            address.setGeo(geo);
            address.setCity(Configuration.getData().read(USER_ADDRESS + "city"));
            address.setStreet(Configuration.getData().read(USER_ADDRESS + "street"));
            address.setSuite(Configuration.getData().read(USER_ADDRESS + "suite"));
            address.setZipcode(Configuration.getData().read(USER_ADDRESS + "zipcode"));
            company.setName(Configuration.getData().read(USER_COMPANY + "name"));
            company.setCatchPhrase(Configuration.getData().read(USER_COMPANY + "catchPhrase"));
            company.setBs(Configuration.getData().read(USER_COMPANY + "bs"));
            user.setAddress(address);
            user.setCompany(company);
            user.setId(Configuration.getData().read(USER_PATH + "id"));
            user.setName(Configuration.getData().read(USER_PATH + "name"));
            user.setUserName(Configuration.getData().read(USER_PATH + "username"));
            user.setEmail(Configuration.getData().read(USER_PATH + "email"));
            user.setPhone(Configuration.getData().read(USER_PATH + "phone"));
            user.setWebsite(Configuration.getData().read(USER_PATH + "website"));
            return user;
        }
        catch (Exception ex){
            Log.error("An error occurred during create expected user.", ex);
        }
        return null;
    }

    public static String getTestingData(String path){
        Log.info("Read the testing data by path: \"" + path + "\" from the testing data file.");
        return Configuration.getData().read(path).toString();
    }

    public static <T,K> JavaType typeFactory(Class<T> typeList, Class<K> typeObject){
        try{
            return new ObjectMapper().getTypeFactory().constructParametricType(typeList,
                    typeObject);
        }
        catch (Exception ex){
            Log.error(String.format("An error occurred creating JavaType: %s , %s.",
                    typeList, typeObject), ex);
        }
        return null;
    }
}
