# ASP-NET: GAME CATALOG
Web-Application di un catalogo di videogiochi in C# (ASP_NET).

---
### **[NuGet](https://www.nuget.org/)**

**dotnet-ef**:
  ```cs
    dotnet tool install --global dotnet-ef --version 8.0.2
  ```
**Microsoft.EntityFrameworkCore.Sqlite**:
  ```cs
    dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.2
  ```
**Microsoft.EntityFrameworkCore.Desing**:
  ```cs
    dotnet add package Microsoft.EntityFrameworkCore.Desing --version 8.0.2
  ```

---

### **Build/Run Project**
  ```cs
    ../WebApplication.API > dotnet build
  ```

  ```cs
    ../WebApplication.API > dotnet run
  ```

---

### **MigrationDB**
  - Initial Migration:
    ```cs
      dotnet ef migrations add InitialCreate --output-dir Data\Migrations
    ```
  - SeedGenres Migration:
    ```cs
      dotnet ef migrations add SeedGenres --output-dir Data\Migrations
    ```
  - Update Database:
    ```cs
      dotnet ef database update
    ```
