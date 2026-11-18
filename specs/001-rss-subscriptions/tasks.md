# Tasks: RSS Subscription Management

**Input**: Design documents from `/specs/001-rss-subscriptions/`

**Prerequisites**: plan.md, spec.md, research.md, data-model.md,
contracts/subscriptions.openapi.yaml, quickstart.md

**Tests**: No test-first task is required because the specification and request did not require
a TDD workflow. The tasks include focused verification through builds and quickstart validation.

**Organization**: Tasks are grouped by user story to enable independent implementation and
testing of each story.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel with other tasks that touch different files.
- **[Story]**: The user story covered by the task.
- Include exact file paths in descriptions.

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Create the backend and frontend projects needed for the MVP.

- [ ] T001 Create the ASP.NET Core Web API project in `backend/RSSFeedReader.Api/`.
- [ ] T002 [P] Create the Blazor WebAssembly project in `frontend/RSSFeedReader.UI/`.
- [ ] T003 [P] Create test project directories in `tests/backend/` and `tests/frontend/`.
- [ ] T004 Configure the backend launch port in
  `backend/RSSFeedReader.Api/Properties/launchSettings.json`.
- [ ] T005 Configure the frontend launch port in
  `frontend/RSSFeedReader.UI/Properties/launchSettings.json`.
- [ ] T006 Configure the frontend API base URL in
  `frontend/RSSFeedReader.UI/wwwroot/appsettings.json`.

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Complete shared infrastructure before user-story implementation.

**CRITICAL**: No user story work can begin until this phase is complete.

- [ ] T007 Remove template demo pages `Home.razor`, `Counter.razor`, and `Weather.razor` from
  `frontend/RSSFeedReader.UI/Pages/`.
- [ ] T008 [P] Update demo navigation in
  `frontend/RSSFeedReader.UI/Layout/NavMenu.razor` to expose only MVP navigation.
- [ ] T009 [P] Configure local frontend CORS in `backend/RSSFeedReader.Api/Program.cs`.
- [ ] T010 [P] Configure a configured API `HttpClient` base address in
  `frontend/RSSFeedReader.UI/Program.cs`.
- [ ] T011 [P] Configure the backend subscription API routing and JSON behavior in
  `backend/RSSFeedReader.Api/Program.cs`.
- [ ] T012 Verify that exactly one page uses `@page "/"` in
  `frontend/RSSFeedReader.UI/Pages/`.

**Checkpoint**: Backend and frontend projects start and share matching local configuration.

---

## Phase 3: User Story 1 - Add a Subscription (Priority: P1) MVP

**Goal**: A user can submit a non-empty URL and see it added to the subscription list.

**Independent Test**: Start the application, submit a URL, and verify that the URL appears in
the list without feed fetching or parsing.

### Implementation for User Story 1

- [ ] T013 [P] [US1] Create the `Subscription` DTO in
  `backend/RSSFeedReader.Api/Models/Subscription.cs`, satisfying `data-model.md`
  `Subscription.id` and `Subscription.url`.
- [ ] T014 [P] [US1] Create the add-subscription request model in
  `backend/RSSFeedReader.Api/Models/AddSubscriptionRequest.cs`, enforcing
  `data-model.md Subscription.url` as a non-empty, whitespace-trimmed value.
- [ ] T015 [US1] Implement the in-memory subscription service in
  `backend/RSSFeedReader.Api/Services/SubscriptionService.cs`, using
  `data-model.md Subscription.id` for process-unique identifiers and preserving creation order.
- [ ] T016 [US1] Implement `POST /api/subscriptions` in
  `backend/RSSFeedReader.Api/Program.cs`, using `SubscriptionService` and returning `201` for a
  valid entry or `400` for an empty entry as defined by
  `contracts/subscriptions.openapi.yaml`.
- [ ] T017 [P] [US1] Create the frontend subscription API service in
  `frontend/RSSFeedReader.UI/Services/SubscriptionService.cs`.
- [ ] T018 [US1] Create the subscription entry page in
  `frontend/RSSFeedReader.UI/Pages/Subscriptions.razor`.
- [ ] T019 [US1] Add non-empty submission feedback and clear the input after a successful add in
  `frontend/RSSFeedReader.UI/Pages/Subscriptions.razor`.

**Checkpoint**: User Story 1 is independently usable: a submitted URL is added and listed.

---

## Phase 4: User Story 2 - View Subscriptions (Priority: P1)

**Goal**: A user can view all subscriptions added during the current session and a clear empty
state before the first addition.

**Independent Test**: Add one or more subscriptions, observe all entries in the list, restart
the backend, and verify that the session-only list is empty.

### Implementation for User Story 2

- [ ] T020 [P] [US2] Implement `GET /api/subscriptions` in
  `backend/RSSFeedReader.Api/Program.cs` according to
  `contracts/subscriptions.openapi.yaml`.
- [ ] T021 [P] [US2] Add the frontend list-loading operation to
  `frontend/RSSFeedReader.UI/Services/SubscriptionService.cs`.
- [ ] T022 [US2] Render all session subscriptions and an empty-state message in
  `frontend/RSSFeedReader.UI/Pages/Subscriptions.razor`.
- [ ] T023 [US2] Refresh the frontend list after a successful addition in
  `frontend/RSSFeedReader.UI/Pages/Subscriptions.razor`.

**Checkpoint**: User Stories 1 and 2 provide a complete MVP add-and-view loop.

---

## Phase 5: Polish and Cross-Cutting Concerns

**Purpose**: Verify configuration, quality, scope boundaries, and end-to-end behavior.

- [ ] T024 [P] Add focused backend tests for in-memory add/list behavior in
  `tests/backend/`.
- [ ] T025 [P] Add frontend tests for empty-state and successful-add rendering in
  `tests/frontend/`.
- [ ] T026 Verify all deferred features remain absent: no feed fetching, parsing, persistence,
  removal, duplicate prevention, or background refresh.
- [ ] T027 Validate backend port, frontend port, API base URL, and CORS alignment across
  `backend/RSSFeedReader.Api/Properties/launchSettings.json`,
  `frontend/RSSFeedReader.UI/Properties/launchSettings.json`,
  `frontend/RSSFeedReader.UI/wwwroot/appsettings.json`, and
  `backend/RSSFeedReader.Api/Program.cs`.
- [ ] T028 Run backend and frontend builds and the quickstart validation in
  `specs/001-rss-subscriptions/quickstart.md`.

---

## Dependencies and Execution Order

### Phase Dependencies

- **Phase 1 (Setup)**: No dependencies.
- **Phase 2 (Foundational)**: Depends on Setup; blocks every user story.
- **Phase 3 (User Story 1)**: Depends on foundational completion.
- **Phase 4 (User Story 2)**: Depends on foundational completion and shares the subscription
  page/service with User Story 1.
- **Phase 5 (Polish)**: Depends on all desired user stories.

### User Story Dependencies

- **User Story 1**: No dependency on User Story 2.
- **User Story 2**: Can reuse the subscription service and page created for User Story 1, but
  the empty-state and list behavior can be validated independently.

### Parallel Opportunities

- T002 and T003 can run while T001 proceeds.
- T008 through T012 can run in parallel after project setup.
- T013 and T014 can run in parallel.
- T017 can run in parallel with backend model and service work.
- T020 and T021 can run in parallel.
- T024 and T025 can run in parallel.

---

## Parallel Example: User Story 1

```text
Backend model: T013 Create Subscription DTO
Backend request model: T014 Create AddSubscriptionRequest
Frontend API service: T017 Create frontend SubscriptionService
```

## Parallel Example: User Story 2

```text
Backend list endpoint: T020 Implement GET /api/subscriptions
Frontend list operation: T021 Implement list loading
```

---

## Implementation Strategy

### MVP First

1. Complete Setup and Foundational phases.
2. Complete User Story 1.
3. Stop and validate subscription submission independently.
4. Complete User Story 2.
5. Stop and validate add/list behavior end to end.

### Incremental Delivery

1. Establish projects and configuration.
2. Deliver a working add-subscription increment.
3. Add session list and empty-state behavior.
4. Perform final build, test, and quickstart verification.

### Parallel Team Strategy

With multiple developers:

1. One developer completes backend project setup and foundational API configuration.
2. One developer completes frontend project setup and configuration.
3. After foundations, developers divide backend service, frontend service, and subscription page
   work by file boundaries.
