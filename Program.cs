using GiftFormAPI.Controllers;
using GiftFormAPI.Models;
using GiftFormAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureService();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GiftFormAPI", Version = "v1" });
});

builder.Services.AddDbContext<GiftServiceDbContext>(options =>
{
    options.UseInMemoryDatabase("GiftDatabase");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GiftFormAPI v1"));

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<GiftServiceDbContext>();

        SeedData(dbContext);
    }

    Console.WriteLine($"EF Core In-Memory Database Provider:");
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync(app.Environment.ApplicationName + System.Environment.NewLine +
        "Environment:" + app.Environment.EnvironmentName);
});

app.Run();

void SeedData(GiftServiceDbContext dbContext)
{
    var children = new List<Child>
    {
        new Child { Name = "Petras", LastName = "Jonaitis" },
        new Child { Name = "Romas", LastName = "Mikða" },
        new Child { Name = "Jonas", LastName = "Petraitis" }
    };

    dbContext.Children.AddRange(children);
    dbContext.SaveChanges();

    var gifts = new List<Gift>
    {
        new Gift { Name = "Horse", ChildId = children[0].Id },
        new Gift { Name = "Balloons", ChildId = children[0].Id },
        new Gift { Name = "Toy Car", ChildId = children[1].Id },
        new Gift { Name = "Doll", ChildId = children[1].Id },
        new Gift { Name = "Board Game", ChildId = children[2].Id }
    };

    dbContext.Gifts.AddRange(gifts);
    dbContext.SaveChanges();
}