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
       
        //Helper method to check the depth level of the tournament
        public int GetDepthLevel()
        {
            int level = 0;
            var currentTournament = this;
            while (currentTournament.ParentTournament is not null)
            {
                level++;
                currentTournament = currentTournament.ParentTournament;

            }
            return level;
        }
       



    }
}
