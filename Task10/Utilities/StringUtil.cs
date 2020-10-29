using System;
using System.Linq;
namespace Utilities
{
    public static class StringUtil
    {
        public static string GeneraterText(int lenght)
        {
            var random = new Random();
            return new String(Enumerable.Range(0, lenght).Select(n => (Char)(random.Next(65, 90))).ToArray());
        }

        public static int GetIdVkByChar(string elementId, char siporateChar)
        {
            string[] seporatedId = elementId.Split(siporateChar);
            return Convert.ToInt32(seporatedId[seporatedId.Length - 1]);
        }

        public static int GetAuthorPostIdVk(string attributValue)
        {
            string[] seporateId = attributValue.Split("id");
            return Convert.ToInt32(seporateId[seporateId.Length - 1]);
        }        
    }
}
