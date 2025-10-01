using EffSln.DatabaseMigration.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace EffSln.DatabaseMigration.Extensions
{
    public static class MigrateExtensions
    {
        public static async Task RunMigrateAndScriptsAsync(this IServiceScope scope, DatabaseMigrationOptions options, ILogger logger, string connStr, string connectionStringName, string path)
        {
            if (!string.IsNullOrEmpty(connStr))
            {
                if (options.MigrateDbContext != null)
                {
                    await options.MigrateDbContext(scope.ServiceProvider, connStr, logger, connectionStringName);
                }

                logger.LogInformation("Running SQL scripts on {connectionStringName}", connectionStringName);
                connStr.RunSqlScripts(path);
            }
        }

        public static void RunSqlScripts(this string connString, string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                return;
            }

            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            var files = Directory.GetFiles(folderPath, "*.sql");
            if (files.Length == 0)
            {
                return;
            }

            foreach (var file in files)
            {
                var sql = File.ReadAllText(file);
                using var cmd = new NpgsqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
