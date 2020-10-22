using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Task7.Utilities.Logging;
namespace Task7.Utilities
{
    public static class JsonUtil
    {
        private static string pathDirectory = $"{Directory.GetCurrentDirectory()}\\Source\\TestingDataFiles";
        public static void Write<T>(IEnumerable<T> listObject, string fileName)            
        {
            if (!Directory.Exists(pathDirectory))
                Directory.CreateDirectory(pathDirectory);            
            string pathOutFile = $"{pathDirectory}\\{fileName}";
            try 
            { 
                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                string jsonString = JsonSerializer.Serialize(listObject, serializeOptions);
                File.WriteAllText(pathOutFile, jsonString); 
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Unexpected error occurred during deserializing json file \"{pathOutFile}\".");                
            }
        }

        public static List<T> Read<T>(string fileName)            
        {
            List<T> listObject = new List<T>();
            string pathOutFile = $"{pathDirectory}\\{fileName}";
            if (File.Exists(pathOutFile))
            {
                try 
                { 
                    string jsonString = File.ReadAllText(pathOutFile);
                    listObject = JsonSerializer.Deserialize<List<T>>(jsonString);                
                    return listObject;
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"Unexpected error occurred during deserializing json file \"{pathOutFile}\".");
                    return listObject;
                }
            }
            return listObject;
        }
    }
}
