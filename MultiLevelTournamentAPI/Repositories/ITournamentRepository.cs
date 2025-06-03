using MultiLevelTournament.Models;

namespace MultiLevelTournament.Repositories
{
    public interface ITournamentRepository
    {
        /// <summary>
        /// Returns every tournament (roots + subs) in a flat list, each with only immediate SubTournaments loaded.
        /// </summary>
        Task<IEnumerable<Tournament>> GetAllTournamentsFlatAsync();

        /// <summary>
        /// Returns only root tournaments, but each includes up to five nested levels of SubTournaments.
        /// </summary>
        Task<IEnumerable<Tournament>> GetAllTournamentHierarchyAsync();
        Task<Tournament?> GetTournamentByIdAsync(int id);
       
        Task<int> CalculateDepthLevelAsync(int tournamentId);

        Task<Tournament> CreateTournamentAsync(Tournament tournament);
        Task<Tournament?> UpdateTournamentAsync(int id, Tournament updatedTournament);
        Task<bool> DeleteTournamentAsync(int id);

        Task<bool> RegisterPlayerAsync(int tournamentId, int playerId);
      
        Task<IEnumerable<Player>> GetPlayersInTournamentAsync(int tournamentId);
    }
}
