package framework.configuration;

import com.jayway.jsonpath.DocumentContext;
import java.nio.file.Paths;

import static framework.utilities.FileHelper.*;

public class Configuration {
    private static final String TEST_DATA_FILE_NAME = "testingData.json";
    private static final String TEST_CONFIG_FILE_NAME = "testingConfig.json";
    private static final String TEST_FILE_PATH = "\\src\\test\\java\\resources\\";
    private static DocumentContext testingConfig;
    private static DocumentContext testingData;

    static {
        testingConfig = convertTestingConfigFromFile();
        testingData = convertTestingDataFromFile();
    }

    private static DocumentContext convertTestingConfigFromFile(){
        return getJsonContextFomFile(createPath(TEST_CONFIG_FILE_NAME));
    }

    private static DocumentContext convertTestingDataFromFile (){
        return getJsonContextFomFile(createPath(TEST_DATA_FILE_NAME));
    }

    private static String createPath(String fileName){
        StringBuilder pathToTestingDataFile = new StringBuilder();
        pathToTestingDataFile.append(Paths.get("").toAbsolutePath().normalize().toString());
        pathToTestingDataFile.append(TEST_FILE_PATH);
        pathToTestingDataFile.append(fileName);
        return pathToTestingDataFile.toString();
    }

    public static DocumentContext getConfig(){
        return testingConfig;
    }

    public static DocumentContext getData(){
        return testingData;
    }
}
