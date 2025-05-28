namespace MultiLevelTournament.Models
{
    public class CreateTournamentModel
    {
        public string Name { get; set; }
        public int? ParentTournamentId { get; set; }
    }
}
