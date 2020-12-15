package framework.logging;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

public class Log {
    private static final String LOG4J2_CONFIGURATION_FILE = "log4j.configurationFile";
    private static final String PATH_TO_LOG4J2_XML = "src\\test\\java\\resources\\log4j2.xml";
    private static Logger loger;
    static {
        String s = System.getProperty(LOG4J2_CONFIGURATION_FILE);
        if(System.getProperty(LOG4J2_CONFIGURATION_FILE) == null)
            System.setProperty(LOG4J2_CONFIGURATION_FILE,PATH_TO_LOG4J2_XML);
        loger = LogManager.getLogger(Log.class);
    }

    public static void error(String message){
        String s = System.getProperty(LOG4J2_CONFIGURATION_FILE);
        loger.error(message);
    }

    public static void error(String message, Exception exception){
        loger.error(String.format("MESSAGE: %s, EXCEPTION: %s",message, exception.getMessage()));
    }

    public static void info(String message){
        loger.info(message);
    }

    public static void step(int numberOfStep, String message){
        loger.info(String.format("STEP: %d MESSAGE: %s", numberOfStep,message));
    }

}
