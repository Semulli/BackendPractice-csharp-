using System;
using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string EndpointName = "GetGame";

    private static readonly List<GameDto> games = [
      new(1,
     "The Legend of Zelda: Breath of the Wild",
     "Nintendo Switch",
      59.99m,
      new DateOnly(2017, 3, 3)
      ),
    new(2,
    "God of War",
    "PlayStation 4",
     39.99m,
     new DateOnly(2018, 4, 20)),
    new(3,
     "Red Dead Redemption 2",
      "Xbox One",
       49.99m,
     new DateOnly(2019, 10, 23))
  ];

    public static void MapGamesEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("/games");
        //Get /games
        group.MapGet("/", () => games);


        //Get /games/{id}
        group.MapGet("/{id}", (int id) =>
        {
            var game = games.Find(game => game.Id == id);
            return game is not null ? Results.Ok(game) : Results.NotFound();

        }).WithName(EndpointName);

        //Post /games
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            if (string.IsNullOrWhiteSpace(newGame.Name))
            {
                return Results.BadRequest("Game name is required. Name cannot be null or empty");
            }

            GameDto game = new GameDto(
                 games.Count + 1,
                 newGame.Name,
                 newGame.Genre,
                 newGame.Price,
                 newGame.ReleaseDate
    );
            games.Add(game);

            return Results.CreatedAtRoute(EndpointName, new { id = game.Id }, game);
        });

        //Put /games{id}
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            games[index] = new GameDto(
                 id,
                 updatedGame.Name,
                 updatedGame.Genre,
                 updatedGame.Price,
                 updatedGame.ReleaseDate
    );

            return Results.NoContent();
        });


        //Delete /games/{id}
        group.MapDelete("/{id}", (int id) =>
        { 
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });
    }
}
