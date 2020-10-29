using System.Text;
using System.Text.Json.Serialization;
using OpenQA.Selenium;
namespace Task10.Testing.Models.Cookies
{
    public class AppCookie
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("value")]
        public string Value { get; set; }

        public override bool Equals(object obj)
        {
            AppCookie appCookie = obj as AppCookie;
            if (this.Name.Equals(appCookie.Name) && this.Value.Equals(appCookie.Value))
                return true;
            else
                return false;
        }

        public static implicit operator AppCookie(Cookie cookie)
        {
            return new AppCookie { Name = cookie.Name, Value = cookie.Value };
        }

        public static explicit operator Cookie(AppCookie appCookie)
        {
            return new Cookie(appCookie.Name, appCookie.Value);
        }

        public override string ToString()
        {
            StringBuilder appCookieStringBuilder = new StringBuilder();
            appCookieStringBuilder.Append("AppCookie{");
            appCookieStringBuilder.Append($"Name=\"{this.Name}\" ,");
            appCookieStringBuilder.Append($"Value=\"{this.Value}\"");
            appCookieStringBuilder.Append("}");
            return appCookieStringBuilder.ToString();
        }
    }   
}
