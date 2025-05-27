namespace MultiLevelTournament.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }
        // public List<Tournament> Tournaments { get; set; } = new();
        public virtual ICollection<TournamentPlayer> TournamentRegistrations { get; set; } = new List<TournamentPlayer>();
    }
}
