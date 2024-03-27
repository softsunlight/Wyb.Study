using Elastic.Apm.NetCoreAll;
using Nacos.AspNetCore.V2;
using Nacos.V2.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseNacosConfig("nacos");

// Add services to the container.
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSeq();
});
builder.Services.AddNacosV2Config(builder.Configuration);
builder.Services.AddNacosAspNet(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//app.UseAllElasticApm(builder.Configuration);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();


app.MapControllers();

app.Run();
