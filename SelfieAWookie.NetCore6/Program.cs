using Microsoft.EntityFrameworkCore;
using SelfieAWookies.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructures.Data;
using SelfieAWookies.Core.Selfies.Infrastructures.Repositories;
using SelfieAWookie.NetCore6.ExtensionMethods;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

//var factory = new SelfiesContextFactory();
//var context = factory.CreateDbContext(null);

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

builder.Configuration.AddJsonFile("secrets.json", optional: true, reloadOnChange: true);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ISelfieRepository, DefaultSelfieRepository>();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomSecurity(configuration);
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    //options.SignIn.RequireConfirmedEmail = true;
}).AddEntityFrameworkStores<SelfiesContext>();

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

app.UseAuthentication();
app.UseAuthorization();
app.UseCors(SecurityMethods.DEFAULT_POLICY2);
app.MapControllers();

app.Run();
