using Application.Features.Auth.Command.DoctorRegister;
using Application.Features.Auth.Command.Login;
using Application.Features.Auth.Command.PatientRegister;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Auth.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<Patient, PatientRegisterCommand>().ReverseMap();
            CreateMap<Patient, PatientRegisterResponse>().ReverseMap();

            CreateMap<Doctor, DoctorRegisterCommand>().ReverseMap();
            CreateMap<Doctor, DoctorRegisterResponse>().ReverseMap();
            CreateMap<Doctor, DoctorRegisterResponse>()
                .ForMember(x => x.BranchName, opt => opt.MapFrom(x => x.Branch.Name))
                .ForMember(x => x.TitleName, opt => opt.MapFrom(x => x.Title.Name));

            CreateMap<User, LoginResponse>().ReverseMap();
        }
    }
}
