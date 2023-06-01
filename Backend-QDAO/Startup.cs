using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using QDAO.Application.Services;
using QDAO.Application.Handlers.Proposal;
using QDAO.Persistence;
using QDAO.Application.Pipelines;
using Prometheus;
using QDAO.Persistence.Repositories.User;
using QDAO.Persistence.Repositories.Transaction;
using QDAO.Persistence.Repositories.Proposal;
using QDAO.Persistence.Repositories.ProposalQuorum;
using QDAO.Endpoint.HostedServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using QDAO.Application.Services.DTOs.Security;
using System.Security.Claims;

namespace QDAO.Endpoint
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.RequireHttpsMetadata = false;
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                            // укзывает, будет ли валидироваться издатель при валидации токена
                            ValidateIssuer = true,
                            // строка, представляющая издателя
                            ValidIssuer = AuthOptions.ISSUER,

                            // будет ли валидироваться потребитель токена
                            ValidateAudience = true,
                            // установка потребителя токена
                            ValidAudience = AuthOptions.AUDIENCE,
                            // будет ли валидироваться время существования
                            ValidateLifetime = true,

                            // установка ключа безопасности
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(), 
                            // валидация ключа безопасности
                            ValidateIssuerSigningKey = true,
                            RoleClaimType = ClaimsIdentity.DefaultRoleClaimType,
                            NameClaimType = ClaimsIdentity.DefaultNameClaimType
                       };
                   });

            services.AddScoped<TransactionCreator>();
            services.AddScoped<ContractsManager>();
            services.AddScoped<SecurityService>();

            services.AddSingleton<TransactionRepository>();
            services.AddSingleton<ProposalRepository>();
            services.AddSingleton<UserRepository>();

            services.AddScoped<TransactionEventsDecoder>();
         

            services.AddHostedService<EventsProcessingBgService>();
            services.AddScoped<EventsProcessingPipeline>();

            services.AddHostedService<ProposalReadyForVotingBgService>();
            services.AddScoped<ProposalStateTransitionPipeline>();

            services.AddScoped<ProposalQrisisQueueRepository>();
            services.AddSingleton<IDapperExecutor, DapperExecutor>();

          
            services.AddMediatR(typeof(GetCreateProposalTxQuery.Handler));
        }

    
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
          
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
            app.UseDeveloperExceptionPage();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapMetrics();
                endpoints.MapControllers();
            });

            app.UseHttpMetrics();
            //  app.UseMetricServer();

        }
    }
}
