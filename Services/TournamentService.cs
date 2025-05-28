using Microsoft.EntityFrameworkCore;
using MultiLevelTournament.Data;
using MultiLevelTournament.Models;

namespace MultiLevelTournament.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly TournamentDbContext _context;

        public TournamentService(TournamentDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponseModel> GetAllTournamentsAsync()
        {
            var response = new BaseResponseModel();
            try
            {
                var tournaments = await _context.Tournaments
                    .Include(t => t.SubTournaments)
                    .ToListAsync();

                response.Status = true;
                response.Message = "Success";
                response.Data = new { Tournaments = tournaments, Count = tournaments.Count };
            }
            catch
            {
                response.Status = false;
                response.Message = "Something went wrong.";
            }
            return response;
        }

        public async Task<BaseResponseModel> GetTournamentByIdAsync(int id)
        {
            var response = new BaseResponseModel();
            var tournament = await _context.Tournaments
                .Include(t => t.SubTournaments)
                .Include(t => t.PlayerTournaments)
                    .ThenInclude(pt => pt.Player)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tournament == null)
            {
                response.Status = false;
                response.Message = $"Tournament with Id {id} not found.";
                return response;
            }

            response.Status = true;
            response.Message = "Success";
            response.Data = tournament;
            return response;
        }

        public async Task<BaseResponseModel> CreateTournamentAsync(CreateTournamentModel model)
        {
            var response = new BaseResponseModel();

            if (model.ParentTournamentId != null)
            {
                var parent = await _context.Tournaments.FindAsync(model.ParentTournamentId);
                if (parent == null)
                {
                    response.Status = false;
                    response.Message = "Parent tournament not found.";
                    return response;
                }

                int depth = 1;
                var current = parent;
                while (current.ParentTournamentId != null && depth < 6)
                {
                    current = await _context.Tournaments.FindAsync(current.ParentTournamentId);
                    depth++;
                }

                if (depth >= 5)
                {
                    response.Status = false;
                    response.Message = "Cannot nest tournaments deeper than 5 levels.";
                    return response;
                }
            }

            var newTournament = new Tournament
            {
                Name = model.Name,
                ParentTournamentId = model.ParentTournamentId
            };

            await _context.Tournaments.AddAsync(newTournament);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Tournament created successfully.";
            response.Data = newTournament;
            return response;
        }

        public async Task<BaseResponseModel> UpdateTournamentAsync(int id, UpdateTournamentModel model)
        {
            var response = new BaseResponseModel();
            var tournament = await _context.Tournaments.FindAsync(id);

            if (tournament == null)
            {
                response.Status = false;
                response.Message = "Tournament not found.";
                return response;
            }

            tournament.Name = model.Name;
            _context.Tournaments.Update(tournament);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Tournament updated.";
            response.Data = tournament;
            return response;
        }

        public async Task<BaseResponseModel> DeleteTournamentAsync(int id)
        {
            var response = new BaseResponseModel();
            var tournament = await _context.Tournaments.FindAsync(id);

            if (tournament == null)
            {
                response.Status = false;
                response.Message = "Tournament not found.";
                return response;
            }

            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Deleted successfully.";
            return response;
        }

        public async Task<BaseResponseModel> RegisterPlayerAsync(int tournamentId, int playerId)
        {
            var response = new BaseResponseModel();

            var tournament = await _context.Tournaments.FindAsync(tournamentId);
            var player = await _context.Players.FindAsync(playerId);

            if (tournament == null || player == null)
            {
                response.Status = false;
                response.Message = "Player or Tournament not found.";
                return response;
            }

            if (tournament.ParentTournamentId != null)
            {
                bool isInParent = await _context.TournamentPlayers
                    .AnyAsync(pt => pt.PlayerId == playerId && pt.TournamentId == tournament.ParentTournamentId);

                if (!isInParent)
                {
                    response.Status = false;
                    response.Message = "Player must be registered in parent tournament.";
                    return response;
                }
            }

            bool alreadyRegistered = await _context.TournamentPlayers
                .AnyAsync(pt => pt.PlayerId == playerId && pt.TournamentId == tournamentId);

            if (alreadyRegistered)
            {
                response.Status = false;
                response.Message = "Player already registered.";
                return response;
            }

            var registration = new TournamentPlayer
            {
                TournamentId = tournamentId,
                PlayerId = playerId
            };

            await _context.TournamentPlayers.AddAsync(registration);
            await _context.SaveChangesAsync();

            response.Status = true;
            response.Message = "Player registered successfully.";
            return response;
        }
    }
}
