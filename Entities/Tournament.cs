namespace MultiLevelTournament.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public int? ParentTournamentId { get; set; }
        public Tournament? ParentTournament { get; set; }
        public ICollection<Tournament> SubTournaments { get; set; } = new List<Tournament>();
        public ICollection<TournamentPlayer> PlayerTournaments { get; set; } = new List<TournamentPlayer>();


        //public virtual ICollection<Tournament> SubTournaments { get; set; } = new List<Tournament>();
        //public ICollection<Player> Players { get; set; } = new List<Player>();


        //public virtual ICollection<TournamentPlayer> TournamentPlayers { get; set; } = new List<TournamentPlayer>();

    }
}
