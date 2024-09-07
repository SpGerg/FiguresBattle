using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers.Accounts
{
    using Azure.Core;
    using Server.Services.Accounts;
    using System.Data.Common;

    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController(AccountsService accountsService) : ControllerBase
    {
        private readonly AccountsService _accountsService = accountsService;

        [HttpPost("login")]
        public async Task<IResult> PostLogin(string username, string password)
        {
            string jwt;

            try
            {
                jwt = await _accountsService.Login(username, password);
            }
            catch (DbException exception)
            {
                _accountsService.Logger.LogError(exception.ToString());

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
    }
} 
