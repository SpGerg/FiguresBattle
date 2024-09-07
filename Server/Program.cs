using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Server.Controllers.Authentication;
using Server.Controllers.Databases;
using Server.Controllers.Databases.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connection = Environment.GetEnvironmentVariable("FIGURES_BATTLE_CONNECTION");

if (connection is null)
{
    return;
}

if (AuthenticationController.SecretKey is null)
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
        ValidIssuer = AuthenticationController.Issuer,
        ValidateAudience = true,
        ValidAudience = AuthenticationController.Audience,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(AuthenticationController.SecretKeyInBytes),
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddSingleton<IDatabase>(new SqlDatabase(connection));

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
