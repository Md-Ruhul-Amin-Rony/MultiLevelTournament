using MultiLevelTournament.Models;

namespace MultiLevelTournament.Services
{
    public interface ITournamentService
    {
        Task<TournamentViewModel?> GetTournamentByIdAsync(int id);
        Task<IEnumerable<TournamentViewModel>> GetAllTournamentsAsync();
        Task<TournamentViewModel> CreateTournamentAsync(CreateTournamentModel model);
        Task<TournamentViewModel?> UpdateTournamentAsync(int id, UpdateTournamentModel model);
        Task<bool> DeleteTournamentAsync(int id);

        Task<bool> RegisterPlayerAsync(int tournamentId, int playerId);
        Task<IEnumerable<PlayerViewModel>> GetPlayersInTournamentAsync(int tournamentId);
    }
}
