using IzvidaciAkcijeSkoleWebApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// create a configuration (app settings) from the appsettings file, depending on the ENV
IConfiguration configuration = builder.Environment.IsDevelopment()
   ? builder.Configuration.AddJsonFile("appsettings.Development.json").Build()
   : builder.Configuration.AddJsonFile("appsettings.json").Build();
// register the DbContext -EF ORM// this allows the DbContext to be injected
builder.Services.AddDbContext<AkcijeSkoleContext>(options => 
options.UseSqlServer(configuration.GetConnectionString("AkcijeSkole")));

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
