using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Assignment2_3.Models {
public class Family {
    
    //[JsonPropertyName("ID")]
    [JsonIgnore]
    public int Id { get; set; }
    [Required]
    [JsonPropertyName("streetName")]
    public string StreetName { get; set; }
    [Required]
    [JsonPropertyName("houseNumber")]
    public int HouseNumber{ get; set; }
    //[MaxLength(2)]
    [JsonPropertyName("adults")]
    public List<Adult> Adults { get; set; }
    //[MaxLength(7)]
    [JsonPropertyName("children")]
    public List<Child> Children{ get; set; }
    //[MaxLength(3)]
    [JsonPropertyName("pets")]
    public List<Pet> Pets{ get; set; }
    
    public Family() {
        Adults = new List<Adult>();    
        Children = new List<Child>();
        Pets = new List<Pet>();
    }
    

}


}