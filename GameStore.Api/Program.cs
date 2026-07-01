
using GameStore.Api.Dtos;
const string EndpointName = "GetGame";

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


List<GameDto> games = [
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


//Get /games
app.MapGet("/games", () => games);


//Get /games/{id}
app.MapGet("/games/{id}", (int id)=> games.Find(game => game.Id == id)).WithName(EndpointName);

//Post /games
app.MapPost("/games", (CreateGameDto newGame) =>
{
   GameDto game = new GameDto(
    games.Count+1,
    newGame.Name,
    newGame.Genre,
    newGame.Price,
    newGame.ReleaseDate
   ); 
   games.Add(game);

   return Results.CreatedAtRoute(EndpointName, new {id = game.Id}, game);
});

//Put /games{id}
app.MapPut("/games/{id}", (int id, UpdateGameDto updatedGame) =>
{
  var index = games.FindIndex(game => game.Id==id);

  games[index]= new GameDto(
    id,
    updatedGame.Name,
    updatedGame.Genre,
    updatedGame.Price,
    updatedGame.ReleaseDate
  );

  return  Results.NoContent();
});


//Delete /games/{id}
app.MapDelete("/games/{id}", (int id) =>
{
  games.RemoveAll(game => game.Id == id);
  return Results.NoContent();
});
app.Run();
   