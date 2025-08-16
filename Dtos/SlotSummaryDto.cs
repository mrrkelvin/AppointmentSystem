using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppointmentSystem.Dtos
{
    public class SlotSummaryDto
    {
        public int Id { get; set; }
        public string? Date { get; set; }
        public string? Time { get; set; }
        [JsonPropertyName("available_slots")]
        public int AvailableSlots { get; set; }
    }
}