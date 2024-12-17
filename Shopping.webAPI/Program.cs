using System.Linq.Expressions;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder (args);

// Add services to the container.

builder.Services.AddScoped<IProductRepository, ProductRepository> ();
builder.Services.AddControllers ();
builder.Services.AddDbContext<StoreContext> (x => x.UseSqlite (builder.Configuration.GetConnectionString ("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer ();
builder.Services.AddSwaggerGen ();

var app = builder.Build ();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment ()) {
    app.UseSwagger ();
    app.UseSwaggerUI ();
}

app.UseHttpsRedirection ();

app.UseAuthorization ();

app.MapControllers ();

ApplyMigration ();

app.Run ();

async void ApplyMigration () {

    using (var scope = app.Services.CreateScope ()) {
        var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory> ();
        try {
            var ctx = scope.ServiceProvider.GetRequiredService<StoreContext> ();
            await ctx.Database.MigrateAsync ();
            await StoreContextSeed.SeedAsync (ctx, loggerFactory);
        } catch (Exception ex) {
            var logger = loggerFactory.CreateLogger<Program> ();
            logger.LogError (ex, "Error while applying migration ..");
        }
    }
}