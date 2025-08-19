using AppointmentSystem.Common;
using AppointmentSystem.Data;
using AppointmentSystem.Dtos;
using AppointmentSystem.Models;
using AppointmentSystem.Models.Enums;
using AppointmentSystem.Services.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AppointmentSystem.Services
{
    public class SlotService(AppDbContext context, IMapper mapper) : ISlotService
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<ServiceResponse<List<SlotSummaryDto>>> GetSlotsAsync()
        {
            var slotSummaryDtos = await _context.Slots
                .Include(s => s.Appointments)
                .Select(s => new SlotSummaryDto
                {
                    Id = s.Id,
                    Date = s.Date.ToString("yyyy-MM-dd"),
                    Time = s.StartTime.ToString("HH:mm"),
                    AvailableSlots = s.MaxBookings - s.Appointments.Count(a => a.Status == AppointmentStatus.Booked)
                })
                .ToListAsync();

            return new ServiceResponse<List<SlotSummaryDto>>()
            {
                Data = slotSummaryDtos
            };
        }

        public async Task<ServiceResponse<List<SlotDto>>> GenerateSlotAsync(DateOnly date)
        {
            var resp = new ServiceResponse<List<SlotDto>>();
            var hasSlots = await _context.Slots.AnyAsync(s => s.Date.Equals(date));
            if (hasSlots)
            {
                resp.AddError($"Slots have been created for {date}");
                return resp;
            }

            var keys = new[] { "SlotDurationMinutes", "WorkingHoursStart", "WorkingHoursEnd", "MaxBookingsPerSlot" };
            var settings = await _context.Settings
                                         .Where(s => keys.Contains(s.Key))
                                         .ToListAsync();

            var slotDuration = int.Parse(settings.First(s => s.Key == "SlotDurationMinutes").Value);
            var startHour = TimeOnly.Parse(settings.First(s => s.Key == "WorkingHoursStart").Value);
            var endHour = TimeOnly.Parse(settings.First(s => s.Key == "WorkingHoursEnd").Value);
            var maxBookings = int.Parse(settings.First(s => s.Key == "MaxBookingsPerSlot").Value);

            var slots = new List<Slot>();
            var current = startHour;

            while (current < endHour)
            {
                slots.Add(new Slot
                {
                    Date = date,
                    StartTime = current,
                    EndTime = current.AddMinutes(slotDuration),
                    MaxBookings = maxBookings
                });

                current = current.AddMinutes(slotDuration);
            }

            _context.Slots.AddRange(slots);
            await _context.SaveChangesAsync();
            resp.Data = _mapper.Map<List<SlotDto>>(slots);
            return resp;
        }
    }
}