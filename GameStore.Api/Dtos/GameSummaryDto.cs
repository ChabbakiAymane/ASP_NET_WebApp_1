namespace GameStore.Api.Dtos;

public record class GameSummaryDto(
    int ID, // Resource Identifier
    string Name, // Resource Name
    string Genre, // Resource Genre
    decimal Price, // Resource Price
    DateOnly Release // Resource Release Date
);