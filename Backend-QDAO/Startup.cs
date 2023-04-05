using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QDAO.Application;
using QDAO.Application.Services;
using QDAO.Application.Handlers.Proposal;
using QDAO.Application.GrpcClients;
using QDAO.Endpoint.HostedServices;
using QDAO.Persistence.Repositories.ProposalQuorum;
using QDAO.Persistence;
using QDAO.Persistence.Repositories.Transaction;

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


            services.AddScoped<ProposalGrpcClient>();
            services.AddScoped<AdminGrpcClient>();


            services.AddSingleton<TransactionRepository>();
         

            // services.AddHostedService<QuorumCrisisBackgroundService>();

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); 
            });
        }
    }
}
