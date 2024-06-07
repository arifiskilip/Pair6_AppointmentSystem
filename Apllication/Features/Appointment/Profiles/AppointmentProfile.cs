using Application.Features.Appointment.Commands.Add;
using AutoMapper;

namespace Application.Features.Appointment.Profiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Domain.Entities.Appointment, AddAppointmentCommand>().ReverseMap();
            CreateMap<Domain.Entities.Appointment, AddAppointmentResponse>().ReverseMap();
        }
    }
}
