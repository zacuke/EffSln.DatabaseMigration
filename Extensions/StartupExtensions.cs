using EffSln.DatabaseMigration.Models;
using Microsoft.Extensions.DependencyInjection;
namespace EffSln.DatabaseMigration.Extensions;
public static class StartupExtensions
{
    public static DatabaseMigrationOptions AddDatabaseMigration(this IServiceCollection services,
        Action<DatabaseMigrationOptions>? configureOptions = null)
    {
        var options = new DatabaseMigrationOptions();
        configureOptions?.Invoke(options);

        services.AddSingleton(options);
 
        services.AddHostedService<MigrationService>();
     

        return options;
    }
}