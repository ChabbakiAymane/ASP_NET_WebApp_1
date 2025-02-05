using GameStore.Api.Dtos;
using GameStore.Api.Entities;

namespace GameStore.Api.Mapping;

// Classe che si occupa di mappare i vari oggetti tra di loro (DTO, Entity, ecc)
public static class GameMapping
{
    public static Game ToEntity(this CreateGameDto game)
    {
        // Funzione che crea un oggetto Game a partire da un CreateGameDto
        return new Game()
        {
            Name = game.Name,
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.Release
        };
    }

    // Funzione che converte un Game in un GameSummaryDto
    public static GameSummaryDto ToGameSummaryDto(this Game game)
    {
        return new
        (
            game.Id,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
        );
    }

    // Funzione che converte un Game in un GameDetailDto
    public static GameDetailDto ToGameDetailsDto(this Game game)
    {
        return new
        (
            game.Id,
            game.Name,
            game.GenreId,
            game.Price,
            game.ReleaseDate
        );
    }
    // Funzione che converte un UpdateGameDto in un Game
    public static Game ToEntity(this UpdateGameDto game, int id)
    {
        return new Game()
        {
            Id = id,
            Name = game.Name,
            GenreId = game.GenreID,
            Price = game.Price,
            ReleaseDate = game.Release
        };
    }
}
