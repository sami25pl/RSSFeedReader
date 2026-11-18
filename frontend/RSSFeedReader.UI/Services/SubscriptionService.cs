using System.Net.Http.Json;

namespace RSSFeedReader.UI.Services;

public sealed class SubscriptionService(HttpClient httpClient)
{
    public async Task<IReadOnlyList<Subscription>> GetAllAsync()
    {
        var subscriptions = await httpClient.GetFromJsonAsync<List<Subscription>>("subscriptions");
        return subscriptions ?? [];
    }

    public async Task<bool> AddAsync(string url)
    {
        var response = await httpClient.PostAsJsonAsync("subscriptions", new { url });
        return response.IsSuccessStatusCode;
    }
}

public sealed record Subscription(int Id, string Url);
