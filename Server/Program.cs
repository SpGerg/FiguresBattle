using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Server.Controllers.Accounts;
using Server.Controllers.Databases;
using Server.Controllers.Databases.Interfaces;
using Server.Services.Accounts;

var builder = WebApplication.CreateBuilder(args);

var connection = Environment.GetEnvironmentVariable("FIGURES_BATTLE_CONNECTION");

if (connection is null)
{
    return;
}

if (AccountsService.SecretKey is null)
{
    return;
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = AccountsService.Issuer,
        ValidateAudience = true,
        ValidAudience = AccountsService.Audience,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(AccountsService.SecretKeyInBytes),
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddSingleton<IAsyncDatabase>(new SqlDatabase(connection));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
