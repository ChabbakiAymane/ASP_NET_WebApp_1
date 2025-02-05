
using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record class UpdateGameDto(
    // ID viene dato dal Server
    [Required][StringLength(50)] string Name, // Resource Name
    // [Required][StringLength(10)] string Genre, // Resource Genre
    int GenreID, // Resource Genre ID
    [Range(1, 100)] decimal Price, // Resource Price
    DateOnly Release // Resource Release Date
);
