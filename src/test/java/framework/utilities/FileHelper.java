package framework.utilities;

import java.io.FileReader;
import java.io.IOException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.jayway.jsonpath.DocumentContext;
import com.jayway.jsonpath.JsonPath;

public class FileHelper {

    public static <T> T writeFromJsonFile(String pathToFile, final Class<T> valueType){
        ObjectMapper objectMapper = new ObjectMapper();
        try(FileReader reader = new FileReader(pathToFile)){

            return objectMapper.readValue(reader, valueType);
        }
        catch (Exception ex){
            System.out.println(ex.getMessage());
            return null;
        }
    }

    public static DocumentContext getJsonContextFomFile(String pathToFile) {
            return JsonPath.parse(readFromFile(pathToFile));
    }

    public static String readFromFile(String pathToFile){
        StringBuilder stringBuilder = new StringBuilder();
        int symbolFromFile;
        try(FileReader reader = new FileReader(pathToFile))
        {
            while((symbolFromFile = reader.read()) != -1){
                stringBuilder.append((char)symbolFromFile);
            }
            return stringBuilder.toString().toString();
        }
        catch(IOException ex){
            System.out.println(ex.getMessage());
        }
        return null;
    }
}
