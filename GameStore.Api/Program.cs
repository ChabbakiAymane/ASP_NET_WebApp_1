using GameStore.Api.Data;
using GameStore.Api.EndPoints;

var builder = WebApplication.CreateBuilder(args);

// Consigliato mantenere la stringa di connessione in un file: appsettings.json
var connectionString = builder.Configuration.GetConnectionString("GameStore");
// Mi collego al database SQLite
builder.Services.AddSqlite<GameStoreContext>(connectionString);


var app = builder.Build();
app.MapGamesEndpoints();

app.MigrateDB();

app.Run();