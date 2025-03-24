using System.ComponentModel.DataAnnotations.Schema;

namespace MouseTracking.Domain
{
    public class MouseMoveEvent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DataJson { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
