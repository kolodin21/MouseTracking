using MouseTracking.Data.Extensions;
using MouseTracking.Data.Repository;
using MouseTracking.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMouseTrackingDbContext();

builder.Services.AddScoped<IMouseTrackingRepository, MouseTrackingRepository>();

var app = builder.Build();

app.MapPost("/api/mouse", async (IMouseTrackingRepository repository, MouseMoveEvent[] mouseData) =>
{
    await repository.SaveMouseDataAsync(mouseData.ToList());
    return Results.Ok();
});

app.UseHttpsRedirection();

app.Run();
