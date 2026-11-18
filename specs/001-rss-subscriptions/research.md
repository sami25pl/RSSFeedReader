# Phase 0 Research: RSS Subscription Management

## Decision: Keep the MVP fully in memory

**Rationale:** Stakeholder scope requires session-only subscriptions and excludes persistence,
feed fetching, and parsing. A process-lifetime list implements exactly the requested behavior.

**Alternatives considered:** Database storage was rejected because it adds schema and migration
work outside MVP scope.

## Decision: Use separate ASP.NET Core API and Blazor WebAssembly projects

**Rationale:** This preserves the responsibilities documented by stakeholders: the backend owns
subscription data and the frontend owns interaction.

**Alternatives considered:** A standalone Blazor application would be simpler but would not
preserve the specified backend/frontend separation or incremental architecture.

## Decision: Exclude URL validation and feed network activity

**Rationale:** The MVP defines success as adding and displaying a non-empty text entry. URL
validation and RSS/Atom fetching require behavior explicitly deferred from MVP.

**Alternatives considered:** URL checking or feed testing would introduce network dependencies
and parsing complexity contrary to the proof-of-concept goal.

## Decision: Permit duplicate subscriptions

**Rationale:** De-duplication is a post-MVP feature, so duplicate submissions remain separate
entries unless MVP scope is expanded.

**Alternatives considered:** Rejecting duplicates would require identity rules and additional
user feedback not needed for the core demonstration.

## Decision: Configure CORS and API base addresses explicitly

**Rationale:** Matching backend ports, frontend configuration, and CORS origins are required for
the local web application to communicate reliably.

**Alternatives considered:** Hardcoded addresses were rejected because they reduce portability
across local development machines and environments.
