namespace MouseTracking.Domain;

public class MouseMoveEventLog
{
    public int X { get; set; }
    public int Y { get; set; }
    public long T { get; set; } // Время в миллисекундах
}