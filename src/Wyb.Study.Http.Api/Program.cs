using Microsoft.AspNetCore.HttpLogging;
using NLog;
using NLog.Web;
using Wyb.Study.IRepositories;
using Wyb.Study.IServices;
using Wyb.Study.Repositories;
using Wyb.Study.Services;

namespace Wyb.Study.Http.Api
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
                #region add Services
                builder.Services.AddTransient<IUserService, UserService>();
                builder.Services.AddTransient<IRoleService, RoleService>();
                #endregion

                #region add repositories
                builder.Services.AddTransient<IUserRepository, UserRepository>();
                builder.Services.AddTransient<IRoleRepository, RoleRepository>();
                #endregion

                builder.Services.AddSkyAPM();

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                //nlog services
                builder.Logging.ClearProviders();
                builder.Host.UseNLog();

                // cors
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy(name: "MyAllowSpecificOrigins",
                                      policy =>
                                      {
                                          policy.AllowAnyOrigin()
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            ;
                                      });
                });

                builder.Services.AddHttpLogging(logging =>
                {
                    logging.LoggingFields = HttpLoggingFields.All;
                });

                var app = builder.Build();

                app.UseHttpLogging();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseCors("MyAllowSpecificOrigins");

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