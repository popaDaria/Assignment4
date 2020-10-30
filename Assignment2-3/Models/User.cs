using System.Text.Json.Serialization;

namespace Assignment2_3.Models
{
    public class User
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; }
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}