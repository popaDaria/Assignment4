using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Assignment2_3WebApi.Models
{
    public class User
    {
        [Key]
        [JsonPropertyName("userName")]
        public string UserName { get; set; }
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}