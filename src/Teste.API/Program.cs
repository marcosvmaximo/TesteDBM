using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Teste.API;
using Teste.API.Configurations;
using Teste.Core;
using Teste.Infra;
using Teste.Infra.Migrations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("TesteDb"));


// Fluent Migrations
builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(runner => runner
        .AddPostgres()
        .WithGlobalConnectionString("TesteDb")
        .ScanIn(typeof(First).Assembly).For.Migrations()
    )
    .AddLogging(lb => lb.AddConsole());
builder.Services.AddScoped<IMigrationRunner, MigrationRunner>();

// Extensions
builder.Services.AddDependencyInjection();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

// Migração
// using (var scope = app.Services.CreateScope())
// {
//     var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
//     runner.MigrateUp();
// }
app.Run();