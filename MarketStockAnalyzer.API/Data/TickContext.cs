using MongoDB.Driver;

namespace MarketStockAnalyzer.API.Data
{
    public class TickContext
    {
        public TickContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);

            Ticks = database.GetCollection<Tick>(configuration["DatabaseSettings:CollectionName"]);
        }

        public IMongoCollection<Tick> Ticks { get; set; }
    }
}
