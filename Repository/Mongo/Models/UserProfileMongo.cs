using MongoDB.Bson.Serialization.Attributes;

namespace SimpleBot.Repository.Mongo.Models
{
    [BsonIgnoreExtraElements]
    public class UserProfileMongo
    {
        public string _id { get; set; }
        public int Visitas { get; set; }
    }
}