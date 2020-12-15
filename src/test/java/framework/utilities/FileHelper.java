package framework.utilities;



import java.io.FileReader;
import com.fasterxml.jackson.annotation.*;
import com.fasterxml.jackson.databind.ObjectMapper;

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
}
