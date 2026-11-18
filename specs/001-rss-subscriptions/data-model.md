# Data Model: RSS Subscription Management

## Subscription

**Purpose:** Represents one URL entry submitted by the user during the current session.

| Field | Type | Required | Validation |
|-------|------|----------|------------|
| `id` | string or integer | Yes | Unique within the current process |
| `url` | string | Yes | Non-empty after whitespace is trimmed |

**Lifecycle**

- Created after a non-empty submission.
- Stored in memory for the lifetime of the backend process.
- Returned in creation order.
- Cleared when the backend process stops or restarts.

**Out of Scope**

- URL format validation
- RSS/Atom verification
- Duplicate detection
- Persistence
- Removal operations
