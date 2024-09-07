using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace Server.Controllers.Lobbies
{
    using Server.Controllers.Lobbies.Datas;

    [ApiController]
    [Route("api/[controller]")]
    public class LobbyController : ControllerBase
    {
        [HttpGet("create")]
        public async Task<ActionResult<Lobby>> GetNewLobby()
        {

        }
    }
}
