using MouseTracking.Api.Extensions;
using MouseTracking.Application.Service;
using MouseTracking.Data.Repository;
using MouseTracking.Domain.Entities;
using MouseTracking.Domain.Interfaces;
using NLog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

// ������������ �������
builder.Services.AddMouseTrackingDbContext();
builder.Services.AddScoped<IMouseTrackingRepository, MouseTrackingRepository>();
builder.Services.AddScoped<MouseMoveEventService>();

var logger = LogManager.GetCurrentClassLogger();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.MapPost("/mouse-tracking", async (MouseMoveEventService service, List<MouseMoveEventLog> mouseData) =>
{
    try
    {
        if (!mouseData.Any())
        {
            logger.Warn("������� �������� ������ ������.");
            return Results.BadRequest("������ �����������");
        }

        await service.SaveMouseDataAsync(mouseData);
        logger.Info($"������� ��������� {mouseData.Count} �������.");

        return Results.Json(new { message = "������ ���������", count = mouseData.Count });
    }
    catch (Exception ex)
    {
        logger.Error(ex, "������ ��� ��������� ������.");
        return Results.Problem("������ ��� ��������� ������ �� �������.");
    }
});


app.Run();