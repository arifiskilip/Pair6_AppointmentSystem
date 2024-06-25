using Application.Features.Reports.Rules;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.CrossCuttingConcers.Exceptions.Types;
using Core.Utilities.FileHelper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reports.Commands.Add
{
    public class AddReportCommand : IRequest<AddReportResponse> , ISecuredRequest
    {
        public int AppointmentId { get; set; }
        public string? Description { get; set; }
        public IFormFile File { get; set; } //this is ReportFile
        public string[] Roles => ["Doctor"];

        public class AddReportCommandHandler : IRequestHandler<AddReportCommand, AddReportResponse>
        {
            private readonly IReportRepository _reportRepository;
            private readonly ReportBusinessRules _reportBusinessRules;
            private readonly IAuthService _authService;
            private readonly IMapper _mapper;

            public AddReportCommandHandler(IReportRepository reportRepository, ReportBusinessRules reportBusinessRules, IAuthService authService, IMapper mapper)
            {
                _reportRepository = reportRepository;
                _reportBusinessRules = reportBusinessRules;
                _authService = authService;
                _mapper = mapper;
            }

            public async Task<AddReportResponse> Handle(AddReportCommand request, CancellationToken cancellationToken)
            {
                var doctorId = await _authService.GetAuthenticatedUserIdAsync();

                // rules
                //randevu statusu iptal edilmis olmamali
                //(tamamlandi olmali dicektim ama tamamlamadan once report eklemek isterse sikinti)
                await _reportBusinessRules.IsAppointmentBelongstoDoctor(request.AppointmentId, doctorId); //doktor hastaya rapor eklerken randevu o doktora mi ait
                await _reportBusinessRules.IsAppointmentExist(request.AppointmentId);//boyle bir randevu var mi
                await _reportBusinessRules.IsReportExist(request.AppointmentId); //bir de bir adet report ekleyebilsin 

                var newFileUrl = await FileHelper.AddAsync(request.File, FileTypeEnum.Text);

               var report = _mapper.Map<Report>(request);
                report.ReportFile = newFileUrl;
                await _reportRepository.AddAsync(report);
                return _mapper.Map<AddReportResponse>(report);
            }
        }
    }
}
