using AppointmentSystem.Dtos;
using AppointmentSystem.Models;
using AppointmentSystem.Models.Enums;
using AutoMapper;

namespace AppointmentSystem.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppointmentDto, Appointment>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => AppointmentStatus.Booked));

            CreateMap<Slot, SlotDto>();

            CreateMap<Appointment, AppointmentDto>();

            CreateMap<Slot, SlotSummaryDto>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.StartTime.ToString("HH:mm")))
                .ForMember(dest => dest.AvailableSlots,
                    opt => opt.MapFrom(src =>
                        src.MaxBookings - src.Appointments.Count(a => a.Status == AppointmentStatus.Booked)));

            CreateMap<Appointment, AppointmentSummaryDto>();

            CreateMap<Appointment, CancelAppointmentResponseDto>()
                .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Cancelled, opt => opt.MapFrom(src => true));
        }
    }
}