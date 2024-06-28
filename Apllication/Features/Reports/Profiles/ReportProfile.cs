using Application.Features.Reports.Commands.Add;
using Application.Features.Reports.Queries.GetAllReportsPatient;

using Application.Features.Reports.Queries.GetByIdReportsPatient;

using Application.Features.Reports.Queries.GetPaginatedReportsByPatientId;
using Application.Features.Reports.Queries.GetPaginatedReportsByPatientIdAndDoctorId;

using AutoMapper;
using Domain.Entities;

namespace Application.Features.Reports.Profiles
{
    public  class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<Report,AddReportCommand>().ReverseMap();
            CreateMap<Report, AddReportResponse>().ReverseMap();

            CreateMap<Report, PatientReportsDto>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Appointment.Patient.FirstName + " " + src.Appointment.Patient.LastName))
            .ForMember(dest => dest.IntervalDate, opt => opt.MapFrom(src => src.Appointment.AppointmentInterval.IntervalDate))
            .ForMember(dest => dest.AppointmentStatus, opt => opt.MapFrom(src => src.Appointment.AppointmentStatus.Name))
            .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.Appointment.AppointmentInterval.Doctor.Branch.Id))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Appointment.AppointmentInterval.Doctor.Branch.Name))
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Appointment.AppointmentInterval.Doctor.Title.Name + " " + src.Appointment.AppointmentInterval.Doctor.FirstName + " " + src.Appointment.AppointmentInterval.Doctor.LastName))
            .ForMember(dest => dest.ReportId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ReportFile, opt => opt.MapFrom(src => src.ReportFile));

            CreateMap<Report, GetPaginatedReportsByPatientIdResonse>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Appointment.Patient.FirstName + " " + src.Appointment.Patient.LastName))
            .ForMember(dest => dest.IntervalDate, opt => opt.MapFrom(src => src.Appointment.AppointmentInterval.IntervalDate))
            .ForMember(dest => dest.AppointmentStatus, opt => opt.MapFrom(src => src.Appointment.AppointmentStatus.Name))
            .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.Appointment.AppointmentInterval.Doctor.Branch.Id))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Appointment.AppointmentInterval.Doctor.Branch.Name))
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Appointment.AppointmentInterval.Doctor.Title.Name + " " + src.Appointment.AppointmentInterval.Doctor.FirstName + " " + src.Appointment.AppointmentInterval.Doctor.LastName))
            .ForMember(dest => dest.ReportId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ReportFile, opt => opt.MapFrom(src => src.ReportFile));

            CreateMap<Report, GetPaginatedReportsByPatientIdAndDoctorIdResponse>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Appointment.Patient.FirstName + " " + src.Appointment.Patient.LastName))
            .ForMember(dest => dest.IntervalDate, opt => opt.MapFrom(src => src.Appointment.AppointmentInterval.IntervalDate))
            .ForMember(dest => dest.AppointmentStatus, opt => opt.MapFrom(src => src.Appointment.AppointmentStatus.Name))
            .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.Appointment.AppointmentInterval.Doctor.Branch.Id))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Appointment.AppointmentInterval.Doctor.Branch.Name))
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Appointment.AppointmentInterval.Doctor.Title.Name + " " + src.Appointment.AppointmentInterval.Doctor.FirstName + " " + src.Appointment.AppointmentInterval.Doctor.LastName))
            .ForMember(dest => dest.ReportId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ReportFile, opt => opt.MapFrom(src => src.ReportFile));
            
             CreateMap<Report, GetByIdReportsPatientResponse>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Appointment.Patient.FirstName + " " + src.Appointment.Patient.LastName))
            .ForMember(dest => dest.IntervalDate, opt => opt.MapFrom(src => src.Appointment.AppointmentInterval.IntervalDate))
            .ForMember(dest => dest.AppointmentStatus, opt => opt.MapFrom(src => src.Appointment.AppointmentStatus.Name))
            .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.Appointment.AppointmentInterval.Doctor.Branch.Id))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Appointment.AppointmentInterval.Doctor.Branch.Name))
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Appointment.AppointmentInterval.Doctor.Title.Name + " " + src.Appointment.AppointmentInterval.Doctor.FirstName + " " + src.Appointment.AppointmentInterval.Doctor.LastName))
            .ForMember(dest => dest.ReportId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ReportFile, opt => opt.MapFrom(src => src.ReportFile));


        }
    }
}
