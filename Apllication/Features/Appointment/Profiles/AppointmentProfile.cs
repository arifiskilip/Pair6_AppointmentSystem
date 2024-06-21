using Application.Features.Appointment.Commands.Add;
using Application.Features.Appointment.Queries.GetPaginatedAppointmentsByDoctor;
using Application.Features.Appointment.Queries.GetPaginatedAppointmentsByPatient;
using Application.Features.Appointment.Queries.GetPaginatedDoctorAppointments;
using AutoMapper;

namespace Application.Features.Appointment.Profiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Domain.Entities.Appointment, AddAppointmentCommand>().ReverseMap();
            CreateMap<Domain.Entities.Appointment, AddAppointmentResponse>().ReverseMap();

            CreateMap<Domain.Entities.Appointment, AppointmentDto>()
           .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.Patient.Id))
           .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FirstName + " " + src.Patient.LastName))
           .ForMember(dest => dest.IntervalDate, opt => opt.MapFrom(src => src.AppointmentInterval.IntervalDate))
           .ForMember(dest => dest.AppointmentStatus, opt => opt.MapFrom(src => src.AppointmentInterval.AppointmentStatus.Name));

            CreateMap<Domain.Entities.Appointment, AppointmentPatientDto>()
            .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.AppointmentInterval.Doctor.Id))
             .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.AppointmentInterval.Doctor.FirstName + " " + src.AppointmentInterval.Doctor.LastName))
            .ForMember(dest => dest.IntervalDate, opt => opt.MapFrom(src => src.AppointmentInterval.IntervalDate))
            .ForMember(dest => dest.AppointmentStatus, opt => opt.MapFrom(src => src.AppointmentInterval.AppointmentStatus.Name));


            CreateMap<Domain.Entities.Appointment, DoctorAppointmentDto>()
            .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.Patient.Id))
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FirstName + " " + src.Patient.LastName))
            .ForMember(dest => dest.IntervalDate, opt => opt.MapFrom(src => src.AppointmentInterval.IntervalDate))
            .ForMember(dest => dest.AppointmentStatus, opt => opt.MapFrom(src => src.AppointmentInterval.AppointmentStatus.Name));
        }
    }
}
