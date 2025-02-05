using System;

namespace GameStore.Api.Entities;

public class Game
{
    public int Id { get; set; }
    
    public required string Name { get; set; }

    // Collego l'entità Game con l'entità Genre (presa dal Database)
    public int GenreId { get; set; }

    // Con Genre? indico che il campo Genre può essere null
    public Genre? Genre { get; set; }

    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; }
}
