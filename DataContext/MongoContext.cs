using System;

namespace eindtaak_device_programming_backend.DataContext
{
    public interface IMongoContext
    {
        IMongoDatabase Database { get; }
        IMongoCollection<Favorite> favoritesCollection { get; }
    }
    public class MongoContext : IMongoContext
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;

        private readonly DataBaseSettings _settings;

        public IMongoClient Client
        {
            get
            {
                return _client;
            }
        }
        public IMongoDatabase Database => _database;

        public MongoContext(IOptions<DataBaseSettings> dbOptions)
        {
            _settings = dbOptions.Value;
            _client = new MongoClient(_settings.ConnectionString);
            _database = _client.GetDatabase(_settings.DatabaseName);
        }

        public IMongoCollection<Favorite> favoritesCollection
        {
            get
            {
                return _database.GetCollection<Favorite>(_settings.favoritesCollection);
            }
        }
    }
}
