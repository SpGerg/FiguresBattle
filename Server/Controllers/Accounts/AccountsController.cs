using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace Server.Controllers.Accounts
{
    using Server.Controllers.Accounts.Datas.DTOs;
    using Server.Services.Accounts;
    using Server.Services.Accounts.Datas;
    using System.Text.Json;

    [ApiController]
    [Route("api/figuresbattle/[controller]")]
    public class AccountsController(AccountsService accountsService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IResult> PostLogin([FromQuery] string username, [FromQuery] string password)
        {
            string jwt;

            try
            {
                jwt = await accountsService.Login(username, password);
            }
            catch (DbException exception)
            {
                accountsService.Logger.LogError(exception.ToString());

                return Results.Problem("Internal server error", statusCode: 500);
            }
            catch (Exception exception)
            {
                return Results.BadRequest(exception.Message);
            }

            var response = new
            {
                access_token = jwt
            };

            return Results.Ok(response);
        }

        [HttpPost("get_account")]
        public async Task<IResult> PostGetUser([FromQuery] string username)
        {
            Account account;

            try
            {
                account = await accountsService.GetUser(username);
            }
            catch (DbException exception)
            {
                accountsService.Logger.LogError($"{username}: {exception.Message}");

                return Results.Problem("Server internal error", statusCode: 500);
            }
            catch (Exception exception)
            {
                return Results.BadRequest(exception.Message);
            }

            var accountDto = new AccountDTO()
            {
                Username = username
            };

            var serialized = JsonSerializer.Serialize(accountDto);

            return Results.Ok(serialized);
        }
    }
} 
