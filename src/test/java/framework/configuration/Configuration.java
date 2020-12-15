package framework.configuration;

import framework.configuration.models.Config;
import framework.configuration.models.Data;

import java.nio.file.Paths;

import static framework.utilities.FileHelper.*;

public class Configuration {

    private static final String TEST_DATA_FILE_NAME = "testingData.json";
    private static final String TEST_CONFIG_FILE_NAME = "testingConfig.json";
    private static final String TEST_FILE_PATH = "\\src\\test\\java\\resources\\";
    private static Config testingConfig;
    private static Data testingData;

    static {
        testingConfig = convertTestingConfigFromFile();
        testingData = convertTestingDataFromFile();
    }

    private static Config convertTestingConfigFromFile(){
        return writeFromJsonFile(createPath(TEST_CONFIG_FILE_NAME), Config.class);
    }

    private static Data convertTestingDataFromFile (){
        return writeFromJsonFile(createPath(TEST_DATA_FILE_NAME), Data.class);
    }

    private static String createPath(String fileName){
        StringBuilder pathToTestingDataFile = new StringBuilder();
        pathToTestingDataFile.append(Paths.get("").toAbsolutePath().normalize().toString());
        pathToTestingDataFile.append(TEST_FILE_PATH);
        pathToTestingDataFile.append(fileName);
        return pathToTestingDataFile.toString();
    }

    public static Config getConfig(){
        return testingConfig;
    }

    public static Data getData(){
        return testingData;
    }
}
