using System.ComponentModel.DataAnnotations;

namespace MultiLevelTournament.Models
{
    public class CreateTournamentModel
    {
        [Required(ErrorMessage = "Tournament name is required.")]
        [MinLength(2, ErrorMessage = "Tournament name must be at least 2 characters.")]
        public string Name { get; set; } = string.Empty;


        /// <summary>
        /// If omitted (null), this becomes a root tournament.
        /// If provided, must refer to an existing tournament.
        /// </summary>
        public int? ParentTournamentId { get; set; }
    }
}
