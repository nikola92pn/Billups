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
- **Integration tests** do not cover all possible cases for different game modes.
- **Unit tests**: Only few services are currently covered and by NUnit tests.
- **MediatR**: Not currently used, but could be considered if the application expands.
- **Game mode history reset**: The action removes all history no matter which game mode is selected.
- **Game mode switching** is performed via the `X-Game-Mode` HTTP header. x-game-mode: **rps** - for classic mode, or **rpsls** for advanced mode which is consider as defualt value)

---
## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started/)
- (Optional) [Visual Studio 2022+](https://visualstudio.microsoft.com/) or [JetBrains Rider](https://www.jetbrains.com/rider/)

### Running the tests and API without Docker
#### Application is available on http://localhost:5132

```sh
dotnet build
dotnet test
dotnet run --project Billups.Api
```

### Running the tests and API using Docker

```sh
docker build -t billups-api .
docker run -p 5132:80 billups-api

# if 3rd party calls are slow change to 
# docker run --dns 8.8.8.8 -p 5132:80 billups-api
```
