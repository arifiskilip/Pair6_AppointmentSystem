﻿using Application.Features.Doctors.Commands.Update;
using Application.Features.Doctors.Queries;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Doctors.Profiles
{
    public class DoctorProfile : Profile
    {

        public DoctorProfile()
        {
            CreateMap<User, ListDoctorDto>()
            .Include<Doctor, ListDoctorDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.IdentityNumber, opt => opt.MapFrom(src => src.IdentityNumber))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate));

            CreateMap<Doctor, ListDoctorDto>()
                .ForMember(dest => dest.TitleName, opt => opt.MapFrom(src => src.Title.Name))
                .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name))
                .ForMember(dest => dest.TitleId, opt => opt.MapFrom(src => src.TitleId))
                .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId));

            CreateMap<UpdateDoctorCommand, Doctor>().ReverseMap();
                    

            CreateMap<Doctor, UpdateDoctorResponse>()
                .ForMember(dest => dest.TitleName, opt => opt.MapFrom(src => src.Title.Name))
                .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name));


        }
    }
}