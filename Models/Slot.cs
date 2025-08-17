namespace AppointmentSystem.Models
{
    public class Slot
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int MaxBookings { get; set; } = 1;

        // Navigation collection
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}