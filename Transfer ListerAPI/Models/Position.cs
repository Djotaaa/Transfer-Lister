using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Transfer_ListerAPI.Models
{
    public class Position
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = string.Empty;

        [Required]
        public string? PositionName { get; set; }

        [JsonIgnore]
        public ICollection<Player> Players { get; set; } = new List<Player>();
    }
}
