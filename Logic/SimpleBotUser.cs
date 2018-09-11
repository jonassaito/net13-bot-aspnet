using SimpleBot.Repository.Mongo;
using SimpleBot.Repository.Shared.Interfaces;
using SimpleBot.Repository.SQLServer;
using System;

namespace SimpleBot
{
    public class SimpleBotUser
    {
        static IUserProfileRepository _userProfile;

        static SimpleBotUser()
        {
            //_userProfile = new UserProfileMongoRepo();
            _userProfile = new UserProfileSqlRepo();
        }

        public static string Reply(Message message)
        {
            //GravarMensagem(message);

            var id = message.Id;
            var profile = GetProfile(id);

            profile.Mensagens++;

            SetProfile(profile);

            return $"{message.User} disse '{message.Text}' e mandou {profile.Mensagens} mensagens.";
        }


        public static UserProfile GetProfile(string id)
        {
            try
            {
                return _userProfile.GetProfile(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void SetProfile(UserProfile profile)
        {
            _userProfile.SetProfile(profile);
        }
        //public static void GravarMensagem(Message message)
        //{
        //    try
        //    {
        //        var connection = MongoDbConfiguration.Conexao;
        //        var cliente = new MongoClient(connection);

        //        var db = cliente.GetDatabase(MongoDbConfiguration.Banco);

        //        var col = db.GetCollection<BsonDocument>(MongoDbConfiguration.TabelaMensagem);
        //        var bson = new BsonDocument()
        //    {
        //        { "id" , message.Id  },
        //        { "user" , message.User  },
        //        { "text" , message.Text  }
        //    };

        //        col.InsertOne(bson);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
    }
}