using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers.Figures
{
    using Server.Models.Map.Datas;
    using Server.Services.Map;
    using System.Data.Common;
    using System.Text.Json;

    [Authorize]
    [ApiController]
    [Route("api/figuresbattle/[controller]/{id}")]
    public class ChessGameController(ChessGameService chessGameService)
    {
        [HttpPost("chessmoves")]
        public Task<IResult> PostChessMoves([FromQuery] int id, [FromBody] Vector2Int[] oldPositions, [FromBody] Vector2Int[] newPositions)
        {
            if (oldPositions.Length != newPositions.Length)
            {
                return Task.FromResult(Results.BadRequest("Old positions and new positions must have same length"));
            }

            try
            {
                chessGameService.SetNewPositions(id, oldPositions, newPositions);
            }
            catch (DbException)
            {
                return Task.FromResult(Results.Problem("Internal server error", statusCode: 500));
            }
            catch (Exception exception)
            {
                return Task.FromResult(Results.BadRequest(exception.Message));
            }

            return Task.FromResult(Results.Ok());
        }

        [HttpGet("update")]
        public async Task<IResult> GetChessGameUpdate([FromQuery] int id)
        {
            string serialized;

            try
            {
                var chessMoves = await chessGameService.WaitForChessMove(id);

                if (chessMoves is null)
                {
                    return Results.BadRequest("Game cancelled");
                }

                serialized = JsonSerializer.Serialize(chessMoves);
            }
            catch (DbException)
            {
                return Results.Problem("Internal server error", statusCode: 500);
            }
            catch (Exception exception)
            {
                return Results.BadRequest(exception.Message);
            }

            return Results.Ok(serialized);
        }
    }
}
