namespace MultiLevelTournament.Models
{
    public class TournamentPlayer
    {
        public int TournamentId { get; set; }
        public int PlayerId { get; set; }


        public  Tournament Tournament { get; set; } = null!;
        public  Player Player { get; set; } = null!;

    }
}
