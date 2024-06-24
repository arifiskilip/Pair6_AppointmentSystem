using Application.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Appointment.Queries.GetClosest
{
    public class GetClosestAppointmentQuery : IRequest<GetClosestAppointmentResponse> , ISecuredRequest
    {
        public string[] Roles => ["Patient"];

        public class GetClosestAppointmentQueryHandler : IRequestHandler<GetClosestAppointmentQuery, GetClosestAppointmentResponse>
        {
            private readonly IMapper _mapper;
            private readonly IPatientRepository _patientRepository;
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public GetClosestAppointmentQueryHandler(IMapper mapper, IPatientRepository patientRepository, IAppointmentRepository appointmentRepository, IHttpContextAccessor httpContextAccessor)
            {
                _mapper = mapper;
                _patientRepository = patientRepository;
                _appointmentRepository = appointmentRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<GetClosestAppointmentResponse> Handle(GetClosestAppointmentQuery request, CancellationToken cancellationToken)
            {
                var patientId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var patient = await _patientRepository.GetAsync(x => x.Id == int.Parse(patientId));

                var upcomingAppointments = await _appointmentRepository.GetListNotPagedAsync(
                    predicate: x => x.PatientId == int.Parse(patientId) &&
                               x.AppointmentStatus.Id == 4 &&
                               x.AppointmentInterval.IntervalDate > DateTime.Now,
                    include: query => query
                        .Include(a => a.Patient)
                        .Include(a => a.AppointmentStatus)
                        .Include(a => a.AppointmentInterval)
                            .ThenInclude(ai => ai.Doctor)
                                .ThenInclude(d => d.Branch)
                        .Include(a => a.AppointmentInterval)
                            .ThenInclude(ai => ai.Doctor)
                                .ThenInclude(d => d.Title),
                    orderBy: q => q.OrderBy(a => a.AppointmentInterval.IntervalDate), // En yakın randevuyu almak için sıralama
                    enableTracking: false,
                    cancellationToken: cancellationToken
                );
                var closestAppointment = upcomingAppointments.Take(1).FirstOrDefault();

                var response = _mapper.Map<GetClosestAppointmentResponse>(closestAppointment);
                return response;
            }
        }
    }
}
