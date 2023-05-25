using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using QDAO.Application.Services;
using QDAO.Application.Handlers.Proposal;
using QDAO.Application.GrpcClients;
using QDAO.Persistence;
using QDAO.Application.Pipelines;
using Prometheus;
using QDAO.Persistence.Repositories.User;
using QDAO.Persistence.Repositories.Transaction;
using QDAO.Persistence.Repositories.Proposal;
using QDAO.Persistence.Repositories.ProposalQuorum;
using QDAO.Endpoint.HostedServices;

namespace QDAO.Endpoint
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen();

            services.AddScoped<TransactionCreator>();
            services.AddScoped<ContractsManager>();
            services.AddScoped<SecurityService>();


            services.AddScoped<ProposalGrpcClient>();
            services.AddScoped<AdminGrpcClient>();


            services.AddSingleton<TransactionRepository>();
            services.AddSingleton<ProposalRepository>();
            services.AddSingleton<UserRepository>();

            services.AddScoped<TransactionEventsDecoder>();
         

            services.AddHostedService<EventsProcessingBgService>();
            services.AddScoped<EventsProcessingPipeline>();

            services.AddScoped<ProposalQrisisQueueRepository>();
            services.AddSingleton<IDapperExecutor, DapperExecutor>();

          
            services.AddMediatR(typeof(CreateProposalTxQuery.Handler));
        }

    
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

          //  app.UseMetricServer();
            app.UseHttpMetrics();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapMetrics();
                endpoints.MapControllers(); 
            });
        }
    }
}
