using System;
using System.Dynamic;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Aquality.Selenium.Browsers;
namespace Test.Configuration.Common
{
    public class ConfigurationGetter
    {
        public readonly string PathToFile;
        public ConfigurationGetter(string fileName)
        {
            PathToFile = fileName;
            var builder = new ConfigurationBuilder().AddJsonFile(fileName);
            GetConfiguration = builder.Build();            
        }
        public IConfiguration GetConfiguration { get; }

        public T Get<T>(string param)
        {
            dynamic dynamicVarible = 0;
            string readParam = String.Empty;
            string errorMessage = $"The param \"{param}\" is empty or invalid format. \"{param}\" can have one value of false or true.";
            try 
            {
                readParam = GetConfiguration[param];
                switch (typeof(T).Name)
                {
                    case "String":                        
                        if (String.IsNullOrEmpty(readParam))
                            throw new FormatException(errorMessage);
                        else
                            dynamicVarible = (string)readParam;
                        break;
                    case "Boolean":                        
                        if (String.IsNullOrEmpty(readParam))
                            throw new FormatException(errorMessage);
                        else
                            dynamicVarible = Convert.ToBoolean(readParam);
                        break;
                    case "Int32":
                        if (String.IsNullOrEmpty(readParam))
                            throw new FormatException(errorMessage);
                        else
                            dynamicVarible = Convert.ToInt32(readParam);
                        break;
                    default:
                        throw new Exception($"The type {typeof(T).FullName} do not support Test.Framework.Configuration.Get<T>(string param).");
                }
                AqualityServices.Logger.Info($"The param \"{param}\" is writing and has value \"{dynamicVarible}\".");
                return dynamicVarible;
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Fatal($"Unexpected error occurred during to write param {param} from {PathToFile}.", ex);
                return dynamicVarible;
            }        
        }

        public string ConnectionString(string name)
        {
            try
            {
                return GetConfiguration.GetConnectionString(name);
            }
            catch(Exception ex) 
            {
                AqualityServices.Logger.Fatal($"Unexpected error occurred during getting the connection string \"{name}\" from {PathToFile}.", ex);
                return null;
            }
        }
        
        public List<T> GetSectionWithArray<T>(string nameSection)
        {
            AqualityServices.Logger.Info($"Get section \"{nameSection}\" for class \"{typeof(T).ToString()}\"");
            List<T> ts = new List<T>();
            try
            {
                var valuesSection = GetConfiguration.GetSection(nameSection);
                foreach (IConfigurationSection section in valuesSection.GetChildren())
                {
                    ts.Add(section.Get<T>());
                }
                return ts;
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Fatal($"Unexpected error occurred during writing section \"{nameSection}\" for class \"{typeof(T).ToString()}\" from {PathToFile} file.", ex);
            }
            return ts;
        }

        public T GetObjectParam<T>(string nameObject)
            where T: class, new()
        {
            T obj = new T();
            try
            {
                obj = GetConfiguration.GetSection(nameObject).Get<T>();
                return obj;
            }
            catch (Exception ex)
            {
                AqualityServices.Logger.Fatal($"Unexpected error occurred during writing section \"{nameObject}\" for class \"{typeof(T).ToString()}\" from {PathToFile} file.", ex);
                return obj;
            }
        }
    }
}
