using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Backend_QDAO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<QDAO.Endpoint.Startup>();
                });
    }
}
