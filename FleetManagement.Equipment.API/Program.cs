using FleetManagement.Equipment.Infrastructure;
using FleetManagement.Equipment.Application;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var sqlConnectionString = builder.Configuration.GetConnectionString("SqlDatabase");
if (string.IsNullOrWhiteSpace(sqlConnectionString))
  throw new ArgumentNullException(nameof(sqlConnectionString));

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDatabase(sqlConnectionString);
builder.Services.AddApplication();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
  var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
  await db.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
