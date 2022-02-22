using ConsoleHelper;
using SqlHelper;
using Microsoft.Data.SqlClient;

namespace SqlServerUserSample
{
    class Program
    {
        // Here are the connection parameter. Please adjust them to run this sample at your machine.
        private const string SqlServerAddress = "(local)\\SQLEXPRESS";
        private const string SqlServerDatabase = "sample-database";
        private const string SqlServerUser = "sample-sql-user";
        private const string SqlServerUserPassword = "$ample-$ql-u$er-pa$$word";

        static void Main(string[] args)
        {
            // Print application title
            ColorConsole.WriteStrongLine("SqlServerUserSample - Connect with a SQL Server User");
            ColorConsole.WriteStrongLine(string.Empty.PadRight(Console.WindowWidth, '-'));

            // Print application description and specific values
            Console.WriteLine("The application tries to connect to the following SQL Server Instance:");
            Console.WriteLine();
            ColorConsole.WriteHighlightedLabelTextLine("Address", SqlServerAddress);
            ColorConsole.WriteHighlightedLabelTextLine("Database", SqlServerDatabase);
            ColorConsole.WriteHighlightedLabelTextLine("User", SqlServerUser);
            ColorConsole.WriteHighlightedLabelTextLine("Password", SqlServerUserPassword);
            Console.WriteLine();

            // Wait for the user to confirm that everything was set up
            Console.WriteLine("Press ENTER to continue ...");
            Console.ReadLine();
            Console.WriteLine();

            // Setup local user context
            SetupLocalUserContext();

            // Build Connection String
            string sqlConnectionString = BuildConnectionString();

            // Establish connection
            using (SqlConnection sqlConnection = EstablishSqlConnection(sqlConnectionString))
            {
                // Query user context
                QueryUserContext(sqlConnection);
            }
        }

        private static void SetupLocalUserContext()
        {
            Console.WriteLine("Setting up the local user context ...");
            Console.WriteLine();
            ColorConsole.WriteColoredLine("For this sample, nothing needs to be set up!", ConsoleColor.Magenta);
            Console.WriteLine();
        }

        private static string BuildConnectionString()
        {
            Console.WriteLine("Connection String is being built ...");
            Console.WriteLine();
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            sqlConnectionStringBuilder.DataSource = SqlServerAddress;
            sqlConnectionStringBuilder.InitialCatalog = SqlServerDatabase;
            sqlConnectionStringBuilder.UserID = SqlServerUser;
            sqlConnectionStringBuilder.Password = SqlServerUserPassword;
            sqlConnectionStringBuilder.TrustServerCertificate = true;
            string result = sqlConnectionStringBuilder.ToString();

            // NEVER EVER print or log the connection string like this in your production system!
            // It contains the plaintext password before the connection was opened.
            ColorConsole.WriteHighlightedLabelTextLine("Connection String", result);
            Console.WriteLine();

            return result;
        }

        private static SqlConnection EstablishSqlConnection(string sqlConnectionString)
        {
            Console.WriteLine("Connection is being established ...");
            Console.WriteLine();
            SqlConnection result = new SqlConnection(sqlConnectionString);
            result.Open();

            // AFTER the connection was opened, it's safe to log or print SqlConnection.ConnectionString!
            // As you can see, the password got removed.
            ColorConsole.WriteHighlightedLabelTextLine("Connected to", result.ConnectionString);
            Console.WriteLine();

            return result;
        }

        private static void QueryUserContext(SqlConnection sqlConnection)
        {
            Console.WriteLine("User context is being queried ...");
            Console.WriteLine();

            SqlUserContext.QueryUserContext(
                sqlConnection,
                out string? userId,
                out string? userName,
                out string? userSid,
                out string? sUserId,
                out string? sUserName,
                out string? sUserSid,
                out string? sUserSName,
                out string? currentUser,
                out string? originalLogin,
                out string? sessionUser,
                out string? systemUser);

            ColorConsole.WriteHighlightedLabelTextLine("USER_ID() function", userId);
            ColorConsole.WriteHighlightedLabelTextLine("USER_NAME() function", userName);
            ColorConsole.WriteHighlightedLabelTextLine("USER_SID() function", userSid);
            ColorConsole.WriteHighlightedLabelTextLine("SUSER_ID() function", sUserId);
            ColorConsole.WriteHighlightedLabelTextLine("SUSER_NAME() function", sUserName);
            ColorConsole.WriteHighlightedLabelTextLine("SUSER_SID() function", sUserSid);
            ColorConsole.WriteHighlightedLabelTextLine("SUSER_SNAME() function", sUserSName);
            ColorConsole.WriteHighlightedLabelTextLine("CURRENT_USER variable", currentUser);
            ColorConsole.WriteHighlightedLabelTextLine("ORIGINAL_LOGIN() function", originalLogin);
            ColorConsole.WriteHighlightedLabelTextLine("SESSION_USER variable", sessionUser);
            ColorConsole.WriteHighlightedLabelTextLine("SYSTEM_USER variable", systemUser);
            Console.WriteLine();
        }
    }
}