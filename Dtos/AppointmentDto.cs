using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSystem.Dtos
{
    public class AppointmentDto
    {
        public string CustomerName { get; set; } = default!;
        public int SlotId { get; set; }
    }
}