using Microsoft.Data.SqlClient;

namespace Server.Controllers.Databases
{
    using Server.Controllers.Accounts.Datas.DTOs;
    using Server.Controllers.ChessGame.Datas.DTOs;
    using Server.Controllers.Databases.Interfaces;
    using Server.Models.Abilities.Enums;
    using Server.Models.Figures.Enums;
    using Server.Models.Map.Datas;
    using Server.Services.Accounts.Datas;
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class SqlDatabase : IAsyncDatabase
    {
        public SqlDatabase(string connectionString)
        {
            _connection = new(connectionString);
            _connection.Open();

            using (var createAccountsTable = new SqlCommand(
                $"CREATE TABLE IF NOT EXISTS {AccountsTableName} " +
                "(Username VARCHAR(16) NOT NULL, " +
                "Password TEXT NOT NULL, " +
                "Country TEXT NOT NULL" +
                "Status ENUM('Active', 'Banned', 'Freezed') NOT NULL" +
                "Permissions SET('Ban', 'Freeze', 'Comment')" +
                "CreatedDate DATE NOT NULL, " +
                ");",
                _connection))
            {
                createAccountsTable.ExecuteNonQuery();
            }

            using (var createChessGamesTable = new SqlCommand(
                $"CREATE TABLE IF NOT EXISTS {ChessGamesTableName} " +
                "(ChessGameId SMALLINT NOT NULL AUTO_INCREMENT, " +
                "Status ENUM('InProgress', 'Finished', 'Draw')" +
                "PRIMARY KEY (ChessGameId)" +
                ");",
                _connection))
            {
                createChessGamesTable.ExecuteNonQuery();
            }

            using (var createChessGamesPlayersTable = new SqlCommand(
                $"CREATE TABLE IF NOT EXISTS {ChessGamesPlayersTableName} " +
                $"(ChessGameId SMALLINT NOT NULL FOREIGN KEY REFERENCES {ChessGamesTableName}, " +
                "Username VARCHAR(16) NOT NULL" +
                ");",
                _connection))
            {
                createChessGamesPlayersTable.ExecuteNonQuery();
            }

            using (var createChessGamesFigureAbilities = new SqlCommand(
                $"CREATE TABLE IF NOT EXISTS {ChessGamesFigureAbilitiesTableName} " +
                $"(ChessGameId SMALLINT NOT NULL FOREIGN KEY REFERENCES {ChessGamesTableName}, " +
                "FigureId SMALLINT NOT NULL, " +
                "AbilityId SMALLINT NOT NULL" +
                ");",
                _connection))
            {
                createChessGamesFigureAbilities.ExecuteNonQuery();
            }

            using (var createChessGamesMoves = new SqlCommand(
                $"CREATE TABLE IF NOT EXISTS {ChessGamesFigureMovesTableName} " +
                $"(ChessGameId SMALLINT NOT NULL FOREIGN KEY REFERENCES {ChessGamesTableName}, " +
                "FigurePositionX SMALLINT NOT NULL, " +
                "FigurePositionY SMALLINT NOT NULL" +
                "NewFigurePositionX SMALLINT NOT NULL" +
                "NewFigurePositionY SMALLINT NOT NULL" +
                "MoveNumber SMALLINT NOT NULL" +
                ");",
                _connection))
            {
                createChessGamesMoves.ExecuteNonQuery();
            }

            _connection.Close();
        }

        private readonly SqlConnection _connection;

        private readonly string AccountsTableName = "Accounts";

        private readonly string ChessGamesTableName = "ChessGames";

        private readonly string ChessGamesPlayersTableName = "ChessGamesPlayers";

        private readonly string ChessGamesFigureAbilitiesTableName = "ChessGamesFigureAbilities";

        private readonly string ChessGamesFigureMovesTableName = "ChessGamesMoves";

        public async Task DeleteAccount(string username, string password)
        {
            var encrypted = GetEncrypted(password);

            var deleteCommand = new SqlCommand(
                $"DELETE FROM accounts_table WHERE Username LIKE username AND Password LIKE password",
                _connection);
            deleteCommand.Parameters.AddWithValue("accounts_table", AccountsTableName);
            deleteCommand.Parameters.AddWithValue("username", username);
            deleteCommand.Parameters.AddWithValue("password", encrypted);

            await deleteCommand.ExecuteNonQueryAsync();
        }

        public async Task<Account> GetAccount(string username)
        {
            _connection.Open();

            var findUserCommand = new SqlCommand(
                $"SELECT * FROM accounts_table WHERE Username like username",
                _connection);
            findUserCommand.Parameters.AddWithValue("accounts_table", AccountsTableName);
            findUserCommand.Parameters.AddWithValue("username", username);

            Account account;

            using (var reader = await findUserCommand.ExecuteReaderAsync())
            {
                account = new Account()
                {
                    Username = username,
                    EncryptedPassword = (string) reader["Password"],
                    Country = (string) reader["Country"]
                };
            }

            _connection.Close();

            return account;
        }

        public async Task<ChessGameDTO[]> GetChessGames(string username)
        {
            var account = await GetAccount(username) ?? throw new ArgumentException($"Unknown account with {username} username.");

            _connection.Open();

            var chessGames = new SqlCommand(
                $"SELECT * FROM {ChessGamesFigureAbilitiesTableName}, {ChessGamesFigureMovesTableName} CROSS JOIN " +
                $"(SELECT ChessGameId FROM {ChessGamesPlayersTableName} WHERE Username LIKE username) AS player " +
                "CROSS JOIN ChessGamesPlayers WHERE ChessGamesPlayers.ChessGameId = player.ChessGameId " +
                "ChessGamesMoves.ChessGameId = player.ChessGameId " +
                "ChessGamesFigureAbilities.ChessGameId = player.ChessGameId",
                _connection);
            chessGames.Parameters.AddWithValue("username", username);

            var result = await chessGames.ExecuteReaderAsync();

            if (result.FieldCount == 0)
            {
                return [];
            }

            var chessGamesDtos = new ChessGameDTO[result.FieldCount];

            for (var i = 0; i < chessGamesDtos.Length; i++)
            {
                var chessGameDto = new ChessGameDTO();
                chessGameDto.FiguresAbilities = [];

                var usernames = (string[]) result["Username"];
                var accountDtos = new AccountDTO[usernames.Length];

                var movesNumbers = (int) result["MoveNumber"];
                var movesX = (int[]) result["FigurePositionX"];
                var movesY = (int[]) result["FigurePositionY"];
                var newMovesX = (int[]) result["NewFigurePositionX"];
                var newMovesY = (int[]) result["NewFigurePositionY"];
                var chessGameMoves = new ChessMoveDTO[movesNumbers];

                var figuresIds = (int[]) result["FigureId"];
                var abilitiesIds = (int[]) result["AbilityId"];

                for (var j = 0; j < usernames.Length; j++)
                {
                    var usernameInGame = usernames[j];

                    accountDtos[j] = new AccountDTO()
                    {
                        Username = usernameInGame
                    };
                }

                for (var j = 0;j < movesNumbers; j++)
                {
                    chessGameMoves[j] = new ChessMoveDTO()
                    {
                        OldPosition = new Vector2Int(movesX[j], movesY[j]),
                        NewPosition = new Vector2Int(newMovesX[j], newMovesY[j])
                    };
                }

                for (var j = 0;j < figuresIds.Length; j++)
                {
                    var abilities = new List<AbilityType>(4);

                    for (var k = 0; k < abilitiesIds.Length; k++) 
                    {
                        abilities.Add((AbilityType) abilitiesIds[k]);
                    }

                    chessGameDto.FiguresAbilities.Add((FigureType) figuresIds[j], [.. abilities]);
                }

                chessGameDto.Players = accountDtos;
                chessGameDto.ChessMoves = chessGameMoves;
            }

            return chessGamesDtos;
        }

        public async Task Login(string username, string password)
        {
            if (username.Length > 16 || string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Wrong username.");
            }

            if (password.Length > 32 || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Wrong password.");
            }

            var encrypted = GetEncrypted(password);

            var user = await GetAccount(username);

            if (user is null || encrypted != user.EncryptedPassword)
            {
                throw new ArgumentException("Wrong login or password.");
            }
        }

        public async Task Register(string username, string password, string country)
        {
            if (username.Length > 16)
            {
                throw new ArgumentException("Username must not contain more than 16 characters.");
            }

            if (password.Length > 32)
            {
                throw new ArgumentException("Password must not contain more than 32 characters.");
            }

            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username must not be empty.");
            }

            if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password must not be empty.");
            }

            _connection.Open();

            var user = await GetAccount(username);

            if (user is not null)
            {
                _connection.Close();

                throw new ArgumentException("Your username is busy.");
            }

            var encrypted = GetEncrypted(password);

            var register = new SqlCommand(
                $"INSERT INTO {AccountsTableName} (Username, Password, Country, Status, Permissions, CreatedDate)" +
                "VALUES (username, password, country, Active, ('Comment'), createdDate",
                _connection);
            register.Parameters.AddWithValue("username", username);
            register.Parameters.AddWithValue("password", encrypted);
            register.Parameters.AddWithValue("country", country);
            register.Parameters.AddWithValue("createdDate", DateTime.Now.ToString("yyyy-MM-dd"));

            try
            {
                await register.ExecuteNonQueryAsync();
            }
            catch
            {
                _connection.Close();

                throw;
            }
        }

        private string GetEncrypted(string value)
        {
            var inBytes = Encoding.UTF8.GetBytes(value);
            var encrypted = SHA256.HashData(inBytes);

            return Convert.ToHexString(encrypted);
        }
    }
}
