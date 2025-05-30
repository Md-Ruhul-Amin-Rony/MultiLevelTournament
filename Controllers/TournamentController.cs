using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiLevelTournament.Data;
using MultiLevelTournament.Models;
using MultiLevelTournament.Services;
using System;

namespace MultiLevelTournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {

        private readonly ITournamentService _tournamentService;

        public TournamentController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

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

        [HttpGet("{id}")]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
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

