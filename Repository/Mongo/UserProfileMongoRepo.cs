using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBot.Config;
using SimpleBot.Repository.Shared.Interfaces;
namespace SimpleBot.Repository.Mongo
{
    public class UserProfileMongoRepo : IUserProfileRepository
    {
        public UserProfile GetProfile(string id)
        {
            var connection = MongoDbConfiguration.Conexao;
            var cliente = new MongoClient(connection);

            var db = cliente.GetDatabase(MongoDbConfiguration.Banco);

            var col = db.GetCollection<BsonDocument>("usuario");

            var filtro = Builders<BsonDocument>.Filter.Eq("id", id);
            var bson = col.Find(filtro).FirstOrDefault();

            if (bson == null)
            {
                return new UserProfile { Id = id, Mensagens = 0 };
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

        public void SetProfile(UserProfile profile)
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
                bson["mensagens"] = profile.Mensagens;
                col.ReplaceOne(filtro, bson);
            }
        }
    }
}