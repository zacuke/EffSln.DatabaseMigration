using EffSln.DatabaseMigration.Extensions;
using EffSln.DatabaseMigration.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EffSln.DatabaseMigration;


public class MigrationService(
    IServiceProvider serviceProvider,
    IConfiguration configuration,
    DatabaseMigrationOptions options,
    ILogger<MigrationService> logger) : IHostedService
{
 
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var path = Path.Combine(AppContext.BaseDirectory, options.ScriptFolder);
        logger.LogInformation("Deploying scripts {path}", path);
 
        foreach (var connStrName in options.ConnectionStringNames)
        {
            var connStr = configuration.GetConnectionString(connStrName)
                ?? throw new Exception(connStrName + " not found");

            await scope.RunMigrateAndScriptsAsync(options, logger, connStr, connStrName, path);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

   
   
}
