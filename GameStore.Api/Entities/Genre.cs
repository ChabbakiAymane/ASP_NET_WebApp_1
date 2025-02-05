using System;

namespace GameStore.Api.Entities;

public class Genre
{
    public int Id { get; set; }
    
    // Con la keyword 'required' impongo che chi va a costruire istanze della classe Genre, 
    // deve passare un valore al costruttore per i parametri che non possono essere null
    // Altrimenti: 
    //  - string?
    //  - public required string Name { get; set; } = string.Empty;
    public required string Name { get; set; }
}
