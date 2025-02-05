using System;

namespace GameStore.Api.Entities;

public class Game
{
    public int Id { get; set; }
    
    // Con la keyword 'required' impongo che chi va a costruire istanze della classe Genre, 
    // deve passare un valore al costruttore per i parametri che non possono essere null
    // Altrimenti: 
    //  - string?
    //  - public required string Name { get; set; } = string.Empty;
    public required string Name { get; set; }

    // Collego l'entità Game con l'entità Genre (presa dal Database)
    public int GenreId { get; set; }

    // Con Genre? indico che il campo Genre può essere null
    public Genre? Genre { get; set; }

    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; }
}
