using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSystem.Dtos
{
    public class CancelAppointmentResponseDto
    {
        public int AppointmentId { get; set; }
        public bool Cancelled { get; set; }
    }
}