using EvolveDb;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System.IO;
using System.Threading.Tasks;

namespace Backend_QDAO
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            MigrateDatabase();
            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<QDAO.Endpoint.Startup>();
                });

        private static void MigrateDatabase()
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true)
               .Build();
            var connectionString = config.GetValue<string>("ConnectionString");
            var connection = new NpgsqlConnection(connectionString);

            var evolve = new Evolve(connection)
            {
                Locations = new[] { "Migrations" },
                IsEraseDisabled = true
               
            };

            evolve.Migrate();
        }
    }
}
