package framework.utilities;

import java.util.Random;

public class StringHelper {
    public static boolean isJsonFormat(String jsonString){
        int firstIndexLeftSquareBracket = jsonString.indexOf('[');
        int lastIndexRightSquareBracket = jsonString.indexOf(']');
        int firstIndexLeftCurlyBracket = jsonString.indexOf('{');
        int lastRightCurlyBracket = jsonString.lastIndexOf('}');
        if((firstIndexLeftSquareBracket == 0 && lastIndexRightSquareBracket == jsonString.length() - 1) &&
        ((firstIndexLeftCurlyBracket - firstIndexLeftSquareBracket) <= 4 &&
                (lastIndexRightSquareBracket - lastRightCurlyBracket) <= 3)){
            return true;
        }
        if(firstIndexLeftCurlyBracket == 0 && lastRightCurlyBracket == jsonString.length() - 1){
            return true;
        }
        return false;
    }

    public static boolean isNullJsonBody(String jsonString){
        return jsonString.matches("^\\{\\}$");
    }

    public static String GenerateString(int length){
        return new Random().ints(48,122).
                filter(i -> (i < 57 || i > 65) && (i < 90 || i > 97)).
                limit(length).
                mapToObj(a -> (char)a).
                collect(StringBuilder::new, StringBuilder::append, StringBuilder::append).
                toString();
    }
}
