
using GameStore.Api.Dtos;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidators<CreateGameDto>();
builder.Services.AddValidators<UpdateGameDto>();

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
   