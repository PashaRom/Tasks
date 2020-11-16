using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using OpenQA.Selenium;
using Aquality.Selenium.Browsers;
using Configuration;
namespace Utilities
{
    public static class FileUtil
    {
        public static string TakeScreenshot(string outDirectory ,string fileName)
        {
            string pathOutDirectory = $"{Directory.GetCurrentDirectory()}{outDirectory}";
            string pathToFile = $"{pathOutDirectory}\\{fileName}";
            if (!Directory.Exists(pathOutDirectory))
                Directory.CreateDirectory(pathOutDirectory);
            if (File.Exists(pathToFile))
                 File.Delete(pathToFile);
            Screenshot screenshot = ((ITakesScreenshot)AqualityServices.Browser.Driver).GetScreenshot();
            screenshot.SaveAsFile(pathToFile);
            return pathToFile;            
        }

        public static Image ConvertStringToImages(string stringBase64, string deleteString = null)
        {
            if (deleteString != null && stringBase64.Contains(deleteString))
                stringBase64 = stringBase64.Replace(deleteString, "");
            byte[] byteArray = Convert.FromBase64String(stringBase64);
            using (MemoryStream mStream = new MemoryStream(byteArray, 0, byteArray.Length))
            {
                return Image.FromStream(mStream, true);
            }            
        }

        public static string SaveImagePngFromBase64(string outDirectoryPath, string fileName, string stringBase64, string deleteString = null)
        {
            Image img = FileUtil.ConvertStringToImages(stringBase64, deleteString);
            string directoryPath = CreateDirectory(outDirectoryPath);
            string filePath = CheckExistFile(directoryPath, fileName);
            img.Save(filePath, ImageFormat.Png);
            return filePath;
        }

        public static string CreateDirectory(string directoryPath)
        {
            string pathOutDirectory = $"{Directory.GetCurrentDirectory()}{directoryPath}";
            if (!Directory.Exists(pathOutDirectory))
                Directory.CreateDirectory(pathOutDirectory);
            return pathOutDirectory;
        }

        public static string CheckExistFile(string directoryPath, string fileName)
        {
            string pathToFile = $"{directoryPath}\\{fileName}";
            if (File.Exists(pathToFile))
                File.Delete(pathToFile);
            return pathToFile;
        }

        public static float CompareImages(string expectedFilePath, string actualFilePath)
        {
            Bitmap expectedPhoto = new Bitmap(expectedFilePath);
            Bitmap actualPhoto = new Bitmap(actualFilePath);
            float diff = 0;
            if (expectedPhoto.Width == actualPhoto.Width && expectedPhoto.Height == actualPhoto.Height)
            {
                for (int i = 0; i < expectedPhoto.Width; i++)
                {
                    for (int j = 0; j < actualPhoto.Height; j++)
                    {
                        Color expectedPixel = expectedPhoto.GetPixel(i, j);
                        Color actualPixel = actualPhoto.GetPixel(i, j);
                        diff += Math.Abs(expectedPixel.R - actualPixel.R);
                        diff += Math.Abs(expectedPixel.G - actualPixel.G);
                        diff += Math.Abs(expectedPixel.B - actualPixel.B);
                    }
                }
                return 100 * (diff / 255) / (expectedPhoto.Width * expectedPhoto.Height * 3);
            }
            return 100;
        }

        public static string GetJavaScriptFromFile(string fileName)
        {
            string filePath = String.Format("{0}{1}\\{2}", Directory.GetCurrentDirectory(), ConfigurationManager.Configuration.Get<string>("javaScriptsDirectory"), fileName);
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                return streamReader.ReadToEnd();
            }
        }
    }


   
}
