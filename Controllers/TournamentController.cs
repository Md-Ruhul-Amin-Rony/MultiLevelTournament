using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiLevelTournament.Data;
using MultiLevelTournament.Models;
using MultiLevelTournament.Services;
using System;

namespace MultiLevelTournament.Controllers
{
    /// <summary>
    /// Manages tournament operations like creation, deletion, retrieval, and player registration.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {

        private readonly ITournamentService _tournamentService;

        public TournamentController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        /// <summary>
        /// Retrieves all tournaments, including nested sub-tournaments and registered players.
        /// </summary>
        /// <returns>List of tournaments</returns>
        /// <response code="200">Tournaments retrieved successfully</response>

        [HttpGet]

        public async Task<IActionResult> GetAllTournaments()
        {
            var result = await _tournamentService.GetAllTournamentsAsync();
            return Ok(new BaseResponseModel
            {
                Status = true,
                Message = "Tournaments retrieved successfully.",
                Data = result
            });
        }
        /// <summary>
        /// Retrieves details for a specific tournament by ID (includes sub-tournaments and players).
        /// </summary>
        /// <param name="id">The ID of the tournament</param>
        /// <returns>TournamentViewModel</returns>
        /// <response code="200">Returns the requested tournament</response>
        /// <response code="404">Tournament not found</response>

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTournamentById(int id)
        {
            var result = await _tournamentService.GetTournamentByIdAsync(id);
            if (result == null)
            {
                return NotFound(new BaseResponseModel
                {
                    Status = false,
                    Message = $"Tournament with id {id} not found."
                });
            }

            return Ok(new BaseResponseModel
            {
                Status = true,
                Message = "Tournament retrieved successfully.",
                Data = result
            });
        }
        /// <summary>
        /// Creates a new tournament. If ParentTournamentId is provided, it will be treated as a sub-tournament.
        /// </summary>
        /// <param name="model">Tournament creation model</param>
        /// <returns>The created tournament with basic details</returns>
        /// <response code="200">Tournament created successfully</response>
        /// <response code="400">Invalid input or maximum nesting level reached</response>

        [HttpPost]
        public async Task<IActionResult> CreateTournament([FromBody] CreateTournamentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponseModel
                {
                    Status = false,
                    Message = "Validation failed.",
                    Data = ModelState
                });
            }

            var created = await _tournamentService.CreateTournamentAsync(model);
            if (created == null)
            {
                return BadRequest(new BaseResponseModel
                {
                    Status = false,
                    Message = "Cannot create tournament. Exceeded 5-level nesting or invalid parent."
                });
            }

            return Ok(new BaseResponseModel
            {
                Status = true,
                Message = "Tournament created successfully.",
                Data = created
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTournament(int id, [FromBody] UpdateTournamentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponseModel
                {
                    Status = false,
                    Message = "Validation failed.",
                    Data = ModelState
                });
            }

            var updated = await _tournamentService.UpdateTournamentAsync(id, model);
            if (updated == null)
            {
                return NotFound(new BaseResponseModel
                {
                    Status = false,
                    Message = $"Tournament with id {id} not found or update failed."
                });
            }

            return Ok(new BaseResponseModel
            {
                Status = true,
                Message = "Tournament updated successfully.",
                Data = updated
            });
        }

        /// <summary>
        /// Deletes a tournament by ID. Will fail if it has sub-tournaments (due to foreign key restriction).
        /// </summary>
        /// <param name="id">Tournament ID</param>
        /// <returns>Deletion result</returns>
        /// <response code="200">Deleted successfully</response>
        /// <response code="404">Tournament not found</response>

        [HttpDelete("{id}")]
      
        public async Task<IActionResult> DeleteTournament(int id)
        {
            try
            {
                var success = await _tournamentService.DeleteTournamentAsync(id);
                if (!success)
                {
                    return NotFound(new BaseResponseModel
                    {
                        Status = false,
                        Message = $"Tournament with id {id} not found."
                    });
                }

                return Ok(new BaseResponseModel
                {
                    Status = true,
                    Message = "Tournament deleted successfully."
                });

            }
            catch (InvalidOperationException ex)
            {

                return BadRequest(new BaseResponseModel
                {
                    Status = false,
                    Message = ex.Message
                });
            }
            
        }
        /// <summary>
        /// Registers a player in a tournament. If it’s a sub-tournament, the player must already be registered in the parent.
        /// </summary>
        /// <param name="tournamentId">ID of the tournament</param>
        /// <param name="playerId">ID of the player</param>
        /// <returns>Registration result</returns>
        /// <response code="200">Player registered successfully</response>
        /// <response code="400">Registration failed (duplicate or parent constraint)</response>

        [HttpPost("{id}/register")]
        public async Task<IActionResult> RegisterPlayer(int id, [FromQuery] int playerId)
        {
            var result = await _tournamentService.RegisterPlayerAsync(id, playerId);
            if (!result)
            {
                return BadRequest(new BaseResponseModel
                {
                    Status = false,
                    Message = "Registration failed. Player might not be in parent tournament or already registered."
                });
            }

            return Ok(new BaseResponseModel
            {
                Status = true,
                Message = "Player registered successfully."
            });
        }

        [HttpGet("{id}/players")]
        public async Task<IActionResult> GetPlayersInTournament(int id)
        {
            var result = await _tournamentService.GetPlayersInTournamentAsync(id);
            return Ok(new BaseResponseModel
            {
                Status = true,
                Message = "Players retrieved successfully.",
                Data = result
            });
        }
    }
}

