using Moq;
using MultiLevelTournament.Models;
using MultiLevelTournament.Repositories;
using MultiLevelTournament.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLevelTournament.Tests.Services
{
    public class PlayerServiceTests
    {
        private readonly Mock<IPlayerRepository> _mockRepo;
        private readonly PlayerService _service;

        public PlayerServiceTests()
        {
            _mockRepo = new Mock<IPlayerRepository>();
            _service = new PlayerService(_mockRepo.Object);
        }
        [Fact]
        public async Task UpdatePlayerAsync_NameOnly_LeavesAgeUnchanged()
        {
            // ARRANGE 

            // create a player
            var existingPlayer = new Player { Id = 10, Name = "OldName", Age = 30 };

            //  When GetPalyerById(10) is called, return our existingPlayer:
            _mockRepo
                .Setup(r => r.GetPlayerById(10))
                .ReturnsAsync(existingPlayer);

            //  When repository.UpdatePlayer(10, anything) is called, just return that entity back:
            _mockRepo
                .Setup(r => r.UpdatePlayer(10, It.IsAny<Player>()))
                .ReturnsAsync((int id, Player p) => p);

            //  Create an UpdatePlayerModel that only changes the Name, not the Age:
            var updateModel = new UpdatePlayerModel
            {
                Name = "NewName",
                Age = null   // no age update
            };

            // ACT 

            var resultVm = await _service.UpdatePlayerAsync(10, updateModel);

            // ASSERT 

            Assert.NotNull(resultVm);
            Assert.Equal(10, resultVm.Id);
            Assert.Equal("NewName", resultVm.Name);     // name changed
            Assert.Equal(30, resultVm.Age);            // age stayed 30

            // verify that UpdatePlayer(…) was called with the correct patched entity:
            _mockRepo.Verify(r => r.UpdatePlayer(10,
                It.Is<Player>(p => p.Name == "NewName" && p.Age == 30)
            ), Times.Once);
        }
    }
}
