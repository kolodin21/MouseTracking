using MouseTracking.Domain;
using System.Text.Json;

namespace MouseTracking.Data.Repository
{
    public class MouseTrackingRepository : IMouseTrackingRepository
    {
        private readonly MouseTrackingDbContext _context;

        public MouseTrackingRepository(MouseTrackingDbContext context)
        {
            _context = context;
        }

        public async Task SaveMouseDataAsync(List<MouseMoveEvent> mouseData)
        {
            var jsonData = JsonSerializer.Serialize(mouseData);
            var entity = new MouseMoveEvent { DataJson = jsonData };
            _context.MouseMoveEvents.Add(entity);
            await _context.SaveChangesAsync();
        }
    }
}
