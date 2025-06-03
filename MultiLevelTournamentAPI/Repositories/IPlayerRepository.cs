using MultiLevelTournament.Models;

namespace MultiLevelTournament.Repositories
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> GetAllPlayers();
        Task<Player?> GetPlayerById(int id);
        Task<Player> CreatePlayer(Player player);
        Task<Player?> UpdatePlayer(int id, Player player);
        Task<bool> DeletePlayer(int id);
        Task<IEnumerable<Player>> GetPlayersByTournamentIdAsync(int tournamentId);

    }
}
