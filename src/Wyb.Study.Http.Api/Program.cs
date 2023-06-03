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
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region add Services
            builder.Services.AddTransient<IUserService, UserService>();
            #endregion

            #region add repositories
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            #endregion

            builder.Services.AddSkyAPM();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}