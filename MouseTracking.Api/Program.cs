using MouseTracking.Data.Extensions;
using MouseTracking.Data.Repository;
using MouseTracking.Domain;
using NLog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddMouseTrackingDbContext();
builder.Services.AddScoped<IMouseTrackingRepository, MouseTrackingRepository>();

var logger = LogManager.GetCurrentClassLogger();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.MapPost("/mouse-tracking", async (IMouseTrackingRepository repository, List<MouseMoveEventLog> mouseData) =>
{
    try
    {
        if (!mouseData.Any())
        {
            logger.Warn("Попытка отправки пустых данных.");
            return Results.BadRequest("Данные отсутствуют");
        }

        await repository.SaveMouseDataAsync(mouseData);
        logger.Info($"Успешно сохранено {mouseData.Count} событий.");

        return Results.Json(new { message = "Данные сохранены", count = mouseData.Count });
    }
    catch (Exception ex)
    {
        logger.Error(ex, "Ошибка при обработке данных.");
        return Results.Problem("Ошибка при обработке данных на сервере.");
    }
});

app.Run();