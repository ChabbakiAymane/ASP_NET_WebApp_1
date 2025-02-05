using System;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions{
    // Usato per automatizzare la migrazione del database all'avvio dell'applicazione
    // invece che farlo manuamente da terminale tramite comando: dotnet ef database update
    // Ora devo restuire lo stato dell'applicazione, quindi devo cambiare il return-type da void a Task
    // La convenzione prevede di aggiungere il suffisso Async al nome del metodo
    public static async Task MigrateDBAsync(this WebApplication app){
        // Per poter usare il DB ho bisogno di uno scope
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        // Devo rendere anche questa migrazione asincrona
        await dbContext.Database.MigrateAsync();
    }
}
