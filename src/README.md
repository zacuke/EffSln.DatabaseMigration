# EffSln.DatabaseMigration

Automatic SQL script execution on startup. A companion to code first migrations to help deploy arbitrary sql scripts.

On every startup, all the scripts in the `options.ScriptFolder` are ran against the databases(s). Multiple destinations are supported.

## Installation

```bash
dotnet add package EffSln.DatabaseMigration
```

## Quick Start

```csharp
// In Program.cs or Startup.cs
services.AddDatabaseMigration();
```

## Configuration

Use `DefaultConnection` and `data/procs` folder

```csharp
services.AddDatabaseMigration();
```

On multiple databases and specific different folder
```csharp
services.AddDatabaseMigration(o =>
{
    o.ConnectionStringNames = new[] { "DefaultConnection", "SandboxConnection" };
    o.ScriptFolder = "scripts";
});
```

## Additional Config

Run ef core code-first migrations (and run scripts) on multiple databases

```csharp
services.AddDatabaseMigration(o =>
{
    o.ConnectionStringNames = new[] { "DefaultConnection", "SandboxConnection" };
    o.ScriptFolder = "data/procs";
    o.MigrateDbContext = async (services, connectionString) =>
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(connectionString);

        using var dbContext = new AppDbContext(optionsBuilder.Options);
        await dbContext.Database.MigrateAsync();
    };
});
```

