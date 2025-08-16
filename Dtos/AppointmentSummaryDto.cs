using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSystem.Dtos
{
    public class AppointmentSummaryDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = default!;
        public SlotSummaryDto? Slot{ get; set; }
    }
}