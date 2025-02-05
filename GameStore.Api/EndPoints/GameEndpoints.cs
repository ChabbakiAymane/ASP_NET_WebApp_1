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
        // Utilizzo async e await per rendere la chiamata asincrona
        group.MapGet("/", async (GameStoreContext dbContext) => await dbContext.Games
            .Include(game => game.Genre)
            .Select(game => game.ToGameSummaryDto())
            // Per ottimizzare le risorse, non traccio le modifiche
            .AsNoTracking()
            // La rendo asincrona, mettendo un task che potra essere eseguito appena pronto
            .ToListAsync());
        // ---------------------------------------------------------------------------------------------------------------
        // GET /games/1
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) => {
            // Cerco il gioco esistente per aggiornarlo in modo asincrono usando FindAsync()
            Game? game = await dbContext.Games.FindAsync(id);
            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        }).WithName(GetGameEndPointName);
        // ---------------------------------------------------------------------------------------------------------------
        // POST /games
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) => {
            // Dopo aver creato il Mapping, posso fare direttamente questo
            Game game = newGame.ToEntity();

            dbContext.Games.Add(game);
            // Salvo le modifiche nel DB (SQL) in modo asincrono usando SaveChangesAsync()
            await dbContext.SaveChangesAsync();
            // Per semplificare le azioni di mappatura tra entità e DTOs, posso usare AutoMapper/Classe di estensione
            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game.ToGameDetailsDto());
        });
        // ---------------------------------------------------------------------------------------------------------------
        // PUT /games/1
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) => {
            // Cerco il gioco esistente per aggiornarlo in modo asincrono usando FindAsync()
            var existingGame = await dbContext.Games.FindAsync(id);
            if(existingGame is null){
                return Results.NotFound();
            }
            // Devo trovare il gioco e rimpiazzarlo con la nuova entità
            dbContext.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));
            // Salvo le modifiche nel DB (SQL) in modo asincrono usando SaveChangesAsync()
            await dbContext.SaveChangesAsync();
            // Restituisco l'esisto dell'operazione di PUT game (204 No Content)
            return Results.NoContent();
        });
        // ---------------------------------------------------------------------------------------------------------------
        // DELETE /games/1
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) => {
            // Batch delete, efficiente, la faccio in modo asincrono utilizzando ExecuteDeleteAsync()
            await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync(); 
            return Results.NoContent();
        });
        return group;
    }
}