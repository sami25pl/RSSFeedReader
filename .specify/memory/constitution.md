# RSS Feed Reader Constitution

## Core Principles

### I. Security by Design
Every change MUST protect user data and respect trust boundaries. Do not commit credentials,
expose secrets in logs, or render feed-provided HTML as trusted content. While the MVP does not
fetch feeds, any feature that introduces server-side fetching MUST restrict schemes to HTTP and
HTTPS, validate destinations and redirects against loopback, private, and link-local networks,
and enforce timeouts and response-size limits. Local API CORS policies MUST allow only the
intended frontend origins.

### II. Maintainable Simplicity
Code MUST use focused responsibilities and the existing ASP.NET Core Web API and Blazor
WebAssembly architecture. Keep the backend responsible for API and application data and the
frontend responsible for user interaction. Add abstractions, dependencies, and configuration
only when a current requirement needs them; avoid speculative features and duplicated logic.

### III. Verified Quality
Changes MUST be validated at the level of behavior they affect. Add or update focused tests for
new or corrected behavior, including API/UI integration tests when contracts or communication
between those layers change. Before declaring a change complete, run the relevant tests and
builds, and report any validation that could not be run. Failures MUST be visible and handled
explicitly rather than silently ignored.

### IV. MVP Scope and Incremental Delivery
The MVP is limited to adding feed subscriptions by URL and displaying the subscription list.
Subscriptions may remain in memory; feed fetching, parsing, persistence, removal, and other
enhancements are out of MVP scope unless explicitly approved for a later phase. Each change MUST
meet an identified current-phase requirement and preserve a clear path for incremental extension.

### V. Clear User Feedback
User actions MUST produce a clear, timely UI result, including when an operation fails. The
subscription list MUST visibly reflect a successful addition. When network operations are added
in a later phase, errors MUST be communicated without exposing sensitive implementation details;
the UI MUST distinguish failure from success and remain usable.

## Project Constraints

The application is a locally run, single-user proof of concept targeting Windows, macOS, and
Linux. The backend uses ASP.NET Core Web API and the frontend uses Blazor WebAssembly. Keep
development configuration aligned across backend listen addresses, frontend API base URLs, and
CORS origins. Features deferred by the project goals require explicit phase approval before
implementation.

## Development Workflow

Before implementation, identify the applicable MVP or later-phase requirement and its observable
acceptance behavior. Keep changes focused on that behavior. For UI work, verify the application
has no conflicting routes; for API/UI changes, verify configured addresses and CORS permit the
intended local connection. Review changes for the principles above, run the relevant tests and
builds, and summarize the results and any known limitations.

## Governance

This constitution governs project decisions and supersedes conflicting development conventions.
Amendments MUST be reviewed and recorded in this file. Each amendment MUST update the version,
last-amended date, and Sync Impact Report; reviewers MUST check that affected requirements and
implementation guidance remain consistent. Project changes and reviews MUST assess compliance
with the relevant principles. Exceptions MUST be documented with their scope and rationale.

Versioning follows semantic versioning: MAJOR for backward-incompatible removals or
redefinitions of principles, MINOR for new principles or materially expanded governance, and
PATCH for clarifications and non-semantic refinements.

**Version**: 1.0.0 | **Ratified**: TODO(RATIFICATION_DATE): confirm original adoption date |
**Last Amended**: 2026-11-17
