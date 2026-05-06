using FleetManagement.Equipment.Infrastructure;
using FleetManagement.Equipment.Application;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    var appConfigEndpoint = builder.Configuration["Azure:AppConfig"];
    var appConfigLabel = builder.Configuration["Azure:AppConfigLabel"];

    if (!string.IsNullOrEmpty(appConfigEndpoint))
    {
        builder.Configuration.AddAzureAppConfiguration(options =>
        {
            if (appConfigEndpoint.StartsWith("Endpoint="))
            {
                options.Connect(appConfigEndpoint)
                       .Select("*")
                       .Select("*", "dev");
            }
            else if (Uri.TryCreate(appConfigEndpoint, UriKind.Absolute, out var endpoint))
            {
                options.Connect(endpoint, new DefaultAzureCredential())
                       .Select("*")
                       .Select("*", "dev");
            }
        });
    }
}
else
{
    var appConfigEndpoint = builder.Configuration["Azure:AppConfig"];
    var appConfigLabel = builder.Configuration["Azure:AppConfigLabel"];

    if (!string.IsNullOrEmpty(appConfigEndpoint))
    {
        builder.Configuration.AddAzureAppConfiguration(options =>
        {
            options.Connect(new Uri(appConfigEndpoint), new DefaultAzureCredential())
                   .Select("*");

            if (!string.IsNullOrEmpty(appConfigLabel))
            {
                options.Select("*", appConfigLabel);
            }
        });
    }
}

var sqlConnectionString = builder.Configuration.GetConnectionString("SqlDatabase");

if (string.IsNullOrWhiteSpace(sqlConnectionString))
  throw new ArgumentNullException(nameof(sqlConnectionString));

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDatabase(sqlConnectionString);
builder.Services.AddInfrastructure();
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
