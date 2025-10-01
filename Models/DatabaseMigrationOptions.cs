using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
namespace EffSln.DatabaseMigration.Models;
/// <summary>
/// Provides configuration options for controlling database migration behavior, including connection string selection,
/// script folder location, and custom migration logic.
/// </summary>
/// <remarks>Use this class to specify how database migrations should be performed within an application. It
/// allows customization of connection string sources, the folder containing migration scripts, and the migration
/// process itself via a delegate. This type is typically used when initializing or configuring database migration
/// services.</remarks>
public class DatabaseMigrationOptions
{
    /// <summary>
    /// Gets or sets the names of the connection strings available for use. If null or empty, DefaultConnection string will be used.
    /// </summary>
    public string[] ConnectionStringNames { get; set; } = ["DefaultConnection"];
    /// <summary>
    /// Gets or sets the folder path where script files are located. Defaults to "data/procs".
    /// </summary>
    public string ScriptFolder { get; set; } = "data/procs";
 
    /// <summary>
    /// Gets or sets the delegate used to perform database context migration asynchronously.
    /// </summary>
    /// <remarks>The delegate receives an <see cref="IServiceProvider"/> for dependency resolution, the database
    /// connection string, an <see cref="ILogger"/> for logging, and a migration name. It should return a <see cref="Task"/>
    /// that completes when the migration operation finishes. Assign this property to customize how database migrations are
    /// executed during application startup or deployment.</remarks>
    public Func<IServiceProvider, string, ILogger, string, Task>?  MigrateDbContext { get; set; }

     
}
