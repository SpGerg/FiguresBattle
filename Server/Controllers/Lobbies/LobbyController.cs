using Microsoft.AspNetCore.Mvc;
using Server.Controllers.Lobbies.Datas;

namespace Server.Controllers.Lobbies
{
    [ApiController]
    [Route("api/[controller]")]
    public class LobbyController : ControllerBase
    {
        private readonly ConcurrentDictionary<Lobby>

        [HttpGet("create")]
        public async Task<ActionResult<Lobby>> GetNewLobby()
        {

        }
    }
}
