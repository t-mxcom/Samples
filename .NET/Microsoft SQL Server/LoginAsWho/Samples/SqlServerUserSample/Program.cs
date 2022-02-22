using System;
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
            ColorConsole.WriteStrongLine("SqlServerUserSample - Connect with a SQL Server User");
            ColorConsole.WriteStrongLine(string.Empty.PadRight(Console.WindowWidth, '-'));

            Console.WriteLine("The application tries to connect to the following SQL Server Instance:");
            Console.WriteLine();
            ColorConsole.WriteColoredLabelTextLine("Address", SqlServerAddress);
            ColorConsole.WriteColoredLabelTextLine("Database", SqlServerDatabase);
            ColorConsole.WriteColoredLabelTextLine("User", SqlServerUser);
            ColorConsole.WriteColoredLabelTextLine("Password", SqlServerUserPassword);
            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue ...");
            Console.ReadLine();

            Console.WriteLine("Connection String is being built ...");
            Console.WriteLine();
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            sqlConnectionStringBuilder.DataSource = SqlServerAddress;
            sqlConnectionStringBuilder.InitialCatalog = SqlServerDatabase;
            sqlConnectionStringBuilder.UserID = SqlServerUser;
            sqlConnectionStringBuilder.Password = SqlServerUserPassword;
            sqlConnectionStringBuilder.TrustServerCertificate = true;
            string sqlConnectionString = sqlConnectionStringBuilder.ToString();

            // NEVER EVER print or log the connection string like this in your production system!
            // It contains the plaintext password before the connection was opened.
            ColorConsole.WriteColoredLabelTextLine("Connection String", sqlConnectionString);
            Console.WriteLine();

            Console.WriteLine("Connection is being established ...");
            Console.WriteLine();
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                sqlConnection.Open();

                // AFTER the connection was opened, it's safe to log or print SqlConnection.ConnectionString!
                // As you can see, the password got removed.
                ColorConsole.WriteColoredLabelTextLine("Connected to", sqlConnection.ConnectionString);

                // Query and print user context
                Console.WriteLine("User context is being queried ...");
                Console.WriteLine();
                QueryAndPrintUserContext(sqlConnection);
            }
        }

        private static void QueryAndPrintUserContext(SqlConnection sqlConnection)
        {
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

            ColorConsole.WriteColoredLabelTextLine("USER_ID", userId);
            ColorConsole.WriteColoredLabelTextLine("USER_NAME", userName);
            ColorConsole.WriteColoredLabelTextLine("USER_SID", userSid);
            ColorConsole.WriteColoredLabelTextLine("SUSER_ID", sUserId);
            ColorConsole.WriteColoredLabelTextLine("SUSER_NAME", sUserName);
            ColorConsole.WriteColoredLabelTextLine("SUSER_SID", sUserSid);
            ColorConsole.WriteColoredLabelTextLine("SUSER_SNAME", sUserSName);
            ColorConsole.WriteColoredLabelTextLine("CURRENT_USER", currentUser);
            ColorConsole.WriteColoredLabelTextLine("ORIGINAL_LOGIN", originalLogin);
            ColorConsole.WriteColoredLabelTextLine("SESSION_USER", sessionUser);
            ColorConsole.WriteColoredLabelTextLine("SYSTEM_USER", systemUser);
        }
    }
}