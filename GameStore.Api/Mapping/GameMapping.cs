using System;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;

namespace GameStore.Api.Mapping;

public static class GameMapping
{
    public static Game ToEntity(this CreateGameDto game){
        return new Game()
        {
            Name = game.Name,
            // Non posso farlo qua, devo farlo nel controller
            //Genre = dbContext.Genres.Find(game.GenreId),
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.Release
        };
    }

    public static GameSummaryDto ToGameSummaryDto(this Game game){
        return new(
            game.Id,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
        );
    }

    public static GameDetailDto ToGameDetailsDto(this Game game){
        return new(
            game.Id,
            game.Name,
            game.GenreId,
            game.Price,
            game.ReleaseDate
        );
    }

    public static Game ToEntity(this UpdateGameDto game, int id){
        return new Game()
        {
            Id = id,
            Name = game.Name,
            // Non posso farlo qua, devo farlo nel controller
            //Genre = dbContext.Genres.Find(game.GenreId),
            GenreId = game.GenreID,
            Price = game.Price,
            ReleaseDate = game.Release
        };
    }
}
