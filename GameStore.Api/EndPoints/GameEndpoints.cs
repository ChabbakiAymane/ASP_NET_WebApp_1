using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.EndPoints;

public static class GameEndpoints{
    const string GetGameEndPointName = "GetGame";
    // Visto che ho settato il group, devo cambiare il return-type da WebApplication a RouteGroupBuilder
    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app){
        var group = app.MapGroup("games").WithParameterValidation();
        // GET /games
        group.MapGet("/", (GameStoreContext dbContext) => dbContext.Games
            .Include(game => game.Genre)
            .Select(game => game.ToGameSummaryDto())
            // Per ottimizzare le risorse, non traccio le modifiche
            .AsNoTracking());
        // ---------------------------------------------------------------------------------------------------------------
        // GET /games/1
        group.MapGet("/{id}", (int id, GameStoreContext dbContext) => {
            Game? game = dbContext.Games.Find(id);
            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        }).WithName(GetGameEndPointName);
        // ---------------------------------------------------------------------------------------------------------------
        // POST /games
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) => {
            // Dopo aver creato il Mapping, posso fare direttamente questo
            Game game = newGame.ToEntity();

            dbContext.Games.Add(game);
            // Salvo le modifiche nel DB (SQL)
            dbContext.SaveChanges();
            // Per semplificare le azioni di mappatura tra entità e DTOs, posso usare AutoMapper/Classe di estensione
            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game.ToGameDetailsDto());
        });
        // ---------------------------------------------------------------------------------------------------------------
        // PUT /games/1
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) => {
            var existingGame = dbContext.Games.Find(id);
            if(existingGame is null){
                return Results.NotFound();
            }
            // Devo trovare il gioco e rimpiazzarlo con la nuova entità
            dbContext.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));
            // Salvo le modifiche nel DB (SQL)
            dbContext.SaveChanges();
            // Restituisco l'esisto dell'operazione di PUT game (204 No Content)
            return Results.NoContent();
        });
        // ---------------------------------------------------------------------------------------------------------------
        // DELETE /games/1
        group.MapDelete("/{id}", (int id, GameStoreContext dbContext) => {
            // Batch delete, efficiente
            dbContext.Games.Where(game => game.Id == id).ExecuteDelete(); 
            return Results.NoContent();
        });
        return group;
    }
}