using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Common;
using AppointmentSystem.Dtos;
using AppointmentSystem.Models;

namespace AppointmentSystem.Services.Interface
{
    public interface ISlotService
    {
        Task<ServiceResponse<List<SlotSummaryDto>>> GetSlotsAsync();
        Task<ServiceResponse<List<SlotDto>>> GenerateSlotAsync(DateOnly date);
    }
}