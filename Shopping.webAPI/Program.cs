using System.Linq.Expressions;
using AutoMapper;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.webAPI.Errors;
using Shopping.webAPI.Extensions;
using Shopping.webAPI.Helpers;
using Shopping.webAPI.Middleware;

var builder = WebApplication.CreateBuilder (args);

// Add services to the container.

builder.Services.AddAutoMapper (typeof (MappingProfiles));
builder.Services.AddControllers ();
builder.Services.AddDbContext<StoreContext> (x => x.UseSqlite (builder.Configuration.GetConnectionString ("DefaultConnection")));

builder.Services.AddApplicationServices ();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer ();
builder.Services.AddSwaggerGen ();

var app = builder.Build ();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment ()) {
    app.UseSwagger ();
    app.UseSwaggerUI ();
}

app.UseMiddleware<ExceptionMiddleware> ();
//when the client uses a route that doesn't exist we redirect it to the error controller with error code 0 =(404 not found)
app.UseStatusCodePagesWithReExecute ("/errors/{0}");
app.UseHttpsRedirection ();
app.UseStaticFiles ();
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