package framework.logging;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.apache.log4j.BasicConfigurator;

public class Log {
    private static final String LOG4J2_CONFIGURATION_FILE = "log4j.configurationFile";
    private static final Logger loger = LogManager.getLogger(Log.class);

    static{
        BasicConfigurator.configure();
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
