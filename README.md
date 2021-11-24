# Simple, practical EventSourcing with EventStoreDB and EntityFramework


## Main assumptions:
- explain basics of Event Sourcing, both from the write model (EventStoreDB) and read model part (Postgres and EntityFramework),
- CQRS architecture sliced by business features, keeping code that changes together at the same place.
- no aggregates, just data (records) and functions,
- clean, composable (pure) functions for command, events, projections, query handling instead of marker interfaces (the only one used internally is `IEventHandler`). 
- easy to use and self-explanatory fluent API for registering commands and projections with possible fallbacks,
- registering everything into regular DI containers to integrate with other application services.
- pushing the type/signature enforcement on edge, so when plugging to DI.

## Overview

It uses:
- pure data entities, functions and handlers,
- Stores events from the command handler result  EventStoreDB,
- Builds read models using [Subscription to `$all`](https://developers.eventstore.com/clients/grpc/subscribing-to-streams/#subscribing-to-all).
- Read models are stored to Postgres relational tables with [Entity Framework](https://docs.microsoft.com/en-us/ef/core/).
- App has Swagger and predefined [docker-compose](./docker/docker-compose.yml) to run and play with samples.


## Prerequisities

1. Install git - https://git-scm.com/downloads.
2. Install .NET Core 6.0 - https://dotnet.microsoft.com/download/dotnet/6.0.
3. Install Visual Studio 2022, Rider or VSCode.
4. Install docker - https://docs.docker.com/docker-for-windows/install/.
5. Open `ECommerce.sln` solution.

## Running

1. Go to [docker](./docker) and run: `docker-compose up`.
2. Wait until all dockers got are downloaded and running.
3. You should automatically get:
    - EventStoreDB UI (for event store): http://localhost:2113/
    - Postgres DB running (for read models)
    - PG Admin - IDE for postgres. Available at: http://localhost:5050.
        - Login: `admin@pgadmin.org`, Password: `admin`
        - To connect to server Use host: `postgres`, user: `postgres`, password: `Password12!`
4. Open, build and run `ECommerce.sln` solution.
    - Swagger should be available at: http://localhost:5000/index.html
