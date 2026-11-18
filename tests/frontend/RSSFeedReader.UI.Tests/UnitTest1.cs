using System.Net;
using System.Net.Http;
using System.Text;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using RSSFeedReader.UI.Pages;
using RSSFeedReader.UI.Services;

namespace RSSFeedReader.UI.Tests;

public class SubscriptionsTests
{
    [Fact]
    public void InitialPage_ShowsEmptyState()
    {
        using var context = CreateContext();

        var cut = context.RenderComponent<Subscriptions>();

        Assert.Contains("No subscriptions yet.", cut.Markup);
    }

    [Fact]
    public void EmptySubmission_ShowsFeedbackWithoutAdding()
    {
        using var context = CreateContext();
        var cut = context.RenderComponent<Subscriptions>();

        cut.Find("button").Click();

        Assert.Contains("Enter a feed URL.", cut.Markup);
    }

    private static TestContext CreateContext()
    {
        var context = new TestContext();
        var handler = new StubHttpMessageHandler();
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("http://localhost:5151/api/")
        };
        context.Services.AddSingleton(new SubscriptionService(httpClient));
        return context;
    }

    private sealed class StubHttpMessageHandler : HttpMessageHandler
    {
        private bool added;

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Get)
            {
                var body = added
                    ? "[{\"id\":1,\"url\":\"https://example.com/feed.xml\"}]"
                    : "[]";
                return Task.FromResult(JsonResponse(body, HttpStatusCode.OK));
            }

            if (request.Method == HttpMethod.Post)
            {
                added = true;
                return Task.FromResult(JsonResponse(
                    "{\"id\":1,\"url\":\"https://example.com/feed.xml\"}",
                    HttpStatusCode.Created));
            }

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        private static HttpResponseMessage JsonResponse(string body, HttpStatusCode statusCode)
        {
            return new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            };
        }
    }
}