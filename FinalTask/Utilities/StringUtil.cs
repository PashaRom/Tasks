using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
namespace Utilities
{
    public static class StringUtil
    {
        public static string GeneraterText(int lenght)
        {
            var random = new Random();
            return new String(Enumerable.Range(0, lenght).Select(n => (Char)(random.Next(65, 90))).ToArray());
        }

        public static string BuilAuthorizationLink(string url, string login, string password, string seporateSimbol = "//")
        {           
            string[] sepor = url.Split(seporateSimbol);
            return $"{sepor[0]}{seporateSimbol}{login}:{password}@{sepor[1]}";
        }
        
        public static int GetIdFromString(string seporate, string fullString)
        {
            string[] SeporateString = fullString.Split(seporate);
            return Convert.ToInt32(SeporateString[SeporateString.Length - 1]);
        }

        public static string GetPartFromString(string seporate, string fullString) 
        {
            string[] SeporateString = fullString.Split(seporate);
            return SeporateString[SeporateString.Length - 1];
        }

        public static T ConvertXmlStringToObject<T>(string xmlString, string xmlElementName = null)            
        {
            xmlString = xmlString.Trim();
            if(xmlString[0].Equals('['))
                throw new Exception("The response string has json format.");
            if(!xmlString[0].Equals('[') && !xmlString[0].Equals('<'))
                throw new Exception("The response string has text format.");
            if (!xmlString[0].Equals('<'))
                throw new Exception("The string does not have xml format.");
            if (!String.IsNullOrEmpty(xmlElementName)) 
            {
                xmlString = xmlString.Insert(0, $"<{xmlElementName}>");
                xmlString = xmlString.Insert(xmlString.Length, $"</{xmlElementName}>");
            }         
            XmlSerializer xmlSerializer = (!String.IsNullOrEmpty(xmlElementName)) ? new XmlSerializer(typeof(T), new XmlRootAttribute(xmlElementName)): new XmlSerializer(typeof(T));            
            return (T)xmlSerializer.Deserialize(new StringReader(xmlString));
        }     
    }
}
