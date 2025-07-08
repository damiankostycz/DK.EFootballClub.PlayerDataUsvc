using MongoDB.Bson;
using MongoDB.Driver;

namespace DK.EFootballClub.PlayerDataUsvc;

public class MongoDbService
{
    private readonly IMongoCollection<Player> _players;

    public MongoDbService(string? connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _players = database.GetCollection<Player>("Players");
    }

    public async Task<List<Player>> GetAllPlayersAsync()
    {
        return await _players.Find(_ => true).ToListAsync();
    }

    private async Task<Player?> GetPlayerByIdAsync(string id)
    {
        var filter = Builders<Player>.Filter.Eq("_id", ObjectId.Parse(id));
        return await _players.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<Player?> CreatePlayerAsync(Player player)
    {
        await _players.InsertOneAsync(player);
        return player; // player.Id powinien być już ustawiony przez MongoDB driver
    }

    public async Task<Player?> UpdatePlayerAsync(string id, Player updatedPlayer)
    {
        var filter = Builders<Player>.Filter.Eq("_id", ObjectId.Parse(id));
        var result = await _players.ReplaceOneAsync(filter, updatedPlayer);

        if (result.ModifiedCount > 0)
        {
            return await GetPlayerByIdAsync(id); // odczytaj i zwróć nowy stan z bazy
        }

        return null;
    }

    public async Task<bool> DeletePlayerAsync(string id)
    {
        var filter = Builders<Player>.Filter.Eq("_id", ObjectId.Parse(id));
        var result = await _players.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }
}