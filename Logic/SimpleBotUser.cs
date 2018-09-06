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
            profile.Visitas++;

            SetProfile(profile);

            return $"{message.User} disse '{message.Text}' e mandou {profile.Visitas} mensagens.";
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
                    return new UserProfile { Id = id, Visitas = 0 };
                }
                else
                {
                    return new UserProfile
                    {
                        Id = bson["id"].ToString(),
                        Visitas = bson["visitas"].ToInt32()
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


            var col = db.GetCollection<BsonDocument>("usuario");



            var filtro = Builders<BsonDocument>.Filter.Eq("id", profile.Id);
            var bson = col.Find(filtro).FirstOrDefault();

            if (bson == null)
            {
                col.InsertOne(new BsonDocument { { "id", profile.Id }, { "visitas", 0 } });
            }
            else
            {
                bson["visitas"] = profile.Visitas;
                col.ReplaceOne(filtro, bson);
            }


        }
        public static void GravarMensagem(Message message)
        {

            var connection = MongoDbConfiguration.Conexao;
            var cliente = new MongoClient(connection);

            var db = cliente.GetDatabase(MongoDbConfiguration.Banco);

            var col = db.GetCollection<BsonDocument>("message");
            var bson = new BsonDocument()
            {
                { "id" , message.Id  },
                { "user" , message.User  },
                { "text" , message.Text  }
            };

            col.InsertOne(bson);

        }
    }
}