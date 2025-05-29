namespace MultiLevelTournament.Models
{
    public class TournamentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int? ParentTournamentId { get; set; }

        // Recursive sub-tournaments
        public List<TournamentViewModel> SubTournaments { get; set; } = new List<TournamentViewModel>();

        // Optional list of player names or simplified player info
        public List<PlayerViewModel> Players { get; set; } = new List<PlayerViewModel>();
    }
}
