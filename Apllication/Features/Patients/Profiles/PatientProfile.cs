using Application.Features.Doctors.Queries;
using Application.Features.Patients.Commands.Update;
using Application.Features.Patients.Queries.GetAllPaginated;
using Application.Features.Patients.Queries.GetById;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            CreateMap<Patient, GetByIdPatientResponse>()
           .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.Gender.Name))
           .ForMember(dest => dest.BloodTypeName, opt => opt.MapFrom(src => src.BloodType.Name))
           .ReverseMap();
            CreateMap<Patient, UpdatePatientCommand>().ReverseMap();
            CreateMap<Patient, UpdatePatientResponse>().ReverseMap();

        }
    }
}
