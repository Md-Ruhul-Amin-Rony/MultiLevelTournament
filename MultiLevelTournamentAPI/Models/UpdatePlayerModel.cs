using System.ComponentModel.DataAnnotations;

namespace MultiLevelTournament.Models
{
    public class UpdatePlayerModel
    {
        /// <summary>
        /// New name for the player (optional). If omitted or null, name won’t change.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// New age for the player (optional). If omitted, age won’t change.
        /// </summary>
        [Range(1, 100, ErrorMessage = "Age must be between 1 and 100.")]
        public int? Age { get; set; }
    }
}
