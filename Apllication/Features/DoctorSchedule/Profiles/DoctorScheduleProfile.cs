using Application.Features.DoctorSchedule.Command.Add;
using AutoMapper;

namespace Application.Features.DoctorSchedule.Profiles
{
    public class DoctorScheduleProfile : Profile
    {
        public DoctorScheduleProfile()
        {
            CreateMap<Domain.Entities.DoctorSchedule, DoctorScheduleAddCommand>().ReverseMap();
            CreateMap<Domain.Entities.DoctorSchedule, DoctorScheduleAddResponse>().ReverseMap();
        }
    }
}
