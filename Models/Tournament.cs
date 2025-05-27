namespace MultiLevelTournament.Models
{
    public class Tournament
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public int? ParentTournamentId { get; set; }
        public virtual Tournament? ParentTournament { get; set; }
        public virtual ICollection<Tournament> SubTournaments { get; set; } = new List<Tournament>();

     
        public virtual ICollection<TournamentPlayer> TournamentPlayers { get; set; } = new List<TournamentPlayer>();

    }
}
