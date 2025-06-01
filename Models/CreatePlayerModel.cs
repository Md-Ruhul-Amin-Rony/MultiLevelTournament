using System.ComponentModel.DataAnnotations;

namespace MultiLevelTournament.Models
{
    public class CreatePlayerModel
    {
        [Required(ErrorMessage = "Player name is required.")]
        public required string Name { get; set; }
        [Range(1, 100, ErrorMessage = "Age must be between 1 and 100.")]
        public int Age { get; set; }
    }
}
