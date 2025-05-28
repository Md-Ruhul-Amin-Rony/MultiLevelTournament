using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiLevelTournament.Data;
using MultiLevelTournament.Services;
using System;

namespace MultiLevelTournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
      
        private readonly ITournamentService _service;

        public TournamentController( ITournamentService service)
        {
        
            _service = service;
        }
    }
}
