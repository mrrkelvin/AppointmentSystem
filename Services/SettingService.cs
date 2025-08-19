using AppointmentSystem.Common;
using AppointmentSystem.Data;
using AppointmentSystem.Dtos;
using AppointmentSystem.Models;
using AppointmentSystem.Services.Interface;
using AppointmentSystem.Validators;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AppointmentSystem.Services
{
    public class SettingService(AppDbContext context, IMapper mapper) : ISettingService
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<ServiceResponse<SettingsDto>> GetSettings()
        {
            var resp = new ServiceResponse<SettingsDto>();

            var settings = await _context.Settings.ToListAsync();

            var dto = new SettingsDto
            {
                SlotDurationMinutes = int.Parse(settings.First(x => x.Key == "SlotDurationMinutes").Value),
                MaxBookingsPerSlot = int.Parse(settings.First(x => x.Key == "MaxBookingsPerSlot").Value),
                WorkingHoursStart = settings.First(x => x.Key == "WorkingHoursStart").Value,
                WorkingHoursEnd = settings.First(x => x.Key == "WorkingHoursEnd").Value
            };

            resp.Data = dto;
            return resp;
        }

        public async Task<ServiceResponse<SettingsDto>> UpdateSettings(SettingsDto dto)
        {
            var resp = new ServiceResponse<SettingsDto>();

            var validator = new SettingsDtoValidator();
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                resp.AddError(validationResult.Errors.FirstOrDefault()!.ErrorMessage);
                return resp;
            }

            var settings = new List<Setting>
            {
                new() { Key = "SlotDurationMinutes", Value = dto.SlotDurationMinutes.ToString() },
                new() { Key = "MaxBookingsPerSlot", Value = dto.MaxBookingsPerSlot.ToString() },
                new() { Key = "WorkingHoursStart", Value = dto.WorkingHoursStart },
                new() { Key = "WorkingHoursEnd", Value = dto.WorkingHoursEnd }
            };

            foreach (var s in settings)
            {
                var existing = await _context.Settings.FirstOrDefaultAsync(x => x.Key == s.Key);
                if (existing != null)
                {
                    existing.Value = s.Value;
                }
                else
                {
                    _context.Settings.Add(s);
                }
            }
            await _context.SaveChangesAsync();
            resp.Data = dto;
            return resp;
        }
    }
}