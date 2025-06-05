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
    public class TournamentServiceTests
    {
        private readonly Mock<ITournamentRepository> _mockRepo;
        private readonly TournamentService _service;

        public TournamentServiceTests()
        {

            _mockRepo = new Mock<ITournamentRepository>();
            _service = new TournamentService(_mockRepo.Object);
        }

        [Fact]
        public async Task CreateTournamentAsync_ParentAtDepth4_ReturnsNull()
        {
            // SUMMARY: If parent’s depth is already 4, service should return null and never call CreateTournamentAsync.

            // Arrange
            var model = new CreateTournamentModel
            {
                Name = "TooDeep",
                ParentTournamentId = 42
            };

            // Setup repository to indicate parent (ID=42) has depth = 4
            _mockRepo
                .Setup(r => r.CalculateDepthLevelAsync(42))
                .ReturnsAsync(4);

            // Act
            var result = await _service.CreateTournamentAsync(model);

            // Assert
            Assert.Null(result);
            // Ensure service never invoked repository.CreateTournamentAsync
            _mockRepo.Verify(r => r.CreateTournamentAsync(It.IsAny<Tournament>()), Times.Never);
        }
    }
}
