using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiLevelTournament.Data;
using MultiLevelTournament.Models;
using System;

namespace MultiLevelTournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly TournamentDbContext _context;

        public PlayerController(TournamentDbContext context)
        {
            _context = context;
            
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            BaseResponseModel response = new BaseResponseModel();
            try
            {
                var playerCount = await _context.Players.CountAsync();
                var playerList = await _context.Players.ToListAsync();
                response.Status = true;
                response.Message = "Success";
                response.Data = new { Person = playerList, Count = playerCount };
                return Ok(response);

            }
            catch (Exception ex) 
            {

                response.Status = false;
                response.Message = "Something went wrong";
                return BadRequest(response);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreatePlayer(CreatePlayerModel player)
        {
            BaseResponseModel response = new BaseResponseModel();
            try
            {
                if (ModelState.IsValid)
                {
                    var newPlayer = new Player()
                    {
                        Name = player.Name,
                        Age = player.Age
                    };
                    await _context.Players.AddAsync(newPlayer);
                    await _context.SaveChangesAsync();

                    //  player.Id = postedModel.Id;
                    var createdPlayer = new
                    {
                        Id = newPlayer.Id,
                        Name = newPlayer.Name,
                        Age= newPlayer.Age

                    };
                    response.Status = true;
                    response.Message = "Created Successfully";
                    response.Data = createdPlayer;

                    return Ok(response);

                }
                else
                {
                    response.Status = false;
                    response.Message = "Validation failed";
                    response.Data = ModelState;
                    return BadRequest(response);
                }

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
                if (ModelState.IsValid)
                {
                    var existingPlayer = await _context.Players.FindAsync(id);
                    if (existingPlayer == null)
                    {
                        response.Status = false;
                        response.Message = $"Player with Id {id} not found.";
                        return NotFound(response);
                    }

                    existingPlayer.Name = player.Name;
                    existingPlayer.Age = player.Age;

                    _context.Players.Update(existingPlayer);
                    await _context.SaveChangesAsync();

                    var updatedPlayer = new
                    {
                        Id = existingPlayer.Id,
                        Name = existingPlayer.Name,
                        Age = existingPlayer.Age
                    };

                    response.Status = true;
                    response.Message = "Updated successfully";
                    response.Data = updatedPlayer;

                    return Ok(response);
                }
                else
                {
                    response.Status = false;
                    response.Message = "Validation failed";
                    response.Data = ModelState;
                    return BadRequest(response);
                }
            }
            catch (Exception)
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
                var existingPlayer = await _context.Players.FindAsync(id);
                if (existingPlayer == null)
                {
                    response.Status = false;
                    response.Message = $"Player with Id {id} not found.";
                    return NotFound(response);
                }

                _context.Players.Remove(existingPlayer);
                await _context.SaveChangesAsync();

                response.Status = true;
                response.Message = "Player deleted successfully.";
                return Ok(response);
            }
            catch (Exception)
            {
                response.Status = false;
                response.Message = "Something went wrong.";
                return BadRequest(response);
            }
        }


    }
}
