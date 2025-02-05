using System;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.EndPoints;

public static class GameEndpoints{
    // Definisco COSTANTI da usare con la funzione .WItName("")
    const string GetGameEndPointName = "GetGame";

    // Creo una lista che contiene i Games DTO
    // Una volta creata VSCode inserisce 'using...' in modo automatico
    /*private static readonly List<GameSummaryDto> games = [
        new GameSummaryDto(1, "Super Mario Bros", "Kids & Family", 49.99m, new DateOnly(1985, 9, 13)),
        new GameSummaryDto(2, "The Legend of Zelda", "Roleplaying", 29.99m, new DateOnly(1986, 2, 21)),
        new GameSummaryDto(3, "Forza Motor Sport", "Sports", 69.99m, new DateOnly(2011, 11, 18)),
        new GameSummaryDto(4, "Among Us", "Roleplaying", 8.99m, new DateOnly(2018, 6, 15)),
        new GameSummaryDto(5, "Street Figher", "Fighting", 59.99m, new DateOnly(2020, 12, 10))
    ];*/

    // Visto che ho settato il group, devo cambiare il return-type da WebApplication a RouteGroupBuilder
    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app){
        // Modifico tutti i app.Mapget() in modo che siano raggruppati sotto /games
        var group = app.MapGroup("games").WithParameterValidation();
        // Settato il gruppo, modifico tutte le API, ricordando che sono sotto /games
        // GET /games
        // Modifico get di tutti i giochi in modo che prenda i dati dal database
        // Prendo tutti i giochi e li trasformo in GameSummaryDto
        group.MapGet("/", (GameStoreContext dbContext) => 
             dbContext.Games.Include(game => game.Genre)
                            .Select(game => game.ToGameSummaryDto())
                            .AsNoTracking()); // Per ottimizzare le risorse, non traccio le modifiche

        // GET /games/1
        // Sistemo get per usare il context Database dbContext che è injected
        group.MapGet("/{id}", (int id, GameStoreContext dbContext) => {
            //var game = games.Find(game => game.ID == id);
            // GameDto? game = games.Find(game => game.ID == id);
            // Results.NotFound(): restituisce uno status code 404 Not Found
            // Results.Ok(game): restituisce uno status code 200 OK e il game
            Game? game = dbContext.Games.Find(id);
            // Creo un nuovo GameDTO per restituire i dati con GenreID
            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        }).WithName(GetGameEndPointName);





        // POST /games
        // Aggiungo il context 
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) => {
            // Verifico se i dati inseriti sono validi
            // Non efficiente, devo farlo per olte risorse e in più punti
            // Meglio impostare il controllo direttamente nel DTO tramite DataAnnotations
            /*if(string.IsNullOrEmpty(newGame.Name)){
                // Restituisco uno status code 400 Bad Request
                return Results.BadRequest("Name is required");
            }*/
            /*GameDto game = new(
                games.Count+1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.Release
            );*/
            /* Non costruisco più GameDto, ma un entità nel nostro dbContext
            Game game = new()
            {
                Name = newGame.Name,
                Genre = dbContext.Genres.Find(newGame.GenreId),
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.Release
            };*/
            // Dopo aver creato il Mapping, posso fare direttamente questo
            Game game = newGame.ToEntity(); // Devo sistemare Genre
            // Visto che ho creato i DTO summary e Detail, non ho più bisogno 
            // di associare il Genre, ci pensa da sè
            // game.Genre = dbContext.Genres.Find(newGame.GenreId);
            // Aggiungo il game creato alla lista games
            //games.Add(game);
            dbContext.Games.Add(game);
            // Salvo le modifiche nel DB (SQL)
            dbContext.SaveChanges();
            /*
                var newGame = new GameDto(games.Count + 1, 
                                game.Name,
                                game.Genre,
                                game.Price,
                                game.Release);
                games.Add(newGame);
                return newGame;
            */
            // Restituisco l'esisto dell'operazione di PUT game
            // Results: 
            //  - built-in class che contiene i metodi per restituire i risultati delle operazioni
            // CreatedAtRoute: 
            //  - restituisce uno status code 201 Created e un URL per ottenere la risorsa creata
            // GetGameEndPointName: 
            //  - nome dell'endpoint che restituisce il game
            // Non devo restituire l'intera entità game, ma devo restituire DTOs
            // Trasformo game in un DTO

            
            /* Non ne ho più bisogno, ho creato il metodo ToDto() in GameMapping
            GameDto gameDto = new(
                game.Id,
                game.Name,
                // game.Genre non può essere null, altrimenti nel salvataggio con 
                // ForeignKey nel DB non avrebbe funzionato (dico tramite ! che non è null)
                game.Genre!.Name,
                game.Price,
                game.ReleaseDate
            );
            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, gameDto);
            */

            // Per semplificare le azioni di mappatura tra entità e DTOs, posso usare 
            // un AutoMapper o una Classe di estensione
            // Restituisco il game.toDto() direttamente
            return Results.CreatedAtRoute(GetGameEndPointName, new { 
                id = game.Id }, game.ToGameDetailsDto());

            // Dopo aver aggiungo il package MinimalApis.Extensions,
            // posso usare il metodo .WithParameterValidation()
        // invece che impostarlo per un Endpoint, posso assegnarlo al group
        // }).WithParameterValidation()
        });

        // PUT /games/1
        // Sistemo put per usare il context Database dbContext che è injected
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) => {
            var existingGame = dbContext.Games.Find(id);
            /* Non devo più controllare se presente nella lista, ma nel DB
            int index = games.FindIndex(game => game.ID == id);
            // Controllo se trovato un index valido
            if(index == -1){
                // Posso anche creare la risorsa o restituire 'not found'
                return Results.NotFound();
            }*/
            if(existingGame is null){
                return Results.NotFound();
            }
            /* Aggiorno il game
            games[index] = new GameSummaryDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.Release
            );*/
            // Devo trovare il gioco e rimpiazzarlo con la nuova entità
            dbContext.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));
            dbContext.SaveChanges();
            // Restituisco l'esisto dell'operazione di PUT game (204 No Content)
            return Results.NoContent();
            // Problema con la concorrenza, sto usando una lista, non è thread-safe
        });


        // DELETE /games/1
        // Sistemo delete per usare il context Database dbContext che è injected
        group.MapDelete("/{id}", (int id, GameStoreContext dbContext) => {
            // games.RemoveAll(game => game.ID == id);
            // Restituisco l'esisto dell'operazione di DELETE game (204 No Content)
            dbContext.Games.Where(game => game.Id == id).ExecuteDelete(); // Batch delete, efficiente
            return Results.NoContent();
        });
        return group;
    }
}