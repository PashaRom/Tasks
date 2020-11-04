using System;
using System.Drawing;
using System.IO;
using System.Net;
using GroupDocs.Comparison;
using GroupDocs.Comparison.Interfaces;
namespace Utilities
{
    public static class FileUtil
    {
        public static bool DownloadFile(string url, string directoryPath, string fileName)
        {
            CreateDirectories(directoryPath);
            string fullPath = $"{directoryPath}\\{fileName}";
            DeleteExistFile(fullPath);
            WebClient webClient = new WebClient();
            webClient.DownloadFile(url, fullPath);
            return File.Exists(fullPath);
        }

        public static void CreateDirectories(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static void DeleteExistFile(string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public static float ComparePhoto(string expectedFilePath, string actualFilePath)
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
    }
}
