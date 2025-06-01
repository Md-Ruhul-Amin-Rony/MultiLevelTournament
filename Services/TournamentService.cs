using MultiLevelTournament.Models;
using MultiLevelTournament.Repositories;

namespace MultiLevelTournament.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly ITournamentRepository _tournamentRepository;

        public TournamentService(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }

        public async Task<TournamentViewModel> CreateTournamentAsync(CreateTournamentModel model)
        {
            Tournament newTournament = new Tournament
            {
                Name = model.Name,
                ParentTournamentId = model.ParentTournamentId,
            };
            if (model.ParentTournamentId.HasValue)
            {
                var depth = await _tournamentRepository.CalculateDepthLevelAsync(model.ParentTournamentId.Value);

                if (depth >= 4)
                    return null;

                newTournament.ParentTournamentId = model.ParentTournamentId;

               
            }
            var createdTournament = await _tournamentRepository.CreateTournamentAsync(newTournament);
            return MapToViewModel(createdTournament);
        }

        private TournamentViewModel MapToViewModel(Tournament tournament)
        {
            return new TournamentViewModel
            {
                Id = tournament.Id,
                Name = tournament.Name,
                ParentTournamentId = tournament.ParentTournamentId,
                SubTournaments = tournament.SubTournaments?.Select(MapToViewModel).ToList() ?? new List<TournamentViewModel>(),
                Players = tournament.PlayerTournaments?
                .Select(pt => new PlayerViewModel
                {
                    Id = pt.Player.Id,
                    Name = pt.Player.Name,
                    Age = pt.Player.Age
                    
                }).ToList() ?? new List<PlayerViewModel>()

            };
        }

        public async Task<bool> DeleteTournamentAsync(int id)
        {
            return await _tournamentRepository.DeleteTournamentAsync(id);
        }

        public async Task<IEnumerable<TournamentViewModel>> GetAllTournamentsAsync()
        {
            var tournaments = await _tournamentRepository.GetAllTournamentsAsync();
            return tournaments.Select(MapToViewModel).ToList();
        }

        public async Task<IEnumerable<PlayerViewModel>> GetPlayersInTournamentAsync(int tournamentId)
        {
            var players = await _tournamentRepository.GetPlayersInTournamentAsync(tournamentId);
            return players.Select(p => new PlayerViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Age = p.Age
            }).ToList();
        }

        public async Task<TournamentViewModel?> GetTournamentByIdAsync(int id)
        {
            var tournament = await _tournamentRepository.GetTournamentByIdAsync(id);
            return tournament == null ? null : MapToViewModel(tournament);
        }

        public async Task<bool> RegisterPlayerAsync(int tournamentId, int playerId)
        {
            return await _tournamentRepository.RegisterPlayerAsync(tournamentId, playerId);
        }

        public async Task<TournamentViewModel?> UpdateTournamentAsync(int id, UpdateTournamentModel model)
        {
            var existing = await _tournamentRepository.GetTournamentByIdAsync(id);
            if (existing == null) return null;

            existing.Name = model.Name;
            var updated = await _tournamentRepository.UpdateTournamentAsync(id, existing);
            return updated == null ? null : MapToViewModel(updated);
        }
    }
}
