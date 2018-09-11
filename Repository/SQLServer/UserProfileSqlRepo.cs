using SimpleBot.Repository.Shared.Interfaces;
using System;
using Dapper;
using System.Data.SqlClient;

namespace SimpleBot.Repository.SQLServer
{
    public class UserProfileSqlRepo : IUserProfileRepository
    {
        public UserProfile GetProfile(string id)
        {
            string sql = "SELECT * FROM UserProfile WHERE Id = @Id";
            UserProfile user = null;
            using (var connection = new SqlConnection(Config.SqlDbConfiguration.SqlDbConnection))
            {
                var param = new { id };
                user = connection.QueryFirstOrDefault<UserProfile>(sql, param);
            }
            if (user == null)
            {
                return new UserProfile { Id = id, Mensagens = 0 };
            }
            else
            {
                return new UserProfile
                {
                    Id = user.Id,
                    Mensagens = user.Mensagens
                };
            }

        }

        public void SetProfile(UserProfile profile)
        {
            try
            {
                string sql = "SELECT * FROM UserProfile WHERE Id = @Id";
                UserProfile user = null;
                using (var connection = new SqlConnection(Config.SqlDbConfiguration.SqlDbConnection))
                {
                    var param = new { profile.Id };
                    user = connection.QueryFirstOrDefault<UserProfile>(sql, param);
                }
                //user = connection.QueryFirstOrDefault<UserProfile>(sql);
                //user = connection.Get<UserProfileSql>(id);


                if (user == null)
                {
                    var sqlINSERT = $"INSERT INTO UserProfile VALUES (@Id, @Mensagens)";
                    using (var connection = new SqlConnection(Config.SqlDbConfiguration.SqlDbConnection))
                    {
                        var param = new { profile.Id, profile.Mensagens };
                        connection.ExecuteScalar(sqlINSERT, param);
                    }
                }
                else
                {
                    var sqlUpdate = $"UPDATE UserProfile SET Mensagens = @Mensagens WHERE Id = @Id";

                    using (var connection = new SqlConnection(Config.SqlDbConfiguration.SqlDbConnection))
                    {
                        var param = new { profile.Id, profile.Mensagens };
                        connection.ExecuteScalar(sqlUpdate, param);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //private UserProfile InsertProfile()
        //{
        //    string sql = "INSERT UserProfile VALUES()";
        //    UserProfile user = null;

        //    using (var connection = new SqlConnection(Config.SqlDbConfiguration.SqlDbConnection))
        //        user = connection.QueryFirstOrDefault<UserProfile>(sql);


        //    if (user == null)
        //    {
        //        return new UserProfile { Id = id, Mensagens = 1 };
        //    }
        //    else
        //    {
        //        return new UserProfile
        //        {
        //            Id = user.Id,
        //            Mensagens = user.Mensagens
        //        };
        //    }

        //}
    }
}
