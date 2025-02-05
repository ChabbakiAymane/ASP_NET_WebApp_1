namespace GameStore.Api.Dtos;

public record class GameDetailDto(
    int Id, // Resource Id
    string Name, // Resource Name
    int GenreId, // Resource Genre
    decimal Price, // Resource Price
    DateOnly ReleaseDate // Resource Release Date
);
