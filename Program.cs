using Microsoft.EntityFrameworkCore;
using TaskTreeMD.Repository;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TaskTreeDBContext>(options => {
    //options.UseSqlite("Data Source=Data\\TaskTreeMD.sqlite");
    // TODO: Inject Configuration and get connection string from config
    options.UseSqlite(configuration.GetConnectionString("CustomersSqliteConnectionString"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
