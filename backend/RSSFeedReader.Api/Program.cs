using RSSFeedReader.Api.Models;
using RSSFeedReader.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<SubscriptionService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalFrontend", policy =>
        policy.WithOrigins("http://localhost:5213", "https://localhost:7026")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("LocalFrontend");

app.MapGet("/api/subscriptions", (SubscriptionService subscriptions) =>
    Results.Ok(subscriptions.GetAll()));

app.MapPost("/api/subscriptions", (AddSubscriptionRequest request, SubscriptionService subscriptions) =>
{
    var url = request.Url?.Trim();
    if (string.IsNullOrWhiteSpace(url))
    {
        return Results.BadRequest(new { message = "Enter a feed URL." });
    }

    var subscription = subscriptions.Add(url);
    return Results.Created($"/api/subscriptions/{subscription.Id}", subscription);
});

app.Run();
