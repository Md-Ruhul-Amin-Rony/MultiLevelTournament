using Microsoft.EntityFrameworkCore;
using MultiLevelTournament.Data;
using MultiLevelTournament.Models;

namespace MultiLevelTournament.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {

        private readonly TournamentDbContext _context;

        public PlayerRepository(TournamentDbContext context)
        {
            _context = context;
        }


        public async Task<Player> CreatePlayer(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task<bool> DeletePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return false;
            }
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Player>> GetAllPlayers()
        {
            return await _context.Players
                .Include(p => p.PlayerTournaments)
                .ThenInclude(pt =>pt.Tournament)
                .ToListAsync();
        }

        public async Task<Player?> GetPlayerById(int id)
        {
            return await _context.Players
                .Include(t => t.PlayerTournaments)
                .ThenInclude(pt => pt.Tournament)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

       

        public async Task<Player?> UpdatePlayer(int id, Player updatedPlayer)
        {
            var existingPlayer = await _context.Players.FindAsync(id);

            if (existingPlayer == null)
            {
                return null;

            }
            
            existingPlayer.Name = updatedPlayer.Name;
            existingPlayer.Age = updatedPlayer.Age;
            await _context.SaveChangesAsync();
            return existingPlayer;
        }
        public async Task<IEnumerable<Player>> GetPlayersByTournamentIdAsync(int tournamentId)
        {
            return await _context.TournamentPlayers
                .Where(tp => tp.TournamentId == tournamentId)
                .Select(tp => tp.Player)
                .ToListAsync();
       
        }
    }
}
