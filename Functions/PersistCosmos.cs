using System.Text.Json;
using Azure.Cosmos;

namespace Greasyfood_datapipeline.Functions;

public class TestPersistor
{
    private readonly CosmosClient _cosmosClient;
    private const string DatabaseId = "greasecontainerdb";
    private const string ContainerId = "greasecontainer";

    public TestPersistor()
    {
        var conn = Environment.GetEnvironmentVariable("CosmosDbConnection");
        var key = Environment.GetEnvironmentVariable("PRIMARY_KEY");

        if (string.IsNullOrWhiteSpace(conn))
            throw new InvalidOperationException("App setting 'CosmosDbConnection' is missing.");

        // If it's a full connection string, use the 1-arg ctor. Otherwise treat it as endpoint + key.
        _cosmosClient = conn.Contains("AccountEndpoint=", StringComparison.OrdinalIgnoreCase)
            ? new CosmosClient(conn)
            : new CosmosClient(conn, key ?? throw new InvalidOperationException(
                "PRIMARY_KEY app setting is missing (required when 'CosmosDbConnection' is just the endpoint)."));
    }

    public async Task<ItemResponse<PlaceDocument>> PersistToCosmos(PlaceDocument place, CancellationToken ct = default)
    {
        var container = _cosmosClient.GetContainer(DatabaseId, ContainerId);
        if (place is null) throw new ArgumentNullException(nameof(place));
        if (string.IsNullOrWhiteSpace(place.GreaseReviews))
            throw new InvalidOperationException("Partition key 'GreaseReviews' is null/empty on PlaceDocument.");

        return await container.CreateItemAsync(place, new PartitionKey(place.GreaseReviews), cancellationToken: ct);
    }
}
