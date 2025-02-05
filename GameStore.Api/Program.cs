using GameStore.Api.Data;
using GameStore.Api.EndPoints;

var builder = WebApplication.CreateBuilder(args);

// Dopo aver settato il Entity FrameWork Core e le varie entità (Entities)
// Consigliato mantenere la stringa di connessione in un file: appsettings.json
//var connectionString = "Data Source=GameStore.db";
var connectionString = builder.Configuration.GetConnectionString("GameStore");
// Mi collego al database SQLite
// Sarebbe da mettere come Scoped Service (risolve problemi di concorrenza/transazioni, la connessione DB è onerosa)
builder.Services.AddSqlite<GameStoreContext>(connectionString);


var app = builder.Build();
// Dopo aver spostato tutto nella Classe GameEndpoints, posso chiamare il metodo MapGamesEndpoints
app.MapGamesEndpoints();

app.MigrateDB();

app.Run();