namespace MultiLevelTournament.Models
{
    public class Player
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }
        public ICollection<TournamentPlayer> PlayerTournaments { get; set; } = new List<TournamentPlayer>();

       
    }
}
