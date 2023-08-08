using System;

namespace eindtaak_device_programming_backend.Repositories
{
    public interface IFavoriteRepository
    {
        Task<List<Favorite>> GetAllFavorites();
        Task<Favorite> AddFavorite(string newfavorite);
        Task<Favorite> DeleteFavorite(string name);
    }
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly IMongoContext _context;
        public FavoriteRepository(IMongoContext context)
        {
            _context = context;
        }
        public async Task<List<Favorite>> GetAllFavorites()
        {
            return await _context.favoritesCollection.Find(_ => true).ToListAsync();
        }
        public async Task<Favorite> AddFavorite(string newfavorite)
        {
            Favorite favorite = new Favorite
            {
                name = newfavorite
            };

            await _context.favoritesCollection.InsertOneAsync(favorite);

            return favorite;
        }

        public async Task<Favorite> DeleteFavorite(string name)
        {
            return await _context.favoritesCollection.FindOneAndDeleteAsync(Builders<Favorite>.Filter.Eq("name", name));
        }
    }
}
