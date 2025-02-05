using System;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions{
    // Usato per automatizzare la migrazione del database all'avvio dell'applicazione
    // invece che farlo manuamente da terminale tramite comando: dotnet ef database update
    public static void MigrateDB(this WebApplication app){
        // Per poter usare il DB ho bisogno di uno scope
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate();
    }
}
