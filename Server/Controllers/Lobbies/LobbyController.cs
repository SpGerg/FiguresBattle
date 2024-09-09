using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers.Lobbies
{
    using Microsoft.AspNetCore.Authorization;
    using Server.Controllers.Lobbies.Datas.DTOs;
    using Server.Models.Lobbies.Datas;
    using Server.Services.Lobbies;
    using System.Text.Json;

    [ApiController]
    [Authorize]
    [Route("api/figuresbattle/[controller]")]
    public class LobbyController(LobbiesService lobbiesService) : ControllerBase
    {
        [HttpGet("create")]
        public ActionResult<Lobby> GetNewLobby()
        {
            var lobby = lobbiesService.CreateLobby();

            var lobbyDto = new LobbyDTO()
            {
                Id = lobby.Id,
                MaxPlayers = lobby.MaxPlayers,
                Users = lobby.Accounts,
                FiguresAbilities = lobby.FiguresAbilities
            };

            var serialized = JsonSerializer.Serialize(lobbyDto);

            return Ok(serialized);
        }

        [HttpPost("join")]
        public ActionResult PostJoin(int id)
        {

        }
    }
}
