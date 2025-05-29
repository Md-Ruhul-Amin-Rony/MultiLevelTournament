using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiLevelTournament.Data;
using MultiLevelTournament.Models;
using MultiLevelTournament.Services;
using System;

namespace MultiLevelTournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            BaseResponseModel response = new BaseResponseModel();
            try
            {
                var players = await _playerService.GetAllPlayersAsync();
                response.Status = true;
                response.Message = "Players retrieved successfully.";
                response.Data = new { Players = players, Count = players.Count() };
                return Ok(response);
            }
            catch (Exception)
            {
                response.Status = false;
                response.Message = "Something went wrong.";
                return StatusCode(500, response);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = new BaseResponseModel();

            try
            {
                var player = await _playerService.GetPlayerByIdAsync(id);
                if (player == null)
                {
                    response.Status = false;
                    response.Message = $"Player with Id {id} not found.";
                    return NotFound(response);
                }

                response.Status = true;
                response.Message = "Player retrieved successfully.";
                response.Data = player;
                return Ok(response);
            }
            catch (Exception)
            {
                response.Status = false;
                response.Message = "Something went wrong.";
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayer(CreatePlayerModel player)
        {
            BaseResponseModel response = new BaseResponseModel();
            try
            {
                if (!ModelState.IsValid)
                {
                    response.Status = false;
                    response.Message = "Validation failed.";
                    response.Data = ModelState;
                    return BadRequest(response);
                }
                var newPlayer = await _playerService.CreatePlayerAsync(player);
                response.Status = true;
                response.Message = "Player created successfully.";
                response.Data = newPlayer;
                return Ok(response);

            }
            catch (Exception ex)
            {

                response.Status = false;
                response.Message = "Something went wrong";
                return BadRequest(response);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayer(int id, UpdatePlayerModel player)
        {
            BaseResponseModel response = new BaseResponseModel();
            try
            {
                if (!ModelState.IsValid)
                {
                    response.Status = false;
                    response.Message = "VValidation failed";
                    response.Data = ModelState;
                    return BadRequest(response);

                }
                var updatedPlayer = await _playerService.UpdatePlayerAsync(id, player);
                if (updatedPlayer is null)
                {
                    response.Status = false;
                    response.Message = $"Player with {id} not found";
                    return NotFound(response);
                }
                response.Status = true;
                response.Message = "Player updated successfully";
                response.Data = updatedPlayer;
                return Ok(response);

            }
            catch (Exception ex)
            {

                response.Status = false;
                response.Message = "Something went wrong";
                return BadRequest(response); 
            }
           
            
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            BaseResponseModel response = new BaseResponseModel();

            try
            {
                 var deletedPlayer = await _playerService.DeletePlayerAsync(id);
                if (!deletedPlayer)
                {
                    response.Status = false;
                    response.Message = $"Player with Id {id} not found.";
                    return NotFound(response);
                }

                response.Status = true;
                response.Message = "Player deleted successfully.";
                response.Data = null;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = "Something went wrong.";
                return BadRequest(response);
            }
        }


    }
}
