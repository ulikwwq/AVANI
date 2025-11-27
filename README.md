# AVANI — Blazor MVP Scaffold

This repository contains a minimal scaffold for the AVANI Blazor client as requested.

Structure created:

- `Client/Pages` — basic Razor pages: `Login.razor`, `DashboardParent.razor`, `DashboardChild.razor`, `GoalCreate.razor`, `GoalView.razor`, `ShopCompare.razor`, `Penalty.razor`
- `Client/Components` — small UI components: `MiniAvatar.razor`, `CoinBalance.razor`, `GoalCard.razor`
- `Client/Services` — simple service placeholders: `FirebaseService.cs`, `UserService.cs`, `GoalService.cs`, `CoinService.cs`
- `Client/Models` — domain models
- `Client/Styles` — simple CSS files
- `Shared` — `MainLayout.razor`, `NavMenu.razor`
- `Server` — empty folder (left for MVP)
- `wwwroot` — `img/avatars`, `img/icons`, `css`

Next steps:
- Wire services into the DI container in `Program.cs` or `Startup`.
- Implement Firebase initialization and real storage.
- Add unit tests and CI as needed.

