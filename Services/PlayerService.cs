using MultiLevelTournament.Models;
using MultiLevelTournament.Repositories;

namespace MultiLevelTournament.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<PlayerViewModel> CreatePlayerAsync(CreatePlayerModel model)
        {
            var player = new Player
            {
                Name = model.Name,
                Age = model.Age
            };

            var createdPlayer = await _playerRepository.CreatePlayer(player);

            return new PlayerViewModel
            {
                Id = createdPlayer.Id,
                Name = createdPlayer.Name,
                Age = createdPlayer.Age
            };
        }
        public async Task<PlayerViewModel?> GetPlayerByIdAsync(int id)
        {
            var player = await _playerRepository.GetPlayerById(id);
            if (player == null) return null;

            return new PlayerViewModel
            {
                Id = player.Id,
                Name = player.Name,
                Age = player.Age,
                Tournaments = player.PlayerTournaments.Select(pt => new TournamentInfo
                {
                    Id = pt.Tournament.Id,
                    Name = pt.Tournament.Name,
                }).ToList()


            };
        }



        public async Task<IEnumerable<PlayerViewModel>> GetAllPlayersAsync()
        {
            var players = await _playerRepository.GetAllPlayers();
            return players.Select(p => new PlayerViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Age = p.Age,
                Tournaments = p.PlayerTournaments.Select(pt => new TournamentInfo
                {
                    Id = pt.Tournament.Id,
                    Name = pt.Tournament.Name,
                }).ToList()
            });
        }

       

        public async Task<PlayerViewModel?> UpdatePlayerAsync(int id, UpdatePlayerModel model)
        {

            var existingPlayer = await _playerRepository.GetPlayerById(id);
            if (existingPlayer == null) return null;
            if (model.Name != null)
            {
                existingPlayer.Name = model.Name;
            }

            if (model.Age.HasValue)
            {
                existingPlayer.Age = model.Age.Value;
            }
            var updatedPlayer = await _playerRepository.UpdatePlayer(id, existingPlayer);
            if (updatedPlayer == null)
                return null;

            return new PlayerViewModel
            {
                Id = updatedPlayer.Id,
                Name = updatedPlayer.Name,
                Age = updatedPlayer.Age
            };
        }
        
        public async Task<bool> DeletePlayerAsync(int id)
        {
            return await _playerRepository.DeletePlayer(id);
        }
    }
}
