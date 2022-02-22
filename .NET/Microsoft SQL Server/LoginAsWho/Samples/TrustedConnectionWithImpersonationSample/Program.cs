using System;
using ConsoleHelper;
using SqlHelper;
using Microsoft.Data.SqlClient;

namespace TrustedConnectionWithImpersonationSample
{
    class Program
    {
        // Here are the connection parameter. Please adjust them to run this sample at your machine.
        private const string SqlServerAddress = "(local)\\SQLEXPRESS";
        private const string SqlServerDatabase = "sample-database";
        private const string WindowsUser = "sample-sql-user";
        private const string WindowsUserPassword = "$ample-$ql-u$er-pa$$word";

        static void Main(string[] args)
        {
            ColorConsole.WriteStrongLine("TrustedConnectionWithImpersonationSample - Establish a trusted connection with an impersonated user");
            ColorConsole.WriteStrongLine(string.Empty.PadRight(Console.WindowWidth, '-'));

            Console.WriteLine("The application tries to connect to the following SQL Server Instance:");
            Console.WriteLine();
            ColorConsole.WriteHighlightedLabelTextLine("Address", SqlServerAddress);
            ColorConsole.WriteHighlightedLabelTextLine("Database", SqlServerDatabase);
            ColorConsole.WriteHighlightedLabelTextLine("Impersonated Windows-User", WindowsUser);
            ColorConsole.WriteHighlightedLabelTextLine("Impersonated Windows-User Password", WindowsUserPassword);
            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue ...");
            Console.ReadLine();

            Console.WriteLine("Connection String is being built ...");
            Console.WriteLine();
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            sqlConnectionStringBuilder.DataSource = SqlServerAddress;
            sqlConnectionStringBuilder.InitialCatalog = SqlServerDatabase;
            sqlConnectionStringBuilder.IntegratedSecurity = true;
            sqlConnectionStringBuilder.TrustServerCertificate = true;
            string sqlConnectionString = sqlConnectionStringBuilder.ToString();

            // NEVER EVER print or log the connection string like this in your production system!
            // It contains the plaintext password before the connection was opened.
            ColorConsole.WriteHighlightedLabelTextLine("Connection String", sqlConnectionString);
            Console.WriteLine();

            Console.WriteLine("Connection is being established ...");
            Console.WriteLine();
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                sqlConnection.Open();

                // AFTER the connection was opened, it's safe to log or print SqlConnection.ConnectionString!
                // As you can see, the password got removed.
                ColorConsole.WriteHighlightedLabelTextLine("Connected to", sqlConnection.ConnectionString);

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

            ColorConsole.WriteHighlightedLabelTextLine("USER_ID", userId);
            ColorConsole.WriteHighlightedLabelTextLine("USER_NAME", userName);
            ColorConsole.WriteHighlightedLabelTextLine("USER_SID", userSid);
            ColorConsole.WriteHighlightedLabelTextLine("SUSER_ID", sUserId);
            ColorConsole.WriteHighlightedLabelTextLine("SUSER_NAME", sUserName);
            ColorConsole.WriteHighlightedLabelTextLine("SUSER_SID", sUserSid);
            ColorConsole.WriteHighlightedLabelTextLine("SUSER_SNAME", sUserSName);
            ColorConsole.WriteHighlightedLabelTextLine("CURRENT_USER", currentUser);
            ColorConsole.WriteHighlightedLabelTextLine("ORIGINAL_LOGIN", originalLogin);
            ColorConsole.WriteHighlightedLabelTextLine("SESSION_USER", sessionUser);
            ColorConsole.WriteHighlightedLabelTextLine("SYSTEM_USER", systemUser);
        }
    }
}