# Quickstart: RSS Subscription Management

## Prerequisites

- A supported .NET SDK for ASP.NET Core Web API and Blazor WebAssembly.
- Matching backend and frontend local ports.
- A frontend API base URL that points to the backend `/api/` route.
- Backend CORS configured to allow the frontend origin.

## Start the MVP

1. Start the backend API project.
2. Verify it listens on the configured backend port.
3. Start the Blazor WebAssembly frontend project.
4. Open the frontend URL shown by the local development server.

## Validate adding and viewing subscriptions

1. Open the subscription page with no subscriptions.
2. Confirm that the page shows a clear empty state.
3. Submit an empty value.
4. Confirm that no entry is added and the page requests a URL.
5. Enter `https://example.com/feed.xml` and submit.
6. Confirm the URL appears immediately.
7. Add another URL.
8. Confirm both subscriptions remain visible.

## Validate session-only storage

1. Add at least one subscription.
2. Stop and restart the backend process.
3. Confirm the subscription list is empty.

## Build and test

```powershell
dotnet build backend/RSSFeedReader.Api/RSSFeedReader.Api.csproj
dotnet build frontend/RSSFeedReader.UI/RSSFeedReader.UI.csproj
dotnet test
```

Browser validation should confirm that the frontend loads, the API is reachable, and no ambiguous
routes are reported.
