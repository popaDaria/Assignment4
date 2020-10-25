using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Assignment2_3.Models {
public class Pet {
    
    [JsonPropertyName("ID")]
    public int Id { get; set; }
    [Required]
    public string Species { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int Age { get; set; }
}
}