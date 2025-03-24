using MouseTracking.Domain;

namespace MouseTracking.Data.Repository;

public interface IMouseTrackingRepository
{
    Task SaveMouseDataAsync(List<MouseMoveEventLog> mouseData);
}