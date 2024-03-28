using Microsoft.AspNetCore.HttpLogging;
using Nacos.AspNetCore.V2;
using NLog;
using NLog.Web;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Nacos;
using Ocelot.Provider.Polly;

namespace Shop.ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init main");
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.
                builder.Host.UseNacosConfig("nacos");
                builder.Services.AddNacosAspNet(builder.Configuration);

                builder.Services.AddOcelot().AddNacosDiscovery().AddPolly();

                //nlog services
                builder.Logging.ClearProviders();
                builder.Host.UseNLog();

                builder.Services.AddHttpLogging(logging =>
                {
                    logging.LoggingFields = HttpLoggingFields.All;
                });

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                app.UseHttpLogging();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }
    }
}