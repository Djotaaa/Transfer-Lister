using System.ComponentModel.DataAnnotations;

namespace Transfer_ListerAPI.Models.DTOs
{
    public class PlayerDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }


        public ICollection<Position> Positions { get; set; } = new List<Position>();

        [Required]
        public DateOnly BornDate { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public DateOnly ListedDate { get; set; }
    }
}
