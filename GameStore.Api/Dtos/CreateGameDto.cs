using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record class CreateGameDto(
    // Usando DataAnnotations posso impostare le regole di validazione direttamente nel DTO
    [Required][StringLength(50)] string Name, // Resource Name
    int GenreId, // Resource Genre
    [Range(1, 100)] decimal Price, // Resource Price
    DateOnly Release // Resource Release Date
);