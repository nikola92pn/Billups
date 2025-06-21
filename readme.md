# Billups

**Billups** is a modular and testable .NET 9 web API for playing classic “Rock, Paper, Scissors, Lizard, Spock” (and potentially other modes!) against the computer.  
The app is cleanly layered with domain logic, application services, API endpoints, integration tests, and unit tests.

---

## Features

- Play “Rock, Paper, Scissors, Lizard, Spock” (and other game modes) via REST API
- View and reset game history
- Supports multiple game modes (header-based)
- Clean separation: Domain, Application, Infrastructure, API
- Comprehensive unit & integration tests (xUnit, Moq)

---

## Notes & Limitations

- **No "real" repository**: An in-memory implementation is used for game history; there is no persistent storage.
- **Game history** could be extended to include a `GameMode` field.
- **Integration tests** do not cover all possible cases for different game modes.
- **Unit tests**: Only few services are currently covered and by NUnit tests.
- **MediatR**: Not currently used, but could be considered if the application expands.
- **Game mode switching** is performed via the `X-Game-Mode` HTTP header.

---## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- (Optional) [Visual Studio 2022+](https://visualstudio.microsoft.com/) or [JetBrains Rider](https://www.jetbrains.com/rider/)

### Running the tests and API

```sh
dotnet build
dotnet test
dotnet run --project Billups.Api
