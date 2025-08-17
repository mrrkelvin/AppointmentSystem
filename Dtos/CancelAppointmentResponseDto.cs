namespace AppointmentSystem.Dtos
{
    public class CancelAppointmentResponseDto
    {
        public int AppointmentId { get; set; }
        public bool Cancelled { get; set; }
    }
}