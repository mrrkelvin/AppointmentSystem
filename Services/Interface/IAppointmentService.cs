using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Common;
using AppointmentSystem.Dtos;
using AppointmentSystem.Models;

namespace AppointmentSystem.Services.Interface
{
    public interface IAppointmentService
    {
        Task<ServiceResponse<List<AppointmentSummaryDto>>> GetAppointmentsAsync();
        Task<ServiceResponse<AppointmentSummaryDto>> CreateAppointmentAsync(AppointmentDto dto);
        Task<ServiceResponse<CancelAppointmentResponseDto>> CancelAppointmentAsync(int id);
    }
}