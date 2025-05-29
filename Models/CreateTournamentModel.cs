namespace MultiLevelTournament.Models
{
    public class CreateTournamentModel
    {
        public string Name { get; set; } = string.Empty;
        public int? ParentTournamentId { get; set; }
    }
}
