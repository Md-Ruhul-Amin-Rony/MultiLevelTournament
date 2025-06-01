namespace MultiLevelTournament.Models
{
    /// <summary>
    /// Simplified tournament representation (ID + Name) for embedding in PlayerViewModel.
    /// </summary>
    public class TournamentInfo
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
