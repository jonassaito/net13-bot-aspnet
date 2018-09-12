using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBot.Config;
using SimpleBot.Repository.Mongo.Models;
using SimpleBot.Repository.Shared.Interfaces;

namespace SimpleBot.Logic
{
    public class UserProfileMongoRepo : IUserProfileRepository
    {
        private IMongoCollection<UserProfileMongo> _collection;

        public UserProfileMongoRepo(string connectionString)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(MongoDbConfiguration.Banco);
            var collection = db.GetCollection<UserProfileMongo>(MongoDbConfiguration.TabelaUsuario);

            this._collection = collection;
        }

        public UserProfile GetProfile(string id)
        {
            var filter = Builders<UserProfileMongo>.Filter.Eq("_id", id);

            var cursor = _collection.Find(filter);

            var profile = cursor.FirstOrDefault();

            return new UserProfile
            {
                Id = profile == null ? id : profile._id,
                Visitas = profile == null ? 0 : profile.Visitas
            };
        }

        public void SetProfile(string id, UserProfile profile)
        {
            var filter = Builders<UserProfileMongo>.Filter.Eq("_id", id);

            var doc = new UserProfileMongo
            {
                _id = profile.Id,
                Visitas = profile.Visitas
            };

            _collection.ReplaceOne(filter, doc, new UpdateOptions { IsUpsert = true });
        }
    }
}