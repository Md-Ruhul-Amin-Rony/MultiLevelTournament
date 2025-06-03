using System.ComponentModel.DataAnnotations;

namespace MultiLevelTournament.Models
{
    public class UpdateTournamentModel
    {
        [Required(ErrorMessage = "Tournament name is required.")]
        [MinLength(2, ErrorMessage = "Tournament name must be at least 2 characters.")]
        public string Name { get; set; } = string.Empty;
    }
}
