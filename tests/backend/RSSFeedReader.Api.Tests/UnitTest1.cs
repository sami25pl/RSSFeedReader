using RSSFeedReader.Api.Services;

namespace RSSFeedReader.Api.Tests;

public class SubscriptionServiceTests
{
    [Fact]
    public void AddAndGetAll_UseInMemorySessionStorage()
    {
        var service = new SubscriptionService();

        service.Add("https://example.com/feed.xml");
        var subscriptions = service.GetAll();

        Assert.Single(subscriptions);
        Assert.Equal("https://example.com/feed.xml", subscriptions[0].Url);
    }

    [Fact]
    public void Add_AssignsUniqueIdsInCreationOrder()
    {
        var service = new SubscriptionService();

        var first = service.Add("https://example.com/one.xml");
        var second = service.Add("https://example.com/two.xml");

        Assert.Equal(1, first.Id);
        Assert.Equal(2, second.Id);
    }
}