using Microsoft.EntityFrameworkCore;
using MouseTracking.Domain;
using NLog;
using System.Text.Json;

namespace MouseTracking.Data.Repository
{
    public class MouseTrackingRepository(MouseTrackingDbContext _context) : IMouseTrackingRepository
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public async Task SaveMouseDataAsync(List<MouseMoveEventLog> mouseData)
        {
            try
            {
                if (mouseData == null || mouseData.Count == 0)
                {
                    throw new ArgumentException("Список событий мыши пуст или null.");
                }

                var jsonData = JsonSerializer.Serialize(mouseData);
                var entity = new MouseMoveEvent { DataJson = jsonData };

                _context.MouseMoveEvents.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (JsonException ex)
            {
                _logger.Error(ex, "Ошибка сериализации JSON.");
                throw;
            }
            catch (DbUpdateException ex)
            {
                _logger.Error(ex, "Ошибка при сохранении в БД.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Неизвестная ошибка при сохранении данных.");
                throw;
            }
        }
    }
}
