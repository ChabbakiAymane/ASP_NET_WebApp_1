using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record class CreateGameDto(
    // Usando DataAnnotations posso impostare le regole di validazione direttamente nel DTO
    // Required: campo obbligatorio
    // StringLength: lunghezza massima del campo
    // ID viene dato dal Server
    [Required][StringLength(50)] string Name, // Resource Name
    // [Required][StringLength(10)] string Genre, // Resource Genre
    // Devo modificare Genre, perchè il Client mi manda un ID del genere e non il nome
    int GenreId, // Resource Genre
    [Range(1, 100)] decimal Price, // Resource Price
    DateOnly Release // Resource Release Date
);