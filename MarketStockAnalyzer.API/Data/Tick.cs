using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MarketStockAnalyzer.API.Data
{
    public class Tick
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }

    }
}
