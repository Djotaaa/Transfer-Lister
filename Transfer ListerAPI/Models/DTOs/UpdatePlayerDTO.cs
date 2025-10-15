using System.ComponentModel.DataAnnotations;

namespace Transfer_ListerAPI.Models.DTOs
{
    public class UpdatePlayerDTO
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public List<string>? PositionIds { get; set; }

        [Required]
        public DateOnly BornDate { get; set; }

        [Required]
        public float Price { get; set; }
    }
}
