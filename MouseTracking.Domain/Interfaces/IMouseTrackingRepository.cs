using MouseTracking.Domain.Entities;

namespace MouseTracking.Domain.Interfaces;

public interface IMouseTrackingRepository
{
    Task SaveMouseDataAsync(List<MouseMoveEventLog> mouseData);
}