using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers.Lobbies
{
    using Server.Services.Lobbies.Datas;

    [ApiController]
    [Route("api/figuresbattle/[controller]")]
    public class LobbyController : ControllerBase
    {
        [HttpGet("create")]
        public async Task<ActionResult<Lobby>> GetNewLobby()
        {

        }
    }
}
