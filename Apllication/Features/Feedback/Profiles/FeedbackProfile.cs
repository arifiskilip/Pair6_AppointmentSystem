using Application.Features.Feedback.Commands.Add;
using Application.Features.Feedback.Queries.GetAll;
using Application.Features.Feedback.Queries.GetAllAdmin;
using Application.Features.Feedback.Queries.GetAllByPatientId;
using Application.Features.Feedback.Queries.GetById;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Profiles
{
    public class FeedbackProfile : Profile
    {
        public FeedbackProfile()
        {
            CreateMap<Domain.Entities.Feedback,AddFeedbackCommand>().ReverseMap();
            CreateMap<Domain.Entities.Feedback, AddFeedBackResponse>().ReverseMap();
            CreateMap<Domain.Entities.Feedback, FeedbackPatientDto>().ReverseMap();

            CreateMap<Domain.Entities.Feedback, ListFeedbackDto>()
           .ForMember(dest => dest.FeedbackId, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.Patient.Id))
           .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FirstName + " " + src.Patient.LastName))
           .ForMember(dest => dest.IdentityNumber, opt => opt.MapFrom(src => src.Patient.IdentityNumber))
           .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Patient.Gender.Name))
           .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.Appointment.Id))
           .ForMember(dest => dest.IntervalDate, opt => opt.MapFrom(src => src.Appointment.AppointmentInterval.IntervalDate));


            CreateMap<Domain.Entities.Feedback, GetFeedbackByIdResponse>()
            .ForMember(dest => dest.FeedbackId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FirstName + " " + src.Patient.LastName))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Patient.Gender.Name))
            .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.Appointment.Id))
            .ForMember(dest => dest.IntervalDate, opt => opt.MapFrom(src => src.Appointment.AppointmentInterval.IntervalDate))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Appointment.AppointmentInterval.Doctor.Branch.Name))
            .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.Appointment.AppointmentInterval.Doctor.Id))
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Appointment.AppointmentInterval.Doctor.FirstName + " " + src.Appointment.AppointmentInterval.Doctor.LastName))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.Patient.Id));


            CreateMap<Domain.Entities.Feedback, GetAllFeedbacksByPatientIdResponse>()
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FirstName + " " + src.Patient.LastName))
           .ForMember(dest => dest.IdentityNumber, opt => opt.MapFrom(src => src.Patient.IdentityNumber))
           .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Patient.Gender.Name))
           .ForMember(dest => dest.IntervalDate, opt => opt.MapFrom(src => src.Appointment.AppointmentInterval.IntervalDate));


        }
    }
}
