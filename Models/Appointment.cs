using AppointmentSystem.Models.Enums;

namespace AppointmentSystem.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = default!;
        public int SlotId { get; set; }
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Booked;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public Slot? Slot { get; set; }
    }
}