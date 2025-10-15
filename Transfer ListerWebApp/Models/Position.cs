using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transfer_ListerWebApp.Models
{
    public class Position
    {
        [Required]
        public string Id { get; set; } = string.Empty;

        [Required]
        public string? PositionName { get; set; }
    }
}
