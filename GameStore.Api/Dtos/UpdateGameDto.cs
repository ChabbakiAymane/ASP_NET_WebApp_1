
using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record class UpdateGameDto(
    [Required][StringLength(50)] string Name, // Resource Name
    int GenreID, // Resource Genre ID
    [Range(1, 100)] decimal Price, // Resource Price
    DateOnly Release // Resource Release Date
);
