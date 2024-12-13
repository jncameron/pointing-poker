using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PointingPoker.Data;
using PointingPoker.Services;
using PointingPokerBackend.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add InMemory Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("PointingPokerDb"));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });
builder.Services.AddSignalR();
builder.Services.AddHostedService<SessionCleanupService>();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PointingPoker API", Version = "v1" });
});


var app = builder.Build();

app.UseRouting();

app.MapControllers();
app.MapHub<PokerHub>("/pokerHub");

app.Run();
