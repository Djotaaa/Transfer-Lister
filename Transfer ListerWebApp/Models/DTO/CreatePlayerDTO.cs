using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Transfer_ListerWebApp.Models.DTO
{
    public class CreatePlayerDTO
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public List<string>? PositionIds { get; set; }

        [JsonIgnore]
        public string? PositionsInput { get; set; }

        [Required]
        public DateOnly BornDate { get; set; }

        [Required]
        public float Price { get; set; }
    }
}
