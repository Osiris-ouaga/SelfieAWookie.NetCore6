using Microsoft.EntityFrameworkCore;
using SelfieAWookies.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructures.Data;
using SelfieAWookies.Core.Selfies.Infrastructures.Repositories;
using SelfieAWookie.NetCore6.ExtensionMethods;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ISelfieRepository, DefaultSelfieRepository>();
builder.Services.AddSwaggerGen();
// Add services to the container.
builder.Services.AddDbContext<SelfiesContext>(options =>
{
    options.UseSqlServer(sqlOptions => {
        options.UseSqlServer(configuration.GetConnectionString("SelfiesDatabase"), sqlOptions => { });
    });
});

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
