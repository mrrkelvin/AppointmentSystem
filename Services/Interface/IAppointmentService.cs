using AppointmentSystem.Common;
using AppointmentSystem.Dtos;

namespace AppointmentSystem.Services.Interface
{
    public interface IAppointmentService
    {
        Task<ServiceResponse<List<AppointmentSummaryDto>>> GetAppointmentsAsync();
        Task<ServiceResponse<AppointmentSummaryDto>> CreateAppointmentAsync(AppointmentDto dto);
        Task<ServiceResponse<CancelAppointmentResponseDto>> CancelAppointmentAsync(int id);
    }
}