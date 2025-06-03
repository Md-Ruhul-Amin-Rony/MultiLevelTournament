namespace MultiLevelTournament.Models
{
    /// <summary>
    /// Represents a tournament (with recursive sub-tournaments and player list).
    /// </summary>
    public class TournamentViewModel
    {
        /// <summary>Tournament ID</summary>
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Optional parent tournament ID. Null if this is a root tournament.
        /// </summary>
        public int? ParentTournamentId { get; set; }

        // Recursive sub-tournaments
        /// <summary>List of sub-tournaments (nested recursively).</summary>
        public List<TournamentViewModel> SubTournaments { get; set; } = new List<TournamentViewModel>();

        /// <summary>Players registered in this tournament.</summary>
        public List<PlayerViewModel> Players { get; set; } = new List<PlayerViewModel>();
    }
}
