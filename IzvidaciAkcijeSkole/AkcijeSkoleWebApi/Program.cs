using Microsoft.EntityFrameworkCore;
using AkcijeSkole.DataAccess.SqlServer.Data;
using AkcijeSkole.Repositories;
using AkcijeSkole.Repositories.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// create a configuration (app settings) from the appsettings file, depending on the ENV
IConfiguration configuration = builder.Environment.IsDevelopment()
    ? builder.Configuration.AddJsonFile("appsettings.Development.json").Build()
    : builder.Configuration.AddJsonFile("appsettings.json").Build();
// register the DbContext -EF ORM
// this allows the DbContext to be injected
builder.Services.AddDbContext<AkcijeSkoleDbContext>(
    options => options.UseSqlServer(configuration.GetConnectionString("AkcijeSkoleDB"))
);

builder.Services.AddTransient<ISkoleRepository, SkolaRepository>();
builder.Services.AddTransient<IEdukacijeRepository, EdukacijeRepository>();
builder.Services.AddTransient<IAkcijeRepository, AkcijeRepository>();
builder.Services.AddTransient<IAktivnostiRepository, AktivnostiRepository>();
builder.Services.AddTransient<ITerenskaLokacijaRepository, TerenskaLokacijaRepository>();
builder.Services.AddTransient<IMjestoRepository, MjestoRepository>();
builder.Services.AddTransient<IMaterijalnaPotrebaRepository, MaterijalnaPotrebaRepository>();
builder.Services.AddTransient<IZahtjeviRepository, ZahtjeviRepository>();
// Add services to the container.

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
