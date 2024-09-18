using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace Server.Controllers.Lobbies
{
    using Server.Controllers.Lobbies.Datas.DTOs;
    using Server.Models.Lobbies.Datas;
    using Server.Services.Lobbies;
    using Server.Services.Lobbies.Datas.DTOs;
    using System.Data.Common;

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

        [HttpGet("update")]
        [Authorize]
        public async Task<IResult> GetLobbyUpdate([FromBody] int id)
        {
            LobbyActionDTO action;

            try
            {
                action = await lobbiesService.WaitForAction(id);
            }
            catch (DbException)
            {
                return Results.Problem("Server internal error", statusCode: 500);
            }
            catch (Exception exception) 
            {
                return Results.BadRequest(exception.Message);
            }

            var serialized = JsonSerializer.Serialize(action);

            return Results.Ok(serialized);
        }
    }
}
