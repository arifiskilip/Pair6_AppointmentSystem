using Application.Features.AppointmentInterval.Queries.AppointmentIntervalsSearchByPaginated;
using AutoMapper;

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
           .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Doctor.Branch.Name));// Assuming you have a Gender entity with Name property
        CreateMap<Domain.Entities.AppointmentInterval, AppointmentIntervalsSearchByPaginatedResponse>().ReverseMap();
            CreateMap<Domain.Entities.AppointmentInterval, AppointmentIntervalsSearchByPaginatedQuery>().ReverseMap();
        }
    }
}
