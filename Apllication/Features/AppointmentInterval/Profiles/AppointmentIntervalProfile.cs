using Application.Features.AppointmentInterval.Queries.AppointmentIntervalsSearchByPaginated;
using Application.Features.AppointmentInterval.Queries.GetById;
using AutoMapper;
using Domain.Dtos;

namespace Application.Features.AppointmentInterval.Profiles
{
    public class AppointmentIntervalProfile : Profile
    {
        public AppointmentIntervalProfile()
        {
            CreateMap<Domain.Entities.AppointmentInterval, AppointmentIntervalsSearchDto>()
           .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.FirstName + " " + src.Doctor.LastName)) // Assuming you have FirstName and LastName in User class
           .ForMember(dest => dest.TitleId, opt => opt.MapFrom(src => src.Doctor.TitleId))
           .ForMember(dest => dest.TitleName, opt => opt.MapFrom(src => src.Doctor.Title.Name))
           .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.Doctor.BranchId))
           .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Doctor.Branch.Name))
           .ForMember(dest => dest.IntervalDateMessage, opt => opt.MapFrom(src => (src.IntervalDate.Date - DateTime.Now.Date).TotalDays + " Gün Kaldı."))
            .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.Doctor.Gender.Name))
            .ForMember(dest => dest.GenderId, opt => opt.MapFrom(src => src.Doctor.Gender.Id));
            CreateMap<Domain.Entities.AppointmentInterval, AppointmentIntervalsSearchByPaginatedResponse>().ReverseMap();
            CreateMap<Domain.Entities.AppointmentInterval, AppointmentIntervalsSearchByPaginatedQuery>().ReverseMap();

            CreateMap<Domain.Entities.AppointmentInterval, GetByIdAppointmentIntervalResponse>()
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.FirstName + " " + src.Doctor.LastName)) // Assuming you have FirstName and LastName in User class
            .ForMember(dest => dest.TitleId, opt => opt.MapFrom(src => src.Doctor.TitleId))
             .ForMember(dest => dest.TitleName, opt => opt.MapFrom(src => src.Doctor.Title.Name))
            .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.Doctor.BranchId))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Doctor.Branch.Name))
            .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.Doctor.Gender.Name));

            CreateMap<Domain.Entities.AppointmentInterval, GetByIdAppointmentIntervalQuery>().ReverseMap();
        }
    }
}
