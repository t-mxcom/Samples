using ConsoleHelper;
using SqlHelper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace TrustedConnectionWithApplicationRoleSample
{
    class Program
    {
        // Here are the connection parameter. Please adjust them to run this sample at your machine.
        private const string SqlServerAddress = "(local)\\SQLEXPRESS";
        private const string SqlServerDatabase = "sample-database";
        private const string ApplicationRole = "sample-application-role";
        private const string ApplicationRolePassword = "$ample-application-role-pa$$word";

        static void Main(string[] args)
        {
            // Print application title
            ColorConsole.WriteStrongLine("TrustedConnectionWithApplicationRoleSample - Establish a trusted connection and activate an Application Role");
            ColorConsole.WriteStrongLine(string.Empty.PadRight(Console.WindowWidth, '-'));

            // Print application description and specific values
            Console.WriteLine("The application tries to connect to the following SQL Server Instance:");
            Console.WriteLine();
            ColorConsole.WriteHighlightedLabelTextLine("Address", SqlServerAddress);
            ColorConsole.WriteHighlightedLabelTextLine("Database", SqlServerDatabase);
            Console.WriteLine();
            Console.WriteLine("Once connected, it tries to activate the following Application Role:");
            Console.WriteLine();
            ColorConsole.WriteHighlightedLabelTextLine("Application Role", ApplicationRole);
            ColorConsole.WriteHighlightedLabelTextLine("Password", ApplicationRolePassword);
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

                // Activate Application Role
                byte[] applicationRoleCookie = ActivateApplicationRole(sqlConnection);

                // Query user context again
                QueryUserContext(sqlConnection);
                ColorConsole.WriteColoredLine("*** Note, that parts of the SQL user context have changed! ***", ConsoleColor.Yellow, intentionLevel: 1);
                Console.WriteLine();

                // Deactivate Application Role
                DeactivateApplicationRole(sqlConnection, applicationRoleCookie);
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
            sqlConnectionStringBuilder.IntegratedSecurity = true;
            sqlConnectionStringBuilder.TrustServerCertificate = true;
            sqlConnectionStringBuilder.TrustServerCertificate = true;
            string result = sqlConnectionStringBuilder.ToString();

            // As therw is no password needed in the Connection String in this case, printing and logging is safe.
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

            // The Connection String doesn't change after opening in this case.
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

        private static byte[] ActivateApplicationRole(SqlConnection sqlConnection)
        {
            Console.WriteLine("Activating Application Role ...");
            Console.WriteLine();

            byte[] result;
            using (SqlCommand sqlCommand = new SqlCommand("sys.sp_setapprole", sqlConnection))
            {
                // SQL command is of type Stored Procedure
                sqlCommand.CommandType = CommandType.StoredProcedure;

                // Add the parameters
                SqlParameter rolenameParameter = new SqlParameter("@rolename", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = ApplicationRole
                };
                SqlParameter passwordParameter = new SqlParameter("@password", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = ApplicationRolePassword
                };
                SqlParameter fCreateCookieParameter = new SqlParameter("@fCreateCookie", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = 1 // true
                };
                SqlParameter cookieParameter = new SqlParameter("@cookie", SqlDbType.VarBinary)
                {
                    Direction = ParameterDirection.Output,
                    Size = 8000
                };
                sqlCommand.Parameters.Add(rolenameParameter);
                sqlCommand.Parameters.Add(passwordParameter);
                sqlCommand.Parameters.Add(fCreateCookieParameter);
                sqlCommand.Parameters.Add(cookieParameter);

                // Execute the Stored Procedure
                sqlCommand.ExecuteNonQuery();

                ColorConsole.WriteHighlightedLabelTextLine("Application Role", "activated");
                Console.WriteLine();

                // Read cookie parameter
                result = (byte[])cookieParameter.Value;
            }

            return result;
        }

        private static void DeactivateApplicationRole(SqlConnection sqlConnection, byte[] cookie)
        {
            Console.WriteLine("Deactivating Application Role ...");
            Console.WriteLine();

            using (SqlCommand sqlCommand = new SqlCommand("sys.sp_unsetapprole", sqlConnection))
            {
                // SQL command is of type Stored Procedure
                sqlCommand.CommandType = CommandType.StoredProcedure;

                // Add the parameters
                SqlParameter cookieParameter = new SqlParameter("@cookie", SqlDbType.VarBinary)
                {
                    Direction = ParameterDirection.Input,
                    Value = cookie
                };
                sqlCommand.Parameters.Add(cookieParameter);

                // Execute the Stored Procedure
                sqlCommand.ExecuteNonQuery();

                ColorConsole.WriteHighlightedLabelTextLine("Application Role", "deactivated");
                Console.WriteLine();
            }
        }
    }
}