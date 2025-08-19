using AppointmentSystem.Common;
using AppointmentSystem.Dtos;

namespace AppointmentSystem.Services.Interface
{
    public interface ISettingService
    {
        Task<ServiceResponse<SettingsDto>> GetSettings();
        Task<ServiceResponse<SettingsDto>> UpdateSettings(SettingsDto dto);
    }
}