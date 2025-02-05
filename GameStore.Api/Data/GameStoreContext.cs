using System;
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    // Creo una proprietà di tipo DbSet che mi permette di accedere alla tabella Games
    public DbSet<Game> Games => Set<Game>();
    // Creo una proprietà di tipo DbSet che mi permette di accedere alla tabella Genres
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        // Metodo eseguito quando si avvia la migration per creare il DB
        // base.OnModelCreating(modelBuilder);
        // Funzione che controlla che qualsiasi dato inserito qua, deve esistere quando 
        // il processo di migration finisce
        modelBuilder.Entity<Genre>().HasData(
            new {Id = 1, Name = "Fighting", },
            new {Id = 2, Name = "Roleplaying", },
            new {Id = 3, Name = "Sports", },
            new {Id = 4, Name = "Racing", },
            new {Id = 5, Name = "Kids & Family", }
        );
        // Queste categorie verranno create una volta terminata la migration
    }
}