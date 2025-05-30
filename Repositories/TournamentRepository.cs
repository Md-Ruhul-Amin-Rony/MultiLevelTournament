using Microsoft.EntityFrameworkCore;
using MultiLevelTournament.Data;
using MultiLevelTournament.Models;

namespace MultiLevelTournament.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {

        private readonly TournamentDbContext _context;

        public TournamentRepository(TournamentDbContext context)
        {
            _context = context;
        }

        public async Task<Tournament> CreateTournamentAsync(Tournament tournament)
        {
            _context.Tournaments.Add(tournament);
            await _context.SaveChangesAsync();
            return tournament;
        }
        public async Task<Tournament?> GetTournamentByIdAsync(int id)
        {
            return await _context.Tournaments
                .Include(t => t.SubTournaments)
                .Include(t => t.PlayerTournaments)
                    .ThenInclude(pt => pt.Player)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<Tournament?> GetTournamentByIdWithParentsAsync(int id)
        {
            var current = await _context.Tournaments
        .Include(t => t.ParentTournament)
        .FirstOrDefaultAsync(t => t.Id == id);

            // Manually walk up the tree to hydrate full parent chain
            var root = current;
            while (root?.ParentTournamentId != null)
            {
                root.ParentTournament = await _context.Tournaments
                    .Include(t => t.ParentTournament)
                    .FirstOrDefaultAsync(t => t.Id == root.ParentTournamentId);
                root = root.ParentTournament;
            }

            return current;
        }

        public async Task<IEnumerable<Tournament>> GetAllTournamentsAsync()
        {
            return await _context.Tournaments
                .AsNoTracking()
                 .Include(t => t.SubTournaments)
                 .Include(t => t.PlayerTournaments)
                     .ThenInclude(pt => pt.Player)
                 .ToListAsync();
        }
        public async Task<Tournament?> UpdateTournamentAsync(int id, Tournament updatedTournament)
        {
            var existingTournament = await _context.Tournaments.FindAsync(id);
            if (existingTournament == null)
                return null;
            existingTournament.Name = updatedTournament.Name;
            existingTournament.ParentTournamentId = updatedTournament.ParentTournamentId;
            await _context.SaveChangesAsync();
            return existingTournament;
        }

        public async Task<bool> DeleteTournamentAsync(int id)
        {
            var selectedTournament = await _context.Tournaments.FindAsync(id);
            if (selectedTournament is null)
                return false;
            _context.Tournaments.Remove(selectedTournament);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RegisterPlayerAsync(int tournamentId, int playerId)
        {
            var tournament = await _context.Tournaments
                .Include(t => t.ParentTournament)
                .Include(t => t.PlayerTournaments)
                .FirstOrDefaultAsync(t => t.Id == tournamentId);
            if (tournament is null)
                return false;
            bool alreadyRegistered = await _context.TournamentPlayers
                .AnyAsync(tp => tp.TournamentId == tournamentId && tp.PlayerId == playerId);
            if (alreadyRegistered)
                return false;


            //if tournament has a parent, player must be registerd in parent
            if (tournament.ParentTournamentId.HasValue)
            {
                bool isInParent = await _context.TournamentPlayers
                    .AnyAsync(tp => tp.TournamentId == tournament.ParentTournamentId && tp.PlayerId == playerId);

                if (!isInParent)
                    return false;
            }
            var tournamentPlayer = new TournamentPlayer
            {
                TournamentId = tournamentId,
                PlayerId = playerId
            };
            _context.TournamentPlayers.Add(tournamentPlayer);
            await _context.SaveChangesAsync();
            return true;

        }
        
        public async Task<IEnumerable<Player>> GetPlayersInTournamentAsync(int tournamentId)
        {
            return await _context.TournamentPlayers
                .Where(tp => tp.TournamentId == tournamentId)
                .Select(tp => tp.Player)
                .ToListAsync();
        }


    }
}
