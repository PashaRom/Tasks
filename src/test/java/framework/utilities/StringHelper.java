package framework.utilities;

public class StringHelper {
    public static boolean isJsonFormat(String jsonString){
        //boolean isArray =  jsonString.matches("^.");
        int len = jsonString.length();
        int firstIndexLeftSquareBracket = jsonString.indexOf('[');
        int lastIndexRightSquareBracket = jsonString.indexOf(']');
        int firstIndexLeftCurlyBracket = jsonString.indexOf('{');
        int lastRightCurlyBracket = jsonString.lastIndexOf('}');
        if((firstIndexLeftSquareBracket == 0 && lastIndexRightSquareBracket == jsonString.length() - 1) &&
        ((firstIndexLeftCurlyBracket - firstIndexLeftSquareBracket) <= 4 &&
                (lastIndexRightSquareBracket - lastRightCurlyBracket) <= 3))
            return true;
        if(firstIndexLeftCurlyBracket == 0 && lastRightCurlyBracket == jsonString.length() - 1)
            return true;
        return false;
    }

    public static boolean isNullJsonBody(String jsonString){
        return jsonString.matches("^\\{\\}$");
    }
}
