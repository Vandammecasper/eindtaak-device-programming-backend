var builder = WebApplication.CreateBuilder(args);
var DataCon = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DataBaseSettings>(DataCon);
builder.Services.AddTransient<IMongoContext, MongoContext>();
builder.Services.AddTransient<IFavoriteRepository, FavoriteRepository>();
// builder.Services.AddGraphQLServer().ModigyRequestOptions(opt => IncludeExceptionDetails = true);

var app = builder.Build();
// app.MapGraphQL();

app.MapGet("/", () => "Hello World!");

app.MapGet("/favorites", async (IFavoriteRepository repo) =>
{
    return await repo.GetAllFavorites();
});

app.MapPost("/addfavorite", async (IFavoriteRepository repo, Favorite newfavorite) =>
{
    return await repo.AddFavorite(newfavorite);
});

app.MapDelete("/deletefavorite", async (IFavoriteRepository repo, string name) =>
{
    return await repo.DeleteFavorite(name);
});

app.Run("http://localhost:5000");
