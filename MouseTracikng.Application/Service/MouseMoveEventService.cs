using MouseTracking.Domain.Entities;
using MouseTracking.Domain.Interfaces;

namespace MouseTracking.Application.Service
{
    public class MouseMoveEventService(IMouseTrackingRepository mouseTrackingRepository)
    {
        public async Task SaveMouseDataAsync(List<MouseMoveEventLog> mouseData) =>
            await mouseTrackingRepository.SaveMouseDataAsync(mouseData);

    }
}
