using System;
using Microsoft.Data.SqlClient;

namespace SqlHelper
{
    public static class SqlUserContext
    {
        private const string SqlCommandQueryUserContext = @"
SELECT USER_ID() AS [UserId],
       USER_NAME() AS [UserName],
       USER_SID() AS [UserSid],
       SUSER_ID() AS [SUserId],
       SUSER_NAME() AS [SUserName],
       SUSER_SID() AS [SUserSid],
       SUSER_SNAME() AS [SUserSName],
       CURRENT_USER AS [CurrentUser],
       ORIGINAL_LOGIN() AS [OriginalLogin],
       SESSION_USER AS [SessionUser],
       SYSTEM_USER AS [SystemUser]";



        public static void QueryUserContext(
            SqlConnection sqlConnection,
            out string userId,
            out string userName,
            out string userSid,
            out string sUserId,
            out string sUserName,
            out string sUserSid,
            out string sUserSName,
            out string currentUser,
            out string originalLogin,
            out string sessionUser,
            out string systemUser)
        {
            // initialize out-parameters
            userId = userName = userSid = sUserId = sUserName = sUserSid = sUserSName = currentUser = originalLogin = sessionUser = systemUser = null;
            using (SqlCommand sqlCommand = new SqlCommand(SqlCommandQueryUserContext, sqlConnection))
            {
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        if (sqlDataReader.Read())
                        {
                            userId = sqlDataReader["UserId"].ToString();
                            userName = sqlDataReader["UserName"].ToString();
                            userSid = sqlDataReader["UserSid"].ToString();
                            sUserId = sqlDataReader["SUserId"].ToString();
                            sUserName = sqlDataReader["SUserName"].ToString();
                            sUserSid = sqlDataReader["SUserSid"].ToString();
                            sUserSName = sqlDataReader["SUserSName"].ToString();
                            currentUser = sqlDataReader["CurrentUser"].ToString();
                            originalLogin = sqlDataReader["OriginalLogin"].ToString();
                            sessionUser = sqlDataReader["SessionUser"].ToString();
                            systemUser = sqlDataReader["SystemUser"].ToString();
                        }
                    }
                }
            }
        }
    }
}
