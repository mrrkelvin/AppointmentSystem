using AppointmentSystem.Common;
using AppointmentSystem.Data;
using AppointmentSystem.Dtos;
using AppointmentSystem.Models;
using AppointmentSystem.Models.Enums;
using AppointmentSystem.Services.Interface;
using AppointmentSystem.Validators;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AppointmentSystem.Services
{
    public class AppointmentService(AppDbContext context, IMapper mapper) : IAppointmentService
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<ServiceResponse<List<AppointmentSummaryDto>>> GetAppointmentsAsync()
        {
            var appointmentSummaryDtos = await _context.Appointments
                .Where(a => a.Status == AppointmentStatus.Booked)
                .Include(s => s.Slot)
                .ProjectTo<AppointmentSummaryDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new ServiceResponse<List<AppointmentSummaryDto>>()
            {
                Data = appointmentSummaryDtos
            };
        }

        public async Task<ServiceResponse<AppointmentSummaryDto>> CreateAppointmentAsync(AppointmentDto dto)
        {
            var resp = new ServiceResponse<AppointmentSummaryDto>();

            var validator = new AppointmentDtoValidator();
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                resp.AddError(validationResult.Errors.FirstOrDefault()!.ErrorMessage);
                return resp;
            }

            var slot = await _context.Slots.Include(s => s.Appointments)
                .FirstOrDefaultAsync(s => s.Id == dto.SlotId);

            if (slot == null)
            {
                resp.AddError("Slot not available");
                return resp;
            }

            int bookedCount = slot.Appointments.Count(a => a.Status == AppointmentStatus.Booked);
            if (bookedCount >= slot.MaxBookings)
            {
                resp.AddError("Slot is fully booked");
                return resp;
            }

            var appointment = _mapper.Map<Appointment>(dto);

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            resp.Data = _mapper.Map<AppointmentSummaryDto>(appointment);
            return resp;
        }

        public async Task<ServiceResponse<CancelAppointmentResponseDto>> CancelAppointmentAsync(int id)
        {
            var resp = new ServiceResponse<CancelAppointmentResponseDto>();
            var appointment = await _context.Appointments
                .Include(a => a.Slot)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (appointment == null)
            {
                resp.AddError("Appointment not found");
                return resp;
            }

            appointment.Status = AppointmentStatus.Cancelled;
            await _context.SaveChangesAsync();
            resp.Data = _mapper.Map<CancelAppointmentResponseDto>(appointment);
            return resp;
        }
    }

}