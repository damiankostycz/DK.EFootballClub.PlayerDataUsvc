using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace DK.EFootballClub.PlayerDataUsvc;

public class PlayerDataHttpTrigger
{
    private readonly ILogger _logger;
    private readonly string? _dbConnectionString;

    public PlayerDataHttpTrigger(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<PlayerDataHttpTrigger>();
        _dbConnectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING");
    }

    [Function("GetAllPlayers")]
    public async Task<HttpResponseData> GetAllPlayers(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "players")] HttpRequestData req)
    {
        var response = req.CreateResponse();
        try
        {
            var db = new MongoDbService(_dbConnectionString, "teams_coaches_players_db");
            var players = await db.GetAllPlayersAsync();
            await response.WriteAsJsonAsync(players);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching players");
            response.StatusCode = HttpStatusCode.InternalServerError;
            await response.WriteStringAsync("Internal server error");
            return response;
        }
    }

    [Function("CreatePlayer")]
    public async Task<HttpResponseData> CreatePlayer(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "players")] HttpRequestData req)
    {
        var response = req.CreateResponse();

        try
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var player = JsonSerializer.Deserialize<Player>(body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (player == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync("Invalid player data.");
                return response;
            }

            var db = new MongoDbService(_dbConnectionString, "teams_coaches_players_db");
            var createdPlayer = await db.CreatePlayerAsync(player);

            response.StatusCode = HttpStatusCode.Created;
            response.Headers.Add("Location", $"/api/player/{createdPlayer.PlayerId}");
            await response.WriteAsJsonAsync(createdPlayer);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating player");
            response.StatusCode = HttpStatusCode.InternalServerError;
            await response.WriteStringAsync("Internal server error");
            return response;
        }
    }

    [Function("UpdatePlayer")]
    public async Task<HttpResponseData> UpdatePlayer(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "players/{id}")] HttpRequestData req,
        string id)
    {
        var response = req.CreateResponse();

        try
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var updatedPlayerData = JsonSerializer.Deserialize<Player>(body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (updatedPlayerData == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync("Invalid player data.");
                return response;
            }

            var db = new MongoDbService(_dbConnectionString, "teams_coaches_players_db");
            var updatedPlayer = await db.UpdatePlayerAsync(id, updatedPlayerData);

            if (updatedPlayer == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                await response.WriteStringAsync($"Player with ID {id} not found.");
                return response;
            }

            response.StatusCode = HttpStatusCode.OK;
            await response.WriteAsJsonAsync(updatedPlayer);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating player with ID {id}");
            response.StatusCode = HttpStatusCode.InternalServerError;
            await response.WriteStringAsync("Internal server error");
            return response;
        }
    }

    [Function("DeletePlayer")]
    public async Task<HttpResponseData> DeletePlayer(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "players/{id}")] HttpRequestData req,
        string id)
    {
        var response = req.CreateResponse();

        try
        {
            var db = new MongoDbService(_dbConnectionString, "teams_coaches_players_db");
            var success = await db.DeletePlayerAsync(id);

            if (!success)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                await response.WriteStringAsync($"Player with ID {id} not found.");
                return response;
            }

            response.StatusCode = HttpStatusCode.NoContent;
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting player with ID {id}");
            response.StatusCode = HttpStatusCode.InternalServerError;
            await response.WriteStringAsync("Internal server error");
            return response;
        }
    }
}
