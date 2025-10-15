using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transfer_ListerAPI.Models
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
