namespace AppointmentSystem.Dtos
{
    public class SettingsDto
    {
        public int SlotDurationMinutes { get; set; }
        public int MaxBookingsPerSlot { get; set; }
        public string WorkingHoursStart { get; set; } = default!;
        public string WorkingHoursEnd { get; set; } = default!;
    }
}