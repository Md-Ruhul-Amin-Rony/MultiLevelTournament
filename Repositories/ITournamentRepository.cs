using MultiLevelTournament.Models;

namespace MultiLevelTournament.Repositories
{
    public interface ITournamentRepository
    {
        Task<IEnumerable<Tournament>> GetAllTournamentsAsync();
        Task<Tournament?> GetTournamentByIdAsync(int id);
        Task<Tournament?> GetTournamentByIdWithParentsAsync(int id);

        Task<Tournament> CreateTournamentAsync(Tournament tournament);
        Task<Tournament?> UpdateTournamentAsync(int id, Tournament updatedTournament);
        Task<bool> DeleteTournamentAsync(int id);

        Task<bool> RegisterPlayerAsync(int tournamentId, int playerId);
      
        Task<IEnumerable<Player>> GetPlayersInTournamentAsync(int tournamentId);
    }
}
