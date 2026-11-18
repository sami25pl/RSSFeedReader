using RSSFeedReader.Api.Models;

namespace RSSFeedReader.Api.Services;

public sealed class SubscriptionService
{
    private readonly List<Subscription> subscriptions = [];
    private int nextId = 1;

    public IReadOnlyList<Subscription> GetAll() => subscriptions;

    public Subscription Add(string url)
    {
        var subscription = new Subscription(nextId++, url);
        subscriptions.Add(subscription);
        return subscription;
    }
}
