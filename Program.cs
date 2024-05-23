using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuizApp;
using QuizApp.Controllers;
using QuizApp.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    var password = Environment.GetEnvironmentVariable("MSSQL_SA_PASSWORD");
    var host = Environment.GetEnvironmentVariable("MSSQL_HOST");
    connectionString = string.Format(connectionString!, host, password);

    options.UseSqlServer(connectionString);
});

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseEndpoints(_ => { });

app.MapProductEndpoints();

app.Run();
