using Application.Features.Patients.Commands.Update;
using Application.Features.Patients.Queries.GetAllPaginated;
using Application.Features.Patients.Queries.GetById;
using Application.Features.Patients.Queries.SearchPatients;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Patients.Profiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, ListPatientDto>()
           .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.Gender.Name))
           .ForMember(dest => dest.BloodTypeName, opt => opt.MapFrom(src => src.BloodType.Name))
           .ReverseMap();
            CreateMap<Patient, SearchPatientsResponse>()
          .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.Gender.Name))
          .ForMember(dest => dest.BloodTypeName, opt => opt.MapFrom(src => src.BloodType.Name))
          .ReverseMap();
            CreateMap<Patient, GetByIdPatientResponse>()
           .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.Gender.Name))
           .ForMember(dest => dest.BloodTypeName, opt => opt.MapFrom(src => src.BloodType.Name))
           .ReverseMap();
            CreateMap<Patient, UpdatePatientCommand>().ReverseMap();
            CreateMap<Patient, UpdatePatientResponse>().ReverseMap();

        }
    }
}
