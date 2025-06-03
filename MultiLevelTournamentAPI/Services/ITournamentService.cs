using MultiLevelTournament.Models;

namespace MultiLevelTournament.Services
{
    public interface ITournamentService
    {

        /// <summary>
        /// Get a flat list of all tournaments (roots + subs), each with only immediate SubTournaments loaded.
        /// </summary>
        Task<IEnumerable<TournamentViewModel>> GetAllTournamentsFlatAsync();

        /// <summary>
        /// Get only root tournaments, but each includes up to five nested levels of SubTournaments.
        /// </summary>
        Task<IEnumerable<TournamentViewModel>> GetAllTournamentHierarchyAsync();

        Task<TournamentViewModel?> GetTournamentByIdAsync(int id);

        Task<TournamentViewModel> CreateTournamentAsync(CreateTournamentModel model);
        Task<TournamentViewModel?> UpdateTournamentAsync(int id, UpdateTournamentModel model);
        Task<bool> DeleteTournamentAsync(int id);

        Task<bool> RegisterPlayerAsync(int tournamentId, int playerId);
        Task<IEnumerable<PlayerViewModel>> GetPlayersInTournamentAsync(int tournamentId);
    }
}
