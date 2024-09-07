using Microsoft.Data.SqlClient;

namespace Server.Controllers.Databases
{
    using Server.Controllers.Authentications.Datas;
    using Server.Controllers.Databases.Interfaces;
    using System.Security.Cryptography;
    using System.Text;

    public class SqlDatabase : IDatabase
    {
        public SqlDatabase(string connectionString)
        {
            _connection = new(connectionString);
            _connection.Open();

            using (var createAccountsTable = new SqlCommand(
                $"CREATE TABLE IF NOT EXISTS {AccountsTableName} " +
                "(`Username` VARCHAR(16) NOT NULL, " +
                "`Password` VARCHAR(32) NOT NULL, " +
                "`Status` ENUM('Active', 'Banned', 'Freezed') NOT NULL" +
                "`Permissions` SET('Ban', 'Freeze', 'Comment')" +
                "`CreatedDate` DATE NOT NULL, " +
                "`Country` TEXT NOT NULL" +
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
                "(`GameId` SMALLINT NOT NULL, " +
                "`Username` VARCHAR(16) NOT NULL" +
                ");",
                _connection))
            {
                createChessGamesPlayersTable.ExecuteNonQuery();
            }

            using (var createChessGamesFigureAbilities = new SqlCommand(
                $"CREATE TABLE IF NOT EXISTS {ChessGamesFigureAbilitiesTableName} " +
                "(`GameId` SMALLINT NOT NULL, " +
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

        public void Delete(string username, string password)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string username)
        {
            _connection.Open();
        }

        public string Login(string username, string password)
        {
            if (username.Length > 16)
            {
                throw new ArgumentException("Wrong username.");
            }

            if (password.Length > 32)
            {
                throw new ArgumentException("Wrong password.");
            }


        }

        public void Register(string username, string password)
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

            var checker = new SqlCommand(
                $"SELECT Count(*) FROM {AccountsTableName} WHERE Username LIKE username",
                _connection);
            checker.Parameters.AddWithValue("username", username);

            var readed = checker.ExecuteScalar();
            var usersCount = (int) readed;

            if (usersCount > 1)
            {
                _connection.Close();

                throw new ArgumentException("Your username is busy.");
            }

            var passwordInBytes = Encoding.UTF8.GetBytes(password);
            var encrypted = SHA256.HashData(passwordInBytes);

            var register = new SqlCommand(
                $"INSERT INTO {AccountsTableName} (Username, Password, Status, IsMuted, Permissions, CreatedDate, Country)" +
                "VALUES (username, password, Active, 0, NULL, ",
                _connection);
            register.Parameters.AddWithValue("username", username);
            register.Parameters.AddWithValue("password", encrypted);

            _connection.Close();
        }
    }
}
