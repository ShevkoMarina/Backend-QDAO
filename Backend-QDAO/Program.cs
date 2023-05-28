using EvolveDb;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
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
            var connectionString = "User Id=postgres;Password=sukaBlyat123;Server=db.kidrgducdfhhwdjdvwoq.supabase.co;Port=5432;Database=postgres";
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
