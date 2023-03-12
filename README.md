# BingeBot

## Instructions
In order to run this application you need to have docker installed:
1. Run the docker compose file `docker-compose.yaml`.
2. Run `dotnet ef database update` in `Infrastructure.Databases\`.
3. Run the `Api` application: a swagger UI client should open.

# Justification
For this project I used .NET 7 and C# 11 (because: new). I acknowledge this .NET version is not LTS. 
Unfortunately I didn't see a way to list all shows with a specific premiered date in the TVmaze API. 
The only parameter that could be used is `q` which uses fuzzy text searching in episodes, and is therefore not usable. 
I have too little domain knowledge to know if the date of the earliest episode is *always* equal to the premiered date. 
It would be too time consuming to find this out. 
Therefore, I used a 'dumber' approach: the background job retrieves all episodes (using paging with a delay) and filters out shows based on te premiered date.

## Containers
I used a Dockerfile + compose in order to run the API and MySQL database easily. 
At first I wanted to use the new build-in container support for .NET,
see: (https://devblogs.microsoft.com/dotnet/announcing-builtin-container-support-for-the-dotnet-sdk/#comments).
Unfortunately, I couldn't get it to work with my docker compose file, because the functionality is probably not supported yet or it is undocumented.

## Api
The API is a REST / JSON API, I:
- Did not use any authentication, because it was not needed for the assignment. It can always be added later on.
- Did not use minimal APIs as I think controllers are way better to segregate the logic of your endpoints.
- Am using JSON as the protocol of the API, because JSON is ubiquitous and therefore the tooling is good.
- Used HTTPS because I think that HTTP really shouldn't be used anymore for APIs.

### OpenApi / Swagger
I checked if TVMaze has an OpenAPI / Swagger definition, but unfortunately not: https://www.tvmaze.com/threads/2982/swaggeropenapi-for-api.
It would have been perfect because you could easily generate a client with NSwag / Swashbuckle.

I saw multiple .NET clients for TVMaze on GitHub but they were not that great. Therefore, I implemented the communication logic 'manually'.

I used Swashbuckle in order to expose objects as JSON endpoints. This way you also have a simple UI for the API and you can easily create a client.

## Background job
I wanted to use Quartz as a job schedule. Quartz.NET is an advanced scheduler with a built-in mechanism to persist scheduled jobs, and much more.
But because of time constraints I used a simpler approach: When the application starts, a background job starts which uses the new `PeriodicTimer`.
The usage of a background job has a disadvantage: 
when you need to scale up the performance by running more instances of the API, multiple background task will run simultaneously.

## CQRS / CRUD
I chose for CRUD: I often think that CQRS makes code become way less readable, as it has a big impact on the design of the application: 
Concepts get split out because of command/query segregation Also, queries have always to be checked thoroughly and are only SELECT or UPDATE by design. 
I think a repository pattern is clean and logical. Each method of the repo should perform a read or write.

## ORM / Database
I chose for entity framework because I think it integrates very well and is high-level and less verbose than for example Dapper.
It also has code first and migration support which helps. I acknowledge that the data types are not set perfectly. 
For example all the strings have `nvarchar(max)` which is not correct.
I used https://github.com/TheArchitectDev/Architect.EntityFramework.DbContextManagement 
in order to achieve a good repository pattern and have more control over the database transactions.

## Domain modeling
I modelled my domain with a DDD onion design using my own DDD modeling package: https://github.com/Code-Chops/DomainModeling. 

### Value objects
Throughout my code I use value objects which have structural equality. 
These are generated using a source generator, so a lot of boiler-plate does not have to be written, 
e.g.: structure equality, casts, constructors, factories, and validation.

### Entities / Identities
The 3 entities in the domain model `Show`, `Person`, `ShowGenres` all have a strongly typed primary ID that is auto-generated using source generators.

## Contracts
I used my package https://github.com/Code-Chops/Contracts (which is in beta) for easily creating contracts, adapters and using JSON serializers.

## Validation
I am not satisfied yet with the design of the error codes (which you see at the value objects). And especially with the usage of `nameof()`.
These error codes ensure that error responses to external sources are unified and can eventually be parsed and localized to the end user.
In my spare time I am creating a way to unify these error codes in a single MagicEnum, see my MagicEnum package: https://github.com/Code-Chops/MagicEnums.
These error messages (and their parameters) can be localized in a Blazor client using my package https://github.com/Code-Chops/LightResources.

## Global usings
Global usings can be very handy when it comes to using versioning in contracts for external sources and you want to easily change using a specific version.
See BingeBot.Infrastructure.Api.TVmaze.Properties.GlobalUsings.

## Caching
Response caching is enabled, see `Program.cs:20`.

## Rate limiting
The background job polls the TVmaze API once a second (is configurable).
Also included in the TVmazeHTTP client is a `RateLimitingStrategy` which determines how the application should handle when an HTTP status code of `TooManyRequests` is sent from TVmaze.


# Unfinished things
I didn't finish everything but this is to show how I would set up a basic API and background service.

## Complete docker container
As you might have noticed, you first have to start a docker compose file and after that you still have to execute the EF code first command and run the `Api` manually.
I didn't have time to fix this and integrate it into the docker compose file correctly.

## Tests
Many more unit and integration tests can be added. For example, the *.http files in Api could be placed in an integration test.

## Validation messages
Somehow the validation exceptions didn't get caught and are not presented nicely to the user. I didn't have to look into why that happens.

## Genres
I found out that the genres don't get loaded properly. I have to look into that, but I didn't have time for that anymore as I spent 4 hours making this project.

## Persons
Persons are not linked to the shows at all. It is just a small example of how to expand this API.

## Duplicate keys
Exceptions that happen when inserting of duplicate keys are not being caught well. A database exception occurs when it happens.