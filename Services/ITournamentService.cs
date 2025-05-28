using MultiLevelTournament.Models;

namespace MultiLevelTournament.Services
{
    public interface ITournamentService
    {
        Task<BaseResponseModel> GetAllTournamentsAsync();
        Task<BaseResponseModel> GetTournamentByIdAsync(int id);
        Task<BaseResponseModel> CreateTournamentAsync(CreateTournamentModel model);
        Task<BaseResponseModel> UpdateTournamentAsync(int id, UpdateTournamentModel model);
        Task<BaseResponseModel> DeleteTournamentAsync(int id);
        Task<BaseResponseModel> RegisterPlayerAsync(int tournamentId, int playerId);
    }
}
