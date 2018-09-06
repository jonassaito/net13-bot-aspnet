using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBot.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot
{
    public class SimpleBotUser
    {

        public static string Reply(Message message)
        {
            GravarMensagem(message);

            var id = message.Id;
            var profile = GetProfile(id);

            SetProfile(profile);

            return $"{message.User} disse '{message.Text}' e mandou {profile.Mensagens} mensagens.";
        }


        public static UserProfile GetProfile(string id)
        {
            try
            {
                var connection = MongoDbConfiguration.Conexao;
                var cliente = new MongoClient(connection);

                var db = cliente.GetDatabase(MongoDbConfiguration.Banco);

                var col = db.GetCollection<BsonDocument>("usuario");

                var filtro = Builders<BsonDocument>.Filter.Eq("id", id);
                var bson = col.Find(filtro).FirstOrDefault();

                if (bson == null)
                {
                    return new UserProfile { Id = id, Mensagens = 1 };
                }
                else
                {
                    return new UserProfile
                    {
                        Id = bson["id"].ToString(),
                        Mensagens = bson["mensagens"].ToInt32()
                    };
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void SetProfile(UserProfile profile)
        {
            var connection = MongoDbConfiguration.Conexao;
            var cliente = new MongoClient(connection);

            var db = cliente.GetDatabase(MongoDbConfiguration.Banco);

            var col = db.GetCollection<BsonDocument>(MongoDbConfiguration.TabelaUsuario);

            var filtro = Builders<BsonDocument>.Filter.Eq("id", profile.Id);
            var bson = col.Find(filtro).FirstOrDefault();

            if (bson == null)
            {
                col.InsertOne(new BsonDocument { { "id", profile.Id }, { "mensagens", 1 } });
            }
            else
            {
                bson["mensagens"] = profile.Mensagens+1;
                col.ReplaceOne(filtro, bson);
            }
        }
        public static void GravarMensagem(Message message)
        {
            try
            {
                var connection = MongoDbConfiguration.Conexao;
                var cliente = new MongoClient(connection);

                var db = cliente.GetDatabase(MongoDbConfiguration.Banco);

                var col = db.GetCollection<BsonDocument>(MongoDbConfiguration.TabelaMensagem);
                var bson = new BsonDocument()
            {
                { "id" , message.Id  },
                { "user" , message.User  },
                { "text" , message.Text  }
            };

                col.InsertOne(bson);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}