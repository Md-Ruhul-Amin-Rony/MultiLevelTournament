using MultiLevelTournament.Models;

namespace MultiLevelTournament.Services
{
    public interface IPlayerService
    {
        Task<IEnumerable<PlayerViewModel>> GetAllPlayersAsync();
        Task<PlayerViewModel?> GetPlayerByIdAsync(int id);
        Task<PlayerViewModel> CreatePlayerAsync(CreatePlayerModel model);
        Task<PlayerViewModel?> UpdatePlayerAsync(int id, UpdatePlayerModel model);
        Task<bool> DeletePlayerAsync(int id);
    }
}
