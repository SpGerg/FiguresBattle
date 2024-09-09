using Microsoft.Data.SqlClient;

namespace Server.Controllers.Databases
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Win32;
    using Server.Controllers.Databases.Interfaces;
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
                "(`Username` VARCHAR(16) NOT NULL, " +
                "`Password` TEXT NOT NULL, " +
                "`Country` TEXT NOT NULL" +
                "`Status` ENUM('Active', 'Banned', 'Freezed') NOT NULL" +
                "`Permissions` SET('Ban', 'Freeze', 'Comment')" +
                "`CreatedDate` DATE NOT NULL, " +
                ");",
                _connection))
            {
                createAccountsTable.ExecuteNonQuery();
            }

            using (var createChessGamesTable = new SqlCommand(
                $"CREATE TABLE IF NOT EXISTS {ChessGamesTableName} " +
                "(`Id` SMALLINT NOT NULL AUTO_INCREMENT, " +
                "PRIMARY KEY (id)" +
                ");",
                _connection))
            {
                createChessGamesTable.ExecuteNonQuery();
            }

            using (var createChessGamesPlayersTable = new SqlCommand(
                $"CREATE TABLE IF NOT EXISTS {ChessGamesPlayersTableName} " +
                "(`ChessGameId` SMALLINT NOT NULL, " +
                "`Username` VARCHAR(16) NOT NULL" +
                ");",
                _connection))
            {
                createChessGamesPlayersTable.ExecuteNonQuery();
            }

            using (var createChessGamesFigureAbilities = new SqlCommand(
                $"CREATE TABLE IF NOT EXISTS {ChessGamesFigureAbilitiesTableName} " +
                "(`ChessGameId` SMALLINT NOT NULL, " +
                "`FigureId` SMALLINT NOT NULL, " +
                "`AbilityId` SMALLINT NOT NULL" +
                ");",
                _connection))
            {
                createChessGamesFigureAbilities.ExecuteNonQuery();
            }
            
            _connection.Close();
        }

        private readonly SqlConnection _connection;

        private readonly string AccountsTableName = "Accounts";

        private readonly string ChessGamesTableName = "ChessGames";

        private readonly string ChessGamesPlayersTableName = "ChessGamesPlayers";

        private readonly string ChessGamesFigureAbilitiesTableName = "ChessGamesFigureAbilities";

        private readonly int PasswordNumberInTable = 1;

        private readonly int CountryNumberInTable = 2;

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
            
            var result = await findUserCommand.ExecuteReaderAsync();

            _connection.Close();

            if (result is null)
            {
                return null;
            }

            return new Account()
            {
                Username = username,
                EncryptedPassword = result.GetString(PasswordNumberInTable),
                Country = result.GetString(CountryNumberInTable)
            };
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
                $"INSERT INTO accounts_table (Username, Password, Country, Status, Permissions, CreatedDate)" +
                "VALUES (username, password, country, Active, ('Comment'), createdDate",
                _connection);
            register.Parameters.AddWithValue("accounts_table", AccountsTableName);
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
