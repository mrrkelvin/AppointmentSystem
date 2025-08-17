using AppointmentSystem.Common;
using AppointmentSystem.Dtos;

namespace AppointmentSystem.Services.Interface
{
    public interface ISlotService
    {
        Task<ServiceResponse<List<SlotSummaryDto>>> GetSlotsAsync();
        Task<ServiceResponse<List<SlotDto>>> GenerateSlotAsync(DateOnly date);
    }
}